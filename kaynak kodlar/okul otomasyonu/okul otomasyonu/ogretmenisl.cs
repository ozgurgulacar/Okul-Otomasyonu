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
    public partial class ogretmenisl : Form
    {
        public static int niçin = 0;
        public static string ögretmentc = "";
        int a;
        string[] bransid;
        string sınıf2, sube2;
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);

        public ogretmenisl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kim = "";
            bool sınıfsube=false;
            bool braans = false;
            int a = comboBox1.Items.Count;
            if (textad.Text != "")//ARANAN ÖĞRETMENİ BULMAK
            {
                kim = kim + " " + "and  ADI= '" + textad.Text + "'";
            }
            if (textsyad.Text != "")
            {
                kim = kim + " " + "and  SOYADI='" + textsyad.Text + "'";
            }
            if (comboBox1.Text !=" " && comboBox1.Text != "")
            {
                braans = true;
                kim = kim + " " + "and  BRANS.ID='" + bransid[comboBox1.SelectedIndex] + "'";
            }
            if (texttc.Text != "")
            {
                kim = kim + " " + "and  TC='" + texttc.Text + "'";
            }
            if (textsube.Text != "")
            {
                kim = kim + " " + "and  sube='" + textsube.Text + "'";
                sınıfsube = true;
            }
            if (textcins.Text != "")
            {
                kim = kim + " " + "and  CINSIYET='" + textcins.Text + "'";
            }
            if (sınıf.Text != "")
            {
                kim = kim + " " + "and  sınıf='" + sınıf.Text + "'";
                sınıfsube = true;
            }
            if (kim != "")
            {
                if (kim.Substring(0, 4) == " and")
                {
                    kim = kim.Substring(4);
                }
                string sorgu = "";
                if (sınıfsube==true)
                {
                    sorgu= "inner join ogretmensınıf on OGRETMEN.TC = ogretmensınıf.tcno";
                }
                listView1.Items.Clear();
                try
                {
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("select DISTINCT(TC),ADI,SOYADI,TELEFON,BRANSADI,KAYITTARIHI,DOGUMTARIHI,DOGUMYERI,CINSIYET,DEVAMT from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID "+ sorgu +" WHERE " + kim, baglantı);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = oku["TC"].ToString();
                        ekle.SubItems.Add(oku["ADI"].ToString());
                        ekle.SubItems.Add(oku["SOYADI"].ToString());
                        ekle.SubItems.Add(oku["TELEFON"].ToString());
                        ekle.SubItems.Add(oku["BRANSADI"].ToString());
                        ekle.SubItems.Add(oku["KAYITTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMYERI"].ToString());
                        ekle.SubItems.Add(oku["CINSIYET"].ToString());
                        ekle.SubItems.Add(oku["DEVAMT"].ToString());
                        listView1.Items.Add(ekle);
                    }
                    baglantı.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show("BİR HATA OLUŞTU!  " + x.Message);
                }
                baglantı.Close();

                //ARANAN ÖĞRETMENİ BULMAK
            }
            else
            {
                MessageBox.Show("LÜTFEN BİR ALANI DOLDURUP TEKRAR DENEYİNİZ");
            }
            comboBox1.Text = "";

        }

        private void ogretmenisl_Load(object sender, EventArgs e)
        {
            baglantı.Close();
            SqlCommand doldur = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Open();
            SqlDataReader DOLDUR2 = doldur.ExecuteReader();
            while (DOLDUR2.Read())
            {
                comboBox1.Items.Add(DOLDUR2["BRANSADI"].ToString());
            }
            baglantı.Close();

            bransid = new string[comboBox1.Items.Count];
            SqlCommand idler = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Open();
            SqlDataReader idoku = idler.ExecuteReader();
            a = 0;

            while (idoku.Read())
            {
                bransid[a] = idoku["ID"].ToString();
                a++;
            }
            baglantı.Close();

            comboBox1.Items.Add("");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            baglantı.Close();
            ögretmentc = listView1.SelectedItems[0].Text;
            SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + ögretmentc + "'", baglantı);
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

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SqlCommand LİST = new SqlCommand("select * from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID", baglantı);
            baglantı.Open();
            SqlDataReader lis = LİST.ExecuteReader();
            while (lis.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = lis["TC"].ToString();
                ekle.SubItems.Add(lis["ADI"].ToString());
                ekle.SubItems.Add(lis["SOYADI"].ToString());
                ekle.SubItems.Add(lis["TELEFON"].ToString());
                ekle.SubItems.Add(lis["BRANSADI"].ToString());
                ekle.SubItems.Add(lis["KAYITTARIHI"].ToString());
                ekle.SubItems.Add(lis["DOGUMTARIHI"].ToString());
                ekle.SubItems.Add(lis["DOGUMYERI"].ToString());
                ekle.SubItems.Add(lis["CINSIYET"].ToString());
                ekle.SubItems.Add(lis["DEVAMT"].ToString());
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Close();
            if (ögretmentc=="")
            {
                MessageBox.Show("BİR ÖĞRETMEN SEÇİLMEDİ! LİSTEYE ÇİFT TIKLAYARAK BİR ÖĞRETMEN SEÇEBİLİRSİNİZ","HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    
                    textBox2.Text = "TC NO'SU " + ögretmentc + " OLAN " + listView1.SelectedItems[0].SubItems[1].Text + " " + listView1.SelectedItems[0].SubItems[2].Text + " İSİMLİ BİR " + listView1.SelectedItems[0].SubItems[4].Text + " ÖĞRETMENİDİR. KURUMUMUZDA  " + listView1.SelectedItems[0].SubItems[5].Text.Substring(0, 10) + " TARİHİNDEN BERİ ÇALIŞMAKTADIR.";
                    button7.Visible = true; button8.Visible = true; textBox2.Visible = true;
                    button2.Visible = false; label9.Visible = false; button1.Visible = false; listView1.Visible = false; label10.Visible = false; listView2.Visible = true;
                    button4.Enabled = true; button4.Visible = true; button3.Visible = false; button6.Visible = true; label11.Visible = true; textBox1.Visible = true; button5.Visible = false;
                    button10.Visible = true; button11.Visible = true; button9.Visible = false;
                    SINIFLARIGÜNC();
                }
                catch (Exception)
                {
                    MessageBox.Show("ÖNCELİKLE BİR ÖĞRETMEN SEÇİNİZ");
                }

                    
            }
            baglantı.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = true; label9.Visible = true; button1.Visible = true; listView1.Visible = true; label10.Visible = true; listView2.Visible = false;
            button4.Enabled = false; button4.Visible = false; button3.Visible = true; button9.Visible = true;
            button6.Visible = false; label11.Visible = false; textBox1.Visible = false;
            button5.Visible = true;  button7.Visible = false; button8.Visible = false;
            textBox2.Visible = false; button11.Visible = false; button10.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            niçin = 1;
            ÖĞREG AC = new ÖĞREG();
            AC.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string İDSİ = textBox1.Text;
            if (textBox1.Text!="")
            {

                SqlCommand SİL = new SqlCommand("DELETE  FROM ogretmensınıf where ıd='" + İDSİ + "'", baglantı);
                baglantı.Close();
                baglantı.Open();
                SİL.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("SINIFTAN KALDIRILDI");
                SINIFLARIGÜNC();
            }
            else
            {
                MessageBox.Show("LÜTFEN İD ALANINI BOŞ BIRAKMAYINIZ");
            }
        }
        public void SINIFLARIGÜNC()
        {
            baglantı.Close();
            listView2.Items.Clear();
            baglantı.Open();
            SqlCommand liss = new SqlCommand("select * from OGRETMEN  inner join ogretmensınıf on OGRETMEN.TC=ogretmensınıf.tcno where TC='" + ögretmentc + "'", baglantı);
            SqlDataReader OKKU = liss.ExecuteReader();
            while (OKKU.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = OKKU["ADI"].ToString() + " " + OKKU["SOYADI"].ToString();
                ekle.SubItems.Add(OKKU["sınıf"].ToString() + "/" + OKKU["sube"].ToString());
                ekle.SubItems.Add(OKKU["ıd"].ToString());
                listView2.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ögretmentc=="")
            {
                MessageBox.Show("ÖNCE LİSTEDEN BİR ÖĞRETMEN SEÇİNİZ");
            }
            else
            {
                niçin = 2;
                ÖĞREG AC = new ÖĞREG();
                AC.ShowDialog();
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SINIFLARIGÜNC();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mudur SAYFA = new mudur();
            SAYFA.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (ögretmentc=="")
            {
                MessageBox.Show("LÜTFEN ÖNCE LİSTEDEN BİR ÖĞRETMEN SEÇİNİZ!");
            }
            else
            {

                try
                {
                    DialogResult eminmisin = new DialogResult();
                    String YAZI = "TC NO'SU " + ögretmentc + " OLAN " + listView1.SelectedItems[0].SubItems[1].Text + " " + listView1.SelectedItems[0].SubItems[2].Text + " İSİMLİ ÖĞRETMENİ SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ?";
                    eminmisin = MessageBox.Show(YAZI, " ", MessageBoxButtons.YesNo);
                    if (eminmisin == DialogResult.Yes)
                    {
                        try
                        {
                            baglantı.Close();
                            baglantı.Open();
                            SqlCommand KMOU = new SqlCommand("DELETE FROM OGRETMEN WHERE TC='"+ögretmentc+"' DELETE FROM RESIM WHERE TCNO='"+ ögretmentc +"' DELETE FROM ogretmensınıf WHERE tcno='"+ ögretmentc+"' DELETE FROM MESAJLAR WHERE GTC='"+ ögretmentc+"' OR ATC='"+ ögretmentc+"'", baglantı);
                            KMOU.ExecuteNonQuery();
                            baglantı.Close();
                            MessageBox.Show("ÖĞRETMEN BAŞARI İLE SİLİNDİ");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(),"SİLME İŞLEMİNDE BİR HATA OLUŞTU");
                        }
                        
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("SİLME İŞLEMİNDE BİR HATA OLUŞTU.ÖĞRETMEN SEÇMEMİŞ OLABİLİRSİNİZ!");
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (ögretmentc=="")
            {
                MessageBox.Show("LÜTFEN LİSTEDEN BİR ÖĞRETMENİN BİLGİLERİNE ÇİFT TIKLAYARAK SEÇİNİZ");
            }
            else
            {
                niçin = 3;
                ÖĞREG AC = new ÖĞREG();
                AC.ShowDialog();
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            DEVAMHOCA YENİ = new DEVAMHOCA();
            YENİ.Show();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            
            string ayır = listView2.SelectedItems[0].SubItems[1].Text;
            int a=ayır.IndexOf("/");
            sınıf2 = ayır.Substring(0, a);
            sube2 = ayır.Substring(a+1);

            dersprogramı yeni = new dersprogramı();
            yeni.ssınıf = sınıf2;
            yeni.ssube = sube2;
            yeni.ogrencimi = true;
            yeni.ShowDialog();
        }
    }
}
