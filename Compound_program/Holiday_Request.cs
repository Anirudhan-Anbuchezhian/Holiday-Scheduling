using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compound
{
    public partial class Holiday_Request : Form
    {
        string s = "";
        
        public Holiday_Request()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            try
            {


                if(dataGridView1.CurrentRow.Cells[1].Selected)//dataGridView1.CurrentRow.Cells[0].Value != null && dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Pending")
                {
                    string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
                    MySqlConnection connection = new MySqlConnection(str);

                    connection.Open();
                    int rem = 0;

                    string name = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    string stri = "SELECT `remaining_holidays` FROM `entitled_holidays` WHERE `Name` = '"+ name +"'";

                    MySqlDataAdapter cmdd = new MySqlDataAdapter(stri, connection);

                    DataTable dt = new DataTable();
                    cmdd.Fill(dt);

                    // MySqlDataReader rd = cmd.ExecuteReader();

                   
                    foreach (DataRow dr in dt.Rows)
                    {
                       rem = int.Parse(dr[0].ToString());
                        

                    }
                    int totalDays = Int32.Parse((Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value.ToString()) - Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value.ToString())).Days.ToString());
                    stri = "UPDATE `entitled_holidays` SET `remaining_holidays`='" + (rem - totalDays - 1).ToString() +"' WHERE `Name` = '" + name + "'";

                    MySqlCommand cmd = new MySqlCommand(stri, connection);
                    MySqlDataReader rd;

                    try
                    {
                        rd = cmd.ExecuteReader();

                        rd.Close();
                      

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                   stri = "UPDATE `holiday_request` SET `status`='Accepted' WHERE `Name` = '" + name + "' AND `from_date` = '"+ dataGridView1.CurrentRow.Cells[1].Value.ToString() +"' AND `to_date` = '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";

                    cmd = new MySqlCommand(stri, connection);
                    

                    try
                    {
                        rd = cmd.ExecuteReader();

                        rd.Close();
                        MessageBox.Show("request has been accepted");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (dataGridView2.CurrentRow.Cells[1].Selected)//dataGridView2.CurrentRow.Cells[0].Value != null && dataGridView2.CurrentRow.Cells[3].Value.ToString() == "Pending")
                {
                    MessageBox.Show("you cannot accept this request since its breaks a constraint");
                }
                else
                {
                    MessageBox.Show("select a request that is pending");
                    show();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void show()
        {

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT * FROM `holiday_request` Where `status` = 'Pending'";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

            // MySqlDataReader rd = cmd.ExecuteReader();
            int n;
            int m;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
               s = "";
                
               
               
                variables.name = dr[0].ToString();
                variables.from_date = dr[1].ToString();
                variables.to_date = dr[2].ToString();
                variables.username = dr[4].ToString();
               
                constrints();

                if (s == "")
                {
                    n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = dr[3].ToString();
                    

                }
                else
                {
                    m = dataGridView2.Rows.Add();
                    dataGridView2.Rows[m].Cells[0].Value = dr[0].ToString();
                    dataGridView2.Rows[m].Cells[1].Value = dr[1].ToString();
                    dataGridView2.Rows[m].Cells[2].Value = dr[2].ToString();
                    dataGridView2.Rows[m].Cells[3].Value = dr[3].ToString();
                   
                }

            }

        }

        // constraint check
        void con_1()
        {
           
            double i = 0;
            double j = 0;
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            DataTable dt = new DataTable();
            string stri = "SELECT `Department`, `Position` FROM `Employee` WHERE `Name` = '" + variables.name + "'";
            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            cmd.Fill(dt);
            DateTime from = Convert.ToDateTime(variables.from_date);
            DateTime to = Convert.ToDateTime(variables.to_date);
            DateTime n = from;

            foreach (DataRow dr in dt.Rows)
            {



                if (dr[1].ToString() == "Head" || dr[1].ToString() == "Deputy Head")
                {
                    

                    while (n <= to)
                    {
                        i = 0;
                        j = 0;
                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` IN ('Head', 'Deputy Head') and `Department` = '" + dr[0].ToString() + "' and `Name` <> '" + variables.name + "'";
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
                            s = "you cant request an holiday on the date " + n.ToString() + " since there should be atleast one head or deputy on duty";
                            
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
                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` = 'Manager' and `Department` = '" + dr[0].ToString() + "'  and `Name` <> '" + variables.name + "'";
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
                           s = "you cant request an holiday on the day " + n.ToString() + " since there should be atleast one Manager on duty";
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
                        stri = "SELECT `Name` FROM `Employee` WHERE `Position` = 'Senior member' and `Department` = '" + dr[0].ToString() + "'  and `Name` <> '" + variables.name + "'";
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
                            s = "you cant request an holiday since there should be atleast one head or deputy on duty";
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
                        if ((i + 1) / j >= 0.4)
                        {
                           s = "you cant request an holiday on the day " + n.ToString() + " since there should be atleast 60% of employees to be present in a department";
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
            string stri = "SELECT `remaining_holidays` FROM `entitled_holidays` WHERE `Name` = '" + variables.name + "'";
            DataTable dt = new DataTable();
            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            cmd.Fill(dt);

            DateTime from = Convert.ToDateTime(variables.from_date);
            DateTime to = Convert.ToDateTime(variables.to_date);

            foreach (DataRow dr in dt.Rows)
            {


                if ((Int64.Parse(dr[0].ToString()) - ((to - from).TotalDays + 1)) <= 0)//Int64.Parse(dr[0].ToString()) >= 0)
                {
                  s = "you have used all your holiday entilement";
                }




            }

            con_1();
        }

        // constraint check
        private void Button4_Click(object sender, EventArgs e)
        {
            show();
        }

        private void Holiday_Request_Load(object sender, EventArgs e)
        {

            show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dataGridView1.CurrentRow.Cells[1].Selected)//dataGridView1.CurrentRow.Cells[0].Value != null && dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Pending")
                {
                    string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
                    MySqlConnection connection = new MySqlConnection(str);

                    connection.Open();
                    string name = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    string stri = "UPDATE `holiday_request` SET `status`='Rejected' WHERE `Name` = '" + name + "'  AND `from_date` = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' AND `to_date` = '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";

                    MySqlCommand cmd = new MySqlCommand(stri, connection);
                    MySqlDataReader rd;

                    try
                    {
                        rd = cmd.ExecuteReader();


                        MessageBox.Show("request has been Rejected");



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (dataGridView2.CurrentRow.Cells[1].Selected)//dataGridView2.CurrentRow.Cells[0].Value != null && dataGridView2.CurrentRow.Cells[3].Value.ToString() == "Pending")
                {
                    string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
                    MySqlConnection connection = new MySqlConnection(str);

                    connection.Open();
                    string name = dataGridView2.CurrentRow.Cells[0].Value.ToString();

                    string stri = "UPDATE `holiday_request` SET `status`='Rejected' WHERE `Name` = '" + name + "'  AND `from_date` = '" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "' AND `to_date` = '" + dataGridView2.CurrentRow.Cells[2].Value.ToString() + "'";

                    MySqlCommand cmd = new MySqlCommand(stri, connection);
                    MySqlDataReader rd;

                    try
                    {
                        rd = cmd.ExecuteReader();


                        MessageBox.Show("request has been Rejected");



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("choose a request from the table or an action has already been taken for this request");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("no request avaliable for an action");
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            view_all_bookings f = new view_all_bookings();
            f.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            working_Leave f = new working_Leave();
            f.label4.Text = dateTimePicker1.Text;
            f.ShowDialog();
            
        }

        
    }
}
