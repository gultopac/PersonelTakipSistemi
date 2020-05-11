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
                    ("select TcKimlikNo , Adi,Soyadi,Yetki ,KullaniciAdi,Parola from kullanicilarim ",baglantim);
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
            kullanicilar_goster();
            personelleri_goster();
        }
    }
}
