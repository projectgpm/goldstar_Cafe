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
    public partial class frmXacNhan : DevExpress.XtraEditors.XtraForm
    {
        public delegate void GetString(int PhuThuTheoGio, int PhuThuTheoKhuVuc);
        public GetString getdate;
        public frmXacNhan()
        {
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            int PhuThuTheoGio = 0;
            int PhuThuTheoKhuVuc = 0;
            if (checkGio.Checked == true)
                PhuThuTheoGio = 1;
            if (checkKhuVuc.Checked == true)
                PhuThuTheoKhuVuc = 1;
            if (getdate != null)
            {
                getdate(PhuThuTheoGio, PhuThuTheoKhuVuc);
            }
            this.Close();
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

            this.Close();
           
        }

       
    }
}