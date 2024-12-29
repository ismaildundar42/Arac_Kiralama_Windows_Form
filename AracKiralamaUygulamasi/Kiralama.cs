using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AracKiralamaUygulamasi
{
    public partial class Kiralama : Form
    {
        public Kiralama()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");
        int toplam = 0;

        private string selectedPlaka = ""; // Seçilen aracın plakasını tutacak değişken

        private void Kiralama_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select plaka from tbl_araclar where durum='Bosta'", baglanti);
            NpgsqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                // Her plakayı ComboBox'a ekle
                cmbKiralanacakPlaka.Items.Add(dr["plaka"].ToString());
            }
            dr.Close(); // DataReader'ı kapat
            baglanti.Close();

            // müşteri ad soyad cmb çekilmesi
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("select adsoyad from tbl_musteriler", baglanti);
            NpgsqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                // Her plakayı ComboBox'a ekle
                cmbMusteriAdSoyad.Items.Add(dr2["adsoyad"].ToString());
            }
            dr2.Close(); // DataReader'ı kapat
            baglanti.Close();



            string sorgu = "select * from tbl_kiralama";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        // dataGridView'de satıra tıklanınca plaka bilgisini alıyoruz
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan hücre veri satırına aitse (başlık satırı değilse)
            if (e.RowIndex >= 0)
            {
                // Seçilen satırdaki verileri alıyoruz
                string plaka = dataGridView1.Rows[e.RowIndex].Cells["plaka"].Value.ToString();

                // Plaka bilgisini bir değişkende tutalım
                selectedPlaka = plaka;
            }
        }

        // Teslim al butonuna tıklanınca işlemleri yapıyoruz
        private void button1_Click(object sender, EventArgs e)
        {
            // Eğer bir plaka seçildiyse
            if (!string.IsNullOrEmpty(selectedPlaka))
            {
                // Veritabanı bağlantısını açıyoruz
                baglanti.Open();

                // Başlatılan işlemi bir transaction içinde gerçekleştiriyoruz
                using (var transaction = baglanti.BeginTransaction())
                {
                    try
                    {
                        // Araç durumunu 'Bosta' olarak güncelliyoruz
                        NpgsqlCommand komut1 = new NpgsqlCommand("UPDATE tbl_araclar SET durum='Bosta' WHERE plaka=@plaka", baglanti, transaction);
                        komut1.Parameters.AddWithValue("@plaka", selectedPlaka);
                        komut1.ExecuteNonQuery();

                        // Kiralama kaydını siliyoruz
                        NpgsqlCommand komut2 = new NpgsqlCommand("DELETE FROM tbl_kiralama WHERE plaka=@plaka", baglanti, transaction);
                        komut2.Parameters.AddWithValue("@plaka", selectedPlaka);
                        komut2.ExecuteNonQuery();

                        // Her iki işlemi de başarılı bir şekilde tamamladıysak işlemi onaylıyoruz
                        transaction.Commit();

                        // Kiralama listemizi güncelliyoruz
                        btnListe_Click(sender, e);

                        // Başarılı bir işlem mesajı gösteriyoruz
                        MessageBox.Show("Araç teslim alındı ve kiralama kaydı güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Veritabanı bağlantısını kapatıyoruz
                        baglanti.Close();
                    }
                    catch (Exception ex)
                    {
                        // Eğer hata olursa işlemi geri alıyoruz
                        transaction.Rollback();
                        MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen teslim almak istediğiniz aracı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into tbl_kiralama (plaka,isim,tc,baslangic,bitis,toplam) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbKiralanacakPlaka.Text);
            komut.Parameters.AddWithValue("@p2", cmbMusteriAdSoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtMusteriTC.Text);
            komut.Parameters.AddWithValue("@p4", DateTime.Parse(dateTimePicker1.Text));
            komut.Parameters.AddWithValue("@p5", DateTime.Parse(dateTimePicker2.Text));
            komut.Parameters.AddWithValue("@p6", int.Parse(txtUcret.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("update tbl_araclar set durum='Dolu' where plaka=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", cmbKiralanacakPlaka.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Araç kiralama işlemi başarıyla gerçekleşti!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnListe_Click(sender, e);
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value.Date; // Sadece tarih kısmını al
            DateTime date2 = dateTimePicker2.Value.Date; // Sadece tarih kısmını al

            // İki tarih arasındaki farkı hesapla
            TimeSpan difference = date2 - date1;

            // Fark negatifse (örneğin, başlangıç tarihi bitiş tarihinden büyükse)
            if (difference.TotalDays < 0)
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden önce olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gün farkını al
            int dayDifference = difference.Days;

            // En az 1 gün ücreti olması için kontrol ekleyelim
            if (dayDifference == 0)
            {
                dayDifference = 1;
            }

            // Gün farkını 2000 ile çarp
            int toplam = dayDifference * 2000;
            txtUcret.Text = toplam.ToString();
        }


        private void btnListe_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from tbl_kiralama";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
