using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        string json = System.IO.File.ReadAllText("database.json");
        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            List<Product> ProductList = new List<Product>();
            var jsonObj = JObject.Parse(json);
            JArray ProductArray = jsonObj.GetValue("products") as JArray;

            foreach (var obj in ProductArray)
            {
                Product product = obj.ToObject<Product>();
                ProductList.Add(product);
            }

            return ProductList;

        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProduct")]
        public Product GetProduct(int id)
        {
            var jsonObj = JObject.Parse(json);
            JArray ProductArray = jsonObj.GetValue("products") as JArray;

            var productObj = ProductArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);
            Product product = productObj.ToObject<Product>();

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public void AddProduct([FromBody] string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void UpdateProduct(int id, [FromBody] Product product)
        {
            var jsonObj = JObject.Parse(json);
            JArray ProductArray = jsonObj.GetValue("products") as JArray;

            var productObj = ProductArray.FirstOrDefault(obj => obj["id"].Value<int>() == id);

            productObj["description"] = product.Description;
            productObj["price"] = product.Price;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

            System.IO.File.WriteAllText("database.json", output);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
        }
    }
}