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
    public partial class Login : Form
    {
       WebService1SoapClient obj = new WebService1SoapClient();
        public Login()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string success = "";
            string Username = textBox1.Text;
            string password = textBox2.Text;

            try
            {
               success = obj.login(Username, password);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
             
            
            if(success == "Login successful")
            {
                
                username_class.username = Username;
                this.Hide();
                view_request F = new view_request();
                F.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        
    }
}
