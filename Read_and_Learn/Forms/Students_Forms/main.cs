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
using System.Net;
using MySql.Data.MySqlClient;

namespace Read_and_Learn.Forms
{ 
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }
        public MySqlConnection mycon;
        public MySqlCommand mycom;
        private void main_Load(object sender, EventArgs e)
        {
            mycon = new MySqlConnection(Variable.connection.connect);
            mycon.Open();
            MySqlCommand query02 = new MySqlCommand("SELECT id_photo FROM groyp WHERE id_group =" +  Variable.global.user_group, mycon);


            /*
            string photo_group = query02.ExecuteScalar().ToString();
            pictureBox1.BackgroundImage = Image.FromFile(@"ftp://31.31.198.171//u1602320//photo_group//" + photo_group);


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://31.31.198.171/u1602320/photo_group/DRED_IS-319.jpg");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("u1602320", "33war77saki1raok");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            //client.UploadFile("ftp://31.31.198.171/photo_group/Text1.txt", @"D:\Text1.txt"); Добавление файла на хостинг

            */
            try
            {
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential("volkill73me_a91ck6p9", "qazqwe123");
                client.DownloadFile("ftp://31.31.198.171/photo_group/DRED_IS-319.jpg", "DRED_IS-319.jpg");
                pictureBox1.BackgroundImage = Image.FromFile("DRED_IS-319.jpg");
            }
            catch
            {

            }

            MySqlCommand query03 = new MySqlCommand("SELECT groyp FROM groyp WHERE id_group =" + "'" + Variable.global.user_group + "'", mycon);
            MySqlCommand query04 = new MySqlCommand("SELECT surname FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            MySqlCommand query05 = new MySqlCommand("SELECT name FROM user_info WHERE id_users =" + Variable.global.id_user, mycon);
            
            label3.Text = "Ваше имя: " 
                + query04.ExecuteScalar().ToString() 
                + " " 
                + query05.ExecuteScalar().ToString() 
                + "<br>" 
                + "Ваша группа: " 
                + query03.ExecuteScalar().ToString();
            MySqlCommand query06 = new MySqlCommand("SELECT surname FROM user_info WHERE id_group = " 
                + Variable.global.user_group 
                + " AND id_type = " 
                + Convert.ToInt32(Variable.functionality.EncodeDecrypt("222", Variable.crypto.secretKey)), mycon);
            MySqlCommand query07 = new MySqlCommand("SELECT name FROM user_info WHERE id_group = " 
                + Variable.global.user_group 
                + " AND id_type = " 
                + Convert.ToInt32(Variable.functionality.EncodeDecrypt("222", Variable.crypto.secretKey)), mycon);
            label5.Text = "Мой куратор: " 
                + query06.ExecuteScalar().ToString() 
                + " " + query07.ExecuteScalar().ToString();
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
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            openChildForm(new options());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
