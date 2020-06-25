using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media_Bazaar
{
    public class Logger
    {
        private string user;
        private string pass;

   
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //user = tbUsername.Text;
            //pass = tbPassword.Text;
            //MySqlConnection conn = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi428501;Database=dbi428501;Pwd=1234;");

            //MySqlCommand cmdAdmin = new MySqlCommand($"SELECT Username, Password FROM Employee WHERE Username = '{user}' AND Password = '{pass}' AND Position='ADMINISTRATOR';", conn);
            //MySqlCommand cmdManager = new MySqlCommand($"SELECT Username, Password FROM Employee WHERE Username = '{user}' AND Password = '{pass}' AND Position='MANAGER';", conn);
            //MySqlCommand cmdDepot = new MySqlCommand($"SELECT Username, Password FROM Employee WHERE Username = '{user}' AND Password = '{pass}' AND Position='DEPOT';", conn);
            //conn.Open();

            //MySqlDataAdapter sdaAdmin = new MySqlDataAdapter(cmdAdmin);
            //DataTable dtAdmin = new DataTable();
            //sdaAdmin.Fill(dtAdmin);

            //MySqlDataAdapter sdaManager = new MySqlDataAdapter(cmdManager);
            //DataTable dtManager = new DataTable();
            //sdaManager.Fill(dtManager);

            //MySqlDataAdapter sdaDepot = new MySqlDataAdapter(cmdDepot);
            //DataTable dtDepot = new DataTable();
            //sdaDepot.Fill(dtDepot);

            //if (dtAdmin.Rows.Count > 0 || (user == "Admin" && pass == "1234"))
            //{
            //    tbUsername.Text = "Username";
            //    tbPassword.Text = "Password";
            //    MainAdmin MainAdmin = new MainAdmin();
            //    MainAdmin.Show();
            //    this.Visible = false;
            //    conn.Close();
            //}
            //else if (dtManager.Rows.Count > 0 || (user == "Manager" && pass == "1234"))
            //{
            //    tbUsername.Text = "Username";
            //    tbPassword.Text = "Password";
            //    MainManager MainManager = new MainManager();
            //    MainManager.Show();
            //    this.Visible = false;
            //    conn.Close();
            //}
            //else if (dtDepot.Rows.Count > 0 || (user == "Depot" && pass == "1234"))
            //{
            //    tbUsername.Text = "Username";
            //    tbPassword.Text = "Password";
            //    MainDepot MainDepot = new MainDepot();
            //    MainDepot.Show();
            //    this.Visible = false;
            //    conn.Close();
            //}
            //else
            //{

            //    MessageBox.Show("Wrong Username/Password");
            //    tbUsername.Text = "Username";
            //    tbPassword.Text = "Password";
            //    conn.Close();
            //}

        }
    }
}
