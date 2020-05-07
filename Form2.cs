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
using System.Text.RegularExpressions; //regex kütüphanesi,güvenli parola işlemlerinde yardımcı olacak
using System.IO; //giriş çıkış işlemlerinde yardımcı olucak
namespace PersonelTakipSistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=personel.mdb");
        
        private void kullanicilar_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select TcKimlikno AS[TC Kimlik No], Adi as[Adı],Soyadi as[Soyadı]," +
                    "Yetki as[Yetki],KullaniciAdi as[Kullanıcı Adı],Parola as[Parola] from kullanicilar order by Adi ASC",baglantim);
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
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select TcKimlikno AS[TC Kimlik No], Ad as[Adı],Soyad as[Soyadı], Cinsiyet as[Cinsiyeti] " +
                    "Mezuniyet as[Mezuniyet],DogumTarihi as[Doğum Tarihi],Gorevi as[Görevi],GorevYeri as[Görev Yeri],Maasi as[Maaş] from personeller order by Ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error); baglantim.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            kullanicilar_goster();
            personelleri_goster();
        }
    }
}
