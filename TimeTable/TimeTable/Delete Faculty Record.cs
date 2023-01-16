using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TimeTable
{
    public partial class Delete_Faculty_Record : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlDataAdapter adapt;
        DataTable dt;
        MySqlCommand cmd;
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
        public Delete_Faculty_Record()
        {
            InitializeComponent();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            new Faculty().Show();
            this.Hide();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            new Update_Faculty_Record().Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DeleteTeacher.Text != "")
            {
                cmd = new MySqlCommand("delete from timetable.faculty where Teacher_ID= '" + DeleteTeacher.Text + "'", con);
                OpenConnection();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.CloseConnection();

            }
            else
            {
                MessageBox.Show("Delete Box is empty!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminPanel().Show();
            this.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Courses().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FacultyView().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new StudentView().Show();
            this.Hide();
        }
    }
}
