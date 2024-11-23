using StremoCloud.Domain.Enum;

namespace StremoCloud.Domain.Entities;

public class OrderDetail
{
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public OrderStatusEnum Status { get; set; }
    public bool IsIncomingCall { get; set; }
    public string ActionButtonText { get; set; } = "View Order";
}
