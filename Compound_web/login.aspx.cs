using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;


namespace compound_web
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri = "SELECT * FROM `user` WHERE `Username` = '" + username + "' AND `password` ='" + password + "' AND `Access_Level` = 'member'";

            MySqlCommand cmd = new MySqlCommand(stri, connection);
            MySqlDataReader rd = cmd.ExecuteReader();



            if (rd.Read())
            {
                connection.Close();
                Session["Username"] = username;
                Response.Redirect("requests.aspx");

            }
            else
            {
                Label1.Text = "Wrong username or password";
            }
           
        }
    }
}