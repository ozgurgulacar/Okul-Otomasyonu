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
    public partial class ogrislem : Form
    {
        public static string ögr="",ögrno,vgünc="";
        public static int yeni=0;
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);
        string sınavadı,dersadı,okulno="",ad2,soyad2,sınıf2,sube2;
        
        public ogrislem()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            ögrlistgünc();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            
            baglantı.Close();
            ögr = listView1.SelectedItems[0].Text;
            okulno = listView1.SelectedItems[0].SubItems[1].Text;
            ad2 = listView1.SelectedItems[0].SubItems[2].Text;
            soyad2 = listView1.SelectedItems[0].SubItems[3].Text;
            sınıf2= listView1.SelectedItems[0].SubItems[4].Text;
            sube2 = listView1.SelectedItems[0].SubItems[5].Text;
            MessageBox.Show("SEÇİLENİN ADI " + ad2 + " " + soyad2);
            SqlCommand kma = new SqlCommand("select * from RESIM WHERE TCNO='" + ögr + "'", baglantı);
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

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox7.Visible = true;
            button16.Visible = true;
            label21.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (silinecek.Visible==false)
            {
                silinecek.Visible = true;
                label11.Visible = true;
            }
            else
            {
                try
                {
                    baglantı.Open();
                    SqlCommand komt = new SqlCommand("select * from OGRENCI where NUMARASI='" + silinecek.Text + "'", baglantı);
                    SqlDataReader who = komt.ExecuteReader();
                    string[] bilgiler = new string[5];
                    
                    if (who.Read())
                    {
                        bilgiler[0] = who["TC"].ToString();
                        bilgiler[1] = who["ADI"].ToString();
                        bilgiler[2] = who["SOYADI"].ToString();
                        bilgiler[3] = who["SINIFI"].ToString();
                        bilgiler[4] = who["SUBE"].ToString();
                        baglantı.Close();
                        DialogResult eminmisin = new DialogResult();
                        eminmisin = MessageBox.Show("TC NO'SU=" + bilgiler[0] + " OLAN " + bilgiler[3] + "/" + bilgiler[4] + " SINIFINDAN " + bilgiler[1] + " " + bilgiler[2] + " İSİMLİ ÖĞRENCİYİ SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ?", " ", MessageBoxButtons.YesNo);
                        if (eminmisin == DialogResult.Yes)
                        {
                            baglantı.Open();
                            SqlCommand KMOU = new SqlCommand("DELETE  FROM OGRENCI WHERE NUMARASI ='" + silinecek.Text + "' DELETE FROM VELI WHERE OGRENCINO='" + silinecek.Text + "' DELETE FROM RESIM WHERE TCNO='"+bilgiler[0]+"' DELETE FROM MESAJLAR WHERE ATC = '"+bilgiler[0]+ "' DELETE FROM NOTLAR WHERE NUMARASI ='" + silinecek.Text+ "' DELETE FROM öğrencidevam where NUMARASI ='" + silinecek.Text+"'" , baglantı);
                            KMOU.ExecuteNonQuery();
                            baglantı.Close();
                        }
                        baglantı.Close();
                    }
                    ögrlistgünc();

                }
                catch (Exception)
                {
                    MessageBox.Show("BİR HATA OLUŞTU"," HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                silinecek.Visible = false;
                label11.Visible = false;
            }
        }//ÖĞRENCİ SİL

        private void button1_Click_1(object sender, EventArgs e)
        { 
            string kim = "";
                if (textad.Text != "")//ARANAN ÖĞRENCİYİ BULMAK
                {
                    kim = kim + " " + "and  ADI= '" + textad.Text + "'";
                }
                if (textsyad.Text != "")
                {
                    kim = kim + " " + "and  SOYADI='" + textsyad.Text + "'";
                }
                if (textno.Text != "")
                {
                    kim = kim + " " + "and  NUMARASI='" + textno.Text + "'";
                }
                if (texttc.Text != "")
                {
                    kim = kim + " " + "and  TC='" + texttc.Text + "'";
                }
                if (textsube.Text != "")
                {
                    kim = kim + " " + "and  SUBE='" + textsube.Text + "'";
                }
                if (textcins.Text != "")
                {
                    kim = kim + " " + "and  CINSIYET='" + textcins.Text + "'";
                }
                if (comboBox1.Text != " " && comboBox1.Text != "")
                {
                    kim = kim + " " + "and  SINIFI='" + comboBox1.Text + "'";
                }
            if (kim != "")
            {
                if (kim.Substring(0, 4) == " and")
                {
                    kim = kim.Substring(4);
                }
                //ARANAN ÖĞRENCİYİ BULMAK
                if (tabControl1.SelectedTab==tabPage1)
                {
                    listView1.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("select * from OGRENCI WHERE" + kim, baglantı);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = oku["TC"].ToString();
                        ekle.SubItems.Add(oku["NUMARASI"].ToString());
                        ekle.SubItems.Add(oku["ADI"].ToString());
                        ekle.SubItems.Add(oku["SOYADI"].ToString());
                        ekle.SubItems.Add(oku["SINIFI"].ToString());
                        ekle.SubItems.Add(oku["SUBE"].ToString());
                        ekle.SubItems.Add(oku["KAYITTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMTARIHI"].ToString());
                        ekle.SubItems.Add(oku["DOGUMYERI"].ToString());
                        ekle.SubItems.Add(oku["CINSIYET"].ToString());
                        listView1.Items.Add(ekle);
                    }
                    baglantı.Close();
                }
                if (tabControl1.SelectedTab == tabPage2)
                {
                    listView2.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut1 = new SqlCommand("select  * from VELI inner join OGRENCI ON OGRENCI.NUMARASI=VELI.OGRENCINO where" + kim, baglantı);
                    SqlDataReader oku = komut1.ExecuteReader();
                    while (oku.Read())
                    {
                        ListViewItem EKLE = new ListViewItem();
                        EKLE.Text = oku["ID"].ToString();
                        EKLE.SubItems.Add(oku["NUMARASI"].ToString());
                        EKLE.SubItems.Add(oku["ADI"].ToString());
                        EKLE.SubItems.Add(oku["SOYADI"].ToString());
                        EKLE.SubItems.Add(oku["VELITC"].ToString());
                        EKLE.SubItems.Add(oku["VELIADI"].ToString());
                        EKLE.SubItems.Add(oku["VELISOYADI"].ToString());
                        EKLE.SubItems.Add(oku["TELEFON"].ToString());
                        EKLE.SubItems.Add(oku["YAKINLIK"].ToString());
                        EKLE.SubItems.Add(oku["VDOGUMTARIHI"].ToString());
                        EKLE.SubItems.Add(oku["VDOGUMYERI"].ToString());
                        listView2.Items.Add(EKLE);
                    }

                    
                }

                if (tabControl1.SelectedTab == tabPage4)
                {
                    listView3.Items.Clear();
                    baglantı.Close();
                    baglantı.Open();
                    SqlCommand komut = new SqlCommand("select * from OGRENCI WHERE" + kim, baglantı);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        ListViewItem ekle = new ListViewItem();
                        ekle.Text = oku["NUMARASI"].ToString();
                        ekle.SubItems.Add(oku["ADI"].ToString());
                        ekle.SubItems.Add(oku["SOYADI"].ToString());
                        ekle.SubItems.Add(oku["SINIFI"].ToString());
                        ekle.SubItems.Add(oku["TDEVAM"].ToString());
                        listView3.Items.Add(ekle);
                    }
                    baglantı.Close();
                }
            }
            else
            {
                MessageBox.Show("LÜTFEN BİR KUTUCUĞU DOLDURUP TEKRAR SORGULAYINIZ");
            }
            comboBox1.Text = " ";

        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            string KİMLİK="";
            baglantı.Close();
            vgünc = listView2.SelectedItems[0].Text;
            SqlCommand kma = new SqlCommand("select TC from OGRENCI WHERE NUMARASI='" + listView2.SelectedItems[0].SubItems[1].Text + "'", baglantı);
            baglantı.Open();
            SqlDataReader ökü1 = kma.ExecuteReader();
            if (ökü1.Read())
            {
                KİMLİK = ökü1["TC"].ToString();
            }
            baglantı.Close();
            baglantı.Open();
            SqlCommand kma2 = new SqlCommand("select * from RESIM WHERE TCNO='" + KİMLİK + "'", baglantı);
            SqlDataReader ökü = kma2.ExecuteReader();
            if (ökü.Read())
            {
                byte[] gelen = new byte[0];
                gelen = (byte[])ökü["FOTO"];
                MemoryStream MS = new MemoryStream(gelen);
                pictureBox2.Image = Image.FromStream(MS);

            }
            baglantı.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (vgünc == "")
            {
                MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ VELİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    vgünc = listView2.SelectedItems[0].Text;
                    mudur.mudurpage = 3;
                    bilgigüncelleme frm = new bilgigüncelleme();
                    frm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ VELİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            velilistgünc();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mudur.mudurpage= 4;
            bilgigüncelleme frm = new bilgigüncelleme();
            frm.Show();
            velilistgünc();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            velilistgünc();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (label13.Visible == false)
            {
                label13.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                try
                {
                    baglantı.Open();
                    SqlCommand komt = new SqlCommand("select * from VELI where ID='" + textBox1.Text + "'", baglantı);
                    SqlDataReader who = komt.ExecuteReader();
                    string[] bilgiler = new string[4];

                    if (who.Read())
                    {
                        bilgiler[0] = who["VELITC"].ToString();
                        bilgiler[1] = who["VELIADI"].ToString();
                        bilgiler[2] = who["VELISOYADI"].ToString();
                        
                        
                        baglantı.Close();
                        DialogResult eminmisin = new DialogResult();
                        eminmisin = MessageBox.Show("TC NO'SU=" + bilgiler[0] + " OLAN " + bilgiler[1] + " " + bilgiler[2] + " İSİMLİ VELİYİ SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ?", " ", MessageBoxButtons.YesNo);
                        if (eminmisin == DialogResult.Yes)
                        {
                            baglantı.Open();
                            SqlCommand KMOU = new SqlCommand("DELETE FROM VELI WHERE ID='" + textBox1.Text + "'", baglantı);
                            KMOU.ExecuteNonQuery();
                            baglantı.Close();
                        }
                        baglantı.Close();
                    }
                    ögrlistgünc();

                }
                catch (Exception)
                {
                    MessageBox.Show("BİR HATA OLUŞTU", " HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                label13.Visible = false;
                textBox1.Visible = false;
            }
            velilistgünc();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ögr=="")
            {
                MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ ÖĞRENCİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    ögrno = listView1.SelectedItems[0].SubItems[1].Text;
                    mudur.mudurpage = 2;
                    bilgigüncelleme frm = new bilgigüncelleme();
                    frm.Show();
                }
                catch (Exception)
                {
                    MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ ÖĞRENCİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                ögrlistgünc();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ogrislem_Load(object sender, EventArgs e)
        {
            ögrlistgünc();
            velilistgünc();
            baglantı.Close();
            SqlCommand komut = new SqlCommand("select * from BRANS", baglantı);
            baglantı.Open();
            SqlDataReader lis = komut.ExecuteReader();
            while (lis.Read())
            {
                comboBox2.Items.Add(lis["BRANSADI"].ToString());
            }
            baglantı.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mudur AC = new mudur();
            AC.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text=="" || comboBox3.Text=="" || textBox2.Text=="")
            {
                MessageBox.Show("LÜTFEN TÜM ALANLARI DOLDURUNUZ");
            }
            else
            {
                button11.Visible=false;
                label10.Visible = true;
                comboBox4.Visible = true;
                button14.Visible = true;
                button13.Visible = true;
                dersadı = comboBox2.Text;
                
                baglantı.Close();
                SqlCommand data = new SqlCommand("select * from NOTLAR where DERS='" + comboBox2.Text + "' and SINIF='" + comboBox3.Text + "' and SUBE='" + textBox2.Text + "' and DÖNEM='"+comboBox6.Text+"' and DURUM='AKTİF'", baglantı);
                SqlDataAdapter DA = new SqlDataAdapter(data);
                DataTable dt = new DataTable();
                DA.Fill(dt);
                dataGridView1.DataSource = dt;

                foreach (DataGridViewColumn item in dataGridView1.Columns)
                {
                    item.ReadOnly = true;
                }
                

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            int A = dataGridView1.Rows.Count;
            string[] dizi = new string[A];
            string[] numaraları = new string[A];
            for (int i = 0; i < A-1; i++)
            {
                numaraları[i]= dataGridView1.Rows[i].Cells["NUMARA"].Value.ToString();
                dizi[i] = dataGridView1.Rows[i].Cells[sınavadı].Value.ToString();
            }
            for (int i = 0; i < A-1; i++)
            {
                baglantı.Close();
                SqlCommand güncel = new SqlCommand("update NOTLAR set " + sınavadı + "='" + dizi[i] + "' where NUMARA='" + numaraları[i] + "' and DERS='" + dersadı + "' and DÖNEM='"+comboBox6.Text+ "' and DURUM='AKTİF'", baglantı);
                baglantı.Open();
                güncel.ExecuteNonQuery();
                baglantı.Close();
            }
            baglantı.Close();
            SqlCommand ortalama = new SqlCommand("update NOTLAR set ORTALAMA=(SINAV1+SINAV2+SÖZLÜ)/3 where SINIF='" + comboBox3.Text + "' and SUBE='" + textBox2.Text + "'", baglantı);
            baglantı.Open();
            ortalama.ExecuteNonQuery();
            baglantı.Close();


            baglantı.Close();
            SqlCommand data = new SqlCommand("select * from NOTLAR where DERS='" + comboBox2.Text + "' and SINIF='" + comboBox3.Text + "' and SUBE='" + textBox2.Text + "' and DÖNEM='" + comboBox6.Text + "' and DURUM='AKTİF'", baglantı);
            SqlDataAdapter DA = new SqlDataAdapter(data);
            DataTable dt = new DataTable();
            DA.Fill(dt);
            dataGridView1.DataSource = dt;

            button12.Visible = false;
            button13.Visible = false;
            button11.Visible = true;
            button14.Visible = false;
            label10.Visible = false;
            comboBox4.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            button13.Visible = false;
            button11.Visible = true;
            button14.Visible = false;
            label10.Visible = false;
            comboBox4.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            string say="2";
            if (comboBox5.Text== "TAM GÜN")
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
            DateTime TARİH = dateTimePicker2.Value;
            string tarih1 = TARİH.ToString("yyyy-MM-dd");
            
            baglantı.Close();
            if (say== "2")
            {
                MessageBox.Show("LÜTFEN DEVAMSIZLIK TÜRÜNÜ SEÇİNİZ", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    for (int i = 0; i < listView3.Items.Count; i++)
                    {
                        if (listView3.Items[i].Checked == true)
                        {
                            baglantı.Open();
                            string no = listView3.Items[i].SubItems[0].Text;
                            SqlCommand komut = new SqlCommand("insert into öğrencidevam(ÖGRNO,TARİH,DURUM) values('" + no + "','" + tarih1 + "','"+say+"')",baglantı);
                            komut.ExecuteNonQuery();
                            baglantı.Close();
                            SqlCommand KOMUT2 = new SqlCommand("UPDATE  OGRENCI set TDEVAM=TDEVAM+" + say + " WHERE NUMARASI='" + no + "'", baglantı);
                            baglantı.Open();
                            KOMUT2.ExecuteNonQuery();
                            baglantı.Close();

                        }
                    }
                    MessageBox.Show("SEÇİLEN ÖĞRENCİLERE BAŞARILI İLE "+comboBox5.Text+"DEVAMSIZLIK EKLENDİ");
                }
                catch (Exception X)
                {
                    MessageBox.Show("BİR HATA OLUŞTU " + X.ToString(), "HATA!!");

                }


            }
            
            

        }

        private void listView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            devamdetay.no = listView3.SelectedItems[0].SubItems[0].Text;
            devamdetay.ad = listView3.SelectedItems[0].SubItems[1].Text;
            devamdetay.soyad = listView3.SelectedItems[0].SubItems[2].Text;
            devamdetay.ogretmenmi = false;
            devamdetay.görünürlük = true;
            devamdetay göster = new devamdetay();
            göster.ShowDialog();

        }

        private void button17_Click(object sender, EventArgs e)
        {
            
            if (okulno!="")
            {
                ogrencinot yeni = new ogrencinot();
                yeni.ögrno = okulno;
                yeni.ad = ad2;
                yeni.soyad = soyad2;
                yeni.sınıf = sınıf2;
                yeni.sube = sube2;
                yeni.Show();
            }
            else
            {
                MessageBox.Show("LÜTFEN ÖNCE GÜNCELLEMEK İSTEDİĞİNİZ ÖĞRENCİNİN BİLGİSİNE LİSTEDEN ÇİFT TIKLAYINIZ", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            mudur.mudurpage = 1;
            bilgigüncelleme frm = new bilgigüncelleme();
            frm.dönem = comboBox7.Text;
            frm.Show();
            ögrlistgünc();
            comboBox7.Visible = false;
            button16.Visible = false;
            label21.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
            try
            {
                sınavadı = comboBox4.Text;

                dataGridView1.Columns[sınavadı].ReadOnly = false;


                button14.Visible = false; button12.Visible = true; button13.Visible = true;
                label10.Visible = false;
                comboBox4.Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("BİR HATA OLUŞTU");
            }
            

        }

        private void velilistgünc()
        {
            listView2.Items.Clear();
            baglantı.Close();
            baglantı.Open();
            SqlCommand KMT = new SqlCommand("select  * from VELI inner join OGRENCI ON OGRENCI.NUMARASI=VELI.OGRENCINO", baglantı);
            SqlDataReader oku = KMT.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem EKLE = new ListViewItem();
                EKLE.Text = oku["ID"].ToString();
                EKLE.SubItems.Add(oku["NUMARASI"].ToString());
                EKLE.SubItems.Add(oku["ADI"].ToString());
                EKLE.SubItems.Add(oku["SOYADI"].ToString());
                EKLE.SubItems.Add(oku["VELITC"].ToString());
                EKLE.SubItems.Add(oku["VELIADI"].ToString());
                EKLE.SubItems.Add(oku["VELISOYADI"].ToString());
                EKLE.SubItems.Add(oku["TELEFON"].ToString());
                EKLE.SubItems.Add(oku["YAKINLIK"].ToString());
                EKLE.SubItems.Add(oku["VDOGUMTARIHI"].ToString());
                EKLE.SubItems.Add(oku["VDOGUMYERI"].ToString());
                listView2.Items.Add(EKLE);
            }
            baglantı.Close();
        }
        private void ögrlistgünc()
        {
            baglantı.Close();
            listView1.Items.Clear();
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("select * from OGRENCI", baglantı);
            SqlDataReader oku2 = komut2.ExecuteReader();
            while (oku2.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku2["TC"].ToString();
                ekle.SubItems.Add(oku2["NUMARASI"].ToString());
                ekle.SubItems.Add(oku2["ADI"].ToString());
                ekle.SubItems.Add(oku2["SOYADI"].ToString());
                ekle.SubItems.Add(oku2["SINIFI"].ToString());
                ekle.SubItems.Add(oku2["SUBE"].ToString());
                ekle.SubItems.Add(oku2["KAYITTARIHI"].ToString());
                ekle.SubItems.Add(oku2["DOGUMTARIHI"].ToString());
                ekle.SubItems.Add(oku2["DOGUMYERI"].ToString());
                ekle.SubItems.Add(oku2["CINSIYET"].ToString());
                listView1.Items.Add(ekle);
            }
            baglantı.Close();
        }
    }
}
