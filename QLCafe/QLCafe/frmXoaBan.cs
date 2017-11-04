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
using QLCafe.DAO;

namespace QLCafe
{
    public partial class frmXoaBan : DevExpress.XtraEditors.XtraForm
    {
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        public delegate void GetString(int KT, string LyDoXoa, int IDHoaDon,int IDBan);
        public GetString MyGetData;
        public frmXoaBan()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (txtLyDoXoa.Text != "")
            {
                if (MyGetData != null)
                {
                    MyGetData(1, txtLyDoXoa.Text.ToString(), IDHoaDon, IDBan);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lý do để xóa? ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}