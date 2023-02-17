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

namespace Read_and_Learn.Forms.Teachers_Forms
{
    public partial class teachers_schudule : Form
    {
        public teachers_schudule()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;


        private void teachers_schudule_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query01 = new MySqlCommand("SELECT COUNT(id_test) FROM tests", mycon);
            for (int i = 1; i <= Convert.ToInt32(query01.ExecuteScalar().ToString());)
            {
                MySqlCommand query11 = new MySqlCommand("SELECT name_test FROM tests WHERE id_test = " + i, mycon);
                label3.Text += +i + ". " + query11.ExecuteScalar().ToString() + "\n";
                i++;
                bunifuPanel1.Height += 20;
            }

            mycon.Close();
        }
    }
}
