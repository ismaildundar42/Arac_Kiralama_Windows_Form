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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432;Database=arackiralamaproje;user ID=postgres;password=12345");

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminSifreGuncelle sayfa = new AdminSifreGuncelle();
            sayfa.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı giriş bilgilerini alıyoruz
            string kullaniciAdi = txtKadi.Text; // Kullanıcı adı alanından değer
            string sifre = txtSifre.Text; // Şifre alanından değer

            try
            {
                // Veritabanı bağlantısını açıyoruz
                baglanti.Open();

                // Giriş bilgilerini doğrulamak için sorgu oluşturuyoruz
                string sorgu = "SELECT COUNT(*) FROM tbl_adminler WHERE kadi = @kadi AND sifre = @sifre";
                NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti);

                // Parametre değerlerini ekliyoruz
                komut.Parameters.AddWithValue("@kadi",txtKadi.Text);
                komut.Parameters.AddWithValue("@sifre", txtSifre.Text);

                // Sorguyu çalıştırıyoruz ve eşleşen kayıt sayısını alıyoruz
                int sonuc = Convert.ToInt32(komut.ExecuteScalar());

                // Kullanıcı adı ve şifre eşleşiyorsa
                if (sonuc > 0)
                {
                    MessageBox.Show("Hoşgeldiniz!", "Başarılı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Menü formuna yönlendirme
                    Menu menu = new Menu(); // Menü formu oluşturulmalı
                    menu.Show();
                    this.Hide(); // Login formunu gizle
                }
                else
                {
                    // Kullanıcı adı veya şifre hatalıysa
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapat
                baglanti.Close();
            }
        }

    }
}
