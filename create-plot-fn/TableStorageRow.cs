using System;
using Azure;

namespace NowySwiat.Function;

public class TableStorageRow : Azure.Data.Tables.ITableEntity
{
    public string MonthlyAmount { get; set; }

    public string NoOfPatrons {get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}