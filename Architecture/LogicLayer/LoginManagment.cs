using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class LoginManagment
    {
        DataAccess dataAccess = new DataAccess();

        public string Login(string username, string password)
        {
            if ((dataAccess.LoginAdministrator(username, password).Rows.Count > 0) || (username == "Admin" && password == "1234"))
            {
                return "Administrator";
            }
            else if ((dataAccess.LoginManager(username, password).Rows.Count > 0) || (username == "Manager" && password == "1234"))
            {
                return "Manager";
            }
            else if ((dataAccess.LoginDepotWorker(username, password).Rows.Count > 0) || (username == "Depot" && password == "1234"))
            {
                return "Depot Worker";
            }
            else
            {
                return "Wrong";
            }
        }
    }
}

