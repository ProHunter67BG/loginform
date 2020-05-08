using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LoginForm
{
    public partial class Login : Form
    {
        static string path = Path.GetFullPath(Environment.CurrentDirectory);
        static string databaseName = "sqldb.mdf";

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; AttachDbFilename=" + path + @"\" + databaseName + "; Integrated Security=True;";

        Thread thread;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Users where Username='" + textBox1.Text + "' and Password ='" + textBox2.Text + "'";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if(dataTable.Rows.Count == 1)
                {
                    this.Close();
                    thread = new Thread(opendashboard);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
        }
        private void opendashboard(object obj)
        {
            Application.Run(new Dashboard());
        }
    }
}
