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
using System.Data.SqlClient;

namespace Read_and_Learn
{
    public partial class not_a_programm : Form
    {
        public not_a_programm()
        {
            InitializeComponent();
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        int id_type = 0;
        private void not_a_programm_Load(object sender, EventArgs e)
        {
            
        }
        Random rnd = new Random(100000);
        private static string randb(int len)
        {
            string s = "", symb = "1234567890abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();

            for (int i = 0; i < len; i++)
                s += symb[rnd.Next(0, symb.Length)];
            return s;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            string password;
            password = Variable.functionality.randomb(15);
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_user) FROM user_table", mycon);
            int count_user_one = Convert.ToInt32(query01.ExecuteScalar().ToString()) + 1;

            if (bunifuTextBox2.Text == bunifuTextBox3.Text)
            {
                MySqlCommand query11 = new MySqlCommand("SELECT id_group FROM groyp WHERE groyp = '" + bunifuDropdown1.SelectedItem.ToString() + "'", mycon);
                int id_gr = Convert.ToInt32(query11.ExecuteScalar().ToString());
                MySqlCommand query12 = new MySqlCommand("INSERT INTO user_table (id_user, email, login, password, registration) VALUES (" 
                    + count_user_one + ",'" 
                    + bunifuTextBox1.Text + "','" 
                    + password + "','" 
                    + bunifuTextBox2.Text + "'," 
                    + 0 + ");", mycon);
                query12.ExecuteNonQuery();
                MySqlCommand query13 = new MySqlCommand("SELECT COUNT(id_guest) FROM user_info", mycon);
                int count_user_two = Convert.ToInt32(query13.ExecuteScalar().ToString()) + 1;
                MySqlCommand query14 = new MySqlCommand("INSERT INTO user_info (`id_guest`, `id_users`, `id_type`, `surname`, `name`, `patronymic`, `telephone`, `birthday`, `id_group`) VALUES (" 
                    + count_user_two + "," 
                    + count_user_one + "," 
                    + id_type + ",'" 
                    + 0 + "','" 
                    + 0 + "','" 
                    + 0 + "','" 
                    + 0 + "','" 
                    + "0000-00-00" + "'," 
                    + id_gr + ");", mycon);
                query14.ExecuteNonQuery();
                MessageBox.Show("Добавлен пользователь:" + bunifuTextBox1.Text);
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }

            mycon.Close();
        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT groyp FROM groyp WHERE groyp != 'none'", mycon);
            MySqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                bunifuDropdown1.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
        }

        string idenf;
        private void bunifuDropdown2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( bunifuDropdown2.SelectedItem.ToString() == "Преподаватель")
            {
                idenf = "222";
            }
            else if(bunifuDropdown2.SelectedItem.ToString() == "Ученик")
            {
                idenf = "111";
            }
            else if (bunifuDropdown2.SelectedItem.ToString() == "Куратор")
            {
                idenf = "333";
            }
            else if (bunifuDropdown2.SelectedItem.ToString() == "Администратор")
            {
                idenf = "444";
                
            }
            id_type = Convert.ToInt32(Variable.functionality.EncodeDecrypt(idenf, Variable.crypto.secretKey));
        }
        private void not_a_programm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query11 = new MySqlCommand("SELECT COUNT(id_group) FROM groyp", mycon);
            int count_group = Convert.ToInt32(query11.ExecuteScalar().ToString()) + 1;
            MySqlCommand query12 = new MySqlCommand("INSERT INTO groyp (id_group, groyp, id_photo) VALUES ("
                    + count_group + ",'"
                    + bunifuTextBox4.Text + "', 'none');", mycon);
            query12.ExecuteNonQuery();
            mycon.Close();
        }
    }
}
