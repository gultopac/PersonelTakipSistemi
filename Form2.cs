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
using System.Text.RegularExpressions;  //regex kütüphanesi,güvenli parola işlemlerinde yardımcı olacak
using System.IO; //giriş çıkış işlemlerinde yardımcı olucak
namespace PersonelTakipSistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=personel3.mdb");

        private void kullanicilar_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter
                    ("select TcKimlikNo , Adi,Soyadi,Yetki ,KullaniciAdi,Parola from kullanicilarim ", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error); baglantim.Close();

            }
        }
        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select TcKimlikNo, Ad,Soyad, Cinsiyet,Mezuniyet,DogumTarihi ,GorevYeri,Gorevi,Maasi  from personeller ", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error); baglantim.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//genişlik ve yüksekliğe göre resmi içine ayarla demek

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.TcKimlikNo + ".jpg");//bindeki debug
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok.jpg");
            }
            //kullanici işlemleri sekmesi
            this.Text = "Yönetici işlemleri";
            label12.ForeColor = Color.DarkRed;
            label12.Text = Form1.Adi + " " + Form1.Soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "TC 11 karakterden az olamaz!");//mause üzerine gelince uyarı verir
            radioButton1.Checked = true;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;//100 parçaya böldük
            progressBar1.Value = 0;

            kullanicilar_goster();

            //personel işlemleri
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";//istediğimiz veri grişine zorluyoruz,11 tane rakam girmek zorunda kalma 0 girme zorunluğu
            maskedTextBox2.Mask = "LL?????????";//enaz 2 karekter girilmesi lazım geri kalan ? girilmesede de olur
            maskedTextBox3.Mask = "LL?????????";
            maskedTextBox4.Mask = "0000";//1000-10000 arası maaş girlmesi zorunlu
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("ilköğretim");
            comboBox1.Items.Add("ortaöğretim");
            comboBox1.Items.Add("lise");
            comboBox1.Items.Add("üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şoför");
            comboBox2.Items.Add("İşçi");

            comboBox3.Items.Add("ARGE");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Muhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Paketleme");
            comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gün = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);//50 yaşından faazla çalışan olmasın
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gün);//18 yaşından küçükler çalışamasın
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButton3.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
            {
                errorProvider1.SetError(textBox1, "TC kimlik no 11 karekter olmalı!");
            }
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//her tuşa bastığımzda tetiklenir harf yazmayı engeller
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8) //0 la 9 arası 48-57 ascii kodlarını ifade eder 8 de back space tuşunu ifade eder
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                //e.keychar tuş bilgisi alma
                //ısletter=harf demek oluyor 
                //ıscontrol=back space e basılmışsa
                //ısseparator boşluk koyabiliir 2 isimli olup
                e.Handled = false;//tuşlar aktif olsun
            }
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;

            }
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
            {
                errorProvider1.SetError(textBox4, " Kullanıcı adı 8 Karakter olmalı");
            }
            else
                errorProvider1.Clear();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            //digit sayı girme kontrolü
            {
                e.Handled = false;

            }
            else
                e.Handled = true;
        }

        int parola_skor = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";//zayıf güçlü çok güçlü durumu
            int kucuk_harf_skoru = 0;
            int buyuk_harf_skoru = 0;
            int rakam_skoru = 0;
            int sembol_skoru = 0;
            string sifre = textBox5.Text;
            //regex güvenli parola işlemleride kullanılan kütüphanedir.
            //regex ing karakterleri baz alır ,şifre string ifadesindeki türkçe karakterleri ing karaktere dönüştürmeliyiz
            string duzeltismis_sifre = "";
            duzeltismis_sifre = sifre;
            duzeltismis_sifre = duzeltismis_sifre.Replace('İ', 'I');//yer değiştirme
            duzeltismis_sifre = duzeltismis_sifre.Replace('ı', 'i');
            duzeltismis_sifre = duzeltismis_sifre.Replace('Ç', 'C');
            duzeltismis_sifre = duzeltismis_sifre.Replace('ç', 'c');
            duzeltismis_sifre = duzeltismis_sifre.Replace('Ş', 'S');
            duzeltismis_sifre = duzeltismis_sifre.Replace('ş', 's');
            duzeltismis_sifre = duzeltismis_sifre.Replace('Ğ', 'G');
            duzeltismis_sifre = duzeltismis_sifre.Replace('ğ', 'g');
            duzeltismis_sifre = duzeltismis_sifre.Replace('Ü', 'U');
            duzeltismis_sifre = duzeltismis_sifre.Replace('ü', 'u');
            duzeltismis_sifre = duzeltismis_sifre.Replace('Ö', 'O');
            duzeltismis_sifre = duzeltismis_sifre.Replace('ö', 'o');
            if (sifre != duzeltismis_sifre)
            {
                sifre = duzeltismis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki türkçe karakterler ingilizce karakterlere dönüştürülmüştür.");
            }
            //bir küçük harf 10 puan 2 ve üzeri 20 puan
            //şirefnin uzunluğu - küçük harf olmayan hali replace yer değiştir//küçük harf olanları siliyoruz bir nevi 
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;//regexreplace şifredeki küçük karakterleri çıkarıp uzunluğunu verir
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;//2 mi daha yoksa küçük harf sayısı mı küçük olanı 10 la çarptık

            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skor = sembol_skoru + kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru;
            //100lük sistemde tamamlamak için 
            if (sifre.Length == 9)
            {
                parola_skor += 10;

            }
            else if (sifre.Length == 10)
            {
                parola_skor += 20;
            }

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
            {
                label22.Text = "Büyük harf,Küçük harf,Rakam ve Sembol Kullanmalısın !";
            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label22.Text = "";
            }
            if (parola_skor < 70)
            {
                parola_seviyesi = "Kabul edilemedi";

            }
            else if (parola_skor == 70 || parola_skor == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skor == 90 || parola_skor == 100)
                parola_seviyesi = "Çok güçlü";

            label9.Text = "%" + Convert.ToString(parola_skor);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skor;//progress barı değere göre doldurduk.
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parolalar eşleşmiyor");
            else
                errorProvider1.Clear();
        }
        private void topPage1_temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void topPage2_temizle()
        {

            pictureBox2.Image = null;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            maskedTextBox3.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;//seçiili olan indisi boşalt

        }


        private void button2_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;//daha önceden kayıt var mı kontrol edicez,şuan olmadığını farz ediyoruz

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilarim where TcKimlikNo='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//select dorgusu sonucunu readera aktardık

            while (kayitokuma.Read())//herhangi bir kayıt varsa
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //tc kimlik no kontrolü
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                {
                    label1.ForeColor = Color.Red;
                }
                else
                    label1.ForeColor = Color.Black;
                //ad veri kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                {
                    label2.ForeColor = Color.Red;
                }
                else
                    label2.ForeColor = Color.Black;
                //soyad veri kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                {
                    label3.ForeColor = Color.Red;
                }
                else
                    label3.ForeColor = Color.Black;
                //kullanici adı kontrol
                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                {
                    label5.ForeColor = Color.Red;
                }
                else
                    label5.ForeColor = Color.Black;
                //parola veri kontrol
                if (parola_skor < 70 || textBox5.Text == "")
                {
                    label6.ForeColor = Color.Red;
                }
                else
                    label6.ForeColor = Color.Black;
                //parola tekrar veri konstrolü
                if (textBox5.Text != textBox6.Text || textBox6.Text == "")
                {
                    label7.ForeColor = Color.Red;
                }
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != ""
                    && textBox2.Text != "" && textBox2.Text.Length > 1
                    && textBox3.Text != "" && textBox3.Text.Length > 1
                    && textBox4.Text != "" && textBox5.Text != "" &&
                    textBox5.Text != "" &&
                    textBox5.Text == textBox6.Text &&
                    parola_skor >= 70){
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanici";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomut = new OleDbCommand("insert into kullanicilarim values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);
                        eklekomut.ExecuteNonQuery();//ekle komutuun sorgu sonucunu tabloa işle
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı eklendi", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);// içerik,başlık,buu-lunacak buttonlar,bilgilendirme iconu demek
                        topPage1_temizle();

                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kısmızı olan yerleri tekrar gözden geçiriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
             }
            else
            {
                MessageBox.Show("Tc Kimlik no daha önceden kayıtlıdır.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgusu = new OleDbCommand("select * from kullanicilarim where TcKimlikNo='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgusu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();//ad bilgisini vt den aldık stringe dnüştürmemiz gerekiyor
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu = false) 
                    MessageBox.Show("Aranan kayıt bulunamadı", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli bir TCkimlik no giriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }
    }
}

