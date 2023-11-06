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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {


            // var degerler = db.TBLOGRENCİ.OrderBy(x=>x.SEHIR).GroupBy(y=>y.SEHIR).Select

            //   (

            // z=>new 

            // { Sehir=z.Key,Toplam=z.Count()});

            // dataGridView1.DataSource = degerler.ToList();

            //label1.Text = db.TBLNOTLAR.Max(x => x.ORTALAMA).ToString();
            label1.Text= db.TBLNOTLAR.Min(x => x.ORTALAMA).ToString();
        
        }

       
    }
}
