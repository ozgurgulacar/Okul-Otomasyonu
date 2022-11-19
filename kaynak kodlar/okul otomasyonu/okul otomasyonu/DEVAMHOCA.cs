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
    public partial class DEVAMHOCA : Form
    {
        static string baglantı2 = ConfigurationManager.ConnectionStrings["okul_otomasyonu.Properties.Settings.Ayar"].ConnectionString;
        SqlConnection baglantı = new SqlConnection(baglantı2);

        string[] ıdler;
        public DEVAMHOCA()
        {
            InitializeComponent();
        }

        private void DEVAMHOCA_Load(object sender, EventArgs e)
        {
            int a = 0;
            baglantı.Close();
            SqlCommand doldur = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Open();
            SqlDataReader DOLDUR2 = doldur.ExecuteReader();
            while (DOLDUR2.Read())
            {
                a++;
                comboBox1.Items.Add(DOLDUR2["BRANSADI"].ToString());
            }
            ıdler = new string[a];
            baglantı.Close();
            a = 0;
            SqlCommand doldur3 = new SqlCommand("SELECT * FROM BRANS", baglantı);
            baglantı.Open();
            SqlDataReader DOLDUR4 = doldur3.ExecuteReader();
            while (DOLDUR4.Read())
            {
               
               ıdler[a]= DOLDUR4["ID"].ToString();
                a++;
            }
            comboBox1.Items.Add("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="" && textBox1.Text=="" && textBox2.Text == "")
            {
                string text = "select * from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID";
                list(text);
            }
            else
            {
                if (comboBox1.Text!="")
                {
                    string text2 = "select * from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID where OGRETMEN.BRANS='"+ıdler[comboBox1.SelectedIndex]+"'";
                    list(text2);
                }
                if (textBox1.Text != "")
                {
                    string text3 = "select * from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID where OGRETMEN.ADI='" + textBox1.Text + "'";
                    list(text3);
                }
                if (textBox2.Text != "")
                {
                    string text4 = "select * from OGRETMEN inner join BRANS on OGRETMEN.BRANS=BRANS.ID where OGRETMEN.SOYADI='" + textBox2.Text + "'";
                    list(text4);
                }
                
            }
            comboBox1.Text = "";
        }




        private void list(string comtext)
        {
            baglantı.Close();
            listView3.Items.Clear();
            SqlCommand LİST = new SqlCommand(comtext, baglantı);
            baglantı.Open();
            SqlDataReader lis = LİST.ExecuteReader();
            while (lis.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = lis["TC"].ToString();
                ekle.SubItems.Add(lis["ADI"].ToString());
                ekle.SubItems.Add(lis["SOYADI"].ToString());
                
                ekle.SubItems.Add(lis["BRANSADI"].ToString());
                
                ekle.SubItems.Add(lis["DEVAMT"].ToString());
                listView3.Items.Add(ekle);
            }
            baglantı.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string say = "2";
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
            DateTime TARİH = dateTimePicker2.Value;
            string tarih1 = TARİH.ToString("yyyy-MM-dd");

            baglantı.Close();
            if (say == "2")
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
                            SqlCommand komut = new SqlCommand("insert into HOCADEVAM(TCNO,TARİH,DURUM) values('" + no + "','" + tarih1 + "','" + say + "')", baglantı);
                            komut.ExecuteNonQuery();
                            baglantı.Close();
                            SqlCommand KOMUT2 = new SqlCommand("UPDATE  OGRETMEN set DEVAMT=DEVAMT+" + say + " WHERE TC='" + no + "'", baglantı);
                            baglantı.Open();
                            KOMUT2.ExecuteNonQuery();
                            baglantı.Close();

                        }
                    }
                    MessageBox.Show("SEÇİLEN ÖĞRETMENLERE BAŞARILI İLE " + comboBox5.Text + "DEVAMSIZLIK EKLENDİ");
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
            devamdetay.ogretmenmi = true;
            devamdetay.görünürlük = true;
            devamdetay göster = new devamdetay();
            göster.ShowDialog();
        }
    }
}
