using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Read_and_Learn.Forms
{
    public partial class workstation : Form
    {
        public workstation()
        {
            InitializeComponent();
            
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        private Form activeForm = null;
        Timer timer = new Timer();
        private void workstation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelchildForm.Controls.Add(childForm);
            panelchildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void workstation_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query01 = new MySqlCommand("SELECT name FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query02 = new MySqlCommand("SELECT surname FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            label2.Text += query02.ExecuteScalar().ToString() + " " + query01.ExecuteScalar().ToString() + "\n";
            mycon.Close();

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;

            string time = "";

            if (h < 10)
            {
                time += "0" + h;
            }
            else
            {
                time += h;
            }

            time += ":";

            if (m < 10)
            {
                time += "0" + m;
            }
            else
            {
                time += m;
            }

            time += ":";

            if (s < 10)
            {
                time += "0" + s;
            }
            else
            {
                time += s;
            }

            label3.Text = time;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new main());
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new main());
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new main());
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            openChildForm(new schedule());
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
           
        }
        

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Variable.global.id_user = 0;
            Form1 dlg = new Form1();
            Hide();
            dlg.Show();
        }


        private void bunifuGradientPanel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            openChildForm(new test_selection());
        }
    }
}