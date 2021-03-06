﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrderApprovalServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrderApprovalController : Controller
    {
        private bool orderApproved { get; set; }

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public bool Get(int id)
        {
            Thread.Sleep(5000);
            return true;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]int id)
        {
            orderApproved = true;
        }

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
