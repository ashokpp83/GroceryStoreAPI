using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using GroceryStoreAPI.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        string json = System.IO.File.ReadAllText("database.json");
        // GET: api/Order
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            List<Order> OrderList = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray)
            {
                Order order = obj.ToObject<Order>();
                OrderList.Add(order);
            }

            return OrderList;
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "GetOrder")]
        public Order GetOrder(int id)
        {
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            var orderObj = OrderArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);
            Order order = orderObj.ToObject<Order>();

            return order;
        }

        // GET: api/order/OrdersForDate/5
        //[Route("[action]/{date}")]
        [HttpGet("[action]/{date}", Name = "GetOrdersForDate")]
        public IEnumerable<Order> GetOrdersForDate(DateTime date)
        {
            List<Order> OrderList = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray.Where(obj => obj["date"].Value<DateTime>() == date))
            {
                Order order = obj.ToObject<Order>();
                OrderList.Add(order);
            }

            return OrderList;
        }

        // GET: api/order/OrdersForCustomer/5
        //[Route("[action]/{date}")]
        [HttpGet("[action]/{customerid}", Name = "GetOrdersForCustomer")]
        public IEnumerable<Order> GetOrdersForCustomer(int customerId)
        {
            List<Order> OrderList = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray.Where(obj => obj["customerId"].Value<int>() == customerId))
            {
                Order order = obj.ToObject<Order>();
                OrderList.Add(order);
            }

            return OrderList;
        }


        // POST: api/Order
        [HttpPost]
        public void PostOrder([FromBody] string value)
        {
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void PutOrder(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteOrder(int id)
        {
        }
    }
}