using StremoCloud.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Shared.Response;

public class OrderResponse
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerImage { get; set; }
    public string Address { get; set; }
    public DateTime DatePlaced { get; set; }
    public decimal Amount { get; set; }
    public bool IsSuccessful { get; set; }
    public OrderRenderingStatusEnum OrderRenderStatus { get; set; }
}


//public string Id { get; set; }
//public string OrderStatus { get; set; }