using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace okul_otomasyonu
{
    public partial class devamdetay : Form
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);

        string seç = ".";
        string say = "";

        public static bool görünürlük = true;
        public static bool ogretmenmi = false;
        public static string ad, soyad, no;
        
        public devamdetay()
        {
            InitializeComponent();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Convert.ToString(say);
            Convert.ToString(seç);
            string sç = listView1.SelectedItems[0].Text;
            DateTime YENİ = Convert.ToDateTime(sç);
            dateTimePicker2.Value = YENİ;
            comboBox5.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
            if (comboBox5.Text == "TAM GÜN")
            {
                seç = "1";
            }
            else if (comboBox5.Text == "YARIM GÜN")
            {
                seç = "0.5";
            }
            else if (comboBox5.Text == "İZİNLİ")
            {
                seç = "0";
            }
            

        }

        private void button15_Click(object sender, EventArgs e)
        {
            string sorgu = "";
            if (textBox1.Text=="")
            {
                MessageBox.Show("LÜTFEN ÖNCE LİSTEDEN DEĞİŞTİRMEK İSTEDİĞİNİZ VERİYİ ÇİFT TIKLAYARAK SEÇİNİZ!", "HATA");
            }
            else
            {
                try
                {

                    DateTime TARİH = dateTimePicker2.Value;
                    string tarih1 = TARİH.ToString("yyyy-MM-dd");
                    if (comboBox5.Text == "TAM GÜN")
                    {
                        say = "1";
                    }
                    else if (comboBox5.Text == "YARIM GÜN")
                    {
                        say = "0.5";
                    }
                    else if (comboBox5.Text == "İZİNLİ")
                    {
                        say = "0";
                    }
                    if (ogretmenmi==false)
                    {
                        sorgu = "update öğrencidevam set DURUM='" + say + "' , TARİH='" + tarih1 + "' where İD='" + textBox1.Text + "'";
                    }
                    else
                    {
                        sorgu = "update HOCADEVAM set DURUM = '" + say + "', TARİH = '" + tarih1 + "' where İD = '" + textBox1.Text + "'";
                    }
                    SqlCommand güncel = new SqlCommand(sorgu, baglantı);
                    baglantı.Close();
                    baglantı.Open();
                    güncel.ExecuteNonQuery();
                    baglantı.Close();
                    string toplam = "";
                    if (say != seç)
                    {
                        if (say == "0.5")
                        {
                            say = "0,5";
                        }
                        if (seç == "0.5")
                        {
                            seç = "0,5";
                        }
                        if (seç == "1")
                        {

                            float a = float.Parse(say) - float.Parse(seç);
                            toplam = a.ToString();
                        }
                        if (seç == "0")
                        {
                            float a = float.Parse(say) + float.Parse(seç);
                            toplam = a.ToString();
                        }
                        if (seç == "0.5" || seç == "0,5")
                        {
                            float a = float.Parse(say) - float.Parse(seç);
                            toplam = a.ToString();
                        }
                        string sonu = toplam.Replace(',', '.');
                        //where komutunda bir problem çıktı kardeşim.
                        if (ogretmenmi == false)
                        {
                            sorgu = "UPDATE  OGRENCI set TDEVAM=TDEVAM+" + sonu + " WHERE NUMARASI='" + no + "'";
                        }
                        else
                        {
                            sorgu = "UPDATE  OGRETMEN set DEVAMT=DEVAMT+" + sonu + " WHERE TC='" + no + "'";
                        }

                        SqlCommand KOMUT2 = new SqlCommand(sorgu, baglantı);
                        baglantı.Open();
                        KOMUT2.ExecuteNonQuery();
                        baglantı.Close();
                        Convert.ToString(say);
                    }
                }
                catch (Exception i)
                {

                    MessageBox.Show(i.Message);
                }
                Convert.ToString(seç);
            }
            textBox1.Text = "";


            tablo();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string say2 = "";
            string sor = "";
            if (textBox1.Text == "")
            {
                MessageBox.Show("LÜTFEN ÖNCE LİSTEDEN DEĞİŞTİRMEK İSTEDİĞİNİZ VERİYİ ÇİFT TIKLAYARAK SEÇİNİZ!", "HATA");
            }
            else
            {
                if (comboBox5.Text == "TAM GÜN")
                {
                    say2 = "1";
                }
                else if (comboBox5.Text == "YARIM GÜN")
                {
                    say2 = "0.5";
                }
                else if (comboBox5.Text == "İZİNLİ")
                {
                    say2 = "0";
                }
                if (ogretmenmi==false)
                {
                    sor = "delete from öğrencidevam where İD='" + textBox1.Text + "' update OGRENCI set TDEVAM=TDEVAM-'"+ say2+ "' where NUMARASI='" + no +"'";
                }
                else
                {
                    
                       sor = "delete from HOCADEVAM where İD='" + textBox1.Text + "' update OGRETMEN set DEVAMT=DEVAMT-'" + say2 + "'where TC='" + no + "'";
                }
                SqlCommand sil = new SqlCommand(sor, baglantı);
                baglantı.Close();
                baglantı.Open();
                sil.ExecuteNonQuery();
                baglantı.Close();

                textBox1.Text = "";


                tablo();

            }
        }

        private void devamdetay_Load(object sender, EventArgs e)
        {
            if (görünürlük==false)
            {
                label1.Visible = false; label18.Visible = false; label19.Visible = false;
                textBox1.Visible = false; comboBox5.Visible = false; dateTimePicker2.Visible = false;
                button1.Visible = false; button15.Visible = false;
            }
            else
            {
                label1.Visible = true; label18.Visible = true; label19.Visible = true;
                textBox1.Visible = true; comboBox5.Visible = true; dateTimePicker2.Visible = true;
                button1.Visible = true; button15.Visible = true;
            }
            tablo();

            label2.Text = ad + " " + soyad + " " + "kişisinin toplam devamsızlıkları";

        }
        private void tablo()
        {
            listView1.Items.Clear();

            string sql = "";
            if (ogretmenmi==false)
            {
                sql = "select * from öğrencidevam where ÖGRNO='" + no + "'";
            }
            else
            {
                sql = "select * from HOCADEVAM where TCNO='" + no + "'";
            }
            baglantı.Close();
            SqlCommand komut = new SqlCommand(sql, baglantı);
            baglantı.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["TARİH"].ToString();
                string HANGİ = oku["DURUM"].ToString();
                string durumu = "";
                if (HANGİ == "1")
                {
                    durumu = "TAM GÜN";

                }
                else if (HANGİ == "0,5")
                {
                    durumu = "YARIM GÜN";
                }
                else if (HANGİ == "0")
                {
                    durumu = "İZİNLİ";
                }
                ekle.SubItems.Add(durumu);
                ekle.SubItems.Add(oku["İD"].ToString());
                listView1.Items.Add(ekle);
            }

        }
    }
}
