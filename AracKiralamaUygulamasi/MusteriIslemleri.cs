using Npgsql;
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
    public partial class MusteriIslemleri : Form
    {
        public MusteriIslemleri()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");

        private void MusteriIslemleri_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from tbl_musteriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into tbl_musteriler (tc,adsoyad,telefon,mail,cinsiyet) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtTC.Text);
            komut.Parameters.AddWithValue("@p2", txtAd.Text);
            komut.Parameters.AddWithValue("@p3", txtTelefon.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            btnListele_Click(sender, e);

            MessageBox.Show("Müşteri ekleme işlemi başarıyla gerçekleşti!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from tbl_musteriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Seçili satırın index numarasını al
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;

            // Eğer geçerli bir satır seçilmişse
            if (secilenSatir >= 0)
            {
                // Seçilen satırdaki verilere eriş
                DataGridViewRow satir = dataGridView1.Rows[secilenSatir];

                // Güncelleme işlemi için SQL sorgusu
                string sorgu = "UPDATE tbl_musteriler SET tc = @p1, adsoyad = @p2, telefon = @p3, mail = @p4, cinsiyet = @p5 WHERE tc = @p6";

                // Veritabanına bağlan
                baglanti.Open();

                // Komut oluştur
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                // Parametreleri ekle
                komut.Parameters.AddWithValue("@p1", txtTC.Text);
                komut.Parameters.AddWithValue("@p2", txtAd.Text);
                komut.Parameters.AddWithValue("@p3", txtTelefon.Text);
                komut.Parameters.AddWithValue("@p4", txtMail.Text);
                komut.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
                komut.Parameters.AddWithValue("@p6", satir.Cells["tc"].Value.ToString()); // Güncellenmesi gereken satırın tc değeri

                // Komutu çalıştır
                komut.ExecuteNonQuery();

                // Bağlantıyı kapat
                baglanti.Close();

                // Güncelleme işlemi başarılı mesajı
                MessageBox.Show("Müşteri bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Güncelleme sonrası listeyi yenile
                btnListele_Click(sender, e);
            }
            else
            {
                // Eğer hiçbir satır seçilmediyse
                MessageBox.Show("Lütfen güncellemek için bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Tıklanan satırın index numarasını al
            int secilenSatir = e.RowIndex;

            // Eğer başlık satırına tıklanmadıysa (e.RowIndex >= 0)
            if (secilenSatir >= 0)
            {
                // Seçilen satırdaki verilere ulaş
                DataGridViewRow satir = dataGridView1.Rows[secilenSatir];

                // TextBox'lara bu verileri aktar
                txtTC.Text = satir.Cells["tc"].Value.ToString();
                txtAd.Text = satir.Cells["adsoyad"].Value.ToString();
                txtTelefon.Text = satir.Cells["telefon"].Value.ToString();
                txtMail.Text = satir.Cells["mail"].Value.ToString();
                cmbCinsiyet.Text = satir.Cells["cinsiyet"].Value.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Seçili satırın index numarasını al
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;

            // Eğer geçerli bir satır seçilmişse
            if (secilenSatir >= 0)
            {
                // Seçilen satırdaki verilere eriş
                DataGridViewRow satir = dataGridView1.Rows[secilenSatir];

                // Kullanıcıdan silme işlemi için onay al
                DialogResult onay = MessageBox.Show("Bu müşteriyi silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Eğer kullanıcı 'Evet' derse
                if (onay == DialogResult.Yes)
                {
                    // Silmek için SQL sorgusu
                    string sorgu = "DELETE FROM tbl_musteriler WHERE tc = @p1";

                    // Veritabanına bağlan
                    baglanti.Open();

                    // Komut oluştur
                    NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                    // Parametreyi ekle
                    komut.Parameters.AddWithValue("@p1", satir.Cells["tc"].Value.ToString());  // Seçilen satırın TC numarasını kullan

                    // Komutu çalıştır
                    komut.ExecuteNonQuery();

                    // Bağlantıyı kapat
                    baglanti.Close();

                    // Silme işlemi başarılı mesajı
                    MessageBox.Show("Müşteri başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Listeyi yenile
                    btnListele_Click(sender, e);
                }
            }
            else
            {
                // Eğer hiçbir satır seçilmediyse
                MessageBox.Show("Lütfen silmek için bir müşteri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            // TextBox'a yazılan müşteri bilgilerini al (Örneğin, TC, ad veya telefon olabilir)
            string aramaKelimesi = txtMusteriAra.Text.Trim();

            // Eğer arama kelimesi boşsa, kullanıcıyı uyar
            if (string.IsNullOrEmpty(aramaKelimesi))
            {
                MessageBox.Show("Lütfen bir arama kelimesi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Müşteri tablosunda arama yapılacak sorguyu oluştur
            string sorgu = "SELECT * FROM tbl_musteriler WHERE tc LIKE @arama OR adsoyad LIKE @arama OR telefon LIKE @arama";

            // Veritabanına bağlan
            baglanti.Open();

            // Komut oluştur
            NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

            // Parametreyi ekle (arama kelimesine göre)
            komut.Parameters.AddWithValue("@arama", "%" + aramaKelimesi + "%");

            // Veriyi al
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // DataGridView'de göster
            dataGridView1.DataSource = ds.Tables[0];

            // Bağlantıyı kapat
            baglanti.Close();
        }

    }
}
