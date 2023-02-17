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
    public partial class test_selection : Form
    {
        public test_selection()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        private void test_selection_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_test) FROM tests", mycon);
            mycon.Open();
            for (int i = 1; i <= Convert.ToInt32(query01.ExecuteScalar().ToString());)
            {
                MySqlCommand query11 = new MySqlCommand("SELECT name_test FROM tests WHERE id_test = " + i, mycon);
                label3.Text += + i + ". " + query11.ExecuteScalar().ToString() + "\n";
                i++;
                panel1.Height += 20;
            }
            MySqlCommand cmd = new MySqlCommand("SELECT name_test FROM tests", mycon);
            MySqlDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                comboBox1.Items.Add(DR[0]);

            }
            DR.Close();
            mycon.Close();
        }
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
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();

            string number_test = comboBox1.Text;
            
            MySqlCommand query01 = new MySqlCommand("SELECT id_test FROM tests WHERE name_test = '" +  number_test + "'", mycon);
            Variable.testing.id_test = Convert.ToInt32(query01.ExecuteScalar().ToString()); //айди выбранного теста
            MySqlCommand query02 = new MySqlCommand("SELECT COUNT(id_question) FROM question WHERE id_test = " + Variable.testing.id_test, mycon);
            
            if (Convert.ToInt32(query02.ExecuteScalar().ToString()) >= 1)
            {
                MySqlCommand query03 = new MySqlCommand("SELECT id_question FROM question WHERE id_test = " + Variable.testing.id_test + " LIMIT " + 0 + "," + 1, mycon);
                Variable.testing.id_question = Convert.ToInt32(query03.ExecuteScalar().ToString());
                openChildForm(new Forms.test_ing());
            }
            else
            {
                MessageBox.Show("Ошибка в выводе теста", "Ошибка 201");
            }
            
            mycon.Close();
        }
    }
}
