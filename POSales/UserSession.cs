using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSales
{
    public static class UserSession
    {
        public static string Username { get; set; }
        public static string Name { get; set; }
        public static string Role { get; set; }
        public static bool IsActivated { get; set; }
    }

}
