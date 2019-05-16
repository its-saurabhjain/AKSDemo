using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileCart.Managers;
using MobileCart.Models;
using MobileCart.Requests;

namespace MobileCart.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _iconfiguration;
        private ILogger<HomeController> _log;
        public HomeController(IConfiguration configuration,ILogger<HomeController> log)
        {
            _log = log;
            _iconfiguration = configuration;
        }

        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/ProcessOrder")]
        public IActionResult ProcessOrder()
        {
            try
            {
                if (StatusManager.OrderLst.ContainsKey(1))
                {
                    string status;
                    StatusManager.OrderLst.TryRemove(1,out status);
                }
                    ProcessOrderManager request = new ProcessOrderManager(_iconfiguration, _log);
                request.ProcessOrder(new OrderRequest() { Id = 1, Products = new List<int>() { 1, 2 }, CustomerId = 1 });
            }
            catch(Exception ex)
            {
                return Json(ex.Message + ex.StackTrace);
            }
            return Json("ProcessOrder");
        }

        [Route("Home/GetResponse")]
        public IActionResult GetResponse()
        {
            try
            {
                var id = 1;
                var value = string.Empty;
                if (StatusManager.OrderLst.ContainsKey(id))
                    value = StatusManager.OrderLst[id];
                return Json(value);
            }
            catch(Exception ex)
            {
                return Json(ex.Message + ex.StackTrace);
            }
        }

        [Route("Home/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

    }
}
