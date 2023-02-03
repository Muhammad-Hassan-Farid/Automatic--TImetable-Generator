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
    public partial class StudentView : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        
        public StudentView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminPanel().Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(SelectSemester.Text))
            {
                MessageBox.Show("Enter the Semester !!!");
            }
            else
            {

                string query = "SELECT c.Course_Name, d.Day_Name, s.Slot_Time, t.Teacher_Name,m.Room_No,m.Floor_No FROM "+
                    "timetable.combine k JOIN timetable.courses c ON k.Course_ID = c.Course_ID" +
                    " JOIN timetable.days d ON k.Day_Num = d.Day_Num " +
                    " JOIN timetable.slottiming s ON k.Slot_Num = s.Slot_Num " +
                    " JOIN timetable.faculty t ON k.Teacher_ID = t.Teacher_ID " +
                    " JOIN timetable.semester m ON k.Semester_No = m.Semester_No AND k.Semester_No = '"+SelectSemester.Text+"' order by k.Day_Num ";



                con.Open();

                MySqlCommand cmd = new MySqlCommand(query, con);

                var reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                dataGridView1.DataSource = table;

                con.Close();
            }
        }
    }
}
