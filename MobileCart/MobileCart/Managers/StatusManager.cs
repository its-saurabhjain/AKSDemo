using MobileCart.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileCart.Managers
{
    public static class StatusManager
    {
        public static ConcurrentDictionary<int, string> OrderLst;

        static StatusManager()
        {
            OrderLst = new ConcurrentDictionary<int, string>();
        }        
    }
}
