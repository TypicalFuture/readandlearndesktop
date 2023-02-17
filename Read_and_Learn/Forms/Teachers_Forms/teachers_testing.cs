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
using System.Data.SqlClient;

namespace Read_and_Learn.Forms.Teachers_Forms
{
    public partial class teachers_testing : Form
    {
        public teachers_testing()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        int y_loc_1, x_loc_1;
        int y_loc_2, x_loc_2;
        int number_txt1 = 1, number_txt2 = 1;
        List<TextBox> txtbox = new List<TextBox>();
        Dictionary<int, TextBox> textbox = new Dictionary<int, TextBox>(); //создание динамический текстовых полей
        List<RadioButton> rdbt = new List<RadioButton>();
        Dictionary<int, RadioButton> radiobut = new Dictionary<int, RadioButton>(); //создание лейбл
        int vopr = 0;
        int count_test;
        string name_test;
        private DataTable table = null;
        private void teachers_testing_Load(object sender, EventArgs e)
        {
            textbox[number_txt1] = new TextBox();
            textbox[number_txt1].Name = "textbox1_" + number_txt1;
            textbox[number_txt1].AutoSize = true;
            textbox[number_txt1].Font = new Font("Microsoft YaHei", 11);
            textbox[number_txt1].Size = new Size(184, 27);
            textbox[number_txt1].Location = new Point(216, 45);
            panelquestion.Controls.Add(textbox[number_txt1]);

            radiobut[number_txt1] = new RadioButton();
            radiobut[number_txt1].Text = "";
            radiobut[number_txt1].BackColor = Color.Transparent;
            radiobut[number_txt1].Location = new Point(489, 52);
            panelquestion.Controls.Add(radiobut[number_txt1]);
            x_loc_1 = textbox[number_txt1].Location.X; y_loc_1 = textbox[number_txt1].Location.Y;
            x_loc_2 = radiobut[number_txt1].Location.X; y_loc_2 = radiobut[number_txt1].Location.Y;
        }
        
        private void textBox_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                panel8.Visible = false;
                panel6.Visible = false;
                panel5.Visible = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            checktable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (number_txt1 != 9)
            {
                if (number_txt1 == 8)
                {
                    button2.Visible = false;
                    panelquestion.Height += 40;
                    panel5.Height += 40;
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 40);
                    txt_cmd();
                }
                else
                {
                    panelquestion.Height += 40;
                    panel5.Height += 40;
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 40);
                    txt_cmd();
                }
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            comboBox3.Items.Clear();
            mycon.Open();
            MySqlCommand cmd1 = new MySqlCommand("SELECT groyp FROM groyp WHERE groyp != 'none'", mycon);
            MySqlCommand cmd2 = new MySqlCommand("SELECT name_test FROM tests", mycon);
            MySqlDataReader DR = cmd1.ExecuteReader();
            while (DR.Read())
            {
                combochangetest.Items.Add(DR[0]);
            }
            DR.Close();
            DR = cmd2.ExecuteReader();
            while (DR.Read())
            {
                comboBox3.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
            panelvis(1);
            txt_delete_cmd();
            panelcreate();
            //проверка студентов на отвеченые вопросы или тесты
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            panelvis(2);
            //сделать добавление вопросов и теста
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            if(textBox_0.Text != " ")
            {
                for (int j = 1; j <= number_txt1;)
                {
                    if(radiobut[j].Checked == true)
                    {
                        MySqlCommand query1 = new MySqlCommand("SELECT COUNT(id_question) FROM question", mycon);
                        int count_quest = Convert.ToInt32(query1.ExecuteScalar().ToString()) + 1;
                        MySqlCommand query2 = new MySqlCommand("INSERT INTO question (id_question, question, id_test, answer_the_question, score) VALUES (" 
                            + count_quest 
                            + ",'" + textBox_0.Text 
                            + "?','" + count_test 
                            + "','" + textbox[j].Text 
                            + "','" + combotextBox1.Text 
                            + "');", mycon);
                        query2.ExecuteNonQuery();
                        for (int i = 1; i <= number_txt1;)
                        {
                            MySqlCommand query11 = new MySqlCommand("SELECT COUNT(id_answer) FROM answer",mycon);
                            int count_answers = Convert.ToInt32(query11.ExecuteScalar().ToString()) + 1;
                            MySqlCommand query13 = new MySqlCommand("INSERT INTO answer (id_answer, id_question, answer) VALUES (" 
                                + count_answers 
                                + ",'" + count_quest 
                                + "','" + textbox[i].Text 
                                + "');", mycon);
                            query13.ExecuteNonQuery();
                            i++;
                        }
                    }
                    else
                    {

                    }
                    j++;
                }
                txt_delete_cmd(); panelcreate();
                 vopr = vopr + 1;
                label5.Text = "Название теста: " 
                    + textBox_2.Text 
                    + "\nКоличество вопросов в тесте: " 
                    + vopr;
                textBox_0.Text = " "; combotextBox1.Text = " ";
                //label10.Visible = true;
            }
            else
            {
                MessageBox.Show("Вы не ввели вопрос");
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (number_txt1 != 9)
            {
                if (number_txt1 == 8)
                {
                    button2.Visible = false;
                    panelquestion.Height += 40;
                    panel5.Height += 40;
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 46);
                    txt_cmd();
                }
                else
                {
                    panelquestion.Height += 40;
                    panel5.Height += 40;
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 46);
                    txt_cmd();
                }
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query1 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + textBox_2.Text + "'", mycon);
            if (Convert.ToInt32(query1.ExecuteScalar()) != 0)
            {
                MySqlCommand query11 = new MySqlCommand("SELECT COUNT(id_test) FROM tests", mycon);
                count_test = Convert.ToInt32(query11.ExecuteScalar());
                textBox_2.Enabled = false;
                MySqlCommand query12 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + textBox_2.Text + "'", mycon);
                MySqlCommand query13 = new MySqlCommand("SELECT COUNT(id_question) FROM question WHERE id_test = '" + Convert.ToInt32(query12.ExecuteScalar()) + "'", mycon);
                name_test = textBox_2.Text;
                vopr = Convert.ToInt32(query13.ExecuteScalar());
                label5.Text = "Название теста: " + name_test + "\nКоличество вопросов в тесте: " + vopr;
                panelquestion.Visible = true;
                Variable.global.atantion = true;
                mycon.Close();
            }
            else
            {
                if (textBox_2.TextLength != 0)
                {
                    /*
                    MySqlCommand query1 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + textBox_2.Text + "'", mycon);

                    if (query1.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Такой тест уже существует.");
                    }
                    else
                    {*/
                    textBox_2.Enabled = false;
                    MySqlCommand query11 = new MySqlCommand("SELECT COUNT(id_test) FROM tests", mycon);
                    count_test = Convert.ToInt32(query11.ExecuteScalar()) + 1;
                    MySqlCommand query12 = new MySqlCommand("INSERT INTO tests (id_test, name_test, id_user) VALUES ("
                        + count_test
                        + ",'" + textBox_2.Text
                        + "','" + Variable.global.id_user
                        + "');", mycon);
                    query12.ExecuteNonQuery();
                    mycon.Close();
                    name_test = textBox_2.Text;
                    label5.Text = "Название теста: " + name_test + "\nКоличество вопросов в тесте: " + vopr;
                    panelquestion.Visible = true;
                    Variable.global.atantion = true;
                }
                else
                {
                    MessageBox.Show("Вы не ввели название теста.");
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            panelvis(3);
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            mycon = new MySqlConnection(Variable.connection.connect);
            MySqlCommand query1 = new MySqlCommand("SELECT name_test FROM tests", mycon);
            mycon.Open();
            MySqlDataReader DR = query1.ExecuteReader();
            while (DR.Read())
            {
                comboBox2.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
        }

        int x_2 = 150, y_2 = 15;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();

            int j = 1;
            while (j <= 2)
            {
                switch (j)
                {
                    case 1:
                        if (number_txt2 != 1)
                        {
                            for (int i = 0; i < number_txt2;)
                            {
                                bunifuPanel7.Controls.Remove(radiobut[i]);
                                bunifuPanel7.Controls.Remove(textbox[i]);
                                i++;
                            }
                            number_txt2 = 1;
                            panel8.Size = new Size(925, 207);
                            y_2 = 15;
                        } // удаляем текстовые поля
                        break;
                    case 2:
                        MySqlCommand query1 = new MySqlCommand("SELECT id_question FROM question WHERE question = '" + comboBox1.Text + "'", mycon);
                        MySqlCommand query2 = new MySqlCommand("SELECT COUNT(id_answer) FROM answer WHERE id_question = " + Convert.ToInt32(query1.ExecuteScalar()), mycon);
                        MySqlCommand query3 = new MySqlCommand("SELECT answer_the_question FROM question WHERE question = '" + comboBox1.Text + "'", mycon);
                        int count_answer = Convert.ToInt32(query2.ExecuteScalar());
                        for (number_txt2 = 0; number_txt2 < count_answer;)
                        {
                            MySqlCommand query11 = new MySqlCommand("SELECT answer FROM answer WHERE id_question = " + Convert.ToInt32(query1.ExecuteScalar()) 
                                + " LIMIT " + number_txt2 + ", 1", mycon);
                            textbox[number_txt2] = new TextBox();
                            textbox[number_txt2].Name = "textbox2_" + number_txt2;
                            textbox[number_txt2].Font = new Font("Microsoft YaHei", 11);
                            textbox[number_txt2].Size = new Size(184, 27);
                            textbox[number_txt2].Location = new Point(x_2, y_2);
                            textbox[number_txt2].Text = query11.ExecuteScalar().ToString();
                            bunifuPanel7.Controls.Add(textbox[number_txt2]);

                            radiobut[number_txt2] = new RadioButton();
                            if(textbox[number_txt2].Text == query3.ExecuteScalar().ToString())
                            {
                                radiobut[number_txt2].Checked = true;
                            }
                            radiobut[number_txt2].Text = "";
                            radiobut[number_txt2].BackColor = Color.Transparent;
                            radiobut[number_txt2].Location = new Point(350, y_2);
                            bunifuPanel7.Controls.Add(radiobut[number_txt2]);

                            y_2 = y_2 + 40;
                            number_txt2++;
                            panel8.Size = new Size(panel8.Width, panel8.Height + 30);
                        }
                        //добавляем текстовые поля
                        break;
                }

                j++;
            }
            mycon.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query1 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + comboBox2.Text + "'", mycon);
            MySqlCommand query2 = new MySqlCommand("SELECT question FROM question, tests WHERE tests.id_user = "
                + Variable.global.id_user
                + " AND tests.id_test = question.id_test "
                + " AND tests.id_test = " + Convert.ToInt32(query1.ExecuteScalar()), mycon);
            MySqlDataReader DR = query2.ExecuteReader();
            while (DR.Read())
            {
                comboBox1.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            checktable();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox5.Items.Count == 0)
            { 
                MessageBox.Show("Вы не выбрали тест");
            }
            else
            {
                bunifuButton8.Text = "Удалить вопрос";
            }

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query1 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + comboBox5.Text + "'", mycon);
            MySqlCommand query2 = new MySqlCommand("SELECT question FROM question, tests WHERE tests.id_user = "
                + Variable.global.id_user
                + " AND tests.id_test = question.id_test "
                + " AND tests.id_test = " + Convert.ToInt32(query1.ExecuteScalar()), mycon);
            MySqlDataReader DR = query2.ExecuteReader();
            while (DR.Read())
            {
                comboBox4.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            comboBox4.Items.Clear();
            mycon = new MySqlConnection(Variable.connection.connect);
            MySqlCommand query1 = new MySqlCommand("SELECT name_test FROM tests WHERE id_user = " + Variable.global.id_user, mycon);
            mycon.Open();
            MySqlDataReader DR = query1.ExecuteReader();
            while (DR.Read())
            {
                comboBox5.Items.Add(DR[0]);
            }
            DR.Close();
            mycon.Close();
            panelvis(4);
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            if(comboBox4.SelectedIndex > -1) //удаление вопроса
            {
                MySqlCommand query11 = new MySqlCommand("SELECT id_question FROM question WHERE question = '" + comboBox4.Text + "'", mycon);
                MySqlCommand query12 = new MySqlCommand("DELETE FROM answer WHERE id_question = " + Convert.ToInt32(query11.ExecuteScalar()), mycon);
                query12.ExecuteNonQuery();
                MySqlCommand query13 = new MySqlCommand("DELETE FROM question WHERE id_question = " + Convert.ToInt32(query11.ExecuteScalar()), mycon);
                query13.ExecuteNonQuery();
                MessageBox.Show("Вопрос удален.");
            }
            else if(comboBox5.SelectedIndex > -1) //удаление теста
            {
                //проверка пользователю на удаление данных!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                MySqlCommand query11 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + comboBox5.Text + "'", mycon);
                MySqlCommand query12 = new MySqlCommand("SELECT COUNT(id_question) FROM question WHERE id_test = " + Convert.ToInt32(query11.ExecuteScalar()) + "", mycon);
                for (int i = 0;i <= Convert.ToInt32(query12.ExecuteScalar());)
                {
                    MySqlCommand query21 = new MySqlCommand("SELECT id_question FROM question WHERE id_test = " + Convert.ToInt32(query11.ExecuteScalar()) + " LIMIT " + i + ",1", mycon);
                    MySqlCommand query22 = new MySqlCommand("DELETE FROM answer WHERE id_question = " + Convert.ToInt32(query21.ExecuteScalar()), mycon);
                    query22.ExecuteNonQuery();
                    i++;
                }
                MySqlCommand query13 = new MySqlCommand("DELETE FROM question WHERE id_test = " + Convert.ToInt32(query11.ExecuteScalar()), mycon);
                query13.ExecuteNonQuery();
                MySqlCommand query14 = new MySqlCommand("DELETE FROM tests WHERE id_test = " + Convert.ToInt32(query11.ExecuteScalar()), mycon);
                query14.ExecuteNonQuery();
                MessageBox.Show("Тест удален.");
            }
            else//ошибка на выборку
            {
                MessageBox.Show("Вы не выбрали тест/вопрос, который хотите удалить.");
            }
            comboBox4.Text = " ";
            mycon.Close();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();



            mycon.Close();
        }

        private void txt_cmd()
        {
            number_txt1++;
            textbox[number_txt1] = new TextBox();
            textbox[number_txt1].Name = "textbox1_" + number_txt1;
            textbox[number_txt1].Font = new Font("Microsoft YaHei", 11);
            textbox[number_txt1].Size = new Size(184, 27);
            y_loc_1 = y_loc_1 + 40;
            textbox[number_txt1].Location = new Point(x_loc_1, y_loc_1);
            panelquestion.Controls.Add(textbox[number_txt1]);

            radiobut[number_txt1] = new RadioButton();
            radiobut[number_txt1].Text = "";
            radiobut[number_txt1].BackColor = Color.Transparent;
            y_loc_2 = y_loc_2 + 40;
            radiobut[number_txt1].Location = new Point(x_loc_2, y_loc_2);
            panelquestion.Controls.Add(radiobut[number_txt1]);
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();

            mycon.Close();
        }

        /*
private void txt_cmd_2(int cont)
{
   for (number_txt2 = 1; number_txt2 >= cont;)
   {
       textbox[number_txt2] = new TextBox();
       textbox[number_txt2].Name = "textbox1_" + number_txt2;
       textbox[number_txt2].Font = new Font("Microsoft YaHei", 11);
       textbox[number_txt2].Size = new Size(184, 27);
       textbox[number_txt2].Location = new Point(x_2, y_2);
       bunifuPanel7.Controls.Add(textbox[number_txt2]);
       y_2 = y_2 + 40;
       number_txt2++;

       radiobut[i] = new RadioButton();
       radiobut[i].Text = "";
       radiobut[i].BackColor = Color.Transparent;
       radiobut[i].Location = new Point(x_loc_2, y_2);
       bunifuPanel7.Controls.Add(radiobut[i]);


   }
}*/

        private void txt_delete_cmd()
        {
            try
            {
                if (number_txt1 >= 2)
                {

                    for (int i = 2; i <= number_txt1;)
                    {
                        panelquestion.Controls.Remove(radiobut[i]);
                        panelquestion.Controls.Remove(textbox[i]);
                        i++;
                    }
                }
            }
            catch
            {

            }
        }

        private void panelcreate()
        {
            x_loc_1 = textbox[1].Location.X; y_loc_1 = textbox[1].Location.Y;
            x_loc_2 = radiobut[1].Location.X; y_loc_2 = radiobut[1].Location.Y;
            panelquestion.Size = new Size(695, 126);
            button2.Location = new Point(button2.Location.X, 46);
            button2.Visible = true;
            number_txt1 = 1;
            textbox[1].Text = " ";
            panel5.Size = new Size(925, 294);
        }

        private void panelvis(int i)
        {
            switch (i)
            {
                case 1:
                    panel2.Visible = true;
                    panel8.Visible = false;
                    panel5.Visible = false;
                    panel6.Visible = true;
                    panel1.Visible = false;
                    break;
                case 2:
                    button2.Location = new Point(button2.Location.X, button2.Location.Y);
                    panel2.Visible = false;
                    panel5.Visible = true;
                    panel6.Visible = false;
                    panel8.Visible = false;
                    panel1.Visible = false;
                    break;
                case 3:
                    panel2.Visible = false;
                    panel5.Visible = false;
                    panel6.Visible = false;
                    panel8.Visible = true;
                    panel1.Visible = false;
                    break;
                case 4:
                    panel2.Visible = false;
                    panel5.Visible = false;
                    panel6.Visible = false;
                    panel8.Visible = false;
                    panel1.Visible = true;
                    break;
            }
        }
        private void checktable()
        {
            dataGridView1.Columns.Clear();
            int i = 0;
            if(combochangetest.Text.Length > 0 && comboBox3.Text.Length == 0)
            {
                i = 1;
            }
            else if (comboBox3.Text.Length > 0 && combochangetest.Text.Length == 0)
            {
                i = 2;
            }
            else
            {
                i = 3;
            }
            panel6.Height = 600;
            MySqlDataAdapter adapter;
            MySqlCommand query;
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            switch (i)
            {
                case 1:
                    query = new MySqlCommand("SELECT id_group FROM groyp WHERE groyp = '" + combochangetest.Text + "'", mycon);
                    adapter = new MySqlDataAdapter("SELECT surname, name, patronymic, answered_questions FROM user_info, answer_test WHERE user_info.id_group = " 
                        + Convert.ToInt32(query.ExecuteScalar().ToString()) 
                        + " AND user_info.id_users = answer_test.id_users", mycon);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].HeaderText = "Фамилия";
                    dataGridView1.Columns[1].HeaderText = "Имя";
                    dataGridView1.Columns[2].HeaderText = "Отчество";
                    dataGridView1.Columns[3].HeaderText = "Правильно решено вопросов";
                    mycon.Close();
                    break;
                case 2:
                    query = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + comboBox3.Text + "'", mycon);
                    adapter = new MySqlDataAdapter("SELECT surname, name ,question ,answer FROM user_info, question, answer_questions WHERE id_test = '"
                        + Convert.ToInt32(query.ExecuteScalar().ToString())
                        + "' AND user_info.id_users = answer_questions.id_user"
                        + " AND answer_questions.id_question = question.id_question", mycon);
                    table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].HeaderText = "Фамилия";
                    dataGridView1.Columns[1].HeaderText = "Имя";
                    dataGridView1.Columns[2].HeaderText = "Вопрос";
                    dataGridView1.Columns[3].HeaderText = "Ответ студента";
                    mycon.Close();
                    break;
                case 3:
                    query = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" + comboBox3.Text + "'", mycon);
                    MySqlCommand query1 = new MySqlCommand("SELECT id_group FROM groyp WHERE groyp = '" + combochangetest.Text + "'", mycon);
                    adapter = new MySqlDataAdapter("SELECT surname, name, question, answer FROM user_info, question, answer_questions WHERE id_test = "
                        + Convert.ToInt32(query.ExecuteScalar().ToString())
                        + " AND user_info.id_group = "
                        + Convert.ToInt32(query1.ExecuteScalar().ToString())
                        + " AND user_info.id_users = answer_questions.id_user"
                        + " AND answer_questions.id_question = question.id_question", mycon);
                    table = new DataTable();
                    adapter.Fill(table); //ошибка
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].HeaderText = "Фамилия";
                    dataGridView1.Columns[1].HeaderText = "Имя";
                    dataGridView1.Columns[2].HeaderText = "Вопрос";
                    dataGridView1.Columns[3].HeaderText = "Ответ студента";
                    mycon.Close();
                    break;
            }
        }
    }
}
