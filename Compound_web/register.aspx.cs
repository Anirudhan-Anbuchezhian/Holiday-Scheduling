using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace compound_web
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Label1.Text = "";

            string Username = TextBox1.Text;
            string Email = TextBox2.Text;
            string password = TextBox3.Text;
            if (TextBox3.Text != TextBox4.Text)
            {
              
               Label1.Text = "passwords do not match";
                
            }
            if(Email == "")
            {
                Label1.Text = "email cant be blank";
               
            }
            if(Username == "")
            {
                Label1.Text = "username cant be blank";
                
            }
            if(password == "" || TextBox4.Text == "")
            {
                Label1.Text = "password cannot be blank";
                
            }
            




            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
           
           string stri = "SELECT `Email` FROM `Employee` Where `Email` = '"+ Email +"'";

           string stri1 = "SELECT `email` FROM `user` Where `email` = '" + Email + "'";
            MySqlCommand cmd = new MySqlCommand(stri, connection);
            MySqlDataReader rd;

            try
            {

                rd = cmd.ExecuteReader();
                if (!rd.Read())
                {
                    Label1.Text = "employee does not exist";
                }
               
                rd.Close();



            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }

            stri = "SELECT `email` FROM `user` Where `email` = '" + Email + "'";
            cmd = new MySqlCommand(stri, connection);
           

            try
            {

                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Label1.Text = "user already exists";
                }
                rd.Close();




            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }





            if (Email != "")
            {
                try
                {
                    MailAddress m = new MailAddress(Email);

                }
                catch (FormatException)
                {
                    Label1.Text = "Not a valid email";
                }
            }
            

             stri = "SELECT `Username` FROM `user` Where `Username` = '" + Username + "'";
            cmd = new MySqlCommand(stri, connection);

            try
            {

                rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    Label1.Text = "user already exists";
                }
                rd.Close();




            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }





            

         
            

            if (Label1.Text =="")
                {

                stri = "INSERT INTO `user`(`Username`, `email`, `password`, `Access_Level`) VALUES ('"+ Username + "','" + Email + "','"+ password + "','member')";
               cmd = new MySqlCommand(stri, connection);

                string stri4 = "UPDATE `Employee` SET `Username`='"+ Username +"' WHERE `Email` ='"+ Email +"'";
                  MySqlCommand cmd3 = new MySqlCommand(stri4, connection);
                MySqlDataReader rd1;
                //MySqlDataReader rd;


                try
                {
                   
                    rd = cmd.ExecuteReader();
                    rd.Close();



                }
                catch (Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
               
                
    


                try
                {
                    rd1 = cmd3.ExecuteReader();
                    rd1.Close();
                    Response.Redirect("login.aspx");
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
               
            }
           
        }

        
    }
}