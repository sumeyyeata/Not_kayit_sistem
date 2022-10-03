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
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-5FU8Q6M;Initial Catalog=DbNotKayit;Integrated Security=True");

        public string id;
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = id;
            //Ad Soyad Çekme
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERS where OGRNUMARA=@p1", bgl); 
            komut.Parameters.AddWithValue("@p1",id);
            SqlDataReader dr = komut.ExecuteReader();

            
            
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2] + " " + dr[3]; // 2 sutun döner (Öğrenci Adı Soyadı)
                LblSinav1.Text = dr[4].ToString();
                LblSinav2.Text = dr[5].ToString();
                LblSinav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();
            }
            if (LblDurum.Text == "True")

            {

                LblDurum.Text = "GEÇTİ";

            }

            if (LblDurum.Text == "False")

            {

                LblDurum.Text = "KALDI";

            }

            bgl.Close();
        }
    }
}
