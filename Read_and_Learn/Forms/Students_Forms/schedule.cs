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
    public partial class schedule : Form
    {
        public schedule()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        
        private void schedule_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            weeknd();

            /*
            MySqlCommand query02 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_group =" + Variable.global.user_group + " AND whats_the_score = " + "1", mycon);
            label3.Text = query02.ExecuteScalar().ToString();
            
            while (i < Convert.ToInt32(query01.ExecuteScalar().ToString()))
            {
                i++;
                MySqlCommand query11 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_group =" + Variable.global.st_teac + " AND  " + i, mycon);
                MySqlCommand query12 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson =" + query11.ExecuteScalar().ToString(), mycon);

                label2.Text = "\n" + i + ".  " + query12.ExecuteScalar().ToString();
            }
            */
        }
        /*
        private void table_box()
        {
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_weekend) FROM weekend", mycon);
            int i_vopros = Convert.ToInt32(query01.ExecuteScalar().ToString());
            int x_loc = 20, y_loc = 20;

            var temp_otvet = new RadioButton[i_vopros];
            for (int i = 0; i < i_vopros;)
            {
                i++;
                temp_otvet[i].Location = new System.Drawing.Point(x_loc, y_loc + 60);
                temp_otvet[i].Size = new Size(30, 15);
                temp_otvet[i].Visible = true;
                MySqlCommand query11 = new MySqlCommand("SELECT answer FROM answer WHERE id_question = " + 1 + " AND id_answer = " + i, mycon);
                temp_otvet[i].Text = query11.ExecuteScalar().ToString();
                temp_otvet[i].Name = "radiobutton_" + i;
                panel1.Controls.Add(temp_otvet[i]);
            }
            mycon.Close();
        }
        */

        private void weeknd()
        {
            
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_schedule) FROM schedule WHERE id_group =" + Variable.global.user_group, mycon);
            int i, week_1 = 1, week_2 = 1, week_3 = 1, week_4 = 1, week_5 = 1;
            int weeknd = 1;
            for (i = 0; i < Convert.ToInt32(query01.ExecuteScalar().ToString());)
            {
                i++;
                MySqlCommand query11 = new MySqlCommand("SELECT id_schedule FROM schedule WHERE id_group = " + Variable.global.user_group + " AND id_schedule = " + i, mycon);
                int fff = Convert.ToInt32(query11.ExecuteScalar().ToString());
                MySqlCommand query12 = new MySqlCommand("SELECT id_weekend FROM schedule WHERE id_schedule = " + query11.ExecuteScalar().ToString(), mycon);
                weeknd = Convert.ToInt32(query12.ExecuteScalar().ToString());
                switch (weeknd)
                {
                    case 1 :
                        MySqlCommand query111 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_schedule = " + Convert.ToInt32(query11.ExecuteScalar().ToString()) 
                            + " AND whats_the_score = " + week_1, mycon);
                        MySqlCommand query112 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson ="
                            + Convert.ToInt32(query111.ExecuteScalar().ToString()), mycon);
                        label2.Text += week_1 + ".  " + query112.ExecuteScalar().ToString() + "\n";
                        week_1 += 1;
                        break;
                    case 2:
                        MySqlCommand query113 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_schedule = " + Convert.ToInt32(query11.ExecuteScalar().ToString()) 
                            + " AND whats_the_score = " + week_2, mycon);
                        MySqlCommand query114 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson ="
                            + Convert.ToInt32(query113.ExecuteScalar().ToString()), mycon);
                        label3.Text += week_2 + ".  " + query114.ExecuteScalar().ToString() + "\n";
                        week_2 += 1;
                        break;
                    case 3:
                        MySqlCommand query115 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_schedule = " + Convert.ToInt32(query11.ExecuteScalar().ToString()) 
                            + " AND whats_the_score = " + week_3, mycon);
                        MySqlCommand query116 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson =" 
                            + Convert.ToInt32(query115.ExecuteScalar().ToString()), mycon);
                        label5.Text += week_3 + ".  " + query116.ExecuteScalar().ToString() + "\n";
                        week_3 += 1;
                        break;
                    case 4:
                        MySqlCommand query117 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_schedule = " + Convert.ToInt32(query11.ExecuteScalar().ToString()) 
                            + " AND whats_the_score = " + week_4, mycon);
                        MySqlCommand query118 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson =" 
                            + Convert.ToInt32(query117.ExecuteScalar().ToString()), mycon);
                        label7.Text += week_4 + ".  " + query118.ExecuteScalar().ToString() + "\n";
                        week_4 += 1;
                        break;
                    case 5:
                        MySqlCommand query119 = new MySqlCommand("SELECT id_subject_of_the_lesson FROM schedule WHERE id_schedule = " + Convert.ToInt32(query11.ExecuteScalar().ToString()) 
                            + " AND whats_the_score = " + week_5, mycon);
                        MySqlCommand query121 = new MySqlCommand("SELECT subject_of_the_lesson FROM subject_of_the_lesson WHERE id_subject_of_the_lesson =" 
                            + Convert.ToInt32(query119.ExecuteScalar().ToString()), mycon);
                        label9.Text += week_5 + ".  " + query121.ExecuteScalar().ToString() + "\n";
                        week_5 += 1;
                        break;

                }
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }
    }
}
