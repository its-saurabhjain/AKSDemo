using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerServiceAPI.Models;
using System.Threading;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        // GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        private readonly IMongoCollection<Customer> _customers = null;

        public CustomerController(IConfiguration config)
        {
            var mongoDB = config["mongo-DB"];
            var mongo = new MongoClient(mongoDB);
            var db = mongo.GetDatabase("default");
            _customers = db.GetCollection<Customer>("customers");
            var count = Convert.ToInt32(_customers.Count(y => y.Name != string.Empty));
            if (count == 0)
            {
                var CustomerSet = new List<Customer>() {
                    new Customer() { Id = 1, Name = "Mark", Address="USA" },
                    new Customer() { Id=2, Name = "Ravi", Address ="IND"},
                    new Customer() { Id=3, Name="Tony", Address="RUS"}
            };
                _customers.InsertMany(CustomerSet);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            Thread.Sleep(5000);
            return _customers.Find<Customer>(customer=>customer.Id == id).FirstOrDefault();
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
