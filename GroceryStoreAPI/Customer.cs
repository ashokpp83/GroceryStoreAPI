using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI.Controllers
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static implicit operator JToken(Customer v)
        {
            throw new NotImplementedException();
        }
    }
}