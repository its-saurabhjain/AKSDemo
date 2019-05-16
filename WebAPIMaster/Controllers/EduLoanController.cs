using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace WebAPIMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EduLoanController : ControllerBase
    {
        private IConfiguration _configuration;
        public EduLoanController(IConfiguration Configuration) {
            _configuration = Configuration;
        }
 
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetBalanceLoan()
        {
            Console.WriteLine("GetBalanceLoan start");
            string grps = await GetDataAsync();
            Console.WriteLine("GetBalanceLoan out");
            return grps;
        }
        [HttpGet("{id}")]
        public ActionResult<string> GetBalanceLoan(string id)
        {
            Console.WriteLine("GetBalanceLoan" + id);
            return id;
        }
        private async Task<string> GetDataAsync() {
            string resultContent = null;
            //var apiUrl = Environment.GetEnvironmentVariable("BACKEND_URL");
            var apiUrl = _configuration.GetSection("BACKEND_URL").GetSection("WebAPIBackEnd").Value;
            Console.WriteLine(apiUrl);
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var result = await client.GetAsync(apiUrl);
                    resultContent = await result.Content.ReadAsStringAsync();
                }
                catch (Exception ex) {

                }
            return resultContent;
            }
        
        }
    }
}
