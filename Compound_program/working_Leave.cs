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
    public partial class working_Leave : Form
    {
        public working_Leave()
        {
            InitializeComponent();
        }

        public string[] emp_leave = {"hi"};
        private void Working_Leave_Load(object sender, EventArgs e)
        {
            leave();
            working();
            
        }

        void leave()
        {
           

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT * FROM `holiday_request` Where `status` = 'Accepted'";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

            // MySqlDataReader rd = cmd.ExecuteReader();
            int i = 0;
            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
               
                
                DateTime searchdate = Convert.ToDateTime(label4.Text);
                DateTime from = Convert.ToDateTime(dr[1].ToString());
                DateTime to = Convert.ToDateTime(dr[2].ToString());
                try
                {
                    if (searchdate >= from && searchdate <= to)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = label4.Text;
                        emp_leave[i] = dr[0].ToString();
                        i++;


                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }

        }
        void working()
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT `Name` FROM `Employee`";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

           

            // MySqlDataReader rd = cmd.ExecuteReader();

            dataGridView2.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
               
                
                
                    if (emp_leave.Contains(dr[0].ToString()) == false)
                   {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = dr[0].ToString();
                       dataGridView2.Rows[n].Cells[1].Value = label4.Text;
                    }
                
                
                
                
               
            }
        }
    }
}
