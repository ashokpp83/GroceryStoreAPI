using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime date { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}