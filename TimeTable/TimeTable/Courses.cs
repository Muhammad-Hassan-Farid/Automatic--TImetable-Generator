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
    public partial class Courses : Form
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
        public Courses()
        {
            InitializeComponent();
        }

       
        

       

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminPanel().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Faculty().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

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

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (!Subjects())
            {
                return;
            }
        }

        bool Subjects()
        {
            if (string.IsNullOrWhiteSpace(Course_ID.Text) &&
               string.IsNullOrWhiteSpace(courseNAME.Text) &&
               string.IsNullOrWhiteSpace(Cr_H.Text) &&
               string.IsNullOrWhiteSpace(SemesterNotxt.Text))
            {
                MessageBox.Show("Fill Boxs", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            else
            {
                string query = "INSERT INTO timetable.courses (Course_ID,Course_Name,Course_CH,Semester_No) VALUES ('" + Course_ID.Text + "','" + courseNAME.Text + "','" + Cr_H.Text + "','"+ SemesterNotxt.Text+ "')";
                OpenConnection();

                MySqlCommand objcommand = new MySqlCommand(query, con);
                objcommand.ExecuteNonQuery();

                this.CloseConnection();
                MessageBox.Show("Inserted.");
                Course_ID.Text="";
                courseNAME.Text = "";
                Cr_H.Text = "";
                SemesterNotxt.Text = "";


                return true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (Search_Course())
            {
                updateOrDelete.Visible = true;
            }
        }
        bool Search_Course()
        {
            
            if (string.IsNullOrWhiteSpace(searchID.Text))
            {
                MessageBox.Show("Enter the ID !!! ");

            }
            else 
            {
                MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM timetable.courses WHERE Course_ID = '" + searchID.Text + "'", con);
                cmd1.Parameters.AddWithValue("Course_ID", searchID.Text);
                OpenConnection();
                bool CourseExists = false;
                using (var dr1 = cmd1.ExecuteReader())
                    if (CourseExists = dr1.HasRows)
                        adapt = new MySqlDataAdapter("select *from timetable.courses where Course_ID = '" + searchID.Text + "'", con);
                dt = new DataTable();
                adapt.Fill(dt);
                dataGridView2.DataSource = dt;
                this.CloseConnection();
                if (!(CourseExists))
                {
                    MessageBox.Show("Course not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }


           
            return true;
        }



      

        private void button6_Click(object sender, EventArgs e)
        {
            cInsertPanel.Visible = true;
            cUpdateOrDeletePanel.Visible = false;
        }


        private void button7_Click(object sender, EventArgs e)
        {
           
            cUpdateOrDeletePanel.Visible = true;
        }

        private void UpdateBTN_Click(object sender, EventArgs e)
        {
            
            cDeleteRecord.Visible = false;
            cUpdateRecord.Visible = true;
        }

        private void deleteBTN_Click(object sender, EventArgs e)
        {
            cDeleteRecord.Visible = true;
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            if (Cdeltebox.Text != "")
            {
                cmd = new MySqlCommand("delete from timetable.courses where Course_ID= '" + Cdeltebox.Text + "'", con);
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

        private void button14_Click_1(object sender, EventArgs e)
        {

            if (cIDtxt.Text != "" && cNAMEtxt.Text != "" && cHOURStxt.Text != "")
            {
                cmd = new MySqlCommand("update timetable.courses set Course_ID= '" + cIDtxt.Text + "', Course_Name='" + cNAMEtxt.Text + "', Course_CH= '" + cHOURStxt.Text + "', Semester_No = '"+ UpdateSemesterNo.Text + "' where Course_ID = '" + cIDtxt.Text + "'", con);
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
