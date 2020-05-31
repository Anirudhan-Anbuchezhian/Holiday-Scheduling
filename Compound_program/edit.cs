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
    public partial class edit : Form
    {
        public edit()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Name = label7.Text;
            string Email = label8.Text;
            string Position = comboBox2.Text;
            string Department = comboBox1.Text;
            string end_date;
           
                end_date = dateTimePicker1.Text;

            int i = 0;


            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            if (Position == "Head" || Position == "Deputy Head")
            {
                string stri = "SELECT `Name` FROM `Employee` WHERE `Department` = '" + Department + "' AND `Position` = '" + Position + "' AND `Name` <> '" + Name + "'";
                MySqlCommand cmd = new MySqlCommand(stri, connection);
                MySqlDataReader rd;

                try
                {

                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        MessageBox.Show("there is already a " + Position + " in Department " + Department + "");
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
                string stri = "UPDATE `Employee` SET `Department`='" + Department + "',`Position`='" + Position + "',`End_Date`='" + end_date + "' WHERE `Name`= '" + Name + "' AND `Email`= '" + Email + "'";

                MySqlCommand cmd = new MySqlCommand(stri, connection);
                MySqlDataReader rd;

                try
                {
                    rd = cmd.ExecuteReader();
                    this.Hide();

                    MessageBox.Show("Employee details Updated");



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

       

        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dateTimePicker1.CustomFormat = "";
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }
    }
}

