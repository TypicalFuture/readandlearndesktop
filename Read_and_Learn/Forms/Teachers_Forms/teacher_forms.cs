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
    public partial class teacher_forms : Form
    {
        public teacher_forms()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        private Form activeForm = null;

        private void teacher_forms_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query01 = new MySqlCommand("SELECT name FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query02 = new MySqlCommand("SELECT surname FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            label2.Text = query02.ExecuteScalar().ToString() + " " + query01.ExecuteScalar().ToString();
            mycon.Close();

            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
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

        private void teacher_forms_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            Form1 dlg = new Form1();
            Hide();
            dlg.Show();
        }
        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if(Variable.global.atantion == true)
            {
                DialogResult dialogResult = MessageBox.Show("Вы переходите перейти на другую страницу? \nВсе данные удалятся", "Уточнение пользователя", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    openChildForm(new Teachers_Forms.teachers_main());
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                openChildForm(new Teachers_Forms.teachers_main());
            }
            
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new Teachers_Forms.teachers_testing());
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            openChildForm(new Teachers_Forms.teachers_schudule());
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
        }


        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Form1 dlg = new Form1();
            Hide();
            dlg.Show();
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
    }
}
