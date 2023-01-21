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
    public partial class HizliButonUrunEkle : Form
    {
        public HizliButonUrunEkle()
        {
            InitializeComponent();
        }

        istocDBEntities db = new istocDBEntities();
        private void HizliUrunAraTextbox_TextChanged(object sender, EventArgs e)
        {
            if (HizliUrunAraTextbox.Text != "")
            {
                string Urunad = HizliUrunAraTextbox.Text;
                var urunler = db.Urunler.Where(a => a.UrunAdi.Contains(Urunad)).ToList();
                HizliUrunEkleUrunlerGrid.DataSource = urunler;
            }
        }

        private void HizliButonUrunEkle_Load(object sender, EventArgs e)
        {

        }

        private void HizliUrunEkleUrunlerGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (HizliUrunEkleUrunlerGrid.Rows.Count > 0)
            {
                string barkod = HizliUrunEkleUrunlerGrid.CurrentRow.Cells["Urunbarkod"].Value.ToString();
                string urunad = HizliUrunEkleUrunlerGrid.CurrentRow.Cells["UrunAdi"].Value.ToString();
                double fiyat = Convert.ToDouble(HizliUrunEkleUrunlerGrid.CurrentRow.Cells["SatisFiyati1"].Value.ToString());
                int id = Convert.ToInt16(BNolabel.Text);
                var guncellenecek = db.HizliUrun.Find(id);
                guncellenecek.Barkod = barkod;
                guncellenecek.UrunAd = urunad;
                guncellenecek.Fiyat = fiyat;
                db.SaveChanges();
                MessageBox.Show("Hızlı Buton Tanımlanmıştır");
                Form1 f = (Form1)Application.OpenForms["Form1"];
                if (f != null)
                {
                    Button b = f.Controls.Find("Hbutton" + id, true).FirstOrDefault() as Button;
                    b.Text = urunad + "\n" + fiyat.ToString("C2");
                }
                this.Close();
            }
        }
    }
}
