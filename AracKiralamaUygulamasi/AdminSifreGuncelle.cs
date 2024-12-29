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
    public partial class AdminSifreGuncelle : Form
    {
        public AdminSifreGuncelle()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");

        private void AdminSifreGuncelle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve yeni şifre alanlarını alıyoruz
            string kullaniciAdi = txtKadi.Text.Trim();
            string yeniSifre = txtSifre.Text.Trim();

            // Kullanıcı adı veya şifre boş ise uyarı ver
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(yeniSifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve yeni şifre alanlarını doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Veritabanı bağlantısını açıyoruz
                baglanti.Open();

                // Şifre güncelleme sorgusu
                string sorgu = "UPDATE tbl_adminler SET sifre = @yeniSifre WHERE kadi = @kadi";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                // Parametre değerlerini ekliyoruz
                komut.Parameters.AddWithValue("@yeniSifre", txtSifre.Text);
                komut.Parameters.AddWithValue("@kadi", txtKadi.Text);

                // Sorguyu çalıştırıyoruz
                int etkilenenSatir = komut.ExecuteNonQuery();

                if (etkilenenSatir > 0)
                {
                    // Şifre güncelleme başarılı
                    MessageBox.Show("Şifre başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kullanıcı adı bulunamadı
                    MessageBox.Show("Kullanıcı adı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Veritabanı bağlantısını kapatıyoruz
                baglanti.Close();
            }
        }
    }
}
