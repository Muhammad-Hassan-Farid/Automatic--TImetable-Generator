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
    public partial class Loginfrm : Form
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

        public bool Islogin()
        {
            string query = $"SELECT * FROM registered WHERE ID = '{UserIDBox.Text}' AND Password = '{UserPassBox.Text}'";  
            
            try
            {
                if(OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query,con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        reader.Close();
                        con.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        con.Close();
                        return false;
                    }
                }
                else
                {
                    con.Close();
                    return false;


                }
            }
            catch(Exception ex)
            {
                con.Close();
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public Loginfrm()
        {
            InitializeComponent();
            InitializeConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Registrationfrm().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if(Islogin()) 
            {
                MessageBox.Show("Welcome!");
                new AdminPanel().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("This user does not exist");
            }


            
        }
    }
}
