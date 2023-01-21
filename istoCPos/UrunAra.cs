using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace istoCPos
{
    public partial class UrunAra : Form
    {
        public UrunAra()
        {
            InitializeComponent();
        }
        public String SatisUrunAdi;

        istocDBEntities db = new istocDBEntities();
        private void SatisUrunAra_Load(object sender, EventArgs e)
        {
            UrunAraTextbox.Text = SatisUrunAdi;


        }

        private void EkleButon_Click_1(object sender, EventArgs e)
        {
            String barkod = AraBarkodTextBox.Text.Trim();

            if (UrunAraTextbox.Text != "")
            {
                string Urunad = UrunAraTextbox.Text.Trim();
                var urunler = db.Urunler.Where(a => a.UrunAdi.Contains(Urunad)).ToList();
                UrunAraGrid.DataSource = urunler;
            }

            if (db.Urunler.Any(a => a.UrunBarkod == barkod))
            {
                var urun = db.Urunler.Where(a => a.UrunBarkod.Contains(barkod)).ToList();
                int satirSayisi = UrunAraGrid.Rows.Count;
                UrunAraGrid.DataSource = urun;

            }
        }
    }
}
