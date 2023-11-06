using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameWorkSqlOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)

            {

                var degerler = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
                dataGridView1.DataSource = degerler.ToList();


            }



        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {



            if (radioButton3.Checked == true)

            {

                var degerler = db.TBLOGRENCİ.Where(x => x.AD == "ali");
                dataGridView1.DataSource = degerler.ToList();


            }


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButton2.Checked == true)

            {

                var degerler = db.TBLOGRENCİ.Where(x => x.AD == textBox2.Text || x.SOYAD == textBox2.Text);
                dataGridView1.DataSource = degerler.ToList();


            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton4.Checked == true)

            {

                var degerler = db.TBLOGRENCİ.Select(x => new { soyadı = x.SOYAD });
                dataGridView1.DataSource = degerler.ToList();




            }


        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton5.Checked == true)

            {

                var degerler = db.TBLOGRENCİ.Select(x => new { Ad = x.AD.ToUpper(), Soyadı = x.SOYAD.ToLower() });
                dataGridView1.DataSource = degerler.ToList();




            }


        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton6.Checked == true)

            {

                var degerler = db.TBLOGRENCİ.Select(x => new

                {
                    Ad = x.AD.ToUpper(),
                    Soyadı = x.SOYAD.ToLower()

                }).Where(x => x.Ad != "Ali");

                // şartlı seçme yaptım ali dışındakileri getirecek  


                dataGridView1.DataSource = degerler.ToList();




            }


        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {


            if (radioButton7.Checked == true)

            {

                var degerler = db.TBLNOTLAR.Select(x => new

                {

                    OgrenciAd = x.OGR,
                    Ortalama = x.ORTALAMA,
                    Durum=x.DURUM==true?"Geçti":"Kaldı"


                });                
                   


                dataGridView1.DataSource = degerler.ToList();



            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)

            {

                // SELECT MANY İKİ TABLOYU BİRLEŞTİRME İŞLEMİDİR
                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCİ.Where(y => y.ID == x.OGR), (x, y) =>

                new

                {

                    y.AD,
                    x.ORTALAMA

                });


                dataGridView1.DataSource = degerler.ToList();



            }

        }
    }

}
