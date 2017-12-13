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

namespace QLCafe
{
    public partial class frmThemKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmThemKhachHang()
        {
            InitializeComponent();
        }

        public delegate void GetKT(int KT);
        public GetKT KTTrangThai;
        private void btnHuy_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Bạn thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            //{
                this.Close();
           // }
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNhomKhachHang.Text != "Chọn nhóm khách hàng" && txtTenKhachHang.Text != "" && txtDienThoai.Text != "" && txtMaKhachHang.Text != "")
            {
                string TenKhachHang = txtTenKhachHang.Text;
                int IDNhom = Int32.Parse(cmbNhomKhachHang.EditValue.ToString());
                DateTime NgaySinh = DateTime.Parse(dateTimeNgaySinh.Text);
                string DienThoai = txtDienThoai.Text;
                string DiaChi = txtDiaChi.Text;
                string CMND = txtCMND.Text;
                string GhiChu = txtGhiChu.Text;
                string MaKhachHang = txtMaKhachHang.Text.ToString();
                if (DAO_KhachHang.KiemTraSDTKhachHang(DienThoai) == 0)
                {
                    if (DAO_KhachHang.KiemTraMaKhachHang(MaKhachHang) == 0)
                    {
                        if (DAO_KhachHang.ThemKhachHang(IDNhom, MaKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, GhiChu) == true)
                        {
                            if (KTTrangThai != null)
                            {
                                KTTrangThai(1);
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thêm khách hàng không thành công?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Số điện thoại này đã được đăng ký", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin có dấu (*)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmThemKhachHang_Load(object sender, EventArgs e)
        {
            List<DTO_NhomKhachHang> danhsach = DAO_NhomKhachHang.Instance.listNhomKhachHang();
            cmbNhomKhachHang.Properties.DataSource = danhsach;
            cmbNhomKhachHang.Properties.ValueMember = "ID";
            cmbNhomKhachHang.Properties.DisplayMember = "TenNhomKhachHang";
        }
    }
}