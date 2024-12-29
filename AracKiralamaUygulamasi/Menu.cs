using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaUygulamasi
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AracIslemleri arac = new AracIslemleri();
            arac.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriIslemleri musteri = new MusteriIslemleri();
            musteri.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminIslemleri admin = new AdminIslemleri();
            admin.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Kiralama kira = new Kiralama();
            kira.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Kasa kasa = new Kasa();
            kasa.Show();
        }
    }
}
