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
    public partial class Faculty : Form
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
        public Faculty()
        {
            InitializeComponent();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            fInsertPanel.Visible = true;
            fUpdateOrDelete.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            fUpdateOrDelete.Visible = true;
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

        private void button8_Click(object sender, EventArgs e)
        {
            fUpdatepanel.Visible = true;
            fDeletePanel.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
         

            if (UPteacherID.Text != "" && UPteacherNAME.Text != "" && UPteacherCOURSE.Text != "" && UPcourseID.Text != "")
            {
                cmd = new MySqlCommand("update timetable.faculty set Teacher_ID= '" + UPteacherID.Text + "', Teacher_Name='" + UPteacherNAME.Text + "', Teacher_Course= '" + UPteacherCOURSE.Text + "', Course_ID= '" + UPcourseID.Text + "' where Teacher_ID = '" + UPteacherID.Text + "'", con);
                OpenConnection();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.CloseConnection();

            }
            else
            {
                MessageBox.Show("Select the record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Search_Teacher())
            {
                fUpdateORdeletePanel.Visible = true;
            }
        }
        bool Search_Teacher()
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM timetable.faculty WHERE Teacher_ID = '" + SearchTeacherID.Text + "'", con);
            cmd1.Parameters.AddWithValue("Teacher_ID", SearchTeacherID.Text);
            OpenConnection();
            bool userExists = false;
            using (var dr1 = cmd1.ExecuteReader())
                if (userExists = dr1.HasRows)
                    adapt = new MySqlDataAdapter("select * from timetable.faculty where Teacher_ID like '" + SearchTeacherID.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView3.DataSource = dt;
            this.CloseConnection();


            if (!(userExists))   
            {
                MessageBox.Show("ID not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void button10_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            fDeletePanel.Visible = true;
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            if (!Teacher())
            {
                return;
            }
        }

        bool Teacher()
        {
            if (string.IsNullOrWhiteSpace(TeacherID.Text) &&
               string.IsNullOrWhiteSpace(TeacherNAME.Text) &&
               string.IsNullOrWhiteSpace(TeacherCOURSE.Text) &&
               string.IsNullOrWhiteSpace(TeacherCOURSEID.Text))
            {
                MessageBox.Show("Fill Boxs", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else if (UserPassBox.Text != UserConfirmBox.Text)
            //{
            //  MessageBox.Show("Course Already Exits.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            // return false;
            //}
            else
            {
                string query = "INSERT INTO timetable.faculty (Teacher_ID,Teacher_Name,Teacher_Course,Course_ID) VALUES ('" + TeacherID.Text + "','" + TeacherNAME.Text + "','" + TeacherCOURSE.Text + "','" + TeacherCOURSEID.Text + "')";
                OpenConnection();

                MySqlCommand objcommand = new MySqlCommand(query, con);
                objcommand.ExecuteNonQuery();

                this.CloseConnection();
                TeacherID.Text = "";
                TeacherNAME.Text = "";
                TeacherCOURSE.Text = "";
                TeacherCOURSEID.Text = "";
                MessageBox.Show("Inserted.");
                return true;
            }
        }
    }
}
