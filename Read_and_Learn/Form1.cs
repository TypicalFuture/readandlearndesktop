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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        string login, password, idenf;
        int reg_vvod;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch
            {

            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            if (mycon.State == ConnectionState.Open)
            {

                login = textBox1.Text;
                password = textBox2.Text;
                MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_user) FROM user_table", mycon);
                int count_user1 = Convert.ToInt32(query01.ExecuteScalar().ToString());
                int i = 0;
                while (i < count_user1)
                {
                    i++;
                    MySqlCommand query11 = new MySqlCommand("SELECT email FROM user_table WHERE id_user =" + i, mycon);
                    MySqlCommand query12 = new MySqlCommand("SELECT password FROM user_table WHERE id_user =" + i, mycon);
                    if (login == query11.ExecuteScalar().ToString() && password == query12.ExecuteScalar().ToString())
                    {
                        Variable.global.id_user = i;
                        login = null;
                        password = null;
                        i = 0;
                        count_user1 = 0;
                        break;
                    }
                    else
                    {

                    }

                }
                try
                {
                    MySqlCommand query02 = new MySqlCommand("SELECT registration FROM user_table WHERE id_user =" + Variable.global.id_user, mycon);
                    if (Variable.global.id_user == 0)
                    {
                        reg_vvod = -1;
                    }
                    else
                    {
                        reg_vvod = Convert.ToInt32(query02.ExecuteScalar().ToString());
                    }
                    if (reg_vvod == 1)
                    {
                        if (Variable.global.id_user != 0)
                        {
                            MySqlCommand query21 = new MySqlCommand("SELECT id_type FROM user_info WHERE id_users = " + Variable.global.id_user, mycon);
                            idenf = Variable.functionality.EncodeDecrypt(query21.ExecuteScalar().ToString(), Variable.crypto.secretKey);
                            MySqlCommand query22 = new MySqlCommand("SELECT id_group FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
                            Variable.global.user_group = query22.ExecuteScalar().ToString();
                            if (idenf == "111")
                            {
                                idenf = null;
                                Forms.workstation dlg = new Forms.workstation();
                                dlg.Show();
                                Hide();
                                mycon.Close();
                            }
                            else if (idenf == "222")
                            {
                                idenf = null;
                                Forms.teacher_forms dlg = new Forms.teacher_forms();
                                dlg.Show();
                                Hide();
                                mycon.Close();
                            }
                            else if (idenf == "444")
                            {
                                not_a_programm dlg1 = new not_a_programm();
                                dlg1.Show();
                                Hide();
                                mycon.Close();
                            }
                            else
                            {
                                MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка пользователя");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка пользователя");
                        }
                    }

                    else if (reg_vvod == 0)
                    {
                        MessageBox.Show("Вам нужно закончить регистрацию", "Регистрация");
                        registration_form dlg = new registration_form();
                        dlg.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверный логин или пароль", "Ошибка пользователя");
                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("В данный момент подключение к базе данных невозможна", "Ошибка подключения 651");
            }
            mycon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            registration_form dlg = new registration_form();
            dlg.Show();
            Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
