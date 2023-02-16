using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace istoCPos
{
    public partial class MusteriHareketleri : Form
    {
        public MusteriHareketleri()
        {
            InitializeComponent();
        }
        istocDBEntities dbhareketler = new istocDBEntities();
        Form1 cekhareketler = (Form1)Application.OpenForms["Form1"];
        private void MusteriHareketleri_Load(object sender, EventArgs e)
        {
            label14.Text = cekhareketler.label7.Text;
            int barkod = Convert.ToInt32(label14.Text.Trim());
            if (dbhareketler.Musteriler.Any(a => a.Id == barkod))
            {
                var verilericek = dbhareketler.Musteriler.Where(a => a.Id == barkod).SingleOrDefault();
                button1.Text += verilericek.MusteriAdi;
                label19.Text += verilericek.Sehir;
                label20.Text += verilericek.ilce;
                label21.Text += verilericek.Ulke;
                label22.Text += verilericek.Adres;
                label23.Text += verilericek.Aciklama;
                label24.Text += verilericek.Telefon+")";
                label25.Text += verilericek.CepTelefon+")";
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Index == 3)
            {
                TahsilatIslemleri tahsilatIslemleri = new TahsilatIslemleri();
                tahsilatIslemleri.ShowDialog(this);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}