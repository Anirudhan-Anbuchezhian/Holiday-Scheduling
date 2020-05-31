using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace compound_web
{
    public partial class requests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                show();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }


        void show()
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT `from_date`, `to_date`, `status` FROM `holiday_request` WHERE `Username` = '" + Session["Username"] + "'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(stri, connection);
            DataTable dt = new DataTable();
            cmd1.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string Name = "";
            Label1.Text = "";
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            DateTime from = Convert.ToDateTime(TextBox1.Text);
            DateTime to = Convert.ToDateTime(TextBox2.Text);
            DateTime n = from;
            DateTime now = DateTime.Now;

            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                Label1.Text = "please enter from and to date";
            }

            else
            {
                

                string stri = "SELECT `Name`, `from_date`, `to_date` FROM `holiday_request` WHERE `Username` = '" + Session["Username"] + "'";
                MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);
                DataTable dt_2 = new DataTable();
                cmd.Fill(dt_2);
                while(n <= to){
                foreach (DataRow dr_2 in dt_2.Rows)
                {
                    
                        DateTime ot_from = Convert.ToDateTime(dr_2[1].ToString());
                        DateTime ot_to = Convert.ToDateTime(dr_2[2].ToString());

                        if (n >= ot_from && n <= ot_to)
                        {
                            Label1.Text += "</br> you cant request an holiday on the date " + n.ToString() + " since you have already requested a day off on that date";
                        }



                    }




                   

                    n = n.AddDays(1);
                }
            
           }
            






            if (from > to)
                {
                    Label1.Text += " </br> please select from date less then to date";
                }

                if (from < now)
                {
                    Label1.Text += "</br>request a holiday for the future";
                }
            



            string stri1 = "SELECT `Name` FROM `Employee` WHERE `Username` = '" + Session["Username"] + "'";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(stri1, connection);
            DataTable dt = new DataTable();
            cmd1.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {


                Name = dr[0].ToString();
                Session["name"] = dr[0].ToString();




            }
            if (Label1.Text == "")
            {
                constrints();
                if (Label1.Text == "")
                {
                    string stri = "INSERT INTO `holiday_request`(`Name`, `from_date`, `to_date`, `status`, `Username`) VALUES ('" + Name + "','" + TextBox1.Text + "','" + TextBox2.Text + "','Pending','" + Session["Username"] + "')";

                    MySqlCommand cmd = new MySqlCommand(stri, connection);
                    MySqlDataReader rd;

                    try
                    {
                        rd = cmd.ExecuteReader();
                        Label1.Text = "Request Submitted";



                    }
                    catch (Exception ex)
                    {
                        Label1.Text = ex.ToString();
                    }
                }
            }
        }

        void con_1()
        {
            double i = 0;
            double j = 0;
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            DataTable dt = new DataTable();
            string stri = "SELECT `Department`, `Position` FROM `Employee` WHERE `Name` = '" + Session["name"] + "'";
            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            cmd.Fill(dt);
            DateTime from = Convert.ToDateTime(TextBox1.Text);
            DateTime to = Convert.ToDateTime(TextBox2.Text);
            DateTime n = from;

            foreach (DataRow dr in dt.Rows)
            {


                
                if (dr[1].ToString() == "Head" || dr[1].ToString() == "Deputy Head")
                                {
                   
                   while (n <= to)
                                    {
                        i = 0;
                        j = 0;
                                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` IN ('Head', 'Deputy Head') and `Department` = '" + dr[0].ToString() + "' and `Name` <> '" + Session["name"] + "'";
                                        cmd = new MySqlDataAdapter(stri, connection);
                                        DataTable dt_1 = new DataTable();
                                        cmd.Fill(dt_1);

                                        foreach (DataRow dr_1 in dt_1.Rows)
                                        {
                                            stri = "SELECT `Name`, `from_date`, `to_date` FROM `holiday_request` WHERE `Name` = '" + dr_1[0].ToString() + "' and `status` = 'Accepted'";
                                            cmd = new MySqlDataAdapter(stri, connection);
                                            DataTable dt_2 = new DataTable();
                                            cmd.Fill(dt_2);

                                            foreach (DataRow dr_2 in dt_2.Rows)
                                            {

                                                DateTime ot_from = Convert.ToDateTime(dr_2[1].ToString());
                                                DateTime ot_to = Convert.ToDateTime(dr_2[2].ToString());

                                                if (n >= ot_from && n <= ot_to)
                                                {
                                                    i++;
                                                    //Label1.Text = "";
                                                }



                                            }

                                            j++;

                                        }
                                        if (i == j)
                                        {
                                            Label1.Text += "</br> you cant request an holiday on the date "+ n.ToString() +" since there should be atleast one head or deputy on duty";
                                        }
                        
                                        n = n.AddDays(1);

                                    }


                                }
 
                                                else if (dr[1].ToString() == "Manager")// || dr[0].ToString() == "Senior member")
                                                {
                                                    while (n <= to)
                                                    {
                        i = 0;
                        j = 0;
                                                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` = 'Manager' and `Department` = '" + dr[0].ToString() + "'  and `Name` <> '" + Session["name"] + "'";
                                                        cmd = new MySqlDataAdapter(stri, connection);
                                                        DataTable dt_1 = new DataTable();
                                                        cmd.Fill(dt_1);

                                                        foreach (DataRow dr_1 in dt_1.Rows)
                                                        {
                                                            stri = "SELECT `Name`, `from_date`, `to_date` FROM `holiday_request` WHERE `Name` = '" + dr_1[0].ToString() + "' and `status` = 'Accepted'";
                                                            cmd = new MySqlDataAdapter(stri, connection);
                                                            DataTable dt_2 = new DataTable();
                                                            cmd.Fill(dt_2);

                                                            foreach (DataRow dr_2 in dt_2.Rows)
                                                            {

                                                                DateTime ot_from = Convert.ToDateTime(dr_2[1].ToString());
                                                                DateTime ot_to = Convert.ToDateTime(dr_2[2].ToString());

                                                                if (n >= ot_from && n <= ot_to)
                                                                {
                                                                    i++;
                                                                    //Label1.Text = "";
                                                                }



                                                            }

                                                            j++;

                                                        }
                                                        if (i == j)
                                                        {
                                                            Label1.Text += "you cant request an holiday on the day "+ n.ToString() +" since there should be atleast one Manager on duty";
                                                        }
                                                        n = n.AddDays(1);
                                                    }
                                                }

                                                else if (dr[1].ToString() == "Senior member")
                                                {
                                                    while (n <= to)
                                                    {
                        i = 0;
                        j = 0;
                                                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` = 'Senior member' and `Department` = '" + dr[0].ToString() + "'  and `Name` <> '" + Session["name"] + "'";
                                                        cmd = new MySqlDataAdapter(stri, connection);
                                                        DataTable dt_1 = new DataTable();
                                                        cmd.Fill(dt_1);

                                                        foreach (DataRow dr_1 in dt_1.Rows)
                                                        {
                                                            stri = "SELECT `Name`, `from_date`, `to_date` FROM `holiday_request` WHERE `Name` = '" + dr_1[0].ToString() + "' and `status` = 'Accepted'";
                                                            cmd = new MySqlDataAdapter(stri, connection);
                                                            DataTable dt_2 = new DataTable();
                                                            cmd.Fill(dt_2);

                                                            foreach (DataRow dr_2 in dt_2.Rows)
                                                            {

                                                                DateTime ot_from = Convert.ToDateTime(dr_2[1].ToString());
                                                                DateTime ot_to = Convert.ToDateTime(dr_2[2].ToString());

                                                                if (n >= ot_from && n <= ot_to)
                                                                {
                                                                    i++;
                                                                    //Label1.Text = "";
                                                                }



                                                            }

                                                            j++;

                                                        }
                                                        if (i == j)
                                                        {
                                                            Label1.Text = "you cant request an holiday since there should be atleast one head or deputy on duty";
                                                        }

                                                        n = n.AddDays(1);

                                                    }


                                                }

                if (true)
                {
                    i = 0;
                    j = 0;
                    n = from;
                     while (n <= to)
                  {
                        i = 0;
                        j = 0;
                        stri = "SELECT `Name` FROM `Employee` WHERE `Department` = '" + dr[0].ToString() + "'";
                cmd = new MySqlDataAdapter(stri, connection);
                  DataTable dt_1 = new DataTable();
                        cmd.Fill(dt_1);

                        foreach (DataRow dr_1 in dt_1.Rows)
                    {
                              stri = "SELECT `Name`, `from_date`, `to_date` FROM `holiday_request` WHERE `Name` = '" + dr_1[0].ToString() + "' and `status` = 'Accepted'";
                                                            cmd = new MySqlDataAdapter(stri, connection);
                                                            DataTable dt_2 = new DataTable();
                                                            cmd.Fill(dt_2);

                                                            foreach (DataRow dr_2 in dt_2.Rows)
                                                            {

                                                                DateTime ot_from = Convert.ToDateTime(dr_2[1].ToString());
                                                                DateTime ot_to = Convert.ToDateTime(dr_2[2].ToString());

                                                                if (n >= ot_from && n <= ot_to)
                                                                {
                                                                    i++;
                                                                    //Label1.Text = "";
                                                                }



                                                            }

                                                            j++;

                                                        }
                                                        if ((i+1)/j >= 0.4)
                                                        {
                                                            Label1.Text += "</br>you cant request an holiday on the day "+ n.ToString() +" since there should be atleast 60% of employees to be present in a department";
                                                        }
                                                        n = n.AddDays(1);
                                                    }
                                                }

                                                
            }
        }


        void constrints()
        {

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri = "SELECT `remaining_holidays` FROM `entitled_holidays` WHERE `Name` = '" + Session["name"] + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            cmd.Fill(dt);

            DateTime from = Convert.ToDateTime(TextBox1.Text);
            DateTime to = Convert.ToDateTime(TextBox2.Text);

            foreach (DataRow dr in dt.Rows)
            {


                if ((Int64.Parse(dr[0].ToString()) - ((to - from).TotalDays + 1)) <= 0)//Int64.Parse(dr[0].ToString()) >= 0)
                {
                    Label1.Text = "you have used all your holiday entilement";
                }




            }

            con_1();
        }










    }





}
        

    
