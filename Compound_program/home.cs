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
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            create f3 = new create();
            f3.ShowDialog();
        }

        void show()
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT * FROM `Employee`";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

            // MySqlDataReader rd = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();
            foreach(DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = dr[4].ToString();
                dataGridView1.Rows[n].Cells[5].Value = dr[5].ToString();
            }


       
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null) {
                edit f4 = new edit();
                f4.label7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                f4.label8.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                f4.comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                f4.comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "")
                {
                   
                    f4.dateTimePicker1.CustomFormat = " ";
                    f4.dateTimePicker1.Format = DateTimePickerFormat.Custom;
                }
                else
                {
                    f4.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                    f4.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                f4.ShowDialog();
            }
            else
            {
                MessageBox.Show("please select a employee from the table to edit");
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
               
                string name = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
                MySqlConnection connection = new MySqlConnection(str);

                connection.Open();
                string stri = "DELETE FROM `Employee` WHERE `Name` = '" + name + "'";

                MySqlCommand cmd = new MySqlCommand(stri, connection);
                MySqlDataReader rd;

                try
                {
                    rd = cmd.ExecuteReader();
                   
                    MessageBox.Show("Employee deleted from database");
                    show();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else
            {
                MessageBox.Show("please select a employee from the table to delete");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Holiday_Request f = new Holiday_Request();
            f.ShowDialog();
        }

        
    }
}
