using StremoCloud.Domain.Common;
using StremoCloud.Domain.Enum;

namespace StremoCloud.Domain.Entities;

public class Order : Entity
{
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public OrderStatusEnum Status { get; set; }
    public bool IsIncomingCall { get; set; }
    public OrderDecisionEnum Action { get; set; }

}
