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
using QLCafe.DTO;
using QLCafe.DAO;
using QLCafe.BUS;

namespace QLCafe
{
    public partial class frmKiemTraThemMayIn : DevExpress.XtraEditors.XtraForm
    {
        public frmKiemTraThemMayIn()
        {
            InitializeComponent();
            txtTenDangNhap.Focus();
        }
        public static DTO_QuanLy QuanLy;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void linkLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gpm.vn/");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void DangNhap()
        {
            string TenDangNhap = txtTenDangNhap.Text.ToUpper();
            string MatKhau = DAO_Setting.GetSHA1HashData(txtMatKhau.Text.ToString());
            bool KT = BUS_DangNhap.KiemTraDangNhap(TenDangNhap, MatKhau);
            if (KT == true)
            {
                DataTable db = DAO_DangNhap.QuanLy(TenDangNhap, MatKhau);
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    QuanLy = new DTO_QuanLy();
                    QuanLy.Id = Int32.Parse(dr["ID"].ToString());
                    QuanLy.Tendangnhap = dr["TenDangNhap"].ToString();
                    QuanLy.Idchinhanh = dr["IDChiNhanh"].ToString();
                    QuanLy.Manhanvien = dr["MaNhanVien"].ToString();
                    QuanLy.Tennguoidung = dr["TenNguoiDung"].ToString();
                    QuanLy.Sdt = dr["SDT"].ToString();
                    QuanLy.IDNhomNguoiDung = Int32.Parse(dr["IDNhomNguoiDung"].ToString());
                    DAO_Setting.ThemLichSuQuanLy(frmKiemTraThemMayIn.QuanLy.Id, frmKiemTraThemMayIn.QuanLy.IDNhomNguoiDung, frmKiemTraThemMayIn.QuanLy.Idchinhanh, "Đăng Nhập", "Cài đặt máy in");
                    frmCauHinhMayIn fr = new frmCauHinhMayIn();
                    txtMatKhau.Text = "";
                    txtMatKhau.Select();
                    this.Hide();
                    fr.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}