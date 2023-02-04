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
        Form1 f = (Form1)Application.OpenForms["Form1"];

        private void MusteriKarti_Load(object sender, EventArgs e)
        {
            f.musteriCek();
            Idlabel2.Text = f.label7.Text;
        }
    }
}
