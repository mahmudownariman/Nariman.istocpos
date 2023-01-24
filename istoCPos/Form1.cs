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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace istoCPos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MiktarTextBox1.Text = Convert.ToString(1);
        }
        istocDBEntities db = new istocDBEntities();
        private void GenelToplam()
        {

            double toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                toplam += Convert.ToDouble(dataGridView1.Rows[i].Cells["Tutar"].Value);
            }
            ToplamtextBox1.Text = toplam.ToString("C2");
            BarkodTextBox1.Clear();
            MiktarTextBox1.Text = "1";

        }
        private void BarkodTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Multiply)
            {
                String barkodB = BarkodTextBox1.Text;
                MiktarTextBox1.Text = barkodB;
                BarkodTextBox1.Clear();
            }


            if (e.KeyCode == Keys.Enter)
            {
                String barkod = BarkodTextBox1.Text.Trim();


                if (db.Urunler.Any(a => a.UrunBarkod == barkod))
                {
                    var urun = db.Urunler.Where(a => a.UrunBarkod == barkod).FirstOrDefault();
                    UrunGetirListeye(urun, barkod, Convert.ToDouble(MiktarTextBox1.Text)); }
                else
                {
                    Console.Beep(900, 1000);
                    MessageBox.Show("Tanımlanmamış Ürün");
                }
            }
        }

        private void UrunGetirListeye(Urunler urun, string barkod, double miktar)
        {
            int SatirSayisi = dataGridView1.Rows.Count;

            bool eklnmismi = false;
            if (SatirSayisi > 0)
            {
                for (int i = 0; i < SatirSayisi; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Barkod"].Value.ToString() == barkod)
                    {
                        dataGridView1.Rows[i].Cells["Miktar"].Value = miktar + Convert.ToDouble(dataGridView1.Rows[i].Cells["Miktar"].Value);
                        dataGridView1.Rows[i].Cells["Tutar"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["Miktar"].Value) * Convert.ToDouble(dataGridView1.Rows[i].Cells["Fiyat"].Value), 2);
                        eklnmismi = true;
                    }
                }
            }
            if (!eklnmismi)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[SatirSayisi].Cells["Barkod"].Value = barkod;
                dataGridView1.Rows[SatirSayisi].Cells["UrunAdi"].Value = urun.UrunAdi;
                dataGridView1.Rows[SatirSayisi].Cells["Miktar"].Value = miktar;
                dataGridView1.Rows[SatirSayisi].Cells["Fiyat"].Value = urun.SatisFiyati1;
                dataGridView1.Rows[SatirSayisi].Cells["KdvTutari"].Value = urun.KdvTutari;
                dataGridView1.Rows[SatirSayisi].Cells["AlisFiyati"].Value = urun.AlisFiyati;
                dataGridView1.Rows[SatirSayisi].Cells["Tutar"].Value = Math.Round(miktar * (double)urun.SatisFiyati1, 2);
            }
            dataGridView1.ClearSelection();
            GenelToplam();
            BarkodTextBox1.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                dataGridView1.ClearSelection();
                GenelToplam();
                BarkodTextBox1.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HizliButonDoldur();
        }

        private void Hbutton6_Click(object sender, EventArgs e)
        {
            Button Hb = (Button)sender;
            int buttonid = Convert.ToInt16(Hb.Name.ToString().Substring(7, Hb.Name.Length - 7));

            if (Hb.Text.ToString().StartsWith("-"))
            {
                HizliButonUrunEkle f = new HizliButonUrunEkle();
                f.BNolabel.Text = buttonid.ToString();
                f.ShowDialog();
            }
            else
            {

                var urunbarkod = db.HizliUrun.Where(a => a.Id == buttonid).Select(a => a.Barkod).FirstOrDefault();
                var urun = db.Urunler.Where(a => a.UrunBarkod == urunbarkod).FirstOrDefault();
                UrunGetirListeye(urun, urunbarkod, 1);
                GenelToplam();
            }

        }
        private void HizliButonDoldur()
        {
            var hizliurun = db.HizliUrun.ToList();
            foreach (var item in hizliurun)
            {
                Button Hbutton = this.Controls.Find("Hbutton" + item.Id, true).FirstOrDefault() as Button;
                if (Hbutton != null)
                {
                    double fiyat = islemler.DoubleYap(item.Fiyat.ToString());
                    Hbutton.Text = item.UrunAd + "\n" + fiyat.ToString("C2");
                }

            }
        }

        private void hizliButonSil(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                Button b = (Button)sender;
                if (!b.Text.StartsWith("-"))
                {
                    int butonid = Convert.ToInt16(b.Name.ToString().Substring(7, b.Name.ToString().Length - 7));
                    ContextMenuStrip s = new ContextMenuStrip();
                    ToolStripMenuItem sil = new ToolStripMenuItem();
                    sil.Text = "Temizle - Buton No:" + butonid.ToString();
                    sil.Click += Sil_Click;
                    s.Items.Add(sil);
                    this.ContextMenuStrip = s;

                }
            }
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            int butonid = Convert.ToInt16(sender.ToString().Substring(19, sender.ToString().Length - 19));
            var guncelle = db.HizliUrun.Find(butonid);
            guncelle.Barkod = "-";
            guncelle.UrunAd = "-";
            guncelle.Fiyat = 0;
            db.SaveChanges();
            Button b = this.Controls.Find("Hbutton" + butonid, true).FirstOrDefault() as Button;
            b.Text = "-" + "\n" + "0";
        }

        private void AraButon_Click(object sender, EventArgs e)
        {
            UrunAra SatisUrunAra = new UrunAra();
            SatisUrunAra.SatisUrunAdi = UrunAdiTextBox.Text;
            SatisUrunAra.ShowDialog();
        }

        private void EkleButon_Click(object sender, EventArgs e)
        {
            if (UrunFiyatTextBox.Text != "")
            {
                int satirSayisi = dataGridView1.Rows.Count;
                dataGridView1.Rows.Add();
                dataGridView1.Rows[satirSayisi].Cells["Barkod"].Value = "0";
                if (UrunAdiTextBox.Text != "") {
                    dataGridView1.Rows[satirSayisi].Cells["UrunAdi"].Value = UrunAdiTextBox.Text;
                }
                else
                {
                    dataGridView1.Rows[satirSayisi].Cells["UrunAdi"].Value = "Muhtelif Ürün";
                }
                dataGridView1.Rows[satirSayisi].Cells["Miktar"].Value = Convert.ToInt32(MiktarTextBox1.Text);
                dataGridView1.Rows[satirSayisi].Cells["Fiyat"].Value = Convert.ToInt32(UrunFiyatTextBox.Text);
                dataGridView1.Rows[satirSayisi].Cells["Tutar"].Value = Convert.ToInt32(UrunFiyatTextBox.Text) * Convert.ToInt32(MiktarTextBox1.Text);
                GenelToplam();
                UrunFiyatTextBox.Clear();
            }
        }

        private void NakitButon_Click(object sender, EventArgs e)
        {
            KasaNakitSatis Nsatis = new KasaNakitSatis();
            Nsatis.Show();
        }

        public void satisYap(string odemesekli)
        {
            int satirSayisi = dataGridView1.Rows.Count;
            bool iadeSatis = checkBoxIade.Checked;
            double alisFiyatToplam = 0;
            if (satirSayisi > 0)
            {
                int? islemNo = db.Islem.First().IslemNo;
                Satis1 satis = new Satis1();
                
                for (int i = 0; i < satirSayisi; i++)
                {
                    satis.IslemNo = islemNo;
                    satis.UrunAd = dataGridView1.Rows[i].Cells["UrunAdi"].Value.ToString();
                    satis.Barkod = dataGridView1.Rows[i].Cells["Barkod"].Value.ToString();
                    satis.SatisFiyat = islemler.DoubleYap(dataGridView1.Rows[i].Cells["Fiyat"].Value.ToString());
                    satis.Miktar = Convert.ToInt32(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.Toplam= islemler.DoubleYap(dataGridView1.Rows[i].Cells["Tutar"].Value.ToString());
                    satis.KdvTutari = islemler.DoubleYap(dataGridView1.Rows[i].Cells["KdvTutari"].Value.ToString()) * islemler.DoubleYap(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString());
                    satis.OdemeSekli = odemesekli;
                    satis.Iade = iadeSatis;
                    satis.Tarih = DateTime.Now;
                    satis.Kullanici = kullaniciLabel.Text;
                    db.Satis1.Add(satis);
                    db.SaveChanges();

                    

                    if (!iadeSatis)
                    {
                        MessageBox.Show("Stok Azalt");
                        islemler.StokAzalt(dataGridView1.Rows[i].Cells["Barkod"].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString()));
                    }
                    else
                    {
                        islemler.StokArttir(dataGridView1.Rows[i].Cells["Barkod"].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[i].Cells["Miktar"].Value.ToString()));
                        MessageBox.Show("Stok Artır");
                    }
                    alisFiyatToplam += islemler.DoubleYap(dataGridView1.Rows[i].Cells["AlisFiyati"].Value.ToString());



                }
                IslemOzet islemOzet = new IslemOzet();
                islemOzet.IslemNo = islemNo;
                islemOzet.Iade = iadeSatis;
                islemOzet.AlisFiyatiToplam = alisFiyatToplam;
                //islemOzet.Gelir = false;
                //islemOzet.Gider = false;
                if (!iadeSatis) {
                    islemOzet.Aciklama = odemesekli + " Satış gerçekleşti.";
                }
                else
                {
                    islemOzet.Aciklama = "İade işlemi (" + odemesekli + ")";
                }
                islemOzet.Kullanci = kullaniciLabel.Text;
                islemOzet.OdemeSekli = odemesekli;
                islemOzet.Tarih = DateTime.Now;

                switch (odemesekli)
                {
                    case "Nakit":
                        islemOzet.Nakit = islemler.DoubleYap(ToplamtextBox1.Text);
                        islemOzet.Kart = 0;
                        break;
                    case "Kart":
                        islemOzet.Kart = islemler.DoubleYap(ToplamtextBox1.Text);
                        islemOzet.Nakit = 0;
                        break;
                    case "Kart-Nakit":
                        islemOzet.Nakit = islemler.DoubleYap(labelNakit.Text);
                        islemOzet.Kart = islemler.DoubleYap(labelKart.Text);
                        break;

                }
                db.IslemOzet.Add(islemOzet);
                db.SaveChanges();

                var islemNoArttir = db.Islem.First();
                islemNoArttir.IslemNo += 1;
                db.SaveChanges();

                MessageBox.Show("yazdırma işlemi");

            }

        }

        private void checkBoxIade_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIade.Checked == true)
            {
                checkBoxIade.Visible = true;

            }
            else
            {
                checkBoxIade.Visible = false;
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBoxIade.Checked == false)
                checkBoxIade.Checked = true;
            else
                checkBoxIade.Checked = false;
           
            
        }

        private void KartButon_Click(object sender, EventArgs e)
        {
            KasaKartSatis Ksatis = new KasaKartSatis();
            Ksatis.Show();
            satisYap("Kart");
        }

        private void BarkodTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Musteributton1_Click(object sender, EventArgs e)
        {

        }

        private void Musteributton1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Index == 0)
            {
                MusteriEkle MusteriKayıt = new MusteriEkle();
                MusteriKayıt.ShowDialog();
            }
           
        }

        private void UrunBildirButon_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void UrunBildirButon_Click(object sender, EventArgs e)
        {

        }
    }
}
