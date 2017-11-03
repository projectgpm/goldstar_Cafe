using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QLCafe
{
    public partial class frmPhaChe : DevExpress.XtraEditors.XtraForm
    {
        public frmPhaChe()
        {
            InitializeComponent();
        }

        private void ckTuDong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTuDong.Checked == true)
            {
                txtSoGiay.Enabled = true;
            }
            else
            {
                txtSoGiay.Enabled = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}