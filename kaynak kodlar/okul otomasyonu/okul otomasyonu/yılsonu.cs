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
    public partial class yılsonu : Form
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);

        string[] dersler = new string[30];


        int ilk = -1;
        Font ffont2 = new Font("Verdana", 12, FontStyle.Bold);
        Font ffont = new Font("Verdana", 12);
        SolidBrush kalem = new SolidBrush(Color.Black);

        string[] kalanlar;
        string[] sınıflari;
        string[] subeleri;
        string[] geçenler;

        public yılsonu()
        {
            InitializeComponent();
        }

        private void yılsonu_Load(object sender, EventArgs e)
        {
            SqlCommand doldur = new SqlCommand("select * from OGRENCI", baglantı);
            baglantı.Close();
            baglantı.Open();
            SqlDataReader oku = doldur.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem yeni = new ListViewItem();
                yeni.Text = oku["NUMARASI"].ToString();
                yeni.SubItems.Add(oku["ADI"].ToString());
                yeni.SubItems.Add(oku["SOYADI"].ToString());
                yeni.SubItems.Add(oku["SINIFI"].ToString());
                yeni.SubItems.Add(oku["SUBE"].ToString());
                listView1.Items.Add(yeni);
            }
            kalanlar = new string[listView1.Items.Count];
            geçenler = new string[listView1.Items.Count];
            sınıflari = new string[listView1.Items.Count];
            subeleri = new string[listView1.Items.Count];

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                kalanlar[i] = "boş";
                geçenler[i] = "boş";
                subeleri[i] = "boş";
                sınıflari[i] = "boş";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
            ilk = -1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            e.HasMorePages = false;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            for (; ilk < listView1.Items.Count;)
            {
                ilk++;
                if (ilk < listView1.Items.Count)
                {
                    e.Graphics.DrawString(" NUMARASI= " + listView1.Items[ilk].SubItems[0].Text, ffont2, kalem, 50, 100);
                    e.Graphics.DrawString(" ADI= " + listView1.Items[ilk].SubItems[1].Text, ffont2, kalem, 50, 120);
                    e.Graphics.DrawString(" SOYADI=" + listView1.Items[ilk].SubItems[2].Text, ffont2, kalem, 400, 120);
                    e.Graphics.DrawString(" SINIFI=" + listView1.Items[ilk].SubItems[3].Text, ffont2, kalem, 50, 140);
                    e.Graphics.DrawString(" ŞUBESİ=" + listView1.Items[ilk].SubItems[4].Text, ffont2, kalem, 400, 140);
                    e.Graphics.DrawString("--------------------------------------------------------------------------------------", ffont2, kalem, 50, 155);
                    e.Graphics.DrawString(" DERSLER", ffont2, kalem, 50, 170);
                    e.Graphics.DrawString(" 1.SINAV", ffont2, kalem, 250, 170);
                    e.Graphics.DrawString(" 2.SINAV", ffont2, kalem, 400, 170);
                    e.Graphics.DrawString("SÖZLÜ", ffont2, kalem, 550, 170);
                    e.Graphics.DrawString("ORTALAMA", ffont2, kalem, 650, 170);
                    e.Graphics.DrawString("----------------------------------------------------------------------------------------", ffont2, kalem, 50, 185);
                    e.Graphics.DrawString("----------------------------------------------------------------------------------------", ffont2, kalem, 50, 186);
                    e.Graphics.DrawString("----------------------------------------------------------------------------------------", ffont2, kalem, 50, 187);
                    e.Graphics.DrawString("----------------------------------------------------------------------------------------", ffont2, kalem, 50, 188);
                    e.Graphics.DrawString("----------------------------------------------------------------------------------------", ffont2, kalem, 50, 189);
                    string donem1ort = "";
                    baglantı.Close();
                    SqlCommand not = new SqlCommand("SELECT NOTLAR.DERS,NOTLAR.SINAV1,NOTLAR.SINAV2,NOTLAR.SÖZLÜ,NOTLAR.ORTALAMA,OGRENCI.TDEVAM,OGRENCI.DONEMORT from NOTLAR inner join OGRENCI ON OGRENCI.NUMARASI=NOTLAR.NUMARA WHERE OGRENCI.NUMARASI='" + listView1.Items[ilk].SubItems[0].Text + "' and NOTLAR.DURUM='AKTİF'", baglantı);
                    baglantı.Open();
                    SqlDataReader oku = not.ExecuteReader();
                    int A = 0;
                    int b = 0;
                    string devam = "0";
                    string[] ders = new string[20];
                    float[] ortalamar = new float[20];
                    while (oku.Read())
                    {
                        A += 50;
                        b++;
                        e.Graphics.DrawString(oku["DERS"].ToString(), ffont, kalem, 50, 170 + A);
                        ders[b] = (oku["DERS"].ToString());
                        e.Graphics.DrawString(oku["SINAV1"].ToString(), ffont, kalem, 250, 170 + A);
                        e.Graphics.DrawString(oku["SINAV2"].ToString(), ffont, kalem, 400, 170 + A);
                        e.Graphics.DrawString(oku["SÖZLÜ"].ToString(), ffont, kalem, 550, 170 + A);
                        e.Graphics.DrawString(oku["ORTALAMA"].ToString(), ffont, kalem, 650, 170 + A);
                        string degisken = (oku["ORTALAMA"].ToString());
                        if (degisken != "")
                        {
                            ortalamar[b] = float.Parse(degisken);
                        }

                        e.Graphics.DrawString("--------------------------------------------------------------------------------------", ffont2, kalem, 50, 170 + A + 10);
                        devam = oku["TDEVAM"].ToString();
                        donem1ort= oku["DONEMORT"].ToString();
                    }
                    baglantı.Close();

                    dersalma(listView1.Items[ilk].SubItems[3].Text, listView1.Items[ilk].SubItems[4].Text);

                    float[] derssaatleri = new float[20];

                    for (int i = 0; i < 20; i++)
                    {
                        int toplamda = 0;

                        for (int q = 0; q < 30; q++)
                        {

                            if (ders[i] == dersler[q])
                            {
                                toplamda++;
                            }

                            derssaatleri[i] = toplamda;

                        }
                    }
                    float çarp = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        if (derssaatleri[i] != 0)
                        {
                            çarp = (derssaatleri[i] * ortalamar[i]) + çarp;

                        }
                    }
                    float sonuc = çarp / 30;


                    e.Graphics.DrawString("DÖNEM SONU NOT ORTALAMASI=", ffont2, kalem, 50, 220 + A);
                    e.Graphics.DrawString(sonuc.ToString("0.##"), ffont2, kalem, 400, 220 + A);
                    e.Graphics.DrawString("1.DÖNEM SONU NOT ORTALAMASI=", ffont2, kalem, 50, 270 + A);
                    e.Graphics.DrawString(donem1ort, ffont2, kalem, 400, 270 + A);
                    e.Graphics.DrawString("TOPLAM DEVAMSIZLIĞI:", ffont2, kalem, 50, 320 + A);
                    e.Graphics.DrawString(devam, ffont2, kalem, 300, 320 + A);
                    if (donem1ort=="")
                    {
                        donem1ort = "2";
                    }
                    float oort = (float.Parse(donem1ort) + sonuc) / 2;

                    if (float.Parse(devam)>20 || oort<50)//öğrenci kaldıysa
                    {
                        kalanlar[ilk] = listView1.Items[ilk].SubItems[0].Text;
                        sınıflari[ilk]= listView1.Items[ilk].SubItems[3].Text;
                        subeleri[ilk]= listView1.Items[ilk].SubItems[4].Text;
                        e.Graphics.DrawString("ÖĞRENCİ SINIF TEKRARINA KALMIŞTIR.", ffont2, kalem, 50, 370 + A);

                    }
                    else // öğrenci geçtiyse
                    {
                        geçenler[ilk] = listView1.Items[ilk].SubItems[0].Text;
                        e.Graphics.DrawString("ÖĞRENCİ SINIFI BAŞARI İLE GEÇİMŞTİR.", ffont2, kalem, 50, 370 + A);
                        
                    }
                    

                    if (ilk < listView1.Items.Count - 1)
                    {
                        e.HasMorePages = true;
                        break;
                    }
                }


            }
        }


        public void dersalma(string ssınıf, string ssube)
        {
            baglantı.Close();
            SqlCommand komut = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PAZARTESİ'", baglantı);
            baglantı.Open();
            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                dersler[0] = oku["DERS1"].ToString();
                dersler[1] = oku["DERS2"].ToString();
                dersler[2] = oku["DERS3"].ToString();
                dersler[3] = oku["DERS4"].ToString();
                dersler[4] = oku["DERS5"].ToString();
                dersler[5] = oku["DERS6"].ToString();
            }
            baglantı.Close();


            SqlCommand komut2 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='SALI'", baglantı);
            baglantı.Open();
            oku = komut2.ExecuteReader();

            if (oku.Read())
            {
                dersler[6] = oku["DERS1"].ToString();
                dersler[7] = oku["DERS2"].ToString();
                dersler[8] = oku["DERS3"].ToString();
                dersler[9] = oku["DERS4"].ToString();
                dersler[10] = oku["DERS5"].ToString();
                dersler[11] = oku["DERS6"].ToString();
            }
            baglantı.Close();

            SqlCommand komut3 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='ÇARŞAMBA'", baglantı);
            baglantı.Open();
            oku = komut3.ExecuteReader();

            if (oku.Read())
            {
                dersler[12] = oku["DERS1"].ToString();
                dersler[13] = oku["DERS2"].ToString();
                dersler[14] = oku["DERS3"].ToString();
                dersler[15] = oku["DERS4"].ToString();
                dersler[16] = oku["DERS5"].ToString();
                dersler[17] = oku["DERS6"].ToString();
            }
            baglantı.Close();

            SqlCommand komut4 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PERŞEMBE'", baglantı);
            baglantı.Open();
            oku = komut4.ExecuteReader();

            if (oku.Read())
            {
                dersler[18] = oku["DERS1"].ToString();
                dersler[19] = oku["DERS2"].ToString();
                dersler[20] = oku["DERS3"].ToString();
                dersler[21] = oku["DERS4"].ToString();
                dersler[22] = oku["DERS5"].ToString();
                dersler[23] = oku["DERS6"].ToString();
            }
            baglantı.Close();

            SqlCommand komut5 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='CUMA'", baglantı);
            baglantı.Open();
            oku = komut5.ExecuteReader();

            if (oku.Read())
            {
                dersler[24] = oku["DERS1"].ToString();
                dersler[25] = oku["DERS2"].ToString();
                dersler[26] = oku["DERS3"].ToString();
                dersler[27] = oku["DERS4"].ToString();
                dersler[28] = oku["DERS5"].ToString();
                dersler[29] = oku["DERS6"].ToString();
            }
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ds = new DialogResult();
            ds = MessageBox.Show("SENEYİ BİTİRMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ!", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ds == DialogResult.Yes)
            {

                for (int i = 0; i < listView1.Items.Count; i++)
                {

                    if (kalanlar[i] != "boş")
                    {
                        SqlCommand kalma = new SqlCommand("delete from NOTLAR where NUMARA='" + kalanlar[i] + "' AND SINIF='"+sınıflari[i]+"' AND SUBE='"+subeleri[i]+"' delete from öğrencidevam where ÖGRNO='" + kalanlar[i] + "'", baglantı);
                        baglantı.Close();
                        baglantı.Open();
                        kalma.ExecuteNonQuery();
                        baglantı.Close();
                        SqlCommand guncel = new SqlCommand("update OGRENCI set TDEVAM='0',DONEMORT='0' where NUMARASI='" + kalanlar[i] + "'", baglantı);
                        baglantı.Open();
                        guncel.ExecuteNonQuery();
                        baglantı.Close();
                        SqlCommand guncel2 = new SqlCommand("update OGRETMEN set DEVAMT='0'", baglantı);
                        baglantı.Open();
                        guncel2.ExecuteNonQuery();
                        baglantı.Close();
                    }
                    else if (geçenler[i] != "boş")
                    {
                        SqlCommand kalma = new SqlCommand("delete from öğrencidevam where ÖGRNO='" + geçenler[i] + "'", baglantı);
                        baglantı.Close();
                        baglantı.Open();
                        kalma.ExecuteNonQuery();
                        baglantı.Close();
                        SqlCommand guncel = new SqlCommand("update OGRENCI set SINIFI=SINIFI+1,TDEVAM='0',DONEMORT='0' where NUMARASI='" + geçenler[i] + "'", baglantı);
                        baglantı.Open();
                        guncel.ExecuteNonQuery();
                        baglantı.Close();
                    }
                }

                SqlCommand sil = new SqlCommand("delete from DERSPROGRAMI delete from öğrencidevam delete from HOCADEVAM delete from ogretmensınıf", baglantı);
                baglantı.Close();
                baglantı.Open();
                sil.ExecuteNonQuery();
                baglantı.Close();
                SqlCommand güncel = new SqlCommand("update NOTLAR set DURUM='PASİF'", baglantı);
                baglantı.Open();
                güncel.ExecuteNonQuery();
                baglantı.Close();
                MessageBox.Show("SENE BAŞARI İLE BİTİRİLDİ!");
            }
                

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mudur AC = new mudur();
            AC.Show();
            this.Close();
        }
    }
}
