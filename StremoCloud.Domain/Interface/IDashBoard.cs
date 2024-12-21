using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StremoCloud.Domain.Interface
{
    public interface IDashBoard
    {
        Task<IEnumerable<Order>> GetOrdersAsync(); 
        Task<Order> GetOrderAsync(string id);
        Task<Order> CreateOrder(Order order);  
    }

    public class Order
    {
        public string Id { get; set; }            // MongoDB's document ID
        public DateTime DatePlaced { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
