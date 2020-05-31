using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client_app.ServiceReference1;

namespace client_app
{
    public partial class view_request : Form
    {
        WebService1SoapClient obj = new WebService1SoapClient();

        public view_request()
        {
            InitializeComponent();
        }

        private void View_request_Load(object sender, EventArgs e)
        {
            label5.Text = username_class.username;
            DataTable dt = new DataTable();
            try
            {
                dt = obj.view(username_class.username);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

            dataGridView1.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr[2].ToString();
                

            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string success = "";

            DateTime from = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime to = Convert.ToDateTime(dateTimePicker2.Text);


            if (from <= to) {
                success = obj.check_existing(username_class.username, from, to);
                if (success != "success")
                {
                    MessageBox.Show("date already submitted");
                }
                else
                {
                    success = "";
                    success = obj.find_name(username_class.username);
                    if (success == "")
                    {
                        MessageBox.Show("error");
                    }
                    else
                    {

                        success = obj.create(username_class.username, from, to, success);
                        if (success == "Request Submitted")
                        {
                            MessageBox.Show("Request Submitted");
                        }
                        else
                        {
                            MessageBox.Show("Request not Submitted");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("from date should be more then to date");
            }
           


        }
    }
}
