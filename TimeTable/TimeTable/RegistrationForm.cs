using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TimeTable
{
    public partial class Registrationfrm : Form
    {
        private MySqlConnection con;
        private string server, database, uid, password;

        //intailizing Connection
        private void InitializeConnection()
        {
            server = "localhost";
            database = "timetable";
            uid = "root";
            password = "";

            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password;

            con = new MySqlConnection(connectionString);
        }
        //open Connection
        private bool OpenConnection()
        {
            try
            {
                con.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        {
                            MessageBox.Show("Cannot connect to Server. Contact to Admin.");
                            break;
                        }
                    case 1045:
                        {
                            MessageBox.Show("Invalide User ID or Password.");
                            break;
                        }
                }
                return false;
            }
        }
        //Close Connection
        private bool CloseConnection()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public Registrationfrm()
        {
            InitializeComponent();
            InitializeConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                return;
            }
        }

        bool Authenticate()
        {
            if (string.IsNullOrWhiteSpace(UserIDBox.Text) &&
               string.IsNullOrWhiteSpace(UserPassBox.Text) &&
               string.IsNullOrWhiteSpace(UserConfirmBox.Text))
            {
                MessageBox.Show("Fill Boxs", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if(UserPassBox.Text != UserConfirmBox.Text )
            {
                MessageBox.Show("Password Incorrect", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                string query = "INSERT INTO registered (ID,Password) VALUES ('" + UserIDBox.Text + "','" + UserPassBox.Text + "')";
                OpenConnection();

                MySqlCommand objcommand = new MySqlCommand(query, con);
                objcommand.ExecuteNonQuery();

                this.CloseConnection();
                MessageBox.Show("Inserted.");
                return true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new Loginfrm().Show();
            this.Hide();
        }
    }
}
