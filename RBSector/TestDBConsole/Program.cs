using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBConsole.TestService;

namespace TestDBConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService us = new UserService();
            us.AddUser("admin", "admin", "admin", "admin", "admin", "admin");
        }
    }
}
