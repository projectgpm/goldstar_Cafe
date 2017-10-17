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
    public partial class frmDatBan : DevExpress.XtraEditors.XtraForm 
    {
        public delegate void GetString(String TenKhachHang, String DienThoai, DateTime GioDat);
        public GetString MyGetData;
        public frmDatBan()
        {
            InitializeComponent();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDatBan_Load(object sender, EventArgs e)
        {
           
            txtTenKhachHang.Select();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string TenKhachHang = txtTenKhachHang.Text;
            string DienThoai = txtDienThoai.Text;
            DateTime GioDat = DateTime.Parse(timeGioDat.Text);
            if (MyGetData != null)
            {
                MyGetData(TenKhachHang, DienThoai, GioDat);
            }
            this.Close();
        } 
    }
}