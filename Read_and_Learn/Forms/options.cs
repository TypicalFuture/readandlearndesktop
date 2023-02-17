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
    public partial class options : Form
    {
        public options()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        
        private void options_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query01 = new MySqlCommand("SELECT surname FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            textBox1.Text = query01.ExecuteScalar().ToString();
            MySqlCommand query02 = new MySqlCommand("SELECT name FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            textBox2.Text = query02.ExecuteScalar().ToString();
            MySqlCommand query03 = new MySqlCommand("SELECT patronymic FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            textBox3.Text = query03.ExecuteScalar().ToString();
            /*MySqlCommand query04 = new MySqlCommand("SELECT email FROM user_table WHERE id_user =" + Variable.global.id_user, mycon);
            textBox4.Text = query04.ExecuteScalar().ToString();
            */
            MySqlCommand query05 = new MySqlCommand("SELECT id_group FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query06 = new MySqlCommand("SELECT groyp FROM groyp WHERE id_group =" + query05.ExecuteScalar().ToString(), mycon);
            textBox8.Text = query06.ExecuteScalar().ToString();

            mycon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            if (textBox4.TextLength != 0 && textBox6.TextLength != 0 && textBox7.TextLength != 0)
            {
                MySqlCommand query01 = new MySqlCommand("SELECT password FROM user_table WHERE id_user =" + Variable.global.id_user, mycon);
                if (textBox4.Text == query01.ExecuteScalar().ToString())
                {
                    if (textBox6.Text == textBox7.Text)
                    {
                        MySqlCommand query02 = new MySqlCommand("UPDATE user_table SET password = '" + textBox6.Text + "' WHERE id_user =" + Variable.global.id_user, mycon);
                        query02.ExecuteNonQuery();
                        MessageBox.Show("Пароль успешно изменен");
                        textBox4.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!", "Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Ваш старый пароль не совпадает! Проверьте правильность");
                }
            }
            else
            {
                MessageBox.Show("Вы не заполнили все поля");
            }

            mycon.Close();
        }
    }
}
