using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreAPI.Controllers;
using Newtonsoft.Json.Linq;


namespace UnitTest_GroceryStoreAPI
{

    [TestClass]
    public class Customer_Test
    {
        public string json = System.IO.File.ReadAllText("database.json");

        [TestMethod]
        public void GetAllCustomers_ShouldReturnAllCustomers()
        {
            var customers = GetAllCustomers();
            var controller = new CustomerController();

            var result = controller.GetCustomers() as List<Customer>;
            Assert.AreEqual(customers.Count, result.Count);
        }


        [TestMethod]
        public void GetCustomer_ShouldReturnCorrectCustomer()
        {
            var customers = GetAllCustomers();
            var controller = new CustomerController();
            var result = controller.GetCustomer(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(customers[1].Name, result.Name);
        }

        [TestMethod]
        public void SaveCustomer_ShouldConfirmSavedCustomer()
        {
            var controller = new CustomerController();
            Customer UpdatedCustomer = new Customer { Id = 2, Name = "Mary Shields" };

            controller.UpdateCustomer(2, UpdatedCustomer);

            var result = controller.GetCustomer(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(UpdatedCustomer.Name, result.Name);
        }

        private List<Customer> GetAllCustomers()
        {
            var Customers = new List<Customer>();
            var jsonObj = JObject.Parse(json);
            JArray CustomerArray = jsonObj.GetValue("customers") as JArray;

            foreach (var obj in CustomerArray)
            {
                Customer customer = obj.ToObject<Customer>();
                Customers.Add(customer);
            }

            return Customers;
        }
    }
}