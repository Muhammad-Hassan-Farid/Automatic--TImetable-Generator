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
            string query = "select f.Teacher_Name, c.Course_Name, c.Course_CH, s.Slot_Time from timetable.faculty f, timetable.courses c, timetable.slottiming s where s.Slot_Num  = 1 AND c.Course_ID = 123456";

            

            MySqlCommand cmd1 = new MySqlCommand("select f.Teacher_Name, c.Course_Name, c.Course_CH, s.Slot_Time from timetable.faculty f, timetable.courses c, timetable.slottiming s where s.Slot_Num  = 1 AND c.Course_ID = 123456", con);
            
            con.Open();

            bool CourseExists = false;
            using (var dr1 = cmd1.ExecuteReader())
                if (CourseExists = dr1.HasRows)
                    adapt = new MySqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
           

        }
    }
}
