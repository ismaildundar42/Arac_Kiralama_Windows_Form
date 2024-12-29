using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AracKiralamaUygulamasi
{
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");

        private void Kasa_Load(object sender, EventArgs e)
        {
            try
            {
                // Araç sayısını fonksiyondan al
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand("SELECT fn_toplam_arac_sayisi()", baglanti);
                int toplamArac = Convert.ToInt32(komut1.ExecuteScalar()); // ExecuteScalar ile sonucu al
                txtToplamAracAdet.Text = toplamArac.ToString(); // Değeri TextBox'a yaz
                baglanti.Close();

                // Müşteri sayısını fonksiyondan al
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("SELECT fn_toplam_musteri_sayisi()", baglanti);
                int toplamMusteri = Convert.ToInt32(komut2.ExecuteScalar()); // ExecuteScalar ile sonucu al
                txtToplamMusteri.Text = toplamMusteri.ToString(); // Değeri TextBox'a yaz
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close(); // Bağlantıyı kapat
                }
            }

            try
            {
                baglanti.Open();
                // Toplam geliri tbl_toplam_gelir tablosundan al
                NpgsqlCommand komut = new NpgsqlCommand("SELECT toplam_gelir FROM tbl_toplam_gelir", baglanti);
                decimal toplamGelir = Convert.ToDecimal(komut.ExecuteScalar());
                txtToplamGelir.Text = toplamGelir.ToString("N2"); // Sayıyı formatlayarak TextBox'a yaz
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close(); // Bağlantıyı kapat
                }
            }

        }
    }
}
