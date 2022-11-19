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
    public partial class ÖĞREG : Form
    {
        int DERSİ;
        int a;
        string[] bransid;
        string txt1, txt2, txt3, txt4, txt5, txt6, cmb;


        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);


        string resimadresi = "",CİNSİYET="";

        string TCÖĞRETMEN = ogretmenisl.ögretmentc;
        int NEDEN = ogretmenisl.niçin;
        int ÖGRETMENMİ= öğretmen.GÜNCELLEME;

        int İFLER=0;


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand ATA = new SqlCommand("insert into ogretmensınıf values ('" + textBox1.Text + "','" + textBox3.Text + "','" + textBox5.Text.ToUpper() + "')", baglantı);
                baglantı.Close();
                baglantı.Open();
                ATA.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("BAŞARI İLE SINIF ATANDI");
                this.Close();
                
            }
            catch (Exception)
            {
                MessageBox.Show("BİR HATA OLUŞTU");
            }
            
        }

        public ÖĞREG()
        {
            InitializeComponent();
        }

        private void ÖĞREG_Load(object sender, EventArgs e)
        {
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
                bransid[a]=idoku["ID"].ToString();
                a++;
            }

            baglantı.Close();

            openFileDialog1.Title = "Resim Seç";
            openFileDialog1.Filter = "Jpeg Dosyalari(*.jpg) | *.jpg";

            if (ÖGRETMENMİ== 1)
            {
                comboBox1.Enabled = false;
                button1.Enabled = false; textBox1.ReadOnly=true; textBox2.ReadOnly = true; textBox3.ReadOnly = true; textBox4.ReadOnly = true;
                radioButton1.Enabled = false; radioButton2.Enabled = false; dateTimePicker1.Enabled = false; dateTimePicker2.Enabled = false;
                baglantı.Open();
                SqlCommand bilgiler = new SqlCommand("select * from OGRETMEN WHERE TC='" + Form1.kimlikno + "'", baglantı);
                SqlDataReader doldur2 = bilgiler.ExecuteReader();

                while (doldur2.Read())
                {
                    textBox1.Text = doldur2["TC"].ToString();
                    textBox2.Text = doldur2["ADI"].ToString();
                    textBox3.Text = doldur2["SOYADI"].ToString();
                    textBox4.Text = doldur2["DOGUMYERI"].ToString();
                    textBox5.Text = doldur2["TELEFON"].ToString();
                    textBox6.Text = doldur2["PAROLA"].ToString();
                    DERSİ = Convert.ToInt32(doldur2["BRANS"].ToString());


                    dateTimePicker1.Text = doldur2["DOGUMTARIHI"].ToString();
                    dateTimePicker2.Text = doldur2["KAYITTARIHI"].ToString();
                    string cins = doldur2["CINSIYET"].ToString();

                    if (cins == "ERKEK")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (bransid[i] == DERSİ.ToString())
                    {
                        comboBox1.SelectedIndex = i;
                    }
                }
                baglantı.Close(); // DEĞERLERİ YERLERİNE ATAMAK

                SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + Form1.kimlikno + "'", baglantı);
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
                İFLER = öğretmen.GÜNCELLEME;
                öğretmen.GÜNCELLEME = 0;
            }
            if (NEDEN==4 && İFLER == 0)
            {
                comboBox1.Enabled = false;
                baglantı.Open();
                SqlCommand bilgiler = new SqlCommand("select * from OGRETMEN WHERE TC='" + Form1.kimlikno + "'", baglantı);
                SqlDataReader doldur2 = bilgiler.ExecuteReader();

                while (doldur2.Read())
                {
                    textBox1.Text = doldur2["TC"].ToString();
                    textBox2.Text = doldur2["ADI"].ToString();
                    textBox3.Text = doldur2["SOYADI"].ToString();
                    textBox4.Text = doldur2["DOGUMYERI"].ToString();
                    textBox5.Text = doldur2["TELEFON"].ToString();
                    textBox6.Text = doldur2["PAROLA"].ToString();
                    DERSİ = Convert.ToInt32(doldur2["BRANS"].ToString());


                    dateTimePicker1.Text = doldur2["DOGUMTARIHI"].ToString();
                    dateTimePicker2.Text = doldur2["KAYITTARIHI"].ToString();
                    string cins = doldur2["CINSIYET"].ToString();

                    if (cins == "ERKEK")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (bransid[i] == DERSİ.ToString())
                    {
                        comboBox1.SelectedIndex = i;
                    }
                }
                baglantı.Close(); // DEĞERLERİ YERLERİNE ATAMAK

                SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + Form1.kimlikno + "'", baglantı);
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
            }//müdür güncelleme
            if (NEDEN==1 && İFLER == 0)
            {
                
                label5.Visible = false; textBox6.Visible = false;
                radioButton2.Checked = true;
            }//YENİ ÖĞRETMEN EKLEME
            if (NEDEN==2 && İFLER == 0)
            {
                SINIFATA();
                textBox1.Text = TCÖĞRETMEN;
            }//SINIF ATAMA
            if (NEDEN==3 && İFLER == 0)
            {

                baglantı.Close();
                baglantı.Open();
                SqlCommand bilgiler = new SqlCommand("select * from OGRETMEN WHERE TC='" + TCÖĞRETMEN + "'", baglantı);
                SqlDataReader doldur2 = bilgiler.ExecuteReader();
                
                while (doldur2.Read())
                {
                    textBox1.Text = doldur2["TC"].ToString();
                    textBox2.Text = doldur2["ADI"].ToString();
                    textBox3.Text = doldur2["SOYADI"].ToString();
                    textBox4.Text = doldur2["DOGUMYERI"].ToString();
                    textBox5.Text = doldur2["TELEFON"].ToString();
                    textBox6.Text = doldur2["PAROLA"].ToString();
                    DERSİ =Convert.ToInt32(doldur2["BRANS"].ToString());
                    
                    
                    dateTimePicker1.Text = doldur2["DOGUMTARIHI"].ToString();
                    dateTimePicker2.Text = doldur2["KAYITTARIHI"].ToString();
                    string cins = doldur2["CINSIYET"].ToString();

                    if (cins=="ERKEK")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }

                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (bransid[i]==DERSİ.ToString())
                    {
                        comboBox1.SelectedIndex = i;
                    }
                }
                baglantı.Close(); // DEĞERLERİ YERLERİNE ATAMAK

                SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + TCÖĞRETMEN + "'", baglantı);
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
            }// ÖĞRETMEN GÜNCELLEME
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                resimadresi = openFileDialog1.FileName.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt1 = textBox1.Text.ToUpper(); txt2 = textBox2.Text.ToUpper(); txt3 = textBox3.Text.ToUpper(); txt4 = textBox4.Text.ToUpper(); txt5 = textBox5.Text.ToUpper(); txt6 = textBox6.Text.ToUpper(); cmb = comboBox1.Text.ToUpper();
            if (NEDEN == 1 && İFLER == 0)
            {
                if (resimadresi == "")
                {
                    MessageBox.Show("Lütfen Bir Fotoğraf seçiniz");
                }
                else
                {
                    if (txt4 == "" || txt5 == "" || txt3 == "" || txt2 == "" || txt1 == "" || cmb == "")
                    {
                        MessageBox.Show("Lütfen tüm alanları doldurunuz seçiniz");
                    }
                    else
                    {
                        if (radioButton1.Checked)
                        {
                            CİNSİYET = "ERKEK";
                        }
                        if (radioButton2.Checked)
                        {
                            CİNSİYET = "KADIN";
                        }
                        DateTime TARİH = dateTimePicker1.Value;
                        string dgko = TARİH.ToString("MM/dd/yyyy");
                        DateTime TARİH2 = dateTimePicker2.Value;
                        string kyt = TARİH2.ToString("MM/dd/yyyy");
                        
                        try
                        {
                            baglantı.Close();
                            baglantı.Open();
                            SqlCommand kmt = new SqlCommand("insert into OGRETMEN VALUES('" + txt1 + "','" + txt2 + "','" + txt3 + "','" + txt1.Substring(0, 6) + "','" + txt5 + "','" + bransid[comboBox1.SelectedIndex] + "','" + txt4 + "','" + dgko + "','" + CİNSİYET + "','" + kyt + "', '0')", baglantı);
                            kmt.ExecuteNonQuery();
                            baglantı.Close();
                            FileStream fs = new FileStream(resimadresi, FileMode.Open, FileAccess.Read);
                            BinaryReader br = new BinaryReader(fs);
                            byte[] foto = br.ReadBytes((int)fs.Length);
                            SqlCommand KQM = new SqlCommand("insert into RESIM VALUES(@tc , @res)", baglantı);
                            KQM.Parameters.Add("@tc", SqlDbType.NVarChar).Value = txt1;
                            KQM.Parameters.Add("@res", SqlDbType.Image, foto.Length).Value = foto;
                            baglantı.Open();
                            KQM.ExecuteNonQuery();
                            baglantı.Close();
                            MessageBox.Show("ÖĞRETMEN BAŞARI İLE KAYDEDİLDİ");
                            SINIFATA();
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show("BİR HATA OLUŞTU", EX.Message.ToString());
                        }
                        
                    }
                }
            }
            if (NEDEN == 3 && İFLER == 0)
            {
                string cinsiyet="";
                if (radioButton1.Checked)
                {
                    cinsiyet = "ERKEK";
                }
                if (radioButton2.Checked)
                {
                    cinsiyet = "KADIN";
                }
                try
                {
                    baglantı.Open();
                    DateTime TARİH = dateTimePicker1.Value;
                    string KYT = TARİH.ToString("MM/dd/yyyy");
                    DateTime TARİHİ = dateTimePicker2.Value;
                    string DGKO = TARİHİ.ToString("MM/dd/yyyy");
                    
                    
                    SqlCommand KOMUT = new SqlCommand("update OGRETMEN set TC = '" + txt1 + "' , BRANS = '"+ bransid[comboBox1.SelectedIndex] +"', ADI = '" + txt2 + "' , SOYADI = '" + txt3 + "' , TELEFON = '" + txt5 + "' , KAYITTARIHI = '" + KYT + "' , DOGUMYERI = '" + txt4 + "' , DOGUMTARIHI = '" + DGKO + "' , CINSIYET = '" + cinsiyet + "' , PAROLA = '" + txt6 + "' WHERE TC = '" + TCÖĞRETMEN + "'", baglantı);
                    KOMUT.ExecuteNonQuery();
                    baglantı.Close();
                    MessageBox.Show("KAYIT GÜNCELLENDİ");
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("BİR HATA OLUŞTU");

                }





                baglantı.Close();
                if (resimadresi == "")
                {

                }
                else
                {
                    FileStream fs = new FileStream(resimadresi, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] foto = br.ReadBytes((int)fs.Length);
                    SqlCommand KQM = new SqlCommand("UPDATE RESIM SET TCNO=@tcn , FOTO=@res where TCNO=@ög", baglantı);
                    KQM.Parameters.Add("@tcn", SqlDbType.NVarChar).Value = txt1;
                    KQM.Parameters.Add("@res", SqlDbType.Image, foto.Length).Value = foto;
                    KQM.Parameters.Add("@ög", SqlDbType.NVarChar).Value = TCÖĞRETMEN;
                    try
                    {
                        baglantı.Open();
                        KQM.ExecuteNonQuery();
                        baglantı.Close();
                        MessageBox.Show("FOTOĞRAF GÜNCELLENDİ");

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("FOTOĞRAF YÜKLENEMEDİ");

                    }
                }
            }
            if (NEDEN==4 && İFLER == 0)
            {
                baglantı.Close();
                DateTime TARİH = dateTimePicker1.Value;
                string dgko = TARİH.ToString("MM/dd/yyyy");
                DateTime TARİH2 = dateTimePicker2.Value;
                string kyt = TARİH2.ToString("MM/dd/yyyy");
                try
                {
                    SqlCommand komut2 = new SqlCommand("UPDATE OGRETMEN SET TC= '" + txt1 + "' , ADI= '" + txt2 + "' , SOYADI= '" + txt3 + "' , TELEFON= '" + txt5 + "' , PAROLA= '" + txt6 + "' , DOGUMTARIHI= '" + dgko + "' , DOGUMYERI= '" + txt4 + "' , KAYITTARIHI='" + kyt + "'  WHERE TC= '" + Form1.kimlikno + "'", baglantı);
                    baglantı.Open();
                    komut2.ExecuteNonQuery();
                    baglantı.Close();
                    MessageBox.Show("GÜNCELLEME BAŞARILI TEKRAR GİRİŞ YAPMANIZ GEREKLİ");

                    baglantı.Close();
                    if (resimadresi == "")
                    {

                    }
                    else
                    {
                        FileStream fs = new FileStream(resimadresi, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        byte[] foto = br.ReadBytes((int)fs.Length);
                        SqlCommand KQM = new SqlCommand("UPDATE RESIM SET TCNO=@tcn , FOTO=@res where TCNO=@ög", baglantı);
                        KQM.Parameters.Add("@tcn", SqlDbType.NVarChar).Value = txt1;
                        KQM.Parameters.Add("@res", SqlDbType.Image, foto.Length).Value = foto;
                        KQM.Parameters.Add("@ög", SqlDbType.NVarChar).Value = Form1.kimlikno;
                        try
                        {
                            baglantı.Open();
                            KQM.ExecuteNonQuery();
                            baglantı.Close();

                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show(EX.Message.ToString(), "FOTOĞRAF YÜKLENEMEDİ");

                        }
                    }

                }
                catch (Exception EX)
                {
                    MessageBox.Show("GÜNCELLEMEDE BİR HATA OLUŞTU", EX.Message.ToString());
                }
            }
            if (İFLER == 1)
            {
                baglantı.Close();
                DateTime TARİH = dateTimePicker1.Value;
                string dgko = TARİH.ToString("MM/dd/yyyy");
                DateTime TARİH2 = dateTimePicker2.Value;
                string kyt = TARİH2.ToString("MM/dd/yyyy");
                try
                {
                    SqlCommand komut2 = new SqlCommand("UPDATE OGRETMEN SET TELEFON= '" + txt5 + "' , PAROLA= '" + txt6 + "' WHERE TC= '" + Form1.kimlikno + "'", baglantı);
                    baglantı.Open();
                    komut2.ExecuteNonQuery();
                    baglantı.Close();
                    MessageBox.Show("GÜNCELLEME BAŞARILI ");
                }
                catch(Exception)
                {
                    MessageBox.Show("GÜNCELLEME BAŞARIZ OLDU");
                }
                
            }    
        }
        private void SINIFATA()
        {
            textBox3.Text = ""; textBox5.Text = "";
            label6.Visible = false; label7.Visible = false; label8.Visible = false; label9.Visible = false; label2.Visible = false;
            textBox2.Visible = false; textBox4.Visible = false; dateTimePicker1.Visible = false; dateTimePicker2.Visible = false; radioButton1.Visible = false; radioButton2.Visible = false;
            label5.Visible = false; textBox6.Visible = false; textBox1.ReadOnly=true; label3.Text = "SINIF"; label4.Text = "ŞUBE"; button1.Visible = false; button2.Visible = false;
            button3.Visible = true; label10.Visible = false; comboBox1.Visible = false;
        }
    }
}
