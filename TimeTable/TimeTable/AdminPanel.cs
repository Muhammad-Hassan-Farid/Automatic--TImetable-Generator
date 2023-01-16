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
    public partial class AdminPanel : Form
    {
       
        public AdminPanel()
        {
            InitializeComponent();
            
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {
            new Courses().Show();
            this.Hide();
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            new Faculty().Show();
            this.Hide();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new RoomsORLabs().Show();
            this.Hide();
        }
    }
}
