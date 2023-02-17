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

namespace Read_and_Learn
{
    public partial class registration_form : Form
    {
        public registration_form()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;

        private void button1_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query01 = new MySqlCommand("SELECT registration FROM user_table WHERE email = '" + textBox5.Text + "'", mycon);
            if(Convert.ToInt32(query01.ExecuteScalar()) != 1)
            {
                if (Variable.global.id_user == 0)
                {
                    MySqlCommand query11 = new MySqlCommand("SELECT id_user FROM user_table WHERE email = '" + textBox5.Text + "'", mycon);
                    Variable.global.id_user = Convert.ToInt32(query11.ExecuteScalar().ToString());
                }
                else
                {

                }

                if (Variable.global.id_user != 0 && textBox4.Text == textBox6.Text)
                {
                    MySqlCommand query21 = new MySqlCommand("UPDATE user_info SET surname = '" + textBox1.Text + "',name = '" + textBox2.Text + "',patronymic = '" + textBox3.Text + "',telephone = '" + maskedTextBox2.Text + "',birthday = '" + maskedTextBox1.Text + "' WHERE id_users = " + Variable.global.id_user, mycon);
                    query21.ExecuteNonQuery();
                    MySqlCommand query22 = new MySqlCommand("UPDATE user_table SET registration = '1' WHERE id_user = " + Variable.global.id_user, mycon);
                    query22.ExecuteNonQuery();
                    MessageBox.Show("Вы зарегистрировались! Обучайтесь!");
                    Form1 dlg = new Form1();
                    dlg.Show();
                    Hide();
                }
                else
                {

                }
            }
            else
            {

            }
            
            

            mycon.Close();
        }

        private void registration_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 dlg = new Form1();
            dlg.Show();
            Hide();
        }
    }
}
