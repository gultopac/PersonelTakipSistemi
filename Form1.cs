using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;
namespace PersonelTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=personel3.mdb");
        //global değişkenler
        public static string TcKimlikNo, Adi, Soyadi, Yetki;

       
        //yerel değişkenler
        int hak = 3;
        bool durum = false;// veri girişi başarısız

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kullanici Girisi";
            this.AcceptButton = button1;
            this.CancelButton = button2;
            label5.Text= Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {

    
               baglantim.Open();
                //access tablosundan verileri çektik
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilarim",baglantim);
                //
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//sorgunun yürütülmesini sağladık,datareadera okunan verileri aktardık
                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["KullaniciAdi"].ToString() ==textBox1.Text
                            && kayitokuma["Parola"].ToString()== textBox2.Text
                            && kayitokuma["Yetki"].ToString()== "Yönetici")
                        {
                            durum = true;
                            TcKimlikNo = kayitokuma.GetValue(0).ToString();//kayitokumadaki 0. alanı al
                            Adi = kayitokuma.GetValue(1).ToString();
                            Soyadi = kayitokuma.GetValue(2).ToString();
                            Yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();//form 1 i gizliyoruz
                            Form2 frm2 = new Form2();
                            frm2.Show();
                            break;//while bulunan kayıttan sonra aramaya devam etmesin diye kapattık.

                        }
                    }
                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["KullaniciAdi"].ToString() == textBox1.Text
                            && kayitokuma["Parola"].ToString() == textBox2.Text
                            && kayitokuma["Yetki"].ToString() == "Kullanici")
                        {
                            durum = true;
                            TcKimlikNo = kayitokuma.GetValue(0).ToString();//kayitokumadaki 0. alanı al
                            Adi = kayitokuma.GetValue(1).ToString();
                            Soyadi = kayitokuma.GetValue(2).ToString();
                            Yetki = kayitokuma.GetValue(3).ToString();
                            this.Hide();//form 1 i gizliyoruz
                            Form3 frm3 = new Form3();
                            frm3.Show();
                            break;//while bulunan kayıttan sonra aramaya devam etmesin diye kapattık.

                        }
                    }

                } if (durum == false)
                    hak--;
                baglantim.Close();

            }
            label5.Text = Convert.ToString(hak);//kalan hakkı yazdırma
            if (hak == 0)
            {
                button1.Enabled = false;//girişi basamasın
                MessageBox.Show("Giriş hakkı kalmadı", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);  //mesaj içeriği,mesaj box başlığı,hangi buttonlar görüncek,icon belirleme
                this.Close();
            }

        }

    }
}
