using QLCafe.BUS;
using QLCafe.DAO;
using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QLCafe
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        
        public frmDangNhap()
        {
           
            InitializeComponent();
            txtTenDangNhap.Focus();
        }
        public static DTO_DangNhap NguoiDung;
        private void linkLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gpm.vn/");
        }

        private void linkCauHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCauHinhHeThong fr = new frmCauHinhHeThong();
            this.Hide();
            fr.ShowDialog();
            this.Show();
        }

        private void linkDoiMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDoiMatKhau fr = new frmDoiMatKhau();
            this.Hide();
            fr.ShowDialog();
            this.Show();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (DAO_TestKetNoi.IsConnectedToInternet("google.com"))
            {
                DangNhap();
            }
            else
            {
                MessageBox.Show("Không có kết nối Internet. Vui lòng kiểm tra lại?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DangNhap()
        {
            string TenDangNhap = txtTenDangNhap.Text.ToUpper();
            string MatKhau = DAO_Setting.GetSHA1HashData(txtMatKhau.Text.ToString());
            bool KT = BUS_DangNhap.KiemTraDangNhap(TenDangNhap, MatKhau);
            if (KT == true)
            {
                DataTable dt = BUS_DangNhap.ThongTin(TenDangNhap, MatKhau);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    NguoiDung = new DTO_DangNhap();
                    NguoiDung.Id = Int32.Parse(dr["ID"].ToString());
                    NguoiDung.Tendangnhap = dr["TenDangNhap"].ToString();
                    NguoiDung.Idchinhanh = dr["IDChiNhanh"].ToString();
                    NguoiDung.Manhanvien = dr["MaNhanVien"].ToString();
                    NguoiDung.Tennguoidung = dr["TenNguoiDung"].ToString();
                    NguoiDung.Sdt = dr["SDT"].ToString();
                    frmBanHang fr = new frmBanHang();
                    txtMatKhau.Text = "";
                    txtMatKhau.Select();
                    this.Hide();
                    fr.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Select();
        }

        private void ckeHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckeHienMatKhau.Checked == true)
            {
                txtMatKhau.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.Properties.UseSystemPasswordChar = true;
            }
        }




  
    }
}
