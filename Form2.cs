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
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0 Data Source=personel.mdb");
        
        private 
       
    }
}
