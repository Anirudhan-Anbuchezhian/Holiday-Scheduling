using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compound
{
    public partial class create : Form
    {
        public create()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            string Email = textBox2.Text;
            string Position = comboBox1.Text;
            string Department = comboBox2.Text;
            string start_date = dateTimePicker1.Text;

            int i = 0;

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string stri = "SELECT `Name` FROM `Employee`";
            MySqlDataAdapter cmd1 = new MySqlDataAdapter(stri, connection);

            if(Name == "" || Email =="" || Position == "" || Department == "" || start_date == "")
            {

                MessageBox.Show("Dont leave any fields blank");
                i++;
            }

            if (Name != "")
            {

                DataTable dt = new DataTable();
                cmd1.Fill(dt);



                // MySqlDataReader rd = cmd.ExecuteReader();

              
                foreach (DataRow dr in dt.Rows)
                {

                       if(Name == dr[0].ToString())
                    {
                        MessageBox.Show("username already taken");
                        i++;
                    }
                       
                
                }

            }
            stri = "SELECT `Email` FROM `Employee`";
            cmd1 = new MySqlDataAdapter(stri, connection);

            if (Email != "")
            {

                DataTable dt = new DataTable();
                cmd1.Fill(dt);



                // MySqlDataReader rd = cmd.ExecuteReader();


                foreach (DataRow dr in dt.Rows)
                {

                    if (Email == dr[0].ToString())
                    {
                        MessageBox.Show("Email already taken");
                        i++;
                    }


                }

            }

            try
            {
                MailAddress m = new MailAddress(Email);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Not a valid email address");
                i++;
            }

            if (Position == "Head" || Position == "Deputy Head") {
                stri = "SELECT `Name` FROM `Employee` WHERE `Department` = '"+ Department +"' AND `Position` = '"+ Position +"'";
                MySqlCommand cmd = new MySqlCommand(stri, connection);
                MySqlDataReader rd;

                try
                {

                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        MessageBox.Show("there is already a "+ Position + " in Department " + Department + "");
                        i++;
                    }
                    rd.Close();




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (i == 0)
            {

                stri = "INSERT INTO `Employee`(`Name`, `Email`, `Department`, `Position`, `Start_Date`, `End_Date`, `Username`) VALUES ('" + Name + "','" + Email + "','" + Department + "','" + Position + "','" + start_date + "','','')";
                string stri1 = "INSERT INTO `entitled_holidays`(`Name`, `remaining_holidays`, `total_holidays`) VALUES ('"+ Name + "','30','30')";

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

                cmd = new MySqlCommand(stri1, connection);
                try
                {
                    rd = cmd.ExecuteReader();
                    rd.Close();
                    this.Hide();

                    MessageBox.Show("Employee saved to the database");



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

          

        }
    }
}
