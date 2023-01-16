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
    public partial class RoomsORLabs : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        public RoomsORLabs()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new AdminPanel().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Courses().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Courses().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FacultyView().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new StudentView().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }


        private void button9_Click(object sender, EventArgs e)
        {
           if(insertRoomOrLab())
            {
                MessageBox.Show("Inserted");
            }
        }
        bool insertRoomOrLab()
        {
            if (string.IsNullOrWhiteSpace(floorno.Text) &&
              string.IsNullOrWhiteSpace(RoomNo.Text) &&
              string.IsNullOrWhiteSpace(RoomType.Text) &&
              string.IsNullOrWhiteSpace(Capacity.Text) &&
              string.IsNullOrWhiteSpace(CourseIDtxt.Text)
              )
            {
                MessageBox.Show("Fill Empty Blanks!!!");
                return false;
            }
            else
            {

                string query = "INSERT INTO timetable.roomorlab (Floor,Room_No,Room_Type,Capacity,Course_ID)  VALUES ('" + floorno.Text + "', '" + RoomNo.Text + "','" + RoomType.Text + "', '" + Capacity.Text + "', '" + CourseIDtxt.Text + "')";

                con.Open();

                MySqlCommand objcommand = new MySqlCommand(query, con);
                objcommand.ExecuteNonQuery();

                con.Close();

                return true;

            }
        }
    }
}
