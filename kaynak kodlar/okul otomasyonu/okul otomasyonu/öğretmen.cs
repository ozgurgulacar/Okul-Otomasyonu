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
    public partial class öğretmen : Form
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);
        string TCNUMARA = Form1.kimlikno;
        public static int GÜNCELLEME=0;
        public string sınıf,sube;
        string DERSİ, sınavadı,sınıf2,sube2;
        string no2="", ad2, soyad2,sınıf3,sube3;
        public öğretmen()
            
        {
            InitializeComponent();
        }

        private void öğretmen_Load(object sender, EventArgs e)
        {
            baglantı.Close();
            SqlCommand komut = new SqlCommand("SELECT BRANSADI FROM OGRETMEN inner join BRANS ON OGRETMEN.BRANS=BRANS.ID WHERE OGRETMEN.TC='" + TCNUMARA + "'", baglantı);
            baglantı.Open();
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                DERSİ = oku["BRANSADI"].ToString();
            }
            else
            {
                MessageBox.Show("BİR HATA OLUŞTU LÜTFEN DAHA SONRA TEKRAR DENEYİNİZ");
            }
            baglantı.Close();


            SqlCommand kkom = new SqlCommand("select * from ogretmensınıf where tcno='" + TCNUMARA + "'", baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader okuu = kkom.ExecuteReader();
            string EKLE;
            while (okuu.Read())
            {
                EKLE = "";
                EKLE= okuu["sınıf"].ToString();
                EKLE +="/"+okuu["sube"].ToString();
                comboBox3.Items.Add(EKLE);
                
                
            }
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kim = "";
            if (textad.Text != "")//ARANAN ÖĞRENCİYİ BULMAK
            {
                kim = kim + " " + "and  ADI= '" + textad.Text + "'";
            }
            if (textsyad.Text != "")
            {
                kim = kim + " " + "and  SOYADI='" + textsyad.Text + "'";
            }
            if (textno.Text != "")
            {
                kim = kim + " " + "and  NUMARASI='" + textno.Text + "'";
            }
            if (texttc.Text != "")
            {
                kim = kim + " " + "and  TC='" + texttc.Text + "'";
            }
            if (textsube.Text != "")
            {
                kim = kim + " " + "and  SUBE='" + textsube.Text + "'";
            }
            if (textcins.Text != "")
            {
                kim = kim + " " + "and  CINSIYET='" + textcins.Text + "'";
            }
            if (comboBox1.Text != "")
            {
                kim = kim + " " + "and  SINIFI='" + comboBox1.Text + "'";
            }
            if (kim != "")
            {
                if (kim.Substring(0, 4) == " and")
                {
                    kim = kim.Substring(4);
                }
                //ARANAN ÖĞRENCİYİ BULMAK
                if (tabControl1.SelectedTab == tabPage1)
                {
                    listView1.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("select * from OGRENCI WHERE" + kim, baglantı);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = oku["TC"].ToString();
                        ekle.SubItems.Add(oku["NUMARASI"].ToString());
                        ekle.SubItems.Add(oku["ADI"].ToString());
                        ekle.SubItems.Add(oku["SOYADI"].ToString());
                        ekle.SubItems.Add(oku["SINIFI"].ToString());
                        ekle.SubItems.Add(oku["SUBE"].ToString());
                        ekle.SubItems.Add(oku["KAYITTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMYERI"].ToString());
                        ekle.SubItems.Add(oku["CINSIYET"].ToString());
                        ekle.SubItems.Add(oku["TDEVAM"].ToString());
                        listView1.Items.Add(ekle);
                    }
                    baglantı.Close();
                }
                if (tabControl1.SelectedTab == tabPage2)
                {
                    listView2.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut1 = new SqlCommand("select  * from VELI inner join OGRENCI ON OGRENCI.NUMARASI=VELI.OGRENCINO where" + kim, baglantı);
                    SqlDataReader oku = komut1.ExecuteReader();
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

                }

            }
            else
            {
                MessageBox.Show("LÜTFEN BİR KUTUCUĞU DOLDURUP TEKRAR SORGULAYINIZ");
            }
        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {
            baglantı.Close();
            listView1.Items.Clear();
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("select * from OGRENCI", baglantı);
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

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            baglantı.Close();
            baglantı.Open();
            SqlCommand kma2 = new SqlCommand("select * from RESIM WHERE TCNO='" + listView1.SelectedItems[0].Text + "'", baglantı);
            SqlDataReader ökü = kma2.ExecuteReader();
            if (ökü.Read())
            {
                byte[] gelen = new byte[0];
                gelen = (byte[])ökü["FOTO"];
                MemoryStream MS = new MemoryStream(gelen);
                pictureBox1.Image = Image.FromStream(MS);

            }
            baglantı.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            baglantı.Close();
            baglantı.Open();
            SqlCommand KMT = new SqlCommand("select  * from VELI inner join OGRENCI ON OGRENCI.NUMARASI=VELI.OGRENCINO", baglantı);
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

        private void exit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void anamenü_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Close();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MESAJLAR AC = new MESAJLAR();
            AC.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblgönderen.Text = ""; richTextBox1.Text = ""; lbltarih.Text = "";
            listView3.Items.Clear();
            listView5.Items.Clear();
            baglantı.Close();
            try
            {
                baglantı.Open();
                SqlCommand message = new SqlCommand("select * from MESAJLAR where ATC='" + TCNUMARA + "'", baglantı);
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
            baglantı.Close();
            try
            {
                baglantı.Close();
                baglantı.Open();
                SqlCommand messageg = new SqlCommand("select * from MESAJLAR where GTC='" + TCNUMARA + "'", baglantı);
                SqlDataReader show1 = messageg.ExecuteReader();
                while (show1.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = show1["ATC"].ToString();
                    ekle.SubItems.Add(show1["MESAJ"].ToString());
                    ekle.SubItems.Add(show1["STARİH"].ToString());
                    listView5.Items.Add(ekle);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString(), "BİR HATA OLUŞTU");
            }
            baglantı.Close();

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

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void bilgignc_Click(object sender, EventArgs e)
        {
            GÜNCELLEME = 1;
            ÖĞREG AC = new ÖĞREG();
            AC.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label17.Visible = true;
            button8.Visible = false; label15.Visible = false; label9.Visible = false;
            listView1.Visible = false; button2.Visible = false; pictureBox1.Visible = false;
            listView4.Visible = true; button6.Visible = true;
            listView4.Items.Clear();
            SqlCommand kkom = new SqlCommand("select * from ogretmensınıf where tcno='" + TCNUMARA + "'", baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader okuu = kkom.ExecuteReader();
            while (okuu.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuu["sınıf"].ToString();
                ekle.SubItems.Add(okuu["sube"].ToString());
                listView4.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label17.Visible = false;
            label9.Visible = true; button8.Visible = true; label15.Visible = true;
            listView1.Visible = true; button2.Visible = true; pictureBox1.Visible = true;
            listView4.Visible = false; button6.Visible = false;
        }

        private void listView5_DoubleClick(object sender, EventArgs e)
        {
            lblgönderen.Text = ""; richTextBox1.Text = ""; lbltarih.Text = "";
            string kmlk = listView5.SelectedItems[0].Text;
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
                richTextBox1.Text = listView5.SelectedItems[0].SubItems[1].Text;
                lbltarih.Text = listView5.SelectedItems[0].SubItems[2].Text;
            }
            catch (Exception)
            {


            }
            baglantı.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string ARAMA = "";
            SqlCommand kkom = new SqlCommand("select * from ogretmensınıf where tcno='" + TCNUMARA + "'", baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader okuu = kkom.ExecuteReader();
            while (okuu.Read())
            {
                ARAMA = ARAMA + " SINIFI='" + okuu["sınıf"].ToString() + "' and  SUBE='" + okuu["sube"].ToString() + "' OR";
            }
            baglantı.Close();
            string TAM= ARAMA.Substring(0, ARAMA.Length - 2);
            SqlCommand kkom2 = new SqlCommand("select * from OGRENCI WHERE"+TAM, baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader okuu2 = kkom2.ExecuteReader();
            while (okuu2.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuu2["TC"].ToString();
                ekle.SubItems.Add(okuu2["NUMARASI"].ToString());
                ekle.SubItems.Add(okuu2["ADI"].ToString());
                ekle.SubItems.Add(okuu2["SOYADI"].ToString());
                ekle.SubItems.Add(okuu2["SINIFI"].ToString());
                ekle.SubItems.Add(okuu2["SUBE"].ToString());
                ekle.SubItems.Add(okuu2["KAYITTARIHI"].ToString());
                ekle.SubItems.Add(okuu2["DOGUMTARIHI"].ToString());
                ekle.SubItems.Add(okuu2["DOGUMYERI"].ToString());
                ekle.SubItems.Add(okuu2["CINSIYET"].ToString());
                ekle.SubItems.Add(okuu2["TDEVAM"].ToString());
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "" || comboBox2.Text=="")
            {
                MessageBox.Show("LÜTFEN BİR SINIF SEÇİNİZ");
            }
            else
            {
                button11.Visible = false;
                label10.Visible = true;
                comboBox4.Visible = true;
                button14.Visible = true;
                button13.Visible = true;
                label20.Visible = true;

                string ayır = comboBox3.Text;
                int a = ayır.IndexOf("/");
                sınıf2 = ayır.Substring(0, a);
                sube2 = ayır.Substring(a + 1);

                baglantı.Close();
                SqlCommand data = new SqlCommand("select * from NOTLAR where DERS='" + DERSİ + "' and SINIF='" + sınıf2 + "' and SUBE='" + sube2 + "' and DÖNEM='"+comboBox2.Text+ "' and DURUM='AKTİF'", baglantı);
                SqlDataAdapter DA = new SqlDataAdapter(data);
                DataTable dt = new DataTable();
                DA.Fill(dt);
                dataGridView1.DataSource = dt;

                foreach (DataGridViewColumn item in dataGridView1.Columns)
                {
                    item.ReadOnly = true;
                }


            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (no2!="")
            {
                devamdetay.no = no2;
                devamdetay.ad = ad2;
                devamdetay.soyad = soyad2;
                devamdetay.ogretmenmi = false;
                devamdetay.görünürlük = false;

                devamdetay göster = new devamdetay();
                göster.ShowDialog();
            }
            else
            {
                MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ ÖĞRENCİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (no2 != "")
            {
                ogrencinot yeni = new ogrencinot();
                yeni.ögrno =no2;
                yeni.ad = ad2;
                yeni.soyad = soyad2;
                yeni.sınıf = sınıf3;
                yeni.sube = sube3;
                yeni.Show();
            }
            else
            {
                MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ ÖĞRENCİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            no2= listView1.SelectedItems[0].SubItems[1].Text;
            ad2= listView1.SelectedItems[0].SubItems[2].Text;
            soyad2 = listView1.SelectedItems[0].SubItems[3].Text;
            sınıf3= listView1.SelectedItems[0].SubItems[4].Text;
            sube3= listView1.SelectedItems[0].SubItems[5].Text;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            devamdetay.no = TCNUMARA;
            devamdetay.ad = "";
            devamdetay.soyad = "";
            devamdetay.ogretmenmi = true;
            devamdetay.görünürlük = false;
            devamdetay göster = new devamdetay();
            göster.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int A = dataGridView1.Rows.Count;
            string[] dizi = new string[A];
            string[] numaraları = new string[A];
            for (int i = 0; i < A - 1; i++)
            {
                numaraları[i] = dataGridView1.Rows[i].Cells["NUMARA"].Value.ToString();
                dizi[i] = dataGridView1.Rows[i].Cells[sınavadı].Value.ToString();
            }
            for (int i = 0; i < A - 1; i++)
            {
                baglantı.Close();
                SqlCommand güncel = new SqlCommand("update NOTLAR set " + sınavadı + "='" + dizi[i] + "' where NUMARA='" + numaraları[i] + "' and DERS='" + DERSİ + "' and DURUM='AKTİF'", baglantı);
                baglantı.Open();
                güncel.ExecuteNonQuery();
                baglantı.Close();
            }
            baglantı.Close();
            SqlCommand ortalama = new SqlCommand("update NOTLAR set ORTALAMA=(SINAV1+SINAV2+SÖZLÜ)/3 where SINIF='" + sınıf2 + "' and SUBE='" + sube2 + "'", baglantı);
            baglantı.Open();
            ortalama.ExecuteNonQuery();
            baglantı.Close();

            string ayır = comboBox3.Text;
            int a = ayır.IndexOf("/");
            sınıf2 = ayır.Substring(0, a);
            sube2 = ayır.Substring(a + 1);

            baglantı.Close();
            SqlCommand data = new SqlCommand("select * from NOTLAR where DERS='" + DERSİ + "' and SINIF='" + sınıf2 + "' and SUBE='" + sube2 + "' and DÖNEM='" + comboBox2.Text + "' and DURUM='AKTİF'", baglantı);
            SqlDataAdapter DA = new SqlDataAdapter(data);
            DataTable dt = new DataTable();
            DA.Fill(dt);
            dataGridView1.DataSource = dt;

            button12.Visible = false;
            button13.Visible = false;
            button11.Visible = true;
            button14.Visible = false;
            label20.Visible = false;
            comboBox4.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            button13.Visible = false;
            button11.Visible = true;
            button14.Visible = false;
            label20.Visible = false;
            comboBox4.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
         
            try
            {
                sınavadı = comboBox4.Text;
                dataGridView1.Columns[sınavadı].ReadOnly = false;

                button14.Visible = false; button12.Visible = true; button13.Visible = true;
                label20.Visible = false;
                comboBox4.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("BİR HATA OLUŞTU");
               
            }
            



        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            sınıf = listView4.SelectedItems[0].Text;
            sube = listView4.SelectedItems[0].SubItems[1].Text;
            dersprogramı yeni = new dersprogramı();
            yeni.ssınıf = sınıf;
            yeni.ssube = sube;
            yeni.ogrencimi = true;
            yeni.ShowDialog();
            
        }
    }
}
