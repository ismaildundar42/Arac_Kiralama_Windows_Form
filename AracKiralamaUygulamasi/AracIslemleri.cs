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
    public partial class AracIslemleri : Form
    {
        public AracIslemleri()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");
        private void AracIslemleri_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from tbl_araclar";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into tbl_araclar (plaka,marka,model,yil,km,renk,yakit,durum) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", txtModel.Text);
            komut.Parameters.AddWithValue("@p4", txtUretimYili.Text);
            komut.Parameters.AddWithValue("@p5", txtKM.Text);
            komut.Parameters.AddWithValue("@p6", cmbRenk.Text);
            komut.Parameters.AddWithValue("@p7", cmbYakitTuru.Text);
            komut.Parameters.AddWithValue("@p8", cmbDurum.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            btnListele_Click(sender, e);
            MessageBox.Show("Araç ekleme işlemi başarıyla gerçekleşti!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from tbl_araclar";
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
                string sorgu = "UPDATE tbl_araclar SET plaka = @p1, marka = @p2, model = @p3, yil = @p4, km = @p5, renk = @p6, yakit = @p7, durum = @p8 WHERE plaka = @p9";

                // Veritabanına bağlan
                baglanti.Open();

                // Komut oluştur
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                // Parametreleri ekle
                komut.Parameters.AddWithValue("@p1", txtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", txtMarka.Text);
                komut.Parameters.AddWithValue("@p3", txtModel.Text);
                komut.Parameters.AddWithValue("@p4", txtUretimYili.Text);
                komut.Parameters.AddWithValue("@p5", txtKM.Text);
                komut.Parameters.AddWithValue("@p6", cmbRenk.Text);
                komut.Parameters.AddWithValue("@p7", cmbYakitTuru.Text);
                komut.Parameters.AddWithValue("@p8", cmbDurum.Text);
                komut.Parameters.AddWithValue("@p9", satir.Cells["plaka"].Value.ToString());  // Güncellenmesi gereken satırın plaka değeri

                // Komutu çalıştır
                komut.ExecuteNonQuery();

                // Bağlantıyı kapat
                baglanti.Close();

                // Güncelleme işlemi başarılı mesajı
                MessageBox.Show("Araç bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Güncelleme sonrası listeyi yenile
                btnListele_Click(sender, e);
            }
            else
            {
                // Eğer hiçbir satır seçilmediyse
                MessageBox.Show("Lütfen güncellemek için bir araç seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                txtPlaka.Text = satir.Cells["plaka"].Value.ToString();
                txtMarka.Text = satir.Cells["marka"].Value.ToString();
                txtModel.Text = satir.Cells["model"].Value.ToString();
                txtUretimYili.Text = satir.Cells["yil"].Value.ToString();
                txtKM.Text = satir.Cells["km"].Value.ToString();
                cmbRenk.Text = satir.Cells["renk"].Value.ToString();
                cmbYakitTuru.Text = satir.Cells["yakit"].Value.ToString();
                cmbDurum.Text = satir.Cells["durum"].Value.ToString();
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            // TextBox'a yazılan markayı al
            string marka = txtMarkaAra.Text.Trim();

            // Marka alanı boşsa kullanıcıyı uyar
            if (string.IsNullOrEmpty(marka))
            {
                MessageBox.Show("Lütfen bir marka girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Marka ile filtrelenmiş SQL sorgusu
            string sorgu = "SELECT * FROM tbl_araclar WHERE marka LIKE @marka";

            // Veritabanına bağlan
            baglanti.Open();

            // Komut oluştur
            NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

            // Parametreyi ekle
            komut.Parameters.AddWithValue("@marka", "%" + marka + "%");

            // Veriyi al
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);

            // DataGridView'de göster
            dataGridView1.DataSource = ds.Tables[0];

            // Bağlantıyı kapat
            baglanti.Close();
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

                // Silme onayı almak için MessageBox göster
                DialogResult dialogResult = MessageBox.Show("Bu aracı silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    // Silmek için SQL sorgusu
                    string sorgu = "DELETE FROM tbl_araclar WHERE plaka = @p1";

                    // Veritabanına bağlan
                    baglanti.Open();

                    // Komut oluştur
                    NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                    // Parametreyi ekle
                    komut.Parameters.AddWithValue("@p1", satir.Cells["plaka"].Value.ToString());  // Seçilen satırın plaka numarasını kullan

                    // Komutu çalıştır
                    komut.ExecuteNonQuery();

                    // Bağlantıyı kapat
                    baglanti.Close();

                    // Silme işlemi başarılı mesajı
                    MessageBox.Show("Araç başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Listeyi yenile
                    btnListele_Click(sender, e);

                    // TextBox'ları temizle
                    txtKM.Text = "";
                    txtMarka.Text = "";
                    txtModel.Text = "";
                    txtPlaka.Text = "";
                    txtUretimYili.Text = "";
                }
                else
                {
                    // Eğer kullanıcı "Hayır" dediyse
                    MessageBox.Show("Araç silme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Eğer hiçbir satır seçilmediyse
                MessageBox.Show("Lütfen silmek için bir araç seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
