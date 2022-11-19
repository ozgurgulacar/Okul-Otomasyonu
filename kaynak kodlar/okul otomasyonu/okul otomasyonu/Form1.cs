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

namespace okul_otomasyonu
{
    public partial class Form1 : Form
    {
        public static string kimlikno;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "MÜDÜR";
            label2.Text = "TC KİMLİK NUMARASI:";
            label1.Visible = true; label2.Visible = true;
            label3.Visible = true; label4.Visible = true;
            textBox1.Enabled = true; textBox2.Enabled=true;
            textBox1.Visible = true; textBox2.Visible = true;
            button4.Enabled = true; button4.Visible = true;
            button2.Enabled = false; button2.Visible = false;
            button3.Enabled = false; button3.Visible = false;
            button5.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "ÖĞRETMEN";
            label2.Text = "TC KİMLİK NUMARASI:";
            label1.Visible = true; label2.Visible = true;
            label3.Visible = true; label4.Visible = true;
            textBox1.Enabled = true; textBox2.Enabled = true;
            textBox1.Visible = true; textBox2.Visible = true;
            button4.Enabled = true; button4.Visible = true;
            button1.Enabled = false; button1.Visible = false;
            button3.Enabled = false; button3.Visible = false;
            button5.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "ÖĞRENCİ";
            label2.Text = "ÖĞRENCİ NO:";
            label1.Visible = true; label2.Visible = true;
            label3.Visible = true; label4.Visible = true;
            textBox1.Enabled = true; textBox2.Enabled = true;
            textBox1.Visible = true; textBox2.Visible = true;
            button4.Enabled = true; button4.Visible = true;
            button2.Enabled = false; button2.Visible = false;
            button1.Enabled = false; button1.Visible = false;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = "";
            label1.Visible = false; label2.Visible = false;
            label3.Visible = false; label4.Visible = false;
            textBox1.Enabled = false; textBox2.Enabled = false;
            textBox1.Visible = false; textBox2.Visible = false;
            button4.Enabled = false; button4.Visible = false;
            button2.Enabled = true; button2.Visible = true;
            button1.Enabled = true; button1.Visible = true;
            button3.Enabled = true; button3.Visible = true;
            button5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            vtsınıfı vt = new vtsınıfı();
            vt.giris(label1.Text,textBox1.Text, textBox2.Text,this);
        }
    }
}
