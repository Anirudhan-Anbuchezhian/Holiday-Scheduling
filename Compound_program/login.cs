using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace compound
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

       

        private void Button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

           connection.Open();
           
             string stri = "SELECT * FROM `user` WHERE `Username` = '"+username+ "' AND `password` ='" + password + "' AND `Access_Level` = 'Admin'";

             MySqlCommand cmd = new MySqlCommand(stri,connection);
            MySqlDataReader rd = cmd.ExecuteReader();



             if(rd.Read())
             {
                connection.Close();
                this.Hide();
                home f2 = new home();
                f2.ShowDialog();
                
             }
            else
            {
                MessageBox.Show("Wrong Password or Username");
            }
        }
    }
}
