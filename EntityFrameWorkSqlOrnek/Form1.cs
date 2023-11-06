using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameWorkSqlOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void BtnListele_Click(object sender, EventArgs e)
        {

            DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
            dataGridView1.DataSource = db.TBLOGRENCİ.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;


        }

        private void BtnDersListesi_Click_1(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=EMIR\SQLEXPRESS;Initial Catalog=DbSınavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("Select * from DERSTABLO", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {

            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NOTID, 
                            item.TBLOGRENCİ.AD,
                            item.TBLOGRENCİ.SOYAD,
                            item.DERSTABLO.DERSAD,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA,
                            item.DURUM


                        };

            dataGridView1.DataSource = query.ToList();


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {


            TBLOGRENCİ t = new TBLOGRENCİ();
            t.AD = textAd.Text;
            t.SOYAD = textSoyad.Text;
            db.TBLOGRENCİ.Add(t); // t nesnesini ekle
            db.SaveChanges(); // değişiklikleri kaydet
            MessageBox.Show("Öğrenci Listeye Eklendi");

        }

        private void BtnSil_Click(object sender, EventArgs e)

        {

            int id = Convert.ToInt32(textOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);
            db.TBLOGRENCİ.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Silindi");


        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(textOgrenciId.Text);
            var x = db.TBLOGRENCİ.Find(id);

            x.AD = textAd.Text;
            x.SOYAD = textSoyad.Text;
            x.FOTOGRAF = textFotograf.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Güncellendi");



        }

        private void BtnProsedür_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();

        }

        private void BtnBul_Click(object sender, EventArgs e)

        {
            dataGridView1.DataSource = db.TBLOGRENCİ.Where(x => x.AD == textAd.Text | x.SOYAD == textSoyad.Text).ToList();

        }

        private void textAd_TextChanged(object sender, EventArgs e)
        {

            string aranan = textAd.Text;
            var degerler = from item in db.TBLOGRENCİ
                           where item.AD.Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnLingEntity_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCİ> liste1 = db.TBLOGRENCİ.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }

            if (radioButton2.Checked == true)
            {

                //DESCENDİNG AZALAN SIRALAMA DESC ARTAN SIRALAMA

                List<TBLOGRENCİ> liste2 = db.TBLOGRENCİ.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked == true)
            {

                List<TBLOGRENCİ> liste3 = db.TBLOGRENCİ.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;

            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton4.Checked == true)
            {

                List<TBLOGRENCİ> liste4 = db.TBLOGRENCİ.OrderBy(p => p.AD).Skip(3).ToList();
                dataGridView1.DataSource = liste4;

            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton5.Checked == true)
            {

                List<TBLOGRENCİ> liste5 = db.TBLOGRENCİ.Where(p => p.AD.StartsWith("A")).ToList();
                dataGridView1.DataSource = liste5;

            }

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton6.Checked == true)
            {

                List<TBLOGRENCİ> liste6 = db.TBLOGRENCİ.Where(p => p.AD.EndsWith("A")).ToList();
                dataGridView1.DataSource = liste6;

            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton7.Checked == true)
            {

                bool deger = db.TBLKULUPLER.Any();
                MessageBox.Show(deger.ToString(), "Bilgi" , MessageBoxButtons.OK , MessageBoxIcon.Information);

            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton8.Checked == true)
            {

                int toplam = db.TBLOGRENCİ.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton9.Checked == true)
            {

                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Sınav1 Toplamı : " + toplam.ToString(), "Toplam Sınav1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

             if (radioButton10.Checked == true)
            {

                var toplam = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("Sınav1 Ortalaması : " + toplam.ToString(), "Ortalama Sınav1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton11.Checked == true)
            {

                var toplam = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("Sınav1 Max : " + toplam.ToString(), "Max Sınav1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton12.Checked == true)
            {

                var toplam = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("Sınav1 Min : " + toplam.ToString(), "Min Sınav1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
               
            if(radioButton13.Checked == true)
            {
    
                     var ortyuksek = db.TBLNOTLAR.Average(p => p.SINAV1);
                     List<NOTLISTESI_Result> liste = db.NOTLISTESI().Where(p => p.SINAV1 > ortyuksek).ToList();
                     dataGridView1.DataSource = liste;
    
                }
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton14.Checked == true)
            {

                var enyuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                List<NOTLISTESI_Result> liste = db.NOTLISTESI().Where(p => p.SINAV1 == enyuksek).ToList();
                dataGridView1.DataSource = liste;

            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {

            var sorgu = from d1 in db.TBLNOTLAR

                        join d2 in db.TBLOGRENCİ

                        on d1.OGR equals d2.ID

                        join d3 in db.DERSTABLO

                        on d1.DERS equals d3.DERSID

                        select new

                        {
                            ÖĞRENCİ = d2.AD + " " + d2.SOYAD,
                            DERS = d3.DERSAD,

                            // SOYAD = d2.SOYAD,
                            

                            SINAV1 = d1.SINAV1, 
                            SINAV2 = d1.SINAV2,
                            SINAV3 = d1.SINAV3 ,
                            ORTALAMA = d1.ORTALAMA,
                            DURUM = d1.DURUM,
                            NOTID = d1.NOTID,
                            
                        
                        };

            dataGridView1.DataSource = sorgu.ToList();

                        // equals karşılaştırma yapıyor
        }
    }

}

