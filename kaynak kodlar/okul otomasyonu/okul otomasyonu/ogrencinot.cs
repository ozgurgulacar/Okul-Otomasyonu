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
    public partial class ogrencinot : Form
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);


        public string ögrno,ad,soyad,sınıf,sube;


        Font ffont2 = new Font("Verdana", 12, FontStyle.Bold);
        Font ffont = new Font("Verdana", 12);
        SolidBrush kalem = new SolidBrush(Color.Black);


        string[] dersler = new string[30];


        public ogrencinot()
        {
            InitializeComponent();
        }

        private void ogrencinot_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglantı.Close();
            SqlCommand yeni = new SqlCommand("select * from NOTLAR where NUMARA='" + ögrno + "' and SINIF='" + comboBox1.Text + "' and DÖNEM='" + comboBox2.Text + "'", baglantı);
            baglantı.Open();
            SqlDataReader oku = yeni.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["DERS"].ToString();
                ekle.SubItems.Add(oku["SINAV1"].ToString());
                ekle.SubItems.Add(oku["SINAV2"].ToString());
                ekle.SubItems.Add(oku["SÖZLÜ"].ToString());
                ekle.SubItems.Add(oku["ORTALAMA"].ToString());
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
                    
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;

            e.Graphics.DrawString(" NUMARASI= " + ögrno, ffont2, kalem, 50, 100);
            e.Graphics.DrawString(" ADI= " + ad, ffont2, kalem, 50, 120);
            e.Graphics.DrawString(" SOYADI=" + soyad, ffont2, kalem, 400, 120);
            e.Graphics.DrawString(" SINIFI=" + sınıf, ffont2, kalem, 50, 140);
            e.Graphics.DrawString(" ŞUBESİ=" + sube, ffont2, kalem, 400, 140);
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

            dersalma(sınıf, sube);
            int i = 0;
            float dönemortalama = 0;
            for (; i < listView1.Items.Count; i++)
            {
                int derssaati = 0;
                e.Graphics.DrawString(listView1.Items[i].SubItems[0].Text, ffont, kalem, 50, 205 + (i * 30));
                e.Graphics.DrawString(listView1.Items[i].SubItems[1].Text, ffont, kalem, 250, 205 + (i * 30));
                e.Graphics.DrawString(listView1.Items[i].SubItems[2].Text, ffont, kalem, 400, 205 + (i * 30));
                e.Graphics.DrawString(listView1.Items[i].SubItems[3].Text, ffont, kalem, 550, 205 + (i * 30));
                e.Graphics.DrawString(listView1.Items[i].SubItems[4].Text, ffont, kalem, 650, 205 + (i * 30));
                
                for (int x = 0; x < 30; x++)
                {//dersin haftada kaç saat olduğunu bulma
                    if (listView1.Items[i].SubItems[0].Text == dersler[x])
                    {
                        derssaati++;
                    }

                }
                if (listView1.Items[i].SubItems[4].Text=="")
                {
                    listView1.Items[i].SubItems[4].Text = "0";
                }
                dönemortalama += derssaati * float.Parse(listView1.Items[i].SubItems[4].Text);

            }
            dönemortalama = dönemortalama / 30;

            e.Graphics.DrawString("DÖNEM SONU NOT ORTALAMASI=", ffont2, kalem, 50, 205 + (i+2)*30);
            e.Graphics.DrawString(dönemortalama.ToString("0.##"), ffont2, kalem, 400, 205 + (i + 2) * 30);

            SqlCommand devamsız = new SqlCommand("select * from OGRENCI where NUMARASI='"+ögrno+"'", baglantı);
            string devam = "";
            baglantı.Close();
            baglantı.Open();
            SqlDataReader okuu = devamsız.ExecuteReader();
            if (okuu.Read())
            {
                devam = okuu["TDEVAM"].ToString();
            }
            e.Graphics.DrawString("TOPLAM DEVAMSIZLIĞI:", ffont2, kalem, 50, 270 + (i + 3) * 30);
            e.Graphics.DrawString(devam, ffont2, kalem, 300, 270 + (i + 3) * 30);
            baglantı.Close();
        }

        void dersalma(string ssınıf, string ssube)
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

    }
}
