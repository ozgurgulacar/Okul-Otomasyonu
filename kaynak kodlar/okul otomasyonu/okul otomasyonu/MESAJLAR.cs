using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace okul_otomasyonu
{
    public partial class MESAJLAR : Form
    {
        public MESAJLAR()
        {
            InitializeComponent();
        }
        string[] bransid;
        int a;

        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);


        string GÖNDERENTC = Form1.kimlikno;



        private void MESAJLAR_Load(object sender, EventArgs e)
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

            SqlCommand idler = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Open();
            SqlDataReader idoku = idler.ExecuteReader();
            a = 0;
            bransid = new string[comboBox1.Items.Count];
            while (idoku.Read())
            {
                bransid[a] = idoku["ID"].ToString();
                a++;
            }
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textno.Text = ""; textad.Text = ""; textsyad.Text = ""; texttc.Text = ""; textsube.Text = ""; textcins.Text = ""; sınıf.Text = "";
            label2.Visible = true; label3.Visible = true; label4.Visible = true; label7.Visible = true; label6.Visible = true; label8.Visible = true;
            textad.Visible = true; textno.Visible = false; textsyad.Visible = true; texttc.Visible = true; textsube.Visible = true; textcins.Visible = true; sınıf.Visible = true; comboBox1.Visible = true;
            button6.Visible = true; button1.Enabled = false; button2.Enabled = false;
            lblno.Visible = false; lblbrans.Visible = true;
            button6.Text = "Öğretmen ara";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textno.Text = ""; textad.Text = ""; textsyad.Text = ""; texttc.Text = ""; textsube.Text = ""; textcins.Text = ""; sınıf.Text = "";
            label2.Visible = true; label3.Visible = true; label4.Visible = true; label7.Visible = true; label6.Visible = true; label8.Visible = true;
            textad.Visible = true; textno.Visible = true; textsyad.Visible = true; texttc.Visible = true; textsube.Visible = true; textcins.Visible = true; sınıf.Visible = true; comboBox1.Visible = false;
            button6.Visible = true; button1.Enabled = false; button2.Enabled = false;
            lblno.Visible = true; lblbrans.Visible = false;
            button6.Text = "Öğrenci ara";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = 0;
            baglantı.Close();
            if (listView1.Items.Count<=0)
            {
                MessageBox.Show("LÜTFEN ÖNCE LİSTEYE ÖĞRENCİ VEYA ÖĞRETMEN EKLEYİN");
            }
            else
            {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        baglantı.Open();
                        if (listView1.Items[i].Checked)
                        {
                            try
                            {
                            DateTime TARİH = dateTimePicker1.Value;
                            string tarih1 = TARİH.ToString("MM/dd/yyyy");
                            SqlCommand mesaj = new SqlCommand("insert into MESAJLAR VALUES('" + GÖNDERENTC + "' , '" + listView1.Items[i].Text + "' , '" + richTextBox1.Text + "' , '" + tarih1 + "')", baglantı);
                                mesaj.ExecuteNonQuery();
                            }
                            catch (Exception EX)
                            {
                                a++;
                                MessageBox.Show(EX.Message.ToString(),listView1.Items[i].Text + " TC NUMARALI ÖĞRENCİYE MESAJ İLETİLEMEDİ");
                            }

                        }
                        baglantı.Close();
                    }
                    if (a == 0)
                    {
                        MessageBox.Show("MESAJINIZ BAŞARI İLE SEÇİLEN KİŞİLERE İLETİLDİ");
                    }
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Checked = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Checked = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Visible = false; label3.Visible = false; label4.Visible = false; label7.Visible = false; label6.Visible = false; label8.Visible = false;
            textad.Visible = false; textno.Visible = false; textsyad.Visible = false; texttc.Visible = false; textsube.Visible = false; textcins.Visible = false; sınıf.Visible = false; comboBox1.Visible = false;
            button6.Visible = false; button1.Enabled = true; button2.Enabled = true;
            lblno.Visible = false; lblbrans.Visible = false; 
            string kim = "";
            bool sınıfsube = false;
            if (textad.Text != "")//ARANAN ÖĞRENCİYİ BULMAK
            {
                kim = kim + " " + "and  ADI= '" + textad.Text + "'";
            }
            if (textsyad.Text != "")
            {
                kim = kim + " " + "and  SOYADI='" + textsyad.Text + "'";
            }
            if (button6.Text == "Öğrenci ara" && textno.Text != "")
            {
                kim = kim + " " + "and  NUMARASI='" + textno.Text + "'";
            }
            if (texttc.Text != "")
            {
                kim = kim + " " + "and  TC='" + texttc.Text + "'";
            }
            if (button6.Text == "Öğrenci ara" && textsube.Text != "")
            {
                kim = kim + " " + "and  SUBE='" + textsube.Text + "'";
            }
            if (textcins.Text != "")
            {
                kim = kim + " " + "and  CINSIYET='" + textcins.Text + "'";
            }
            if (button6.Text == "Öğrenci ara" && sınıf.Text != "")
            {
                kim = kim + " " + "and  SINIFI='" + sınıf.Text + "'";
            }
            if (button6.Text == "Öğretmen ara" && comboBox1.Text != "")
            {
                kim = kim + " " + "and  BRANS.ID='" + bransid[comboBox1.SelectedIndex] + "'";
            }
            if (button6.Text == "Öğretmen ara" && textsube.Text != "")
            {
                kim = kim + " " + "and  sube='" + textsube.Text + "'";
                sınıfsube = true;
            }
            if (button6.Text == "Öğretmen ara" && sınıf.Text != "")
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
                if (button6.Text=="Öğrenci ara")
                {
                    listView1.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut2 = new SqlCommand("select * from OGRENCI WHERE" + kim, baglantı);
                    SqlDataReader oku2 = komut2.ExecuteReader();
                    while (oku2.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = oku2["TC"].ToString();
                        ekle.SubItems.Add(oku2["ADI"].ToString());
                        ekle.SubItems.Add(oku2["SOYADI"].ToString());
                        
                        listView1.Items.Add(ekle);
                    }
                    baglantı.Close();
                }
                if (button6.Text=="Öğretmen ara")
                {
                    string sorgu = "";
                    if (sınıfsube == true)
                    {
                        sorgu = "inner join ogretmensınıf on OGRETMEN.TC = ogretmensınıf.tcno";
                    }
                    listView1.Items.Clear();
                    try
                    {
                        baglantı.Open();
                        SqlCommand komut = new SqlCommand("select DISTINCT(TC),ADI,SOYADI,TELEFON,BRANSADI,KAYITTARIHI,DOGUMTARIHI,DOGUMYERI,CINSIYET from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID " +sorgu +" WHERE " + kim, baglantı);
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            ListViewItem ekle = new ListViewItem();
                            ekle.Text = oku["TC"].ToString();
                            ekle.SubItems.Add(oku["ADI"].ToString());
                            ekle.SubItems.Add(oku["SOYADI"].ToString());
                            
                            listView1.Items.Add(ekle);
                        }
                        baglantı.Close();
                    }
                    catch (Exception y)
                    {
                        MessageBox.Show("BİR HATA OLUŞTU! " +y.Message);
                    }
                    baglantı.Close();
                }
                
            }
            else
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUNUZ");
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand( "SELECT * FROM OGRETMEN", baglantı);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["TC"].ToString();
                    ekle.SubItems.Add(oku["ADI"].ToString());
                    ekle.SubItems.Add(oku["SOYADI"].ToString());

                    listView1.Items.Add(ekle);
                }
                baglantı.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("BİR HATA OLUŞTU!");
            }
            baglantı.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            try
            {
                baglantı.Open();
                SqlCommand komut = new SqlCommand("SELECT * FROM OGRENCI", baglantı);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["TC"].ToString();
                    ekle.SubItems.Add(oku["ADI"].ToString());
                    ekle.SubItems.Add(oku["SOYADI"].ToString());

                    listView1.Items.Add(ekle);
                }
                baglantı.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("BİR HATA OLUŞTU!");
            }
            baglantı.Close();
        }
    }
}
