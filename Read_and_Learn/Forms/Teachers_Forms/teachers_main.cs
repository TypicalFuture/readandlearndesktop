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
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Read_and_Learn.Forms.Teachers_Forms
{
    public partial class teachers_main : Form
    {
        public teachers_main()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        private Form activeForm = null;

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
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            openChildForm(new options());
        }

        private DataTable table = null;

        private void teachers_main_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT surname, name, patronymic, email, password FROM user_info, user_table WHERE user_info.id_group = " + Variable.global.user_group + " AND user_info.id_type = 444 AND NOT user_info.surname = '" + 0 + "'" + " AND user_table.id_user = user_info.id_users ", mycon);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            MySqlCommand query03 = new MySqlCommand("SELECT groyp FROM groyp WHERE id_group =" + Variable.global.user_group, mycon);
            MySqlCommand query04 = new MySqlCommand("SELECT surname FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query05 = new MySqlCommand("SELECT name FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query06 = new MySqlCommand("SELECT COUNT(id_guest) FROM user_info WHERE id_group = " + Variable.global.user_group + " AND id_type = " + 444, mycon);
            label3.Text = "Ваше имя: " + query04.ExecuteScalar().ToString() + " " + query05.ExecuteScalar().ToString() + " \n" + "Ваша группа: " + query03.ExecuteScalar().ToString() + " \n" + " \n" + "Количество студентов в группе: " + query06.ExecuteScalar().ToString(); ;

            MySqlCommand cmd = new MySqlCommand("SELECT groyp FROM groyp WHERE groyp != 'none' ", mycon);
            MySqlDataReader DR = cmd.ExecuteReader();
            
            while (DR.Read())
            {
                comboBox1.Items.Add(DR[0]);
            }
            dataGridView1.Columns[0].HeaderText = "Фамилия";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Отчество";
            dataGridView1.Columns[3].HeaderText = "Почта студента";
            dataGridView1.Columns[4].HeaderText = "Пароль студента";
            mycon.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        Random rnd = new Random(100000);

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if(textBox4.Text != "" && comboBox1.Text != "")
            {
                mycon = new MySqlConnection(Variable.connection.connect);
                mycon.Open();
                string password;
                password = Variable.functionality.randomb(15);
                MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_user) FROM user_table", mycon);
                int count_id_user_table = Convert.ToInt32(query01.ExecuteScalar().ToString()) + 1;
                MySqlCommand query02 = new MySqlCommand("INSERT INTO user_table (id_user, email, login, password, registration) VALUES (" + count_id_user_table + ",'" + textBox4.Text + "','" + password + "','" + password + "'," + 0 + ");", mycon);
                query02.ExecuteNonQuery();
                MySqlCommand query03 = new MySqlCommand("SELECT COUNT(id_guest) FROM user_info", mycon);
                int count_id_user_info = Convert.ToInt32(query03.ExecuteScalar().ToString()) + 1;
                MySqlCommand query04 = new MySqlCommand("SELECT id_group FROM groyp WHERE groyp = '" + comboBox1.Text + "'", mycon);
                int id_group = Convert.ToInt32(query04.ExecuteScalar().ToString());
                try
                {
                    MySqlCommand query05 = new MySqlCommand("INSERT INTO user_info (`id_guest`, `id_users`, `id_type`, `surname`, `name`, `patronymic`, `telephone`, `birthday`, `id_group`) VALUES (" + count_id_user_info + "," + count_id_user_table + "," + Convert.ToInt32(Variable.functionality.EncodeDecrypt("111", Variable.crypto.secretKey)) + ",'" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + "0000-00-00" + "'," + id_group + ");", mycon);
                    query05.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                mycon.Close();
                MessageBox.Show("Пользователь добавлен в группу. \n Почта: " + textBox4.Text + " \n Пароль: " + password + "\n Запишите пароль отдельно!");
                comboBox1.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
