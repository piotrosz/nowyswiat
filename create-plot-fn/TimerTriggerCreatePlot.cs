using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using Azure.Data.Tables;
using Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScottPlot;
using Microsoft.WindowsAzure.Storage;

namespace NowySwiat.Function
{
    public class TimerTriggerCreatePlot
    {
        [FunctionName("TimerTriggerCreatePlot")]
        public void Run([TimerTrigger("0 0 0 * * MON")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //string tableName = "NumberOfPatrons";
            //string storageUri = "https://nowyswiatfn3bd064.table.core.windows.net/NumberOfPatrons";

            //string storageAccountName = "";
            //string storageAccountKey ="";

            //// Construct a new <see cref="TableClient" /> using a <see cref="TableSharedKeyCredential" />.
            //var tableClient = new TableClient(
            //    new Uri(storageUri),
            //    tableName,
            //    new TableSharedKeyCredential(storageAccountName, storageAccountKey));

            //// Create the table in the service.
            //tableClient.Create();

            //Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(/*filter: $"PartitionKey eq '{partitionKey}'"*/);

            //var records = new List<PlotRecord>();

            //// Iterate the <see cref="Pageable"> to access all queried entities.
            //foreach (TableEntity qEntity in queryResultsFilter)
            //{
            //    Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
            //}

            //Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");

            // ----------------------------------------------------------------------------------------------

            //double[] noOfPatrons = records.Select(x => (double)x.NoOfPatrons).ToArray();
            //double[] dates = records.Select(x => x.Date.ToOADate()).ToArray();
            //var plotNoOfPatrons = new Plot(900, 500);

            //plotNoOfPatrons.AddScatter(dates, noOfPatrons);
            //plotNoOfPatrons.XAxis.DateTimeFormat(true);

            //plotNoOfPatrons.Title("Number of patrons in 2023");
            //plotNoOfPatrons.YAxis.Label("Number of patrons");

            //plotNoOfPatrons.SaveFig(@$"{outDir}\no-of-patrons.png");

            //double[] monthlyAmount = records.Where(x => x.MonthlyAmount.HasValue).Select(x => (double)x.MonthlyAmount.Value).ToArray();
            //double[] monthlyAmountDates = records.Where(x => x.MonthlyAmount.HasValue).Select(x => x.Date.ToOADate()).ToArray();

            //var plotMonthlyAmount = new Plot(900, 500);

            //plotMonthlyAmount.AddScatter(monthlyAmountDates, monthlyAmount);
            //plotMonthlyAmount.XAxis.DateTimeFormat(true);

            //plotMonthlyAmount.Title("Monthly amount in 2023");
            //plotMonthlyAmount.YAxis.Label("Monthly amount");

            //plotMonthlyAmount.SaveFig(@"C:\temp\monthly-amount.png");

            //double[] totalAmount = records.Where(x => x.TotalAmount.HasValue).Select(x => (double)x.TotalAmount.Value).ToArray();
            //double[] totalAmountDates = records.Where(x => x.TotalAmount.HasValue).Select(x => x.Date.ToOADate()).ToArray();
        }
    }
}
