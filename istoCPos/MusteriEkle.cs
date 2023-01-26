using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace istoCPos
{
    public partial class MusteriEkle : Form
    {
        public MusteriEkle()
        {
            InitializeComponent();
        }
        istocDBEntities database = new istocDBEntities();
        public void musteriVerileriKaydet(string veriler)
        {
           


        }
        private void MüsteriEkle_Load(object sender, EventArgs e)
        {

        }
        
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (MusteritextBox1.Text != "" && MusteritextBox2.Text != "")
            {
                Musteriler MusteriKaydet = new Musteriler();
                MusteriKaydet.MusteriAdi = MusteritextBox1.Text;
                MusteriKaydet.Telefon = MusteritextBox2.Text;
                MusteriKaydet.CepTelefon = MusteritextBox3.Text;
                MusteriKaydet.VergiDairesi = MustericomboBox1.Text;
                MusteriKaydet.VergiNumarasi = MusteritextBox6.Text;
                MusteriKaydet.Grup1 = MustericomboBox2.Text;
                MusteriKaydet.Grup2 = MustericomboBox3.Text;
                MusteriKaydet.Grup3 = MustericomboBox4.Text;
                MusteriKaydet.Adres = MusteritextBox5.Text;
                MusteriKaydet.Sehir = MustericomboBox5.Text;
                MusteriKaydet.ilce = MustericomboBox6.Text;
                MusteriKaydet.Ulke = MustericomboBox7.Text;
                MusteriKaydet.FaturaAdres = MusteritextBox10.Text;
                MusteriKaydet.e_mail = MusteritextBox4.Text;
                MusteriKaydet.RiskLimit = MusteritextBox7.Text;
                MusteriKaydet.FiyatSecenegi = MustericomboBox8.Text;
                MusteriKaydet.Doviz = MustericomboBox9.Text;
                MusteriKaydet.Aciklama = MusteritextBox8.Text;
                
                MusteriKaydet.Unvan = MusteritextBox11.Text;
                MusteriKaydet.FirmaAdi = MusteritextBox12.Text;

                database.Musteriler.Add(MusteriKaydet);
                database.SaveChanges();
                MusteritextBox1.Clear();
                MusteritextBox2.Clear();
                MusteritextBox3.Clear();
                MusteritextBox4.Clear();
                MusteritextBox5.Clear();
                MusteritextBox6.Clear();
                MusteritextBox7.Clear();
                MusteritextBox8.Clear();
                MusteritextBox9.Clear();
                MusteritextBox10.Clear();
                MusteritextBox11.Clear();
                MusteritextBox12.Clear();
                MustericomboBox1.Text = "";
                MustericomboBox2.Text = "";
                MustericomboBox3.Text = "";
                MustericomboBox4.Text = "";
                MustericomboBox5.Text = "";
                MustericomboBox6.Text = "";
                MustericomboBox7.Text = "";
                MustericomboBox8.Text = "";
                MustericomboBox9.Text = "";
                
                MessageBox.Show("Kayıt Edildi");
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
