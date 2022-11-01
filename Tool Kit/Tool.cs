using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Xml.Linq;

using MySql.Data.MySqlClient;

using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tool_Kit
{
    public partial class Tool : Form
    {

        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=5.250.245.92;Database=bizimbil_atay;Uid=bizimbil_atay;Pwd=0cbc56dydA*;");
        MySqlCommand cmd;
        

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
            int nleftrect,
            int ntoprect,
            int nrightrect,
            int nbottomrect,
            int widthrevth,
            int nheightrect
            );


        public Tool()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            browse_panel.Location = new Point(106, 45);
            panel_ana.Location = new Point(106, 45);
            panel_user.Location = new Point(106, 45);

        }
        //string ana_clasor = "";

        DateTime bugun = DateTime.Now;

        public string kkad = string.Empty;
        public string ssif = string.Empty;
        public string id = string.Empty;
        public string mail = string.Empty;

        private void Tool_Load(object sender, EventArgs e)
        {
            string host = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(host);
            ip.AddressList[0].ToString();




                    label8.Text = "Kullanıcı : " + kkad;
                    label9.Text = "Mail : " + mail;
                    label10.Text = "Pc Adı : " + host;
                    label11.Text = "İp : " + ip.AddressList[ip.AddressList.Length - 1];

            

        }

        public void cikis()
        {

            try
            {
                mysqlbaglan.Open(); //oluşturtuğumuz tanımı çalıştırarak açılmasını sağlıyoruz
                if (mysqlbaglan.State != ConnectionState.Closed) // tanımın durumunu kontrol ediyoruz bağlı mı değil mi
                {


                    cmd = new MySqlCommand();
                    cmd.Connection = mysqlbaglan;

                        cmd = new MySqlCommand();// bunu kaldır dene bak hata verecekmi
                        cmd.Connection = mysqlbaglan;
                        cmd.CommandText = "UPDATE siberatay SET tarih=@tarih,aktif=@aktif WHERE id=@id";

                        cmd.Parameters.AddWithValue("@tarih", bugun);
                        cmd.Parameters.AddWithValue("@aktif", 0);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        mysqlbaglan.Close();

                        Application.Exit();
                        this.Hide();

                        //break;

                }
                else
                {
                    MessageBox.Show("Hata!  Bağlantı sırasında hata oluştu");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata!  Bağlantı sırasında hata oluştu");
            }


        }


        int btn_aktif = 0;
        private void aktif_button()
        {
            switch (btn_aktif)
            {
                case 0:
                    panel_ana.Visible = true;
                    pictureBox2.Image = Properties.Resources.dashboard_copy;
                    pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;

                    panel_user.Visible = false;
                    browse_panel.Visible = false;
                    pictureBox3.Image = Properties.Resources.account_copy;
                    pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;

                case 1:
                    panel_user.Visible = true;
                    pictureBox3.Image = Properties.Resources.account_copyb;
                    pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;

                    panel_ana.Visible = false;
                    browse_panel.Visible = false;
                    pictureBox2.Image = Properties.Resources.dashboard_copyb;
                    pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;

                case 2:

                    break;

                case 3:

                    break;
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            pictureBox1.Height = 40;
            pictureBox1.Width = 40;


            //pictureBox1.Image = Properties.Resources.dashboard_copy;
            //pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Height = 33;
            pictureBox1.Width = 33;


        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)// HOME
        {
            pictureBox6.Visible = true;
            label1.Visible = true;

            pictureBox2.Location = new Point(17, pictureBox2.Location.Y);

        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.Visible = false;
            label1.Visible = false;
            pictureBox2.Location = new Point(11, pictureBox2.Location.Y);
        }

        private void PictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            btn_aktif = 0;
            aktif_button();
        }

        private void PictureBox3_MouseMove(object sender, MouseEventArgs e)//USER
        {
            pictureBox7.Visible = true;
            label2.Visible = true;
            pictureBox3.Location = new Point(17, pictureBox3.Location.Y);
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Visible = false;
            label2.Visible = false;
            pictureBox3.Location = new Point(11, pictureBox3.Location.Y);
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            btn_aktif = 1;
            aktif_button();
        }

        private void PictureBox4_MouseMove(object sender, MouseEventArgs e)//CLOSE
        {
            pictureBox8.Visible = true;
            label3.Visible = true;
            pictureBox4.Location = new Point(17, pictureBox4.Location.Y);
        }

        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
            label3.Visible = false;
            pictureBox4.Location = new Point(11, pictureBox4.Location.Y);
        }
        private void PictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            cikis();
            this.Close();
            Application.Exit();
        }

        private void PictureBox5_MouseMove(object sender, MouseEventArgs e)//TİME
        {
            pictureBox9.Visible = true;
            label4.Visible = true;
            pictureBox5.Location = new Point(17, pictureBox5.Location.Y);
        }

        private void PictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.Visible = false;
            label4.Visible = false;
            pictureBox5.Location = new Point(11, pictureBox5.Location.Y);
        }






        bool tas_i = false;
        Point sifir_nkt = new Point(0, 0);
        private void Panel1_MouseDown(object sender, MouseEventArgs e)// FORM DRAG LARI
        {
            tas_i = true;
            sifir_nkt = new Point(e.X, e.Y);
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tas_i)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.sifir_nkt.X, p.Y - this.sifir_nkt.Y);
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            tas_i = false;
        }




        XElement root = XElement.Load("sAt");
        DataGridViewImageColumn img1;
        Image image;
        int sayfa_konum;
        string eski_sayfa;


        private void sayfa(string a)
        {
            dataGridView1.Rows.Clear();

            foreach (XElement item in root.Elements())
            {
                string group = item.Element("group").Value;
                string child_group = item.Element("child_group").Value;
                if (group == a && child_group == "0")
                {

                    string name = item.Element("name").Value;
                    string tx = item.Element("tx").Value;
                    string img = item.Element("img").Value;
                    string loc = item.Element("loc").Value;



                    img1 = new DataGridViewImageColumn();
                    image = Image.FromFile(img);
                    img1.Image = image;


                    dataGridView1.Rows.Add(img1.Image, name, tx, loc, child_group);
                }

            }
            eski_sayfa = a;
            dataGridView1.ClearSelection();
        }



        private void child_sayfa(string a)
        {
            dataGridView1.Rows.Clear();

            foreach (XElement item in root.Elements())
            {
                string child_group = item.Element("child_group").Value;
                if (child_group == a)
                {
                    string name = item.Element("name").Value;
                    string tx = item.Element("tx").Value;
                    string img = item.Element("img").Value;
                    string loc = item.Element("loc").Value;


                    img1 = new DataGridViewImageColumn();
                    image = Image.FromFile(img);
                    img1.Image = image;

                    
                    dataGridView1.Rows.Add(img1.Image, name, tx, loc);
                    konum_label.Text = tx;
                }

            }
            dataGridView1.ClearSelection();
        }


        string dosya_yolu;
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string child_group_kontrol;
            dosya_yolu = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //try
            //{
                child_group_kontrol = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //}
            //catch (null)
            //{
            //    // erte
            //}


            if (child_group_kontrol == "")
            {

                sayfa_konum = 1;
                child_sayfa(dataGridView1.CurrentRow.Cells[1].Value.ToString());

            }
            else
            {

                sayfa_konum = 0;
                System.Diagnostics.Process.Start(dosya_yolu);

            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (sayfa_konum == 0)
            {
                browse_panel.Visible = false;
                panel_ana.Visible = true;
                konum_label.Text = "";
            }
            else
            {
                sayfa(eski_sayfa);
                sayfa_konum = 0;
                konum_label.Text = eski_sayfa;
            }

        }









        private void PictureBox10_MouseDown(object sender, MouseEventArgs e)//1,1
        {
            konum_label.Text = "Analysis";
            sayfa("Analysis");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox14_MouseDown(object sender, MouseEventArgs e)//1,2
        {
            konum_label.Text = "Android";
            sayfa("Android");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox18_MouseDown(object sender, MouseEventArgs e)//1,3
        {
            konum_label.Text = "Decompiler";
            sayfa("Decompiler");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox11_MouseDown(object sender, MouseEventArgs e)//2,1
        {
            konum_label.Text = "Detector";
            sayfa("Detector");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox15_MouseDown(object sender, MouseEventArgs e)//2,2
        {
            konum_label.Text = "Hex";
            sayfa("Hex");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox19_MouseDown(object sender, MouseEventArgs e)//2,3
        {
            konum_label.Text = "Packers";
            sayfa("Packers");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox12_MouseDown(object sender, MouseEventArgs e)//3,1
        {
            konum_label.Text = "Patcher";
            sayfa("Patcher");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox16_MouseDown(object sender, MouseEventArgs e)//3,2
        {
            konum_label.Text = "Petools";
            sayfa("Petools");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox20_MouseDown(object sender, MouseEventArgs e)//3,3
        {
            konum_label.Text = "Protector";
            sayfa("Protector");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox13_MouseDown(object sender, MouseEventArgs e)//4,1
        {
            konum_label.Text = "Unpacker";
            sayfa("Unpacker");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox17_MouseDown(object sender, MouseEventArgs e)//4,2
        {
            konum_label.Text = "Up";
            sayfa("Up");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void PictureBox21_MouseDown(object sender, MouseEventArgs e)//4,3
        {
            konum_label.Text = "Extra";
            sayfa("Extra");
            panel_ana.Visible = false;
            browse_panel.Visible = true;
        }

        private void Tool_FormClosing(object sender, FormClosingEventArgs e)
        {
            cikis();
        }
    }
}

