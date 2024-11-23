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
