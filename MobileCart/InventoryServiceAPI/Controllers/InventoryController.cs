using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryServiceAPI.Models;
using System.Threading;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace InventoryServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        private readonly IMongoCollection<Product> _products = null;

        public InventoryController(IConfiguration config)
        {
            var mongoDB = config["mongo-DB"];
            var mongo = new MongoClient(mongoDB);
            var db = mongo.GetDatabase("default");
            _products = db.GetCollection<Product>("products");
            var count = Convert.ToInt32(_products.Count(y=>y.Title != string.Empty));
            if (count == 0)
            {
                var InventorySet = new List<Product>() {
                new Product() { Id = 1, Title = "YPhoneX", Description="2GB RAM, 64GB ROM", Price =89999 , Count = 200  },
                new Product() { Id=2, Title = "XPhoneX", Description ="3GB RAM, 32GB ROM", Price = 64999, Count = 250},
                new Product() { Id=3, Title="ZPhoneX", Description="4GB RAM, 64GB ROM",Price=76999, Count = 300}
            };
                _products.InsertMany(InventorySet);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Thread.Sleep(5000);
            var product = _products.Find<Product>(p => p.Id == id).FirstOrDefault();
            return new Product() { Id = product.Id, Title = product.Title, Description = product.Description, Price = product.Price };
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
