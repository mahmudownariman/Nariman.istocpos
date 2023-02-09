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
    public partial class MusteriKarti : Form
    {
        public MusteriKarti()
        {
            InitializeComponent();
        }
        istocDBEntities musteriler = new istocDBEntities();
        Form1 cek= (Form1)Application.OpenForms["Form1"];
        public string a, b, c, d, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w;
        private void MusteriKarti_Load(object sender, EventArgs e)
        {
           /* Idlabel2.Text = a;
            MusteriKartitextBox1.Text = b;
            MusteriKartitextBox2.Text = c;
            MusteriKartitextBox3.Text = d;
            MusteriKartitextBox4.Text = f;
            MusteriKartitextBox5.Text = g;
            MusteriKartitextBox6.Text = h;
            MusteriKartitextBox7.Text = i;
            MusteriKartitextBox8.Text = j;
            MusteriKartitextBox9.Text = k;
            MusteriKartitextBox10.Text = l;
            MusteriKartitextBox11.Text = m;
            MusteriKartitextBox12.Text = n;
            MusteriKarticomboBox1.Text = o;
            MusteriKarticomboBox2.Text = p;
            MusteriKarticomboBox3.Text = q;
            MusteriKarticomboBox4.Text = r;
            MusteriKarticomboBox5.Text = s;
            MusteriKarticomboBox6.Text = t;
            MusteriKarticomboBox7.Text = u;
            MusteriKarticomboBox8.Text = v;
            MusteriKarticomboBox9.Text = w;*/
            
            //cek.musteriCek();
            //Idlabel2.Text = f.label7.Text;
        }
    }
}
