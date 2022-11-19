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
    public partial class bilgigüncelleme : Form
    {
        public string dönem;
        string TCGÜN;
        string resimadresi="",TCGÜNC;
        string veligünc = ogrislem.vgünc;
        int mudur1 = mudur.mudurpage;
        string ögno = ogrislem.ögrno;
        string yeniögr="";
        int a = öğrenci.ogr;
        int b = mudur.mudurpage;
        string tcno = Form1.kimlikno;
        int ifler=0;
        string yeninumarası,eskitc,eskisınıf,eskisube;

        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglanti = new SqlConnection(baglantı2);

        public bilgigüncelleme()
        {
            InitializeComponent();
        }


        private void bilgigüncelleme_Load(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Seç";
            openFileDialog1.Filter = "Jpeg Dosyalari(*.jpg) | *.jpg";
            
            
            baglanti.Close();
            if (a==1)//öğrenci kendi bilgisini güncellemesi
            {
                button2.Visible = false; dateTimePicker1.Enabled = false;
                kmlkno.ReadOnly = true; ad.ReadOnly = true; soyad.ReadOnly = true; ders.ReadOnly = true; dogyer.ReadOnly = true;
                maskedTextBox1.ReadOnly = true;  tel.ReadOnly = false; tel.ReadOnly = true;
                label10.Text = "SINIFI";
                label8.Text = "NUMARASI";
                SqlCommand kmt = new SqlCommand("select * from OGRENCI where tc='" + tcno + "'", baglanti);
                baglanti.Open();
                SqlDataReader oku2 = kmt.ExecuteReader();
                if (oku2.Read())
                {
                    kmlkno.Text = oku2["TC"].ToString();
                    ad.Text = oku2["ADI"].ToString();
                    soyad.Text = oku2["SOYADI"].ToString();
                    ders.Text = oku2["SINIFI"].ToString() + " / " + oku2["SUBE"].ToString();
                    string cins = oku2["CINSIYET"].ToString();
                    maskedTextBox1.Text = oku2["KAYITTARIHI"].ToString();
                    dateTimePicker1.Text = oku2["DOGUMTARIHI"].ToString();
                    dogyer.Text = oku2["DOGUMYERI"].ToString();
                    tel.Text = oku2["NUMARASI"].ToString();
                    sifre.Text = oku2["PAROLA"].ToString();

                    if (cins == "ERKEK")
                    {
                        radioButton1.Checked = true;
                        radioButton2.Enabled = false;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                        radioButton1.Enabled = false;
                    }
                    
                }
                baglanti.Close();
                ifler = öğrenci.ogr;
                öğrenci.ogr = 0;
            }
            if (b==1 && ifler==0)//yeni öğrenci kaydı
            {
                label8.Visible = false;
                label3.Visible = true;
                tel.Visible = false;
                radioButton1.Checked = true;
                label10.Text = "SINIFI";
                label20.Text = "ŞUBESİ";
                button1.Text = "DEVAM ET";
                
            }
            if (b==2 && ifler == 0)//müdür öğrenci bilgisini güncelleme
            {
                button1.Visible = true;
                label8.Visible = true;
                label3.Visible = true;
                tel.Visible = true;
                radioButton1.Checked = true;
                label10.Text = "SINIFI";
                label20.Text = "ŞUBESİ";
                button1.Text = "GÜNCELLE";
                label8.Text = "PAROLA";
                baglanti.Open();
                SqlCommand kommut = new SqlCommand("select * from OGRENCI where NUMARASI='" + ögno + "'", baglanti);
                SqlDataReader OKKU = kommut.ExecuteReader();
                if (OKKU.Read())
                {
                    kmlkno.Text = OKKU["TC"].ToString();
                    eskitc = kmlkno.Text;
                    ad.Text = OKKU["ADI"].ToString();
                    soyad.Text = OKKU["SOYADI"].ToString();
                    maskedTextBox1.Text = OKKU["KAYITTARIHI"].ToString();
                    dateTimePicker1.Text = OKKU["DOGUMTARIHI"].ToString();
                    dogyer.Text = OKKU["DOGUMYERI"].ToString();
                    ders.Text = OKKU["SINIFI"].ToString();
                    eskisınıf = ders.Text;
                    sifre.Text = OKKU["SUBE"].ToString();
                    eskisube = sifre.Text;
                    tel.Text = OKKU["PAROLA"].ToString();
                    string Cİ = OKKU["CINSIYET"].ToString();
                    if (Cİ == "erkek" || Cİ == "ERKEK")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    TCGÜNC = kmlkno.Text;
                    baglanti.Close();
                }
                baglanti.Close();
                    SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + kmlkno.Text + "'", baglanti);
                    baglanti.Open();
                    SqlDataReader ökü = kma.ExecuteReader();
                    if (ökü.Read())
                    {
                        byte[] gelen = new byte[0];
                        gelen = (byte[])ökü["FOTO"];
                        MemoryStream MS = new MemoryStream(gelen);
                        pictureBox1.Image = Image.FromStream(MS);
                    }
                baglanti.Close();
                

                
            }
            if (b==3 && ifler == 0)
            {
                button2.Visible = false;
                pictureBox1.Visible = false;
                label10.Visible = false; ders.Visible = false; label16.Visible = false; maskedTextBox1.Visible = false;
                label20.Text = "YAKINLIK";
                label18.Visible = false; radioButton1.Visible = false; radioButton2.Visible = false; label3.Visible = false;
                SqlCommand kmm = new SqlCommand("select * from VELI WHERE ID='" + veligünc + "'", baglanti);
                baglanti.Open();
                SqlDataReader oku = kmm.ExecuteReader();
                if (oku.Read())
                {
                    kmlkno.Text = oku["VELITC"].ToString();
                    ad.Text = oku["VELIADI"].ToString();
                    soyad.Text = oku["VELISOYADI"].ToString();
                    tel.Text = oku["TELEFON"].ToString();
                    sifre.Text = oku["YAKINLIK"].ToString();
                    dateTimePicker1.Text = oku["VDOGUMTARIHI"].ToString();
                    dogyer.Text = oku["VDOGUMYERI"].ToString();
                }
            }// MÜDÜR VELİ GÜNCELLEME EKRANI
            if (b==4 && ifler == 0)
            {
                button2.Visible = false;
                pictureBox1.Visible = false;
                label16.Visible = false; maskedTextBox1.Visible = false;
                label10.Visible = true; ders.Visible = true;  label20.Text = "YAKINLIK";
                label10.Text = "ÖĞRENCİ NO";
                label18.Visible = false; radioButton1.Visible = false; radioButton2.Visible = false; label3.Visible = false;
            }// MÜDÜR VELİ EKLEME
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kmlkno2= kmlkno.Text.ToUpper();
            string ad2, soyad2, ders2, tel2, dogyer2, sifre2;
             ad2=ad.Text.ToUpper(); soyad2=soyad.Text.ToUpper(); ders2=ders.Text.ToUpper(); tel2=tel.Text.ToUpper(); dogyer2=dogyer.Text.ToUpper(); sifre2=sifre.Text.ToUpper();
            baglanti.Close();
            string cinsiyet="";
            if (b==1 && ifler == 0)//YENİ ÖĞRENCİ KAYDI
            {
                if (button1.Text=="DEVAM ET")
                {
                    if (resimadresi == "")
                    {
                        MessageBox.Show("Lütfen Bir Fotoğraf seçiniz");
                    }
                    else
                    {

                    
                    
                        if (kmlkno2 == "" || ad2 == "" || soyad2 == "" || ders2 == "" || sifre2 == "" || maskedTextBox1.Text == "" || dogyer2 == "")
                        {
                            MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUNUZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            TCGÜNC = kmlkno2;
                            yeniögr = kmlkno2;
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
                                baglanti.Open();
                                DateTime TARİH = dateTimePicker1.Value;
                                string tarih1 = TARİH.ToString("MM/dd/yyyy");
                                SqlCommand kom = new SqlCommand("insert into OGRENCI values('" + kmlkno2 + "','" + ad2 + "','" + soyad2 + "','" + ders2 + "','" + sifre2 + "','" + maskedTextBox1.Text + "','" + dogyer2 + "','" + tarih1 + "','" + cinsiyet + "','" + kmlkno2.Substring(0, 6) + "' , '0' , '0')", baglanti);
                                kom.ExecuteNonQuery();
                                baglanti.Close();
                                FileStream fs = new FileStream(resimadresi, FileMode.Open, FileAccess.Read);
                                BinaryReader br = new BinaryReader(fs);
                                byte[] foto = br.ReadBytes((int)fs.Length);
                                SqlCommand KQM = new SqlCommand("insert into RESIM VALUES(@tc , @res)", baglanti);
                                KQM.Parameters.Add("@tc", SqlDbType.NVarChar).Value = TCGÜNC;
                                KQM.Parameters.Add("@res", SqlDbType.Image, foto.Length).Value = foto;
                                baglanti.Open();
                                KQM.ExecuteNonQuery();
                                baglanti.Close();
                                

                                
                                button2.Visible = false;
                                DEGİS();
                                dersprogramı dersleriekleme = new dersprogramı();
                                dersleriekleme.ssınıf = ders2;
                                dersleriekleme.ssube = sifre2;
                                dersleriekleme.okulno = yeninumarası;
                                dersleriekleme.ddönem = dönem;
                                dersleriekleme.dersalma();
                                dersleriekleme.dersleriekle();
                            }
                            catch (Exception x )
                            {
                                MessageBox.Show("LÜTFEN DEĞERLERİNİZİ KONTROL EDİNİZ"+ x.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                baglanti.Close();
                                this.Close();
                            }
                            baglanti.Close();
                            ad.Text = "";
                            
                        }
                    }
                }
                if (button1.Text=="VELİYİ KAYDET")
                {
                    if (ad.Text=="" || kmlkno.Text=="" || ders.Text=="" || soyad.Text=="" || tel.Text=="" || sifre.Text=="" || dogyer.Text=="")
                    {
                        MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ");
                    }
                    else
                    {
                        try
                        {
                            DateTime TARİH = dateTimePicker1.Value;
                            string tarih1 = TARİH.ToString("MM/dd/yyyy");
                            baglanti.Open();
                            SqlCommand veli1 = new SqlCommand("insert into VELI values('" + kmlkno2 + "','" + ders2 + "','" + ad2 + "','" + soyad2 + "','" + tel2 + "','" + sifre2 + "','" + tarih1 + "','" + dogyer2 + "')", baglanti);
                            veli1.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("BİR VELİ KAYDEDİLDİ İSTERSENİZ BİR VELİ DAHA EKLEYEBİLİR YA DA KAPATABİLİRSİNİZ", "BAŞARILI", MessageBoxButtons.OK);
                        }
                        catch (Exception EX)
                        {

                            MessageBox.Show("LÜTFEN VELİ BİLGİLERİNİ GİRİNİZ", EX.Message.ToString(), MessageBoxButtons.OK);
                        }
                    }
                    ad.Text = ""; ders.Text= ""; soyad.Text = ""; tel.Text = ""; sifre.Text = ""; dogyer.Text= "";


                }
                
            }
            if (b==2 && ifler == 0)//öğrenci güncelleme
            {

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
                    baglanti.Open();
                    DateTime TARİH = dateTimePicker1.Value;
                    string tarih1 = TARİH.ToString("MM/dd/yyyy");
                    SqlCommand KOMUT = new SqlCommand("update OGRENCI set TC = '" + kmlkno2 + "' , ADI = '" + ad2 + "' , SOYADI = '" + soyad2 + "' , SINIFI = '" + ders2 + "' , SUBE = '" + sifre2 + "' , KAYITTARIHI = '" + maskedTextBox1.Text + "' , DOGUMYERI = '" + dogyer2 + "' , DOGUMTARIHI = '" + tarih1 + "' , CINSIYET = '" + cinsiyet + "' , PAROLA = '" + tel2 + "' WHERE NUMARASI = '" + ögno + "'", baglanti);
                    KOMUT.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("KAYIT GÜNCELLENDİ");
                    TCGÜN = kmlkno.Text;
                    if (eskitc!=TCGÜN)
                    {
                        SqlCommand mesaj = new SqlCommand("update mesajlar set ATC='" + TCGÜN + "' where ATC='" + eskitc + "'", baglanti);
                        baglanti.Open();
                        mesaj.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    if (eskisube!=sifre2 || eskisınıf!=ders2)
                    {
                        SqlCommand notlar = new SqlCommand("update NOTLAR set SINIF='" + ders2 + "', SUBE='"+ sifre2+ " WHERE NUMARASI = '" + ögno + "'", baglanti);
                        baglanti.Open();
                        notlar.ExecuteNonQuery();
                        baglanti.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("BİR HATA OLUŞTU");
                    
                }
                baglanti.Close();
                if (resimadresi=="")
                {

                }
                else
                {
                    FileStream fs = new FileStream(resimadresi, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] foto = br.ReadBytes((int)fs.Length);
                    SqlCommand KQM = new SqlCommand("UPDATE RESIM SET TCNO=@tcn , FOTO=@res where TCNO=@ög", baglanti);
                    KQM.Parameters.Add("@tcn", SqlDbType.NVarChar).Value = TCGÜN;
                    KQM.Parameters.Add("@res", SqlDbType.Image, foto.Length).Value = foto;
                    KQM.Parameters.Add("@ög", SqlDbType.NVarChar).Value = TCGÜNC;
                    try
                    {
                        baglanti.Open();
                        KQM.ExecuteNonQuery();
                        baglanti.Close();

                    }
                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message.ToString(), "FOTOĞRAF YÜKLENEMEDİ");

                    }
                }
                

                this.Close();
            }
            if (b==3 && ifler == 0)
            {
                
                baglanti.Close();
                DateTime TARİH = dateTimePicker1.Value;
                string tarih1 = TARİH.ToString("MM/dd/yyyy");
                try
                {
                    SqlCommand komut2 = new SqlCommand("UPDATE VELI SET VELITC= '" + kmlkno2 + "' , VELIADI= '" + ad2 + "' , VELISOYADI= '" + soyad2 + "' , TELEFON= '" + tel2 + "' , YAKINLIK= '" + sifre2 + "' , VDOGUMTARIHI= '" + tarih1 + "' , VDOGUMYERI= '" + dogyer2 + "'  WHERE ID= '" + veligünc + "'", baglanti);
                    baglanti.Open();
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("GÜNCELLEME BAŞARILI");
                }
                catch (Exception)
                {
                    MessageBox.Show("GÜNCELLEMEDE BİR HATA OLUŞTU");
                }
                
                
            }
            if (b==4 && ifler == 0)
            {
                baglanti.Close();
                if (kmlkno.Text == "" || ad.Text == "" || soyad.Text == "" || ders.Text == "" || sifre.Text == ""  || dogyer.Text == "" || tel.Text == "")
                {
                    MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUNUZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand NEW = new SqlCommand("SELECT * FROM OGRENCI WHERE NUMARASI='" + ders2+"'", baglanti);
                    SqlDataReader new2 = NEW.ExecuteReader();
                    
                    if (new2.Read())
                    {
                        DateTime TARİH = dateTimePicker1.Value;
                        string tarih1 = TARİH.ToString("MM/dd/yyyy");
                        try
                        {
                            baglanti.Close();
                            SqlCommand KMM = new SqlCommand("insert into VELI VALUES('" + ders2 + "' , '" + kmlkno2 + "' , '" + ad2 + "' , '" + soyad2 + "' , '" + tel2 + "' , '" + sifre2 + "' , '" + tarih1 + "' , '" + dogyer2 + "')", baglanti);
                            baglanti.Open();
                            KMM.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("YENİ VELİ EKLENDİ");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("YENİ VELİ EKLENEMEDİ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ÖĞRENCİ NUMARASINI HATALI GİRDİNİZ!");
                    }
                    baglanti.Close();
                    
                }
            }
            if (ifler==1)
            {
                try
                {
                    SqlCommand öggüncel = new SqlCommand("update OGRENCI set PAROLA='" + sifre.Text + "' WHERE TC='" + Form1.kimlikno + "'", baglanti);
                    baglanti.Close();
                    baglanti.Open();
                    öggüncel.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("ŞİFRENİZ GÜNCELLENDİ");
                }
                catch (Exception)
                {
                    MessageBox.Show("ŞİFRENİZ GÜNCELLENEMEDİ!");

                }
                

            }
            
        }

        private void DEGİS()
        {
            label8.Visible = true; tel.Visible = true;
            button1.Text = "VELİYİ KAYDET";
            label1.Text = "Öğrenci No";
            kmlkno.ReadOnly = true;
            ad.Text = ""; soyad.Text = ""; ders.Text = ""; dogyer.Text = ""; sifre.Text = "";
            label10.Text = "TC NO";
            label20.Text = "YAKINLIK";
            baglanti.Open();
            label18.Visible = false; label16.Visible = false; maskedTextBox1.Visible = false; radioButton1.Visible = false; radioButton2.Visible = false;
            SqlCommand kmm = new SqlCommand("select NUMARASI from OGRENCI where TC='"+yeniögr+"'",baglanti);
            SqlDataReader okuu = kmm.ExecuteReader();
            if (okuu.Read())
            {
                yeninumarası= okuu["NUMARASI"].ToString();
                kmlkno.Text = yeninumarası;
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                resimadresi = openFileDialog1.FileName.ToString();
            }
           
            

            
        }

        private void button3_Click_2(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
