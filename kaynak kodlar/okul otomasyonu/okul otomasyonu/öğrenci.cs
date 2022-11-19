using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace okul_otomasyonu
{
    public partial class öğrenci : Form
    {

        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);
        string sınıf, sube;
        public static string numara;//ÖĞRENCİ NUMARASI
        string TCNO = Form1.kimlikno;//ÖĞRENCİ TC
        public static int ogr;
        public öğrenci()
            
        {
            InitializeComponent();
        }

        private void öğrenci_Load(object sender, EventArgs e)
        {
            listView2.Visible = false;
            listView1.Visible = false;
            baglantı.Close();
            SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + TCNO + "'", baglantı);
            baglantı.Open();
            SqlDataReader ökü = kma.ExecuteReader();
            if (ökü.Read())
            {
                byte[] gelen = new byte[0];
                gelen = (byte[])ökü["FOTO"];
                MemoryStream MS = new MemoryStream(gelen);
                pictureBox1.Image = Image.FromStream(MS);

            }
            baglantı.Close();
        }




        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Close();
            frm.Show();
        }

        private void bilgignc_Click_1(object sender, EventArgs e)
        {
            ogr = 1;
            bilgigüncelleme frm = new bilgigüncelleme();
            frm.ShowDialog();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView2.Visible = false;
            listView1.Visible = true;
            baglantı.Close();
            listView1.Items.Clear();
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("select * from OGRENCI WHERE TC='"+TCNO+"'", baglantı);
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku2["TC"].ToString();
                ekle.SubItems.Add(oku2["NUMARASI"].ToString());
                ekle.SubItems.Add(oku2["ADI"].ToString());
                ekle.SubItems.Add(oku2["SOYADI"].ToString());
                ekle.SubItems.Add(oku2["SINIFI"].ToString());
                ekle.SubItems.Add(oku2["SUBE"].ToString());
                ekle.SubItems.Add(oku2["KAYITTARIHI"].ToString());
                ekle.SubItems.Add(oku2["DOGUMTARIHI"].ToString());
                ekle.SubItems.Add(oku2["DOGUMYERI"].ToString());
                ekle.SubItems.Add(oku2["CINSIYET"].ToString());
                ekle.SubItems.Add(oku2["TDEVAM"].ToString());
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Visible = false;
            listView2.Visible = true;
            listView2.Items.Clear();
            baglantı.Close();
            baglantı.Open();
            SqlCommand KMT = new SqlCommand("select  * from VELI inner join OGRENCI ON OGRENCI.NUMARASI=VELI.OGRENCINO WHERE NUMARASI='"+numara+"'", baglantı);
            SqlDataReader oku = KMT.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem EKLE = new ListViewItem();
                EKLE.Text = oku["ID"].ToString();
                EKLE.SubItems.Add(oku["NUMARASI"].ToString());
                EKLE.SubItems.Add(oku["ADI"].ToString());
                EKLE.SubItems.Add(oku["SOYADI"].ToString());
                EKLE.SubItems.Add(oku["VELITC"].ToString());
                EKLE.SubItems.Add(oku["VELIADI"].ToString());
                EKLE.SubItems.Add(oku["VELISOYADI"].ToString());
                EKLE.SubItems.Add(oku["TELEFON"].ToString());
                EKLE.SubItems.Add(oku["YAKINLIK"].ToString());
                EKLE.SubItems.Add(oku["VDOGUMTARIHI"].ToString());
                EKLE.SubItems.Add(oku["VDOGUMYERI"].ToString());
                listView2.Items.Add(EKLE);
            }
            baglantı.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            baglantı.Close();
            try
            {
                baglantı.Open();
                SqlCommand message = new SqlCommand("select * from MESAJLAR where ATC='" + TCNO + "'", baglantı);
                SqlDataReader show = message.ExecuteReader();
                while (show.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = show["GTC"].ToString();
                    ekle.SubItems.Add(show["MESAJ"].ToString());
                    ekle.SubItems.Add(show["STARİH"].ToString());
                    listView3.Items.Add(ekle);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString(), "BİR HATA OLUŞTU");

            }
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            string kmlk = listView3.SelectedItems[0].Text;
            baglantı.Close();
            try
            {
                baglantı.Open();
                SqlCommand adı = new SqlCommand("select ADI,SOYADI FROM OGRETMEN WHERE TC='" + kmlk + "'", baglantı);
                SqlDataReader YAZDIR = adı.ExecuteReader();
                while (YAZDIR.Read())
                {
                    string ad = YAZDIR["ADI"].ToString();
                    string soyad = YAZDIR["SOYADI"].ToString();
                    lblgönderen.Text = ad + " " + soyad;

                }
                baglantı.Close();
                richTextBox1.Text = listView3.SelectedItems[0].SubItems[1].Text;
                lbltarih.Text = listView3.SelectedItems[0].SubItems[2].Text;
            }
            catch (Exception)
            {


            }
            baglantı.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglantı.Close();
            SqlCommand yeni = new SqlCommand("select * from NOTLAR where NUMARA='" + numara + "' and SINIF='" + comboBox1.Text + "' and DÖNEM='"+comboBox2.Text+"'", baglantı);
            SqlDataAdapter DA = new SqlDataAdapter(yeni);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            dataGridView1.DataSource = DT;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                devamdetay.no = listView1.Items[0].SubItems[1].Text;
                devamdetay.ad = listView1.Items[0].SubItems[2].Text;
                devamdetay.soyad = listView1.Items[0].SubItems[3].Text;
                devamdetay.ogretmenmi = false;
                devamdetay.görünürlük = false;

                devamdetay göster = new devamdetay();
                göster.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("LÜTFEN ÖNCE BİLGİLERİM TABLOSUNU AÇINIZ","HATA");
                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("select * from OGRENCI WHERE TC='" + TCNO + "'", baglantı);
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                sınıf = oku2["SINIFI"].ToString();
                sube = oku2["SUBE"].ToString();
            }
            baglantı.Close();
            dersprogramı yeni = new dersprogramı();
            yeni.ssınıf = sınıf;
            yeni.ssube = sube;
            yeni.ogrencimi = true;
            yeni.ShowDialog();
        }
    }
}
