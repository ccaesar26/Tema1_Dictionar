using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar
{
    internal class Admin
    {
        private static string adminUsername = "admin";
        private static string adminPassword = "admin";

        public static string AdminUsername
        {
            get { return adminUsername; }
            set { adminUsername = value; }
        }

        public static string AdminPassword
        {
            get { return adminPassword; }
            set { adminPassword = value; }
        }

        public static bool CheckAdmin(string username, string password)
        {
            return (username == adminUsername && password == adminPassword);
        }
    }
}
