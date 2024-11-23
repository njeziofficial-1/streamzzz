using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Transactions;

namespace StremoCloud.Domain.Entities;

public class Dashboard
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<TransactionStatus> TransactionStatuses { get; set; }
    public List<RevenueRecord> RevenueRecords { get; set; }
    public FilterOptionsEnum Filter { get; set; } = FilterOptionsEnum.AllTime;
}

public class TransactionStatus
{
    public string Status { get; set; } // e.g., "Successful", "Failed"
    public int Percentage { get; set; }
}

public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string TransacionType { get; set; }
    public double Amount { get; set; }
    public string Status { get; set; }
    public DateTime DateCreated { get; set; }
    public string? ApprovedBy { get; set; }
}

public class RevenueRecord
{
    public DateTime Date { get; set; }
    public decimal RevenueAmount { get; set; }
}

public enum FilterOptionsEnum
{
    AllTime,
    LastYear,
    ThisYear,
    LastMonth,
    ThisMonth,
    LastWeek,
    ThisWeek
}
