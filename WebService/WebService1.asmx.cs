using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Data;


namespace compound_client_webservices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string login(string username, string password)
        {
           
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri = "SELECT * FROM `user` WHERE `Username` = '" + username + "' AND `password` ='" + password + "' AND `Access_Level` = 'member'";

            MySqlCommand cmd = new MySqlCommand(stri, connection);
            MySqlDataReader rd;
            try
            {
                rd = cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }



            if (rd.Read())
            {
                connection.Close();
                return "Login successful";
                 
                //  Response.Redirect("requests.aspx"); 

            }
            else
            {
                return "Wrong username or password";
            }

        }

       
    

        [WebMethod]
        public DataTable view(string username)
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            
            string stri = "SELECT `from_date`, `to_date`, `status` FROM `holiday_request` WHERE `Username` = '" + username + "'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(stri, connection);
            DataTable dt = new DataTable("request");
            
            cmd1.Fill(dt);
           
            return dt;
        }

        [WebMethod]
        public string check_existing(string username, DateTime from, DateTime to)
        {
            string suc = "";
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            int i = 0;

            DateTime n = from;
            while ((n <= to))// && !(n>=Convert.ToDateTime("23/12/"+ cur_year +"") && n <= Convert.ToDateTime("03/03/" + nex_year + "")))
            {
                string stri = "SELECT `from_date`, `to_date` FROM `holiday_request` WHERE `Username` = '" + username + "'";
                MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);
                DataTable dt1 = new DataTable();
                cmd.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {

                    DateTime fromm = Convert.ToDateTime(dr[0].ToString());
                    DateTime too = Convert.ToDateTime(dr[1].ToString());



                    if (n >= fromm && n <= too)
                    {
                        i++;
                        //Label1.Text = "";
                    }



                }

                if (i > 0)
                {
                    suc = "you already requested a day off for this date";
                }

                n = n.AddDays(1);


            }

            if(suc == "")
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        [WebMethod]
        public string find_name(string username)
        {
            string name = "";
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri1 = "SELECT `Name` FROM `Employee` WHERE `Username` = '" + username + "'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(stri1, connection);
            DataTable dt = new DataTable();
            cmd1.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {


               
               name = dr[0].ToString();




            }
            return name;
        }

        [WebMethod]
        public string create(string username, DateTime from, DateTime to, string name)
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri = "INSERT INTO `holiday_request`(`Name`, `from_date`, `to_date`, `status`, `Username`) VALUES ('" + name + "','" + from + "','" + to + "','Pending','" + username + "')";

            MySqlCommand cmd = new MySqlCommand(stri, connection);
            MySqlDataReader rd;

            try
            {
                rd = cmd.ExecuteReader();
               return "Request Submitted";



            }
            catch (Exception ex)
            {
               return ex.ToString();
            }
        }

       
       
    }
}
