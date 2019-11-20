using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryStoreAPI;
using GroceryStoreAPI.Controllers;
using Newtonsoft.Json.Linq;

namespace UnitTest_GroceryStoreAPI
{

    [TestClass]
    public class TestProductController
    {
        public string json = System.IO.File.ReadAllText("database.json");

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var Products = GetAllProducts();
            var controller = new ProductController();

            var result = controller.GetProducts() as List<Product>;
            Assert.AreEqual(Products.Count, result.Count);
        }


        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            var Products = GetAllProducts();
            var controller = new ProductController();
            var result = controller.GetProduct(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(Products[1].Description, result.Description);
        }

        [TestMethod]
        public void SaveProduct_ShouldConfirmSavedProduct()
        {
            var Products = GetAllProducts();
            var controller = new ProductController();
            Product UpdatedProduct = new Product { Id = 1, Description = "apple", Price = 0.61M };

            controller.UpdateProduct(1, UpdatedProduct);

            var result = controller.GetProduct(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(UpdatedProduct.Id, result.Id);
            Assert.AreEqual(UpdatedProduct.Description, result.Description);
            Assert.AreEqual(UpdatedProduct.Price, result.Price);
        }

        private List<Product> GetAllProducts()
        {
            var Products = new List<Product>();
            var jsonObj = JObject.Parse(json);
            JArray ProductArray = jsonObj.GetValue("products") as JArray;

            foreach (var obj in ProductArray)
            {
                Product Product = obj.ToObject<Product>();
                Products.Add(Product);
            }

            return Products;
        }
    }
}