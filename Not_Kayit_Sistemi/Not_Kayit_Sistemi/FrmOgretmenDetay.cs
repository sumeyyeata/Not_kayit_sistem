using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-5FU8Q6M;Initial Catalog=DbNotKayit;Integrated Security=True");
        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Öğrenci Kaydetme
            bgl.Open();
            SqlCommand komutkaydet = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@r1,@r2,@r3)", bgl);
            komutkaydet.Parameters.AddWithValue("@r1", MskNumara.Text);
            komutkaydet.Parameters.AddWithValue("@r2", TxtAd.Text);
            komutkaydet.Parameters.AddWithValue("@r3", TxtSoyad.Text);

            komutkaydet.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Öğrenci Sisteme Kaydedildi.");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS); //Yeni eklenen öğrenciyi listeler.
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Seçilen herhangi bir hücredeki satırı (SINAVLARINI) TEXBOXLARA AKTARIR

            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            string durum;
            //Sınavları Güncelleme
            double sinav1,sinav2,sinav3, ortalama;
            sinav1 = double.Parse(TxtSinav1.Text);
            sinav2 = double.Parse(TxtSinav2.Text);
            sinav3 = double.Parse(TxtSinav3.Text);
            ortalama = (sinav1+sinav2+sinav3)/ 3;
            LblOrtalama.Text = ortalama.ToString();

            if (ortalama>=50)
            {
                durum = "true";
            }

            else
            {
                durum = "false";
            }

            //VeriTabanında Güncelleme 
            bgl.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1, OGRS2=@P2, OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6" , bgl);
            komut.Parameters.AddWithValue("@P1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@P2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@P3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MskNumara.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");

            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS); //Otomatik doldurma komutu


            //Geçen Öğrenci Sayısı
            bgl.Open();
            SqlCommand komut2 = new SqlCommand("Select Count(*) From TBLDERS where DURUM=1", bgl);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblGecenSayisi.Text = dr2[0].ToString();  //Burada ki sıfırıncı index -> select count * da tek tablo dönünce toplamı veren ilk index
                
            }

            bgl.Close();

            bgl.Open();
            SqlCommand komut3 = new SqlCommand("Select Count(*) From TBLDERS where DURUM=0", bgl);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblKalanSayisi.Text = dr3[0].ToString();  //Burada ki sıfırıncı index -> select count * da tek tablo dönünce toplamı veren ilk index

            }

            bgl.Close();




        }
    }
}
