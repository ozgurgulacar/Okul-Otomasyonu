using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace okul_otomasyonu
{
    public partial class mudur : Form
    {
        public static int mudurpage=0;
        public static int OLDUMU = 0;


        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);


        string giren = Form1.kimlikno;
        public mudur()
        {
            InitializeComponent();
        }

        private void mudur_Load(object sender, EventArgs e)
        {
            göster();
            table2();
        }
        private void göster()
        {
            
            
            SqlCommand komut, komut1, komut2;
            baglantı.Open();
            komut = new SqlCommand("select count(*) as 'adet' from BRANS", baglantı);
            SqlDataReader oku = komut.ExecuteReader();
            int kac=0;
            while (oku.Read())
            {
                kac = Convert.ToInt32(oku["adet"]);
            }
            oku.Close();
            string[] dersler = new string[kac];
            string[] adedi = new string[kac];
            komut1 = new SqlCommand("select BRANSADI from BRANS", baglantı);
            SqlDataReader oku2 = komut1.ExecuteReader();
            int art = 0;
            while (oku2.Read())
            {
                dersler[art] = oku2["BRANSADI"].ToString();
                art++;
            }
            oku2.Close();
            for (int i = 0; i < kac; i++)
            {
                komut2 = new SqlCommand("select COUNT(*) as 'adet' from OGRETMEN inner join BRANS ON OGRETMEN.BRANS = BRANS.ID WHERE BRANSADI = '"+ dersler[i]+ "'",baglantı);
                SqlDataReader oku3 = komut2.ExecuteReader();
                while (oku3.Read())
                {
                    adedi[i] = (oku3["adet"]).ToString();
                }
                oku3.Close();
                
            }
            for (int i = 0; i<kac; i++)
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dersler[i];
                ekle.SubItems.Add(adedi[i]);
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
            
        }

        private void table2()
        {
            SqlCommand komut;
            komut = new SqlCommand("SELECT  count(*) AS DERSLİK,SINIFI,SUBE   FROM OGRENCI GROUP BY SINIFI,SUBE", baglantı);
            baglantı.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["SINIFI"].ToString()+ " " + oku["SUBE"].ToString();
                ekle.SubItems.Add(oku["DERSLİK"].ToString());
                listView2.Items.Add(ekle);
            }
            baglantı.Close();
        }
        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Close();
            frm.Show();
        }

        private void bilgignc_Click(object sender, EventArgs e)
        {
            ogretmenisl.niçin = 4;
            ÖĞREG frm = new ÖĞREG();
            frm.ShowDialog();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            ogrislem ac = new ogrislem();
            this.Hide();
            ac.Show();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void ogrislem_Click(object sender, EventArgs e)
        {
            ogretmenisl ac = new ogretmenisl();
            ac.Show();
            this.Hide();
                
               
        }

        private void dersislem_Click(object sender, EventArgs e)
        {
            DERSİSLEMGUNCEL();
            button5.Visible = false; ogrislem.Visible = false; button2.Visible = false; button9.Visible = false;
            label1.Visible = false; label2.Visible = false; label3.Visible = true; label4.Visible = true; label5.Visible = true;
            listView1.Visible = false; listView2.Visible = false; listView3.Visible = true; button4.Visible = true;
            textBox1.Visible = true; textBox2.Visible = true; button1.Visible = true; button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglantı.Close();
            if (textBox1.Text=="")
            {
                MessageBox.Show("LÜTFEN BİR DERS ADI GİRİNİZ");
            }
            else
            {
                int A = 0;   
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (textBox1.Text.ToUpper() == listView1.Items[i].Text)
                    {
                        MessageBox.Show("AYNI İSİMDE BİR DERS MEVCUT");
                        A++;
                    }
                }
                
                if(A==0)
                {
                    try
                    {
                        SqlCommand EKLE = new SqlCommand("insert into BRANS VALUES('" + textBox1.Text.ToUpper() + "')", baglantı);
                        baglantı.Open();
                        EKLE.ExecuteNonQuery();
                        baglantı.Close();
                        MessageBox.Show("DERS EKLEME İŞLEMİ BAŞARILI");
                        DERSİSLEMGUNCEL();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DERS EKLEME İŞLEMİ BAŞARISIZ!");
                    }
                }
                
            }
            baglantı.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglantı.Close();
            if (textBox2.Text == "")
            {
                MessageBox.Show("LÜTFEN SİLECEĞİNİZ DERSİN ID NUMARASINI GİRİNİZ");
            }
            else
            {
                baglantı.Close();
                SqlCommand VARMI = new SqlCommand("SELECT * FROM OGRETMEN WHERE BRANS='" + textBox2.Text + "'", baglantı);
                baglantı.Open();
                SqlDataReader KONTROL = VARMI.ExecuteReader();
                if (KONTROL.Read())
                {
                    MessageBox.Show("BU DERSE AİT ÖĞRETMEN BULUNMAKTADIR. ŞU AN BU DERSİ SİLEMEZSİNİZ");
                }                
                else
                {
                    try
                    {
                        baglantı.Close();
                        SqlCommand EKLE = new SqlCommand("DELETE FROM BRANS WHERE ID='" + textBox2.Text + "'", baglantı);
                        baglantı.Open();
                        EKLE.ExecuteNonQuery();
                        baglantı.Close();
                        MessageBox.Show("DERS SİLME İŞLEMİ BAŞARILI");
                        DERSİSLEMGUNCEL();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("DERS SİLME İŞLEMİ BAŞARISIZ!");
                    }
                }
                
            }
            baglantı.Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = true; ogrislem.Visible = true; button2.Visible = true;
            textBox1.Text = ""; textBox2.Text = ""; label1.Visible = true; label2.Visible = true; label3.Visible = false; label4.Visible = false; label5.Visible = false;
            listView1.Visible = true; listView2.Visible = true; listView3.Visible = false;
            textBox1.Visible = false; textBox2.Visible = false; button1.Visible = false; button3.Visible = false; button4.Visible = false; button9.Visible = true;
        }
        private void DERSİSLEMGUNCEL()
        {
            SqlCommand YAZDIR = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader YAZ = YAZDIR.ExecuteReader();
            listView3.Items.Clear();

            while (YAZ.Read())
            {
                ListViewItem EKLE = new ListViewItem();
                EKLE.Text = YAZ["ID"].ToString();
                EKLE.SubItems.Add(YAZ["BRANSADI"].ToString());
                listView3.Items.Add(EKLE);
            }
            baglantı.Close();
            textBox1.Text = ""; textBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button7.Visible = true; button8.Visible = true; button6.Visible = true; listView4.Visible = true; lblgönderen.Visible = true; label10.Visible = true; label13.Visible = true; richTextBox1.Visible = true;   label14.Visible = true; lbltarih.Visible = true;
            label1.Visible = false; label2.Visible = false; listView1.Visible = false; listView2.Visible = false; button2.Visible = false; ogrislem.Visible = false; button1.Visible = false; textBox1.Visible = false;
            dersislem.Visible = false;
            label6.Visible = true; listView5.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            listView5.Items.Clear();
            baglantı.Close();
            try
            {
                baglantı.Open();
                SqlCommand message = new SqlCommand("select * from MESAJLAR where ATC='" + giren + "'", baglantı);
                SqlDataReader show = message.ExecuteReader();
                while (show.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = show["GTC"].ToString();
                    ekle.SubItems.Add(show["MESAJ"].ToString());
                    ekle.SubItems.Add(show["STARİH"].ToString());
                    listView4.Items.Add(ekle);
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
                SqlCommand messageg = new SqlCommand("select * from MESAJLAR where GTC='" + giren + "'", baglantı);
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

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            lblgönderen.Text = ""; richTextBox1.Text = ""; lbltarih.Text = "";
            string kmlk = listView4.SelectedItems[0].Text;
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
                richTextBox1.Text = listView4.SelectedItems[0].SubItems[1].Text;
                lbltarih.Text = listView4.SelectedItems[0].SubItems[2].Text;
            }
            catch (Exception)
            {


            }
            baglantı.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button6.Visible = false; listView4.Visible = false; lblgönderen.Visible = false; label10.Visible = false; label13.Visible = false; richTextBox1.Visible = false; label14.Visible = false; lbltarih.Visible = false;
            label1.Visible = true; label2.Visible = true; listView1.Visible = true; listView2.Visible = true; button2.Visible = true; ogrislem.Visible = true;
            dersislem.Visible = true; listView5.Visible = false; label6.Visible = false;
            button7.Visible = false; button8.Visible = false; button9.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MESAJLAR AC = new MESAJLAR();
            AC.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

        private void button9_Click(object sender, EventArgs e)
        {
            dersprogramı dp = new dersprogramı();
            this.Close();
            dp.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            donemsonu ds = new donemsonu();
            ds.Show();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            yılsonu ds = new yılsonu();
            ds.Show();
            this.Close();
        }
    }
}
