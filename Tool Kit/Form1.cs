using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;




using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;

using System.IO;
using Ionic.Zip;

namespace Tool_Kit
{
    public partial class Form1 : Form
    {

        public MySqlConnection mysqlbaglan = new MySqlConnection("Server=***;Database=***;Uid=***;Pwd=****;");
        MySqlCommand cmd;
        MySqlDataReader dr;


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


        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            panel1.Location = new Point(30, 44);
            groupBox1.Location = new Point(30, 44);
        }

        DateTime bugun = DateTime.Now;

        bool tas_i = false;
        Point sifir_nkt = new Point(0, 0);
        int syc = 1;

        private void Form1_Load(object sender, EventArgs e)
        {

            string path2 = Application.StartupPath;

            StreamReader sr = new StreamReader(path2 + "\\config.ini");

            if (sr.ReadToEnd() == "792go2#h74hfn8#03-4j82-03j")
            {
                sr.Close();
                Form1 a = new Form1();
                a.Enabled = false;
                //label1.Visible = true;
                //label1.Text = "İlk Açılışta gerekli dosyalar kuruluyor /nLütfen bekleyiniz";
                zipcikar(path2);


            }


            textBox2.PasswordChar = '*';
        }

        private void zipcikar(string yol)
        {
            string zipFileName = yol + "\\Tools.zip";
            string outputDirectory = yol;

            ZipFile zip = ZipFile.Read(zipFileName);
            Directory.CreateDirectory(outputDirectory);
            foreach (ZipEntry e in zip)
            {
                e.Extract(outputDirectory, ExtractExistingFileAction.OverwriteSilently);

            }

            zip.Dispose();



            string DosyaKntrl = yol + "\\Tools\\up";
            string DosyaKntrl2 = yol + "\\Tools\\Analysis";
            if (Directory.Exists(DosyaKntrl) && Directory.Exists(DosyaKntrl2))
            {
                TextWriter txt = new StreamWriter(yol + "\\config.ini");//////////////////////////////////
                txt.Write("AA");
                txt.Close();


                File.Delete(yol + "\\Tools.zip");

                Form1 a = new Form1();
                a.Enabled = true;
                label1.Visible = false;
                label1.Text = "Girdiğiniz bilgiler hatalı. Tekrar deneyin.";

            }

        }


        private void PictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            tas_i = true;
            sifir_nkt = new Point(e.X, e.Y);
        }

        private void PictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            tas_i = false;
        }

        private void PictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            if (tas_i)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.sifir_nkt.X, p.Y - this.sifir_nkt.Y);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }


        private void PictureBox8_MouseDown(object sender, MouseEventArgs e)
        {

            if (syc == 0)
            {
                textBox2.PasswordChar = '*';
                pictureBox8.Image = Properties.Resources.kpl;
                pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
                syc = 1;
            }
            else
            {
                textBox2.PasswordChar = '\0';
                pictureBox8.Image = Properties.Resources.açk;
                pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
                syc = 0;
            }


        }



        public void giris()
        {
            string k = textBox1.Text;
            string s = textBox2.Text;

            try
            {
                mysqlbaglan.Open(); //oluşturtuğumuz tanımı çalıştırarak açılmasını sağlıyoruz
                if (mysqlbaglan.State != ConnectionState.Closed) // tanımın durumunu kontrol ediyoruz bağlı mı değil mi
                {


                    cmd = new MySqlCommand();
                    cmd.Connection = mysqlbaglan;
                    cmd.CommandText = "SELECT * FROM siberatay where kullaniciadi='" + k + "' AND sifre='" + s + "' AND ban=0";
                    dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        string id = dr.GetString("id");
                        string ma = dr.GetString("mail");
                        mysqlbaglan.Close();
                        mysqlbaglan.Open();
                        cmd = new MySqlCommand();// bunu kaldır dene bak hata verecekmi
                        cmd.Connection = mysqlbaglan;
                        cmd.CommandText = "UPDATE siberatay SET tarih=@tarih,aktif=@aktif WHERE id=@id";


                        //string sorgu = "Insert into siberatay (tarih,aktif) values (@tarih,@aktif)";
                        //cmd = new MySqlCommand(sorgu, mysqlbaglan);
                        cmd.Parameters.AddWithValue("@tarih", bugun);
                        cmd.Parameters.AddWithValue("@aktif", 1);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        mysqlbaglan.Close();


                        Tool anaform = new Tool();
                        anaform.kkad = k;
                        anaform.ssif = s;
                        anaform.id = id;
                        anaform.mail = ma;

                        anaform.Show();
                        this.Hide();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        //break;

                    }
                    else
                    {
                        label1.Visible = true;
                        pictureBox5.Visible = true;

                    }
                    mysqlbaglan.Close();

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
        private void PictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            giris();
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                giris();
            }
        }


        private void PictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.kapatkpl;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void PictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.kapatcık;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void PictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.Image = Properties.Resources.altal;
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void PictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox10.Image = Properties.Resources.altalacık;
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
        }







        // sistem kayıt


        int cntrl;
        private void Reg_enter_Click(object sender, EventArgs e)
        {

            texttrm(panel1);

            try
            {
                mysqlbaglan.Open(); //oluşturtuğumuz tanımı çalıştırarak açılmasını sağlıyoruz
                if (mysqlbaglan.State != ConnectionState.Closed) // tanımın durumunu kontrol ediyoruz bağlı mı değil mi
                {


                    cmd = new MySqlCommand();
                    cmd.Connection = mysqlbaglan;
                    cmd.CommandText = "SELECT * FROM siberatay where kullaniciadi='" + reg_kadi.Text + "' OR mail='" + reg_mail.Text + "' ";
                    dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        cntrl = 1;
                    }
                    else
                    {

                    }
                    mysqlbaglan.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata!  Bağlantı sırasında hata oluştu");
            }




            if (reg_sifre.Text != reg_sifre2.Text)
            {
                label11.Visible = true;
                pictureBox7.Visible = true;
                label11.Text = "Girdiğiniz şifreler birbiri ile uyuşmuyor!" + "\n" +
                "Lütfen şifreyi tekrar giriniz.";
                reg_sifre.Text = "";
                reg_sifre2.Text = "";
            }
            else if (!reg_mail.Text.Contains("@") || reg_mail.Text.Contains(" "))// belirli karakterden az girildiği zamanda aynı uyarı verilsin
            {
                label11.Visible = true;
                pictureBox7.Visible = true;
                label11.Text = "Lütfen geçerli bir mail adresi girniz!";
            }
            else if (reg_mail.Text == "" || reg_kadi.Text == "" || reg_sifre.Text == "" || reg_sifre2.Text == "")
            {
                label11.Visible = true;
                pictureBox7.Visible = true;
                label11.Text = "Mail, kullanıcı adı ve şifre boş geçilemez!";
            }
            else if (cntrl == 1)
            {


                label11.Visible = true;
                label11.Text = "Bu mail veya kullanıcı adı ile daha önceden bir kayıt oluşturulmuş!" + "\n" +
                "Farklı bir mail yada kullanıcı adı deneyin.";

                pictureBox7.Visible = true;
                reg_mail.Text = "";
                reg_kadi.Text = "";
                reg_sifre.Text = "";
                reg_sifre2.Text = "";
                cntrl = 0;
            }
            else
            {
                mysqlbaglan.Open();
                cmd = new MySqlCommand();
                cmd.Connection = mysqlbaglan;
                cmd.CommandText = "Insert into siberatay (kullaniciadi,sifre,mail,ban,tarih,aktif) values (@kullaniciadi,@sifre,@mail,0,@tarih,0)";

                //string sorgu = "Insert into siberatay (tarih,aktif) values (@tarih,@aktif)";
                //cmd = new MySqlCommand(sorgu, mysqlbaglan);
                cmd.Parameters.AddWithValue("@kullaniciadi", reg_kadi.Text);
                cmd.Parameters.AddWithValue("@sifre", reg_sifre.Text);
                cmd.Parameters.AddWithValue("@mail", reg_mail.Text);
                cmd.Parameters.AddWithValue("@tarih", bugun);


                cmd.ExecuteNonQuery();
                textcl(panel1);
                label1.Visible = false;
                pictureBox5.Visible = false;
                MessageBox.Show("Kayıt Eklendi.");
                // yönlendirmeni yap :))
                mysqlbaglan.Close();
                panel1.Visible = false;
                

            }

        }

        public void texttrm(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is TextBox)
                {
                    (child as TextBox).Text.Trim();
                }
                else
                {
                    textcl(child);
                }
            }
        }

        public void textcl(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is TextBox)
                {
                    (child as TextBox).Text = "";
                }
                else
                {
                    textcl(child);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)// kayıt buton
        {
            panel1.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)// geri buton
        {
            panel1.Visible = false;
            textcl(panel1);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
    }
}