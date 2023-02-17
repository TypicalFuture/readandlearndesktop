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
    public partial class test_ing : Form
    {
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public test_ing()
        {
            InitializeComponent();
        }
        int number_correct_question;
        List<RadioButton> rdbtn = new List<RadioButton>();
        Dictionary<int, RadioButton> radioButton = new Dictionary<int, RadioButton>(); //создание динамический кнопок
        int i_answer, limit_one_answer = 0, limit_question = 0;
        int count_question, i_question = 1;

        private void test_ing_Load(object sender, EventArgs e)
        {
            number_correct_question = 0;
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_answer) FROM answer WHERE id_question = " + Variable.testing.id_question, mycon);
            i_answer = Convert.ToInt32(query01.ExecuteScalar().ToString());
            MySqlCommand query02 = new MySqlCommand("SELECT question FROM question WHERE id_test = " + Variable.testing.id_test + " LIMIT " + limit_question + "," + 1, mycon);
            bunifuLabel2.Text = query02.ExecuteScalar().ToString();
            if (i_answer > 1)
            {
                enter_answer();
            }
            else if (i_answer == 1)
            {

            }
            MySqlCommand query03 = new MySqlCommand("SELECT COUNT(id_question) FROM question WHERE id_test = " + Variable.testing.id_test, mycon);
            count_question = Convert.ToInt32(query03.ExecuteScalar().ToString());
            bunifuLabel1.Text = "Вопрос: " + i_question + "/" + count_question;
            mycon.Close();
        } //создание первого вопроса и первых ответов при загрузке формы

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

        int score;
        private void button1_Click(object sender, EventArgs e)
        {
            limit_question++;
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            limit_one_answer = 0;
            int count_answer = 0; //айди отвеченного теста в бд answer_question
            for (int j = 1; j <= i_answer;) //запись отвеченного вопросса
            {
                if (radioButton[j].Checked == true)
                {
                    string answer = radioButton[j].Text;
                    MySqlCommand query11 = new MySqlCommand("SELECT answer_the_question FROM question WHERE id_question = " 
                        + Variable.testing.id_question, mycon);
                    if (answer == query11.ExecuteScalar().ToString())
                    {
                        number_correct_question++;
                        MySqlCommand query21 = new MySqlCommand("SELECT score FROM question WHERE id_question = " 
                            + Variable.testing.id_question, mycon);
                        score = score + Convert.ToInt32(query21.ExecuteScalar().ToString());
                    }
                    else
                    {

                    }//сравнение правильного ответа
                    MySqlCommand query12 = new MySqlCommand("SELECT COUNT(id_answer) FROM answer_questions", mycon);
                    count_answer = Convert.ToInt32(query12.ExecuteScalar().ToString()) + 1;
                    MySqlCommand query13 = new MySqlCommand("INSERT INTO answer_questions (id_answer, id_user, id_question, answer) VALUES( " 
                        + count_answer 
                        + ", " 
                        + Variable.global.id_user 
                        + ", " 
                        + Variable.testing.id_question 
                        + ",'" 
                        + answer 
                        + "');", mycon);
                    query13.ExecuteNonQuery(); //запись в бд отвеченного вопроса
                }
                else
                {

                }
                j++;
            }

            for (int j = 1; j <= i_answer;) //удаление radiobutton
            {
                Controls.Remove(radioButton[j]);
                radioButton[j].Dispose();
                j++;
            }
            //проверка на отвеченных всех вопросов
            if (i_question != count_question)
            {
                
                //вывод нового вопроса
                MySqlCommand query01 = new MySqlCommand("SELECT id_question FROM question WHERE id_test = " 
                    + Variable.testing.id_test 
                    + " LIMIT " 
                    + limit_question 
                    + "," 
                    + 1, mycon);
                Variable.testing.id_question = Convert.ToInt32(query01.ExecuteScalar().ToString());
                MySqlCommand query02 = new MySqlCommand("SELECT COUNT(id_answer) FROM answer WHERE id_question = " 
                    + Variable.testing.id_question, mycon);
                i_answer = Convert.ToInt32(query02.ExecuteScalar().ToString()); //кол-во ответов
                MySqlCommand query03 = new MySqlCommand("SELECT question FROM question WHERE id_test = " 
                    + Variable.testing.id_test 
                    + " LIMIT " 
                    + limit_question 
                    + ","
                    + 1, mycon);
                bunifuLabel2.Text = query03.ExecuteScalar().ToString(); //вывод вопроса
                i_question++;
                bunifuLabel1.Text = "Вопрос: " + i_question + "/" + count_question;

                if (i_answer > 1)
                {
                    enter_answer();
                }
                else if (i_answer == 1)
                {
                    
                }

            }
            else //завершение тестирования
            {
                MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_answer_test) FROM answer_test", mycon);
                int count_test = Convert.ToInt32(query01.ExecuteScalar().ToString()) + 1;
                MySqlCommand query02 = new MySqlCommand("INSERT INTO answer_test (id_answer_test, id_users, id_test, answered_questions, correct_questions, score) VALUES( " 
                    + count_test 
                    + ", "
                    + Variable.global.id_user 
                    + ", "
                    + Variable.testing.id_test 
                    + ", " + count_question 
                    + ", " 
                    + number_correct_question 
                    + ", " 
                    + score 
                    + ");", mycon);
                query02.ExecuteNonQuery();
                MessageBox.Show("Вы решили тест ");
                openChildForm(new test_selection());
            }
           
            mycon.Close();
        } //кнопка прохождения на другой вопрос

        private void enter_answer()
        {
            int x_loc = bunifuLabel2.Location.X, y_loc = bunifuLabel2.Location.Y;
            for (int j = 1; j <= i_answer;)
            {

                MySqlCommand query11 = new MySqlCommand("SELECT id_answer FROM answer WHERE id_question = " + Variable.testing.id_question + " LIMIT " + limit_one_answer + "," + j, mycon);
                MySqlCommand query12 = new MySqlCommand("SELECT answer FROM answer WHERE id_question = " + Variable.testing.id_question + " AND id_answer = " + Convert.ToInt32(query11.ExecuteScalar().ToString()), mycon);
                radioButton[j] = new RadioButton();
                radioButton[j].AutoSize = true;
                y_loc += 50;
                radioButton[j].Font = new Font("Leelawadee", 14);
                radioButton[j].Text = query12.ExecuteScalar().ToString();
                radioButton[j].Location = new Point(x_loc, y_loc);
                panel1.Controls.Add(radioButton[j]);
                j++;
                limit_one_answer++;
            } //функционал выводов ответов
        }
    }
    
}
