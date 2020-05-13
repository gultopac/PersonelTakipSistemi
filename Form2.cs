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
            dateTimePicker1.MaxDate = new DateTime(yil - 18,ay,gün);//18 yaşından küçükler çalışamasın
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radioButton3.Checked = true;
        }
    }
}
