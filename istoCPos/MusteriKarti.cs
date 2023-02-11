using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace istoCPos
{
    public partial class MusteriKarti : Form
    {
        public MusteriKarti()
        {
            InitializeComponent();
        }
        istocDBEntities musterilerdb = new istocDBEntities();
        Form1 cek= (Form1)Application.OpenForms["Form1"];
        

        private void MusteriKarti_Load(object sender, EventArgs e)
        {
            Idlabel2.Text = cek.MusteridataGridView5.CurrentCell.Value.ToString();
            int barkod = Convert.ToInt32(Idlabel2.Text.Trim());
            if (musterilerdb.Musteriler.Any(a => a.Id == barkod))
            {
                var musteri = musterilerdb.Musteriler.Where(a => a.Id == barkod).SingleOrDefault();

                MusteriKartitextBox1.Text = musteri.MusteriAdi;
                MusteriKartitextBox2.Text = musteri.Telefon;
                MusteriKartitextBox3.Text = musteri.CepTelefon;
                MusteriKartitextBox4.Text = musteri.e_mail;
                MusteriKartitextBox5.Text = musteri.Adres;
                MusteriKartitextBox6.Text = musteri.VergiNumarasi;
                MusteriKartitextBox7.Text = musteri.RiskLimit;
                MusteriKartitextBox8.Text = musteri.Aciklama;
                MusteriKartitextBox9.Text = musteri.Bakiye.ToString();
                MusteriKartitextBox10.Text = musteri.FaturaAdres;
                MusteriKartitextBox11.Text = musteri.Unvan;
                MusteriKartitextBox12.Text = musteri.FirmaAdi;
                MusteriKarticomboBox1.Text = musteri.VergiDairesi;
                MusteriKarticomboBox2.Text = musteri.Grup1;
                MusteriKarticomboBox3.Text = musteri.Grup2;
                MusteriKarticomboBox4.Text = musteri.Grup3;
                MusteriKarticomboBox5.Text = musteri.Sehir;
                MusteriKarticomboBox6.Text = musteri.ilce;
                MusteriKarticomboBox7.Text = musteri.Ulke;
                MusteriKarticomboBox8.Text = musteri.FiyatSecenegi;
                MusteriKarticomboBox9.Text = musteri.Doviz;
               
            }
                /**/

                //cek.musteriCek();

            }
        private void MusteriKartibutton1_Click(object sender, EventArgs e)
        {
            Idlabel2.Text = cek.MusteridataGridView5.CurrentCell.Value.ToString();
            int barkod = Convert.ToInt32(Idlabel2.Text.Trim());
            if (musterilerdb.Musteriler.Any(a => a.Id == barkod))
            {

                var Guncelle = musterilerdb.Musteriler.Where(a => a.Id == barkod).SingleOrDefault();
                Guncelle.MusteriAdi = MusteriKartitextBox1.Text;
                Guncelle.Telefon = MusteriKartitextBox2.Text;
                Guncelle.CepTelefon = MusteriKartitextBox3.Text;
                Guncelle.VergiDairesi = MusteriKarticomboBox1.Text;
                Guncelle.VergiNumarasi = MusteriKarticomboBox6.Text;
                Guncelle.Grup1 = MusteriKarticomboBox2.Text;
                Guncelle.Grup2 = MusteriKarticomboBox3.Text;
                Guncelle.Grup3 = MusteriKarticomboBox4.Text;
                Guncelle.Adres = MusteriKartitextBox5.Text;
                Guncelle.Sehir = MusteriKarticomboBox5.Text;
                Guncelle.ilce = MusteriKarticomboBox6.Text;
                Guncelle.Ulke = MusteriKarticomboBox7.Text;
                Guncelle.FaturaAdres = MusteriKartitextBox10.Text;
                Guncelle.e_mail = MusteriKartitextBox4.Text;
                Guncelle.RiskLimit = MusteriKartitextBox7.Text;
                Guncelle.FiyatSecenegi = MusteriKarticomboBox8.Text;
                Guncelle.Doviz = MusteriKarticomboBox9.Text;
                Guncelle.Aciklama = MusteriKartitextBox8.Text;
                //Guncelle.Bakiye = Math.Round(Convert.ToDouble( MusteriKartitextBox9.Text));
                Guncelle.Unvan = MusteriKartitextBox11.Text;
                Guncelle.FirmaAdi = MusteriKartitextBox12.Text;

                musterilerdb.SaveChanges();

                cek.MusteridataGridView5.DataSource = musterilerdb.Musteriler.OrderByDescending(a => a.Id).Take(10).ToList();
            }
        }
    }
}
