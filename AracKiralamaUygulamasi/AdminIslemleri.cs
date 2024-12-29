using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AracKiralamaUygulamasi
{
    public partial class AdminIslemleri : Form
    {
        public AdminIslemleri()
        {
            InitializeComponent();
        }

        // Bağlantı dizesi
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");

        // Admin ekleme işlemi
        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı veya şifre boşsa işlem yapma
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifreyi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                baglanti.Open();
                // Admin eklemek için SQL komutu
                NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO tbl_adminler (kadi, sifre) VALUES (@p1, @p2)", baglanti);
                komut.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();

                // Ekleme işlemi sonrası DataGridView'i güncelle
                AdminIslemleri_Load(sender, e);

                MessageBox.Show("Admin ekleme işlemi başarıyla gerçekleşti!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                baglanti.Close();
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form yüklendiğinde adminlerin listesini getir
        private void AdminIslemleri_Load(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "SELECT * FROM tbl_adminler";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifre boşsa işlem yapma
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifreyi girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Güncellenecek adminin kadi'sini alıyoruz
                string kadi = txtKullaniciAdi.Text;

                // Veritabanına bağlan
                baglanti.Open();

                // SQL Update komutu oluştur
                NpgsqlCommand komut = new NpgsqlCommand("UPDATE tbl_adminler SET kadi = @p1, sifre = @p2 WHERE kadi = @p3", baglanti);
                komut.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                komut.Parameters.AddWithValue("@p3", kadi); // Eski kadi'sini kullanarak güncelleme yapıyoruz

                // Komutu çalıştır
                komut.ExecuteNonQuery();
                baglanti.Close();

                // Güncelleme sonrası listeyi yenile
                AdminIslemleri_Load(sender, e);

                MessageBox.Show("Admin bilgileri başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                baglanti.Close();
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seçilen satırın index'ini al
            int secilenSatir = e.RowIndex;

            // Satırdaki verilere eriş
            if (secilenSatir >= 0)
            {
                DataGridViewRow satir = dataGridView1.Rows[secilenSatir];

                // Verileri TextBox'lara yükle
                txtKullaniciAdi.Text = satir.Cells["kadi"].Value.ToString();
                txtSifre.Text = satir.Cells["sifre"].Value.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Seçilen satırın index'ini al
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;

            // Eğer geçerli bir satır seçildiyse
            if (secilenSatir >= 0)
            {
                // Seçilen satırdaki verilere eriş
                DataGridViewRow satir = dataGridView1.Rows[secilenSatir];

                // Silmek için kullanılacak kadi bilgisi
                string kadi = satir.Cells["kadi"].Value.ToString();

                // Silme işlemine onay al
                DialogResult dialogResult = MessageBox.Show("Bu admini silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Veritabanına bağlan
                        baglanti.Open();

                        // SQL komutu: admini sil
                        NpgsqlCommand komut = new NpgsqlCommand("DELETE FROM tbl_adminler WHERE kadi = @p1", baglanti);
                        komut.Parameters.AddWithValue("@p1", kadi);  // Seçilen adminin kadi'sini parametre olarak ekle

                        // Komutu çalıştır
                        komut.ExecuteNonQuery();
                        baglanti.Close();

                        // Silme işlemi başarılı mesajı
                        MessageBox.Show("Admin başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Listeyi güncelle
                        AdminIslemleri_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        baglanti.Close();
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir admin seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
