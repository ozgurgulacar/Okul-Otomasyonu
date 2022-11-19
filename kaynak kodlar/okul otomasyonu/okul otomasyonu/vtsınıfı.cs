using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace okul_otomasyonu
{
    class vtsınıfı
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);
        SqlCommand komut;


        public void giris(string kim, string no, string sifre, Form frm1)
        {
            
            try
            {
                SqlCommand KOM = new SqlCommand("DELETE FROM MESAJLAR WHERE STARİH<GETDATE()", baglantı);
                baglantı.Close();
                baglantı.Open();
                KOM.ExecuteNonQuery();
                baglantı.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString(),"BİR HATA OLUŞTU"); 
            }
            if (kim=="ÖĞRENCİ")
            {
                
                baglantı.Open();
                komut = new SqlCommand("select * from OGRENCI where NUMARASI='"+ Convert.ToInt32(no) + "' and PAROLA='"+sifre+"'",baglantı);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    öğrenci.numara = no;
                    Form1.kimlikno=oku["TC"].ToString();
                    öğrenci frm = new öğrenci();
                    frm1.Hide();
                    frm.Show();
                    
                
                }
                else
                {
                    MessageBox.Show("HATALI GİRİŞ YAPTINIZ. LÜTFEN TEKRAR DENEYİNİZ.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantı.Close();
            }
            if (kim== "MÜDÜR")
            {
                no.ToString();
                baglantı.Open();
                komut = new SqlCommand("select * from OGRETMEN where TC='" + no + "' and PAROLA='" + sifre + "'",baglantı);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    
                    Form1.kimlikno = oku["TC"].ToString();
                    string MUDURMU = oku["BRANS"].ToString();
                    if (MUDURMU=="1")
                    {
                        mudur frm = new mudur();
                        frm.Show();
                        frm1.Hide();
                    }
                    else
                    {
                        MessageBox.Show("HATALI GİRİŞ YAPTINIZ");
                    }
                    
                }
                else
                {
                    MessageBox.Show("HATALI GİRİŞ YAPTINIZ. LÜTFEN TEKRAR DENEYİNİZ.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantı.Close();
            }
            if (kim == "ÖĞRETMEN")
            {
                no.ToString();
                baglantı.Open();
                komut = new SqlCommand("select * from OGRETMEN where TC='" + no + "' and PAROLA='" + sifre + "'", baglantı);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    Form1.kimlikno = oku["TC"].ToString();
                    string MUDURMU = oku["BRANS"].ToString();
                    if (MUDURMU != "1")
                    {
                        öğretmen frm = new öğretmen();
                        frm.Show();
                        frm1.Hide();
                    }
                    else
                    {
                        MessageBox.Show("HATALI GİRİŞ YAPTINIZ");
                    }
                    
                    

                }
                else
                {
                    MessageBox.Show("HATALI GİRİŞ YAPTINIZ. LÜTFEN TEKRAR DENEYİNİZ.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                baglantı.Close();
            }
        }

    }
}
