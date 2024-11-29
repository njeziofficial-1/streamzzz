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
    public string Address { get; set; }
    public string OrderStatus { get; set; }
}
