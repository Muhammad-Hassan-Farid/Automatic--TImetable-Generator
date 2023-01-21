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
        MySqlDataAdapter adapt;
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
            //select c.Course_Name, t.Teacher_Name, d.Day_Name, s.Slot_Time from timetable.courses c, timetable.faculty t, timetable.days d, timetable.slottiming s, timetable.combine k where t.Course_ID = c.Course_ID AND s.Slot_Num = K.Slot_Num AND d.Day_Num = k.Day_Num
            // c.Course_ID = t.Course_ID AND k.Slot_Num = s.Slot_Num AND k.Day_Num = d.Day_Num
            string query = "select c.Course_Name, t.Teacher_Name, d.Day_Name, s.Slot_Time from timetable.courses c, timetable.faculty t, timetable.days d, timetable.slottiming s, timetable.combine k where k.PID = 5";

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
