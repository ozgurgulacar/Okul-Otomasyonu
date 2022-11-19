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
    public partial class dersprogramı : Form
    {
        public string ssube,ssınıf;
        public int durum = 0;
        string[] dersler = new string[30];
        string[] eklenecekler = new string[20];
        string[] numaraları;
        public string okulno;
        bool varmı;
        public bool ogrencimi=false;
        int Q = 0,SAYAÇ,SAYAÇ2=0;
        string[] öğretmenseçimi = new string[20];
        string[] öğretmentc;
        public string ddönem;

        public dersprogramı()
        {
            InitializeComponent();
        }


        static string yol = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglan = new SqlConnection(yol);


        private void dersprogramı_Load(object sender, EventArgs e)
        {
            baglan.Close();
            SqlCommand komut= new SqlCommand("select * from BRANS", baglan);
            baglan.Open();
            SqlDataReader DOLDUR2 = komut.ExecuteReader();
            while (DOLDUR2.Read())
            {
                string ders1 = DOLDUR2["BRANSADI"].ToString();
                comboBox2.Items.Add(ders1);
                comboBox3.Items.Add(ders1);
                comboBox4.Items.Add(ders1);
                comboBox5.Items.Add(ders1);
                comboBox6.Items.Add(ders1);
                comboBox7.Items.Add(ders1);
                comboBox8.Items.Add(ders1);
                comboBox9.Items.Add(ders1);
                comboBox10.Items.Add(ders1);
                comboBox11.Items.Add(ders1);
                comboBox12.Items.Add(ders1);
                comboBox13.Items.Add(ders1);
                comboBox14.Items.Add(ders1);
                comboBox15.Items.Add(ders1);
                comboBox16.Items.Add(ders1);
                comboBox17.Items.Add(ders1);
                comboBox18.Items.Add(ders1);
                comboBox19.Items.Add(ders1);
                comboBox20.Items.Add(ders1);
                comboBox21.Items.Add(ders1);
                comboBox22.Items.Add(ders1);
                comboBox23.Items.Add(ders1);
                comboBox24.Items.Add(ders1);
                comboBox25.Items.Add(ders1);
                comboBox26.Items.Add(ders1);
                comboBox27.Items.Add(ders1);
                comboBox28.Items.Add(ders1);
                comboBox29.Items.Add(ders1);
                comboBox30.Items.Add(ders1);
                comboBox31.Items.Add(ders1);
            }
            baglan.Close();


            if (ogrencimi==true)
            {
                ogrencilist();
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                comboBox33.Visible = false;
                label16.Visible = false;
                textBox1.Text=ssube;
                comboBox1.Text=ssınıf;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="" || textBox1.Text=="" || comboBox33.Text == "")
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ");
            }
            else
            {
                ogrencimi = false;
                ssube = textBox1.Text;
                ssınıf = comboBox1.Text;
                ogrencilist();
                if (varmı == true)
                {
                    button3.Visible = true;
                    button2.Visible = false;
                }
                else
                {
                    button2.Visible = true;
                    button3.Visible = false;
                }

                button4.Visible = true;

                button1.Visible = false;
                baglan.Close();

                Q = 0;
                for (int i = 0; i < 20; i++)
                {
                    eklenecekler[i] = null;
                    öğretmenseçimi[i] = null;
                }
                for (int i = 0; i < 30; i++)
                {
                    dersler[i] = null;
                }
                durum = 1;
            }
            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            SAYAÇ = 0;
            SAYAÇ2 = 0;
            button4.Visible = false;
            button3.Visible = false;
            baglan.Close();
            string pzts = " values('" + ssınıf + "','" + ssube + "','PAZARTESİ','" + comboBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "','" + comboBox6.Text + "','" + comboBox7.Text + "')";
            string salı = " values('" + ssınıf + "','" + ssube + "','SALI','" + comboBox13.Text + "','" + comboBox12.Text + "','" + comboBox11.Text + "','" + comboBox10.Text + "','" + comboBox9.Text + "','" + comboBox8.Text + "')";
            string crsmb = " values('" + ssınıf + "','" + ssube + "','ÇARŞAMBA','" + comboBox25.Text + "','" + comboBox24.Text + "','" + comboBox23.Text + "','" + comboBox22.Text + "','" + comboBox21.Text + "','" + comboBox20.Text + "')";
            string prsmb = " values('" + ssınıf + "','" + ssube + "','PERŞEMBE','" + comboBox19.Text + "','" + comboBox18.Text + "','" + comboBox17.Text + "','" + comboBox16.Text + "','" + comboBox15.Text + "','" + comboBox14.Text + "')";
            string cuma = " values('" + ssınıf + "','" + ssube + "','CUMA','" + comboBox31.Text + "','" + comboBox30.Text + "','" + comboBox29.Text + "','" + comboBox28.Text + "','" + comboBox27.Text + "','" + comboBox26.Text + "')";
            SqlCommand yaz = new SqlCommand("insert into DERSPROGRAMI" + pzts + " insert into DERSPROGRAMI" + salı + " insert into DERSPROGRAMI" + crsmb + " insert into DERSPROGRAMI" + prsmb + " insert into DERSPROGRAMI" + cuma, baglan);
            baglan.Open();
            yaz.ExecuteNonQuery();
            baglan.Close();


            for (int i = 0; i < 20; i++)
            {
                eklenecekler[i] = null;
            }
            dersalma();
            dersleriekle();
            

            for (int i = 0; i < 20; i++)
            {
                öğretmenseçimi[i] = "";
            }


            
            int c = 0;
            for (int i = 0; i < 30; i++)
            {
                int uygunmu = 0;
                for (int x = 0; x < 20; x++)
                {
                    if (dersler[i]==öğretmenseçimi[x])
                    {
                        uygunmu = 1;
                    }

                }
                if (uygunmu==0)
                {
                    öğretmenseçimi[c] = dersler[i];
                        c++;
                }
            }
            SAYAÇ = c;


            label9.Text = öğretmenseçimi[0];
            ÖĞRETMENATAMA();


            button6.Visible = true;
            label9.Visible = true;
            label15.Visible = true;
            comboBox32.Visible = true;
            
            button2.Visible = false;


        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            SAYAÇ = 0;
            SAYAÇ2 = 0;
            Q = 0;
            button4.Visible = false;
            string pzts = " update DERSPROGRAMI set DERS1 ='"+comboBox2.Text+"',DERS2 = '"+comboBox3.Text+"',DERS3 = '"+comboBox4.Text+"',DERS4 = '"+comboBox5.Text+"',DERS5 = '"+comboBox6.Text+"',DERS6 = '"+comboBox7.Text+"' WHERE SINIF = '"+ssınıf+"' AND SUBE = '"+ssube+"' AND GUN = 'PAZARTESİ'";
            string salı = " update DERSPROGRAMI set DERS1 ='" + comboBox13.Text + "',DERS2 = '" + comboBox12.Text + "',DERS3 = '" + comboBox11.Text + "',DERS4 = '" + comboBox10.Text + "',DERS5 = '" + comboBox9.Text + "',DERS6 = '" + comboBox8.Text + "' WHERE SINIF = '" + ssınıf + "' AND SUBE = '" + ssube + "' AND GUN = 'SALI'";
            string crsmb = " update DERSPROGRAMI set DERS1 ='" + comboBox25.Text + "',DERS2 = '" + comboBox24.Text + "',DERS3 = '" + comboBox23.Text + "',DERS4 = '" + comboBox22.Text + "',DERS5 = '" + comboBox21.Text + "',DERS6 = '" + comboBox20.Text + "' WHERE SINIF = '" + ssınıf + "' AND SUBE = '" + ssube + "' AND GUN = 'ÇARŞAMBA'";
            string prsmb = " update DERSPROGRAMI set DERS1 ='" + comboBox19.Text + "',DERS2 = '" + comboBox18.Text + "',DERS3 = '" + comboBox17.Text + "',DERS4 = '" + comboBox16.Text + "',DERS5 = '" + comboBox15.Text + "',DERS6 = '" + comboBox14.Text + "' WHERE SINIF = '" + ssınıf + "' AND SUBE = '" + ssube + "' AND GUN = 'PERŞEMBE'";
            string cuma = " update DERSPROGRAMI set DERS1 ='" + comboBox31.Text + "',DERS2 = '" + comboBox30.Text + "',DERS3 = '" + comboBox29.Text + "',DERS4 = '" + comboBox28.Text + "',DERS5 = '" + comboBox27.Text + "',DERS6 = '" + comboBox26.Text + "' WHERE SINIF = '" + ssınıf + "' AND SUBE = '" + ssube + "' AND GUN = 'CUMA'";
            SqlCommand gunc = new SqlCommand(pzts + salı + crsmb + prsmb + cuma, baglan);
            baglan.Close();
            baglan.Open();
            gunc.ExecuteNonQuery();
            baglan.Close();
            SqlCommand sil2 = new SqlCommand("delete from ogretmensınıf where sınıf='" + ssınıf + "' and sube='" + ssube + "'", baglan);
            baglan.Open();
            sil2.ExecuteNonQuery();
            baglan.Close();



            SqlCommand komut = new SqlCommand("select distinct(DERS)  FROM NOTLAR WHERE SINIF='" + ssınıf + "' and SUBE='" + ssube + "'", baglan);
            baglan.Open();
            
            SqlDataReader OKU = komut.ExecuteReader();
            while (OKU.Read())
            {//NOTLAR TABLOSUNDA HANGİ DERSLER MEVCUT
                eklenecekler[Q] = OKU["DERS"].ToString();
                Q++;
            }

            baglan.Close();

            dersalma();
            
            dersleriekle();
            int y = 0;
            for (int i = 0; i < 20; i++)
            {
                int z = 0;
                
                for (y = 0; y < 30; y++)
                {

                    if (eklenecekler[i] ==dersler[y])
                    {
                        z = 1;
                    }
                
                }
                if (z==0)
                {
                    baglan.Close();
                    SqlCommand sil = new SqlCommand("delete from NOTLAR where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and DERS='"+eklenecekler[i]+"' and DÖNEM='"+comboBox33.Text+"'", baglan);
                    baglan.Open();
                    sil.ExecuteNonQuery();
                    baglan.Close();
                }
            }




            for (int i = 0; i < 20; i++)
            {
                öğretmenseçimi[i] = "";
            }



            int c = 0;
            for (int i = 0; i < 30; i++)
            {
                int uygunmu = 0;
                for (int x = 0; x < 20; x++)
                {
                    if (dersler[i] == öğretmenseçimi[x])
                    {
                        uygunmu = 1;
                    }

                }
                if (uygunmu == 0)
                {
                    öğretmenseçimi[c] = dersler[i];
                    c++;
                }
            }
            SAYAÇ = c;


            label9.Text = öğretmenseçimi[0];
            ÖĞRETMENATAMA();


            button6.Visible = true;
            label9.Visible = true;
            label15.Visible = true;
            comboBox32.Visible = true;

            button2.Visible = false;

            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;



        }
        public void dersalma()
        {
            baglan.Close();
            SqlCommand komut = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PAZARTESİ'", baglan);
            baglan.Open();
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
            baglan.Close();


            SqlCommand komut2 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='SALI'", baglan);
            baglan.Open();
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
            baglan.Close();

            SqlCommand komut3 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='ÇARŞAMBA'", baglan);
            baglan.Open();
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
            baglan.Close();

            SqlCommand komut4 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PERŞEMBE'", baglan);
            baglan.Open();
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
            baglan.Close();

            SqlCommand komut5 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='CUMA'", baglan);
            baglan.Open();
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
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ogrencimi==true)
            {
                öğrenci git = new öğrenci();
                
                this.Close();
            }
            else
            {
                mudur AC = new mudur();
                AC.Show();
                this.Close();
            }
            
        }

        public void dersleriekle()
        {

            
            int c = 0;
            if (button3.Visible==true)
            {
                c = Q;//notlar bölümünden çektiğimiz ders sayısı
            }
            for (int i = 0; i < 30; i++)
            {
                int b = 0;

                for (int a = 0; a < 20; a++)
                {
                    if (dersler[i] == eklenecekler[a])
                    {
                        b = 1;
                    }
                }
                if (b == 0)
                {
                    eklenecekler[c] = dersler[i];
                    c++;
                }
            }

            int A = 0;

            SqlCommand no = new SqlCommand(" select COUNT(*) as sayi from OGRENCI where SINIFI='" + ssınıf + "' and SUBE='" + ssube + "'", baglan);
            baglan.Open();
            SqlDataReader oku = no.ExecuteReader();
            while (oku.Read())
            {
                A = Convert.ToInt32(oku["sayi"]);
            }
            baglan.Close();

            numaraları = new string[A];

            SqlCommand kkomut = new SqlCommand("select NUMARASI from OGRENCI where SINIFI='" + ssınıf + "' and SUBE='" + ssube + "'", baglan);
            baglan.Open();
            oku = kkomut.ExecuteReader();
            int index = 0;
            while (oku.Read())
            {
                numaraları[index] = oku["NUMARASI"].ToString();
                index++;
            }
            baglan.Close();

            if (button2.Visible==true )
            {
                string dönemi = "";
                if (comboBox33.Text!="")
                {
                    dönemi = comboBox33.Text;
                }
                for (int i = 0; i < A; i++)     
                {
                    for (int x = 0; x < 20; x++)
                    {
                        if (eklenecekler[x] != null )
                        {
                            SqlCommand yazdır = new SqlCommand("insert into NOTLAR (NUMARA,SINIF,SUBE,DERS,DÖNEM,DURUM) values('" + numaraları[i] + "','" + ssınıf + "','" + ssube + "','" + eklenecekler[x] + "','"+dönemi+"','AKTİF')", baglan);
                            baglan.Open();
                            yazdır.ExecuteNonQuery();
                            baglan.Close();
                        }

                    }

                }
            }
            
            if (button3.Visible == true )
            {
                for (int i = 0; i < A; i++)
                {
                    for (int x = 0; x < 20-Q; x++)
                    {
                        
                        if (eklenecekler[x+Q] != null)
                        {
                            SqlCommand yazdır = new SqlCommand("insert into NOTLAR (NUMARA,SINIF,SUBE,DERS,DÖNEM,DURUM) values('" + numaraları[i] + "','" + ssınıf + "','" + ssube + "','" + eklenecekler[x+Q] + "','" + comboBox33.Text + "','AKTİF')", baglan);
                            baglan.Open();
                            yazdır.ExecuteNonQuery();
                            baglan.Close();
                        }

                    }

                }
                
            }

            if (durum == 0)
            {
                
                for (int x = 0; x < 20; x++)
                {
                    if (eklenecekler[x] != null)
                    {
                        SqlCommand yazdır = new SqlCommand("insert into NOTLAR (NUMARA,SINIF,SUBE,DERS,DÖNEM,DURUM) values('" + okulno + "','" + ssınıf + "','" + ssube + "','" + eklenecekler[x] + "','" + comboBox33.Text + "','AKTİF')", baglan);
                        baglan.Open();
                        yazdır.ExecuteNonQuery();
                        baglan.Close();
                    }

                }
            }

            baglan.Close();
        }

        private void comboBox32_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        
                if (comboBox32.Text!="")
                {
                    baglan.Close();
                    SqlCommand yaz = new SqlCommand("insert into ogretmensınıf values('" + öğretmentc[comboBox32.SelectedIndex] + "','" + ssınıf + "','" + ssube + "')", baglan);
                    baglan.Open();
                    yaz.ExecuteNonQuery();
                    baglan.Close();
                    label9.Text = öğretmenseçimi[SAYAÇ2+1];
                    ÖĞRETMENATAMA();
                    
                }
                else if (comboBox32.Text == "")
                {
                    MessageBox.Show("BİR ÖĞRETMEN SEÇMEDİNİZ!  ");
                    
                }
            if (SAYAÇ2==SAYAÇ-1)
            {
                button6.Visible = false;
                label9.Visible = false;
                label15.Visible = false;
                comboBox32.Visible = false;
                button1.Visible = true;
            }
            SAYAÇ2++;


        }

        public void ogrencilist()
        {
            baglan.Close();
            SqlCommand komut = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PAZARTESİ'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                comboBox2.SelectedItem = oku["DERS1"].ToString();
                comboBox3.SelectedItem = oku["DERS2"].ToString();
                comboBox4.SelectedItem = oku["DERS3"].ToString();
                comboBox5.SelectedItem = oku["DERS4"].ToString();
                comboBox6.SelectedItem = oku["DERS5"].ToString();
                comboBox7.SelectedItem = oku["DERS6"].ToString();

                varmı = true;
            }
            else
            {
                varmı = false;
            }
            baglan.Close();
            SqlCommand komut2 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='SALI'", baglan);
            baglan.Open();
            oku = komut2.ExecuteReader();

            if (oku.Read())
            {
                comboBox13.SelectedItem = oku["DERS1"].ToString();
                comboBox12.SelectedItem = oku["DERS2"].ToString();
                comboBox11.SelectedItem = oku["DERS3"].ToString();
                comboBox10.SelectedItem = oku["DERS4"].ToString();
                comboBox9.SelectedItem = oku["DERS5"].ToString();
                comboBox8.SelectedItem = oku["DERS6"].ToString();
            }
            baglan.Close();



            SqlCommand komut3 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='ÇARŞAMBA'", baglan);
            baglan.Open();
            oku = komut3.ExecuteReader();
            if (oku.Read())
            {
                comboBox25.SelectedItem = oku["DERS1"].ToString();
                comboBox24.SelectedItem = oku["DERS2"].ToString();
                comboBox23.SelectedItem = oku["DERS3"].ToString();
                comboBox22.SelectedItem = oku["DERS4"].ToString();
                comboBox21.SelectedItem = oku["DERS5"].ToString();
                comboBox20.SelectedItem = oku["DERS6"].ToString();
            }
            baglan.Close();



            SqlCommand komut4 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='PERŞEMBE'", baglan);
            baglan.Open();
            oku = komut4.ExecuteReader();
            if (oku.Read())
            {
                comboBox19.SelectedItem = oku["DERS1"].ToString();
                comboBox18.SelectedItem = oku["DERS2"].ToString();
                comboBox17.SelectedItem = oku["DERS3"].ToString();
                comboBox16.SelectedItem = oku["DERS4"].ToString();
                comboBox15.SelectedItem = oku["DERS5"].ToString();
                comboBox14.SelectedItem = oku["DERS6"].ToString();
            }
            baglan.Close();



            SqlCommand komut5 = new SqlCommand("select * from DERSPROGRAMI where SINIF='" + ssınıf + "' and SUBE='" + ssube + "' and GUN='CUMA'", baglan);
            baglan.Open();
            oku = komut5.ExecuteReader();
            if (oku.Read())
            {
                comboBox31.SelectedItem = oku["DERS1"].ToString();
                comboBox30.SelectedItem = oku["DERS2"].ToString();
                comboBox29.SelectedItem = oku["DERS3"].ToString();
                comboBox28.SelectedItem = oku["DERS4"].ToString();
                comboBox27.SelectedItem = oku["DERS5"].ToString();
                comboBox26.SelectedItem = oku["DERS6"].ToString();
            }
            baglan.Close();
        }
        private void ÖĞRETMENATAMA()
        {
            baglan.Close();
            SqlCommand kac = new SqlCommand("select COUNT(*) as sayı from OGRETMEN inner join BRANS ON BRANS.ID=OGRETMEN.BRANS WHERE BRANSADI='" + label9.Text + "'", baglan);
            baglan.Open();
            SqlDataReader OKKU = kac.ExecuteReader();
            if (OKKU.Read())
            {
                öğretmentc = new string[Convert.ToInt32(OKKU["sayı"].ToString())];
            }

            baglan.Close();
            SqlCommand komut = new SqlCommand("select  OGRETMEN.ADI,OGRETMEN.SOYADI,OGRETMEN.TC from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID where BRANSADI='" + label9.Text + "'", baglan);
            baglan.Open();
            SqlDataReader OKU = komut.ExecuteReader();
            comboBox32.Items.Clear();
            int say = 0;
            while (OKU.Read())
            {
                öğretmentc[say] = OKU["TC"].ToString();
                comboBox32.Items.Add(OKU["ADI"].ToString() + " " + OKU["SOYADI"].ToString());
                say++;
            }
            baglan.Close();
        }
    }
}
