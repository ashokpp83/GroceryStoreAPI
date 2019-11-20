using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Linq;
using GroceryStoreAPI.Controllers;
using GroceryStoreAPI;

namespace UnitTest_GroceryStoreAPI
{

    [TestClass]
    public class TestOrderController
    {
        public string json = System.IO.File.ReadAllText("database.json");

        [TestMethod]
        public void GetAllOrders_ShouldReturnAllOrders()
        {
            var Orders = GetAllOrders();
            var controller = new OrderController();

            var result = controller.GetOrders() as List<Order>;
            Assert.AreEqual(Orders.Count, result.Count);
        }


        [TestMethod]
        public void GetOrder_ShouldReturnCorrectOrder()
        {
            var Orders = GetAllOrders();
            var controller = new OrderController();
            var result = controller.GetOrder(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(Orders[0].Id, result.Id);
        }

        [TestMethod]
        public void GetCustomerOrders_ShouldReturnCorrectCustomerOrders()
        {
            var Orders = GetCustomerOrders(1);
            var controller = new OrderController();
            var result = controller.GetOrdersForCustomer(1).ToList();

            //Assert same number of records returned
            Assert.AreEqual(Orders.Count, result.Count);
            //Loop to determine if same order Ids are returned
            for (int i = 0; i < Orders.Count; i++)
            {
                Assert.AreEqual(Orders[i].Id, result[i].Id);
            }
        }

        //[TestMethod]
        //public void GetDateOrders_ShouldReturnCorrectOrders()
        //{
        //    var Orders = GetDateOrders(Convert.ToDateTime("2019-11-20"));
        //    var controller = new OrderController();
        //    var result = controller.GetOrdersForDate(Convert.ToDateTime("2019-11-20")).ToList();

        //    //Assert same number of records returned
        //    Assert.AreEqual(Orders.Count, result.Count);
        //    //Loop to determine if same order Ids are returned
        //    for (int i = 0; i < Orders.Count; i++)
        //    {
        //        Assert.AreEqual(Orders[i].Id, result[i].Id);
        //    }

        //}

        private List<Order> GetAllOrders()
        {
            var Orders = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray)
            {
                Order order = obj.ToObject<Order>();
                Orders.Add(order);
            }

            return Orders;
        }

        private List<Order> GetCustomerOrders(int customerId)
        {
            var Orders = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray.Where(obj => obj["customerId"].Value<int>() == customerId))
            {
                Order order = obj.ToObject<Order>();
                Orders.Add(order);
            }

            return Orders;
        }

        private List<Order> GetDateOrders(DateTime date)
        {
            var Orders = new List<Order>();
            var jsonObj = JObject.Parse(json);
            JArray OrderArray = jsonObj.GetValue("orders") as JArray;

            foreach (var obj in OrderArray.Where(obj => obj["date"].Value<DateTime>() == date))
            {
                Order order = obj.ToObject<Order>();
                Orders.Add(order);
            }

            return Orders;
        }

    }
}