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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            

            FrmOgrenciDetay fr = new FrmOgrenciDetay();
            fr.id = MskOgrNumara.Text;
            fr.Show();
           


        }

        private void MskOgrNumara_TextChanged(object sender, EventArgs e)
        {
            if (MskOgrNumara.Text =="1111")
            {
                FrmOgretmenDetay fr = new FrmOgretmenDetay();
                fr.Show();
            }
        }
    }
}
