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
using QLCafe.DTO;

namespace QLCafe
{
    public partial class frmCapNhatDiem : DevExpress.XtraEditors.XtraForm
    {
        public frmCapNhatDiem()
        {
            InitializeComponent();
        }

        private void frmCapNhatDiem_Load(object sender, EventArgs e)
        {
            KhachHang();
            lblTenCongTy.Text = DAO_Setting.TenCongTy();
            lblDiaChi.Text = DAO_Setting.DiaChiCongTy();
            lblDienThoai.Text = DAO_Setting.DienThoaiCongTy();
            lblNgay.Text = "Ngày hôm nay: " + DateTime.Now.ToString("dd/MM/yyyy");
            txtTenDangNhap.Text = "Nhân viên: " + frmDangNhap.NguoiDung.Tennguoidung;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thoát ứng dụng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cmbLoaiHoaDon.SelectedIndex = 0;
            txtMaHoaDon.Text = "";
            txtMaHoaDon.Select();
            txtTongTien.Text = "0";
            txtDiem.Text = "0";
            cmbTrangThai.SelectedIndex = 0;
            cmbTenKhachHang.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text != "")
            {
                string MaHoaDon = txtMaHoaDon.Text.ToString();
                int LoaiHoaDon = cmbLoaiHoaDon.SelectedIndex;
                switch (LoaiHoaDon)
                {
                    case 0: // bán hàng lẻ
                        TimKiemBanHangLe(MaHoaDon);
                        break;
                    case 1:// bán vé
                        TimKiemBanVe(MaHoaDon);
                        break;
                    case 2://công viên nước
                        TimKiemCaFe(MaHoaDon);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                txtMaHoaDon.Select();
                MessageBox.Show("Vui lòng nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void TimKiemBanHangLe(string MaHoaDon)
        {

        }
        public void TimKiemBanVe(string MaHoaDon)
        {

        }
        public void TimKiemCaFe(string MaHoaDon)
        {
            if (DAO_TimKiem.TimHoaDonCaFe(MaHoaDon) == true)
            {
                // tìm thấy
                DataTable dt = DAO_TimKiem.DanhSachHoaDonCaFeTimThay(MaHoaDon);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtTongTien.Text = dr["KhachCanTra"].ToString();
                    txtDiem.Text = (double.Parse(dr["KhachCanTra"].ToString()) / double.Parse(DAO_Setting.LayTienQuiDoiDiem().ToString())) + "";
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy Mã Hóa Đơn hoặc Hóa Đơn này đã áp dụng điểm tích lũy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void KhachHang()
        {
            cmbTenKhachHang.Properties.DataSource = null;
            cmbTenKhachHang.Refresh();
            List<DTO_KhachHang> listKhachHang = DAO_KhachHang.Instance.listKhachHang();
            cmbTenKhachHang.Properties.DataSource = listKhachHang;
            cmbTenKhachHang.Properties.ValueMember = "ID";
            cmbTenKhachHang.Properties.DisplayMember = "TenKhachHang";

        }
       
    }
}