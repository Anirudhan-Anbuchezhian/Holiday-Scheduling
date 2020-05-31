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
    public partial class view_all_bookings : Form
    {
        public view_all_bookings()
        {
            InitializeComponent();
        }

        private void View_all_bookings_Load(object sender, EventArgs e)
        {
            show();

        }

        void show()
        {

            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();

            string stri = "SELECT * FROM `holiday_request`";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

            // MySqlDataReader rd = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr[3].ToString();

            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string str = "server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";// "Data Source=sql-server.cms.gre.ac.uk;Initial Catalog=aa0854y;User ID=aa0854y;Password=!1SQLServer";//"server=mysql.cms.gre.ac.uk;user id=aa0854y;password=Gertrud9915@;database=mdb_aa0854y";
            MySqlConnection connection = new MySqlConnection(str);

            connection.Open();
            string name = textBox1.Text;
            string stri = "SELECT * FROM `holiday_request` WHERE `Name` = '"+ name +"'";

            MySqlDataAdapter cmd = new MySqlDataAdapter(stri, connection);

            DataTable dt = new DataTable();
            cmd.Fill(dt);

            // MySqlDataReader rd = cmd.ExecuteReader();

            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr[3].ToString();

            }
        }
    }
}
