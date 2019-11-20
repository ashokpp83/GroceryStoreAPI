using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        public string json = System.IO.File.ReadAllText("database.json");

        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {

            List<Customer> CustomerList = new List<Customer>();
            var jsonObj = JObject.Parse(json);
            JArray CustomerArray = jsonObj.GetValue("customers") as JArray;

            foreach (var obj in CustomerArray)
            {
                Customer customer = obj.ToObject<Customer>();
                CustomerList.Add(customer);
            }

            return CustomerList;

        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public Customer GetCustomer(int id)
        {
            var jsonObj = JObject.Parse(json);
            JArray CustomerArray = jsonObj.GetValue("customers") as JArray;

            var customerObj = CustomerArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);
            Customer customer = customerObj.ToObject<Customer>();
            return customer;
        }

        // POST: api/Customer
        [HttpPost]
        public void AddCustomer([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, [FromBody] Customer customer)
        {
            var jsonObj = JObject.Parse(json);
            JArray CustomerArray = jsonObj.GetValue("customers") as JArray;

            var customerObj = CustomerArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);
            customerObj["name"] = customer.Name;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            System.IO.File.WriteAllText("database.json", output);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
        }
    }
}