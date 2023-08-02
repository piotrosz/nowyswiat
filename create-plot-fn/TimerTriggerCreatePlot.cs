using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using ScottPlot;

namespace NowySwiat.Function;

public class TimerTriggerCreatePlot
{
    [FunctionName("TimerTriggerCreatePlot")]
    public async Task Run(
        [TimerTrigger("0 0 12 * * *")]TimerInfo timer,
        ILogger log, 
        ExecutionContext context)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        var config = new ConfigurationBuilder()
            .SetBasePath(context.FunctionAppDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config["AzureWebJobsStorage"];
        var tableClient = new TableClient(connectionString, "NumberOfPatrons");

        var records = await GetPlotRecordsAsync(log, tableClient);

        // ---------------------------------------------------------------------------------------------------

        var blobClient = new BlobServiceClient(connectionString);
        var plotsContainer = blobClient.GetBlobContainerClient("plots");
        log.LogInformation($"Plot container name is {plotsContainer.Name}");

        double[] noOfPatrons = records.Select(x => (double)x.NoOfPatrons).ToArray();
        double[] dates = records.Select(x => x.Date.ToOADate()).ToArray();
        var pngBytes = GetPlotPngBytes(dates, noOfPatrons, "Number of patrons");

        log.LogInformation($"Plot created {pngBytes.Length}");

        var blobName = "NumberOfPatrons.png";

        await plotsContainer.DeleteBlobIfExistsAsync(blobName);
        var result = await plotsContainer.UploadBlobAsync(blobName, BinaryData.FromBytes(pngBytes));
        
        log.LogInformation(result.Value.ToString());
        
        // ---------------------------------------------------------------------------------------------------

        double[] monthlyAmount = records.Where(x => x.MonthlyAmount.HasValue).Select(x => (double)x.MonthlyAmount.Value).ToArray();
        double[] monthlyAmountDates = records.Where(x => x.MonthlyAmount.HasValue).Select(x => x.Date.ToOADate()).ToArray();

        var pngBytesMonthlyAmount = GetPlotPngBytes(monthlyAmountDates, monthlyAmount, "Monthly amount");

        blobName = "MonthlyAmount.png";
        await plotsContainer.DeleteBlobIfExistsAsync(blobName);
        await plotsContainer.UploadBlobAsync(blobName, BinaryData.FromBytes(pngBytesMonthlyAmount));
    }

    private static async Task<List<PlotRecord>> GetPlotRecordsAsync(ILogger log, TableClient tableClient)
    {
        var records = new List<PlotRecord>();

        AsyncPageable<TableStorageRow> queryResults = tableClient.QueryAsync<TableStorageRow>();
        await foreach (TableStorageRow entity in queryResults)
        {
            // log.LogInformation($"{entity.PartitionKey};{entity.RowKey};{entity.Timestamp};{entity.MonthlyAmount};{entity.NoOfPatrons}");
            records.Add(new PlotRecord
            {
                Date = entity.Timestamp.Value.Date,
                MonthlyAmount = int.Parse(entity.MonthlyAmount.Replace(" ", "")),
                NoOfPatrons = int.Parse(entity.NoOfPatrons)
            });
        }

        records = records.OrderByDescending(x => x.Date).ToList();
        return records;
    }

    private static byte[] GetPlotPngBytes(double[] dates, double[] yValues, string title)
    {
        var plot = new Plot();
        
        plot.Add.Scatter(dates, yValues);

        plot.Title.Label.Text = title;
        plot.XAxis.Label.Text = "Date";
        plot.YAxis.Label.Text = title;
        plot.Axes.DateTimeTicks(Edge.Bottom);

        return plot.GetImage(900, 500).GetImageBytes(ImageFormat.Png);
    }
}