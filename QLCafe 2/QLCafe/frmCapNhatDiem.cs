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
            LamMoi();
        }
        public void LamMoi()
        {
            cmbLoaiHoaDon.SelectedIndex = 0;
            txtMaHoaDon.Text = "";
            txtMaHoaDon.Select();
            txtTongTien.Text = "0";
            txtDiem.Text = "0";
            cmbTrangThai.SelectedIndex = 0;
            cmbTenKhachHang.EditValue = "1";
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiem();
        }
        public void TimKiem()
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
                    case 2://cafe
                        TimKiemCaFe(MaHoaDon);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                txtMaHoaDon.Select();
                LamMoi();
                MessageBox.Show("Vui lòng nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void TimKiemBanHangLe(string MaHoaDon)
        {
            if (DAO_TimKiem.TimHoaDonBanHangLe(MaHoaDon) == true)
            {
                // tìm thấy
                DataTable dt = DAO_TimKiem.DanhSachHoaDonBanHangLeTimThay(MaHoaDon);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtTongTien.Text = dr["KhachCanTra"].ToString();
                    txtDiem.Text = (double.Parse(dr["KhachCanTra"].ToString()) / double.Parse(DAO_Setting.LayTienQuiDoiDiem().ToString())) + "";
                }
            }
            else
            {
                LamMoi();
                MessageBox.Show("Không tìm thấy Mã Hóa Đơn hoặc Hóa Đơn này đã áp dụng điểm tích lũy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void TimKiemBanVe(string MaHoaDon)
        {
            if (DAO_TimKiem.TimHoaDonBanVe(MaHoaDon) == true)
            {
                // tìm thấy
                DataTable dt = DAO_TimKiem.DanhSachHoaDonBanVeTimThay(MaHoaDon);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtTongTien.Text = dr["KhachCanTra"].ToString();
                    txtDiem.Text = (double.Parse(dr["KhachCanTra"].ToString()) / double.Parse(DAO_Setting.LayTienQuiDoiDiem().ToString())) + "";
                }
            }
            else
            {
                LamMoi();
                MessageBox.Show("Không tìm thấy Mã Hóa Đơn hoặc Hóa Đơn này đã áp dụng điểm tích lũy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                LamMoi();
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

        private void btnCapNhatDiem_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text != "")
            {
                if (txtDiem.Text != "0")
                {
                    if (cmbTenKhachHang.EditValue != "1")
                    {
                        if (MessageBox.Show("Xác nhận", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            string MaHoaDon = txtMaHoaDon.Text.ToString();
                            string IDKhachHang = cmbTenKhachHang.EditValue.ToString();
                            int LoaiHoaDon = cmbLoaiHoaDon.SelectedIndex;
                            switch (LoaiHoaDon)
                            {
                                case 0: // bán hàng lẻ
                                    CapNhatDiemBanHangLe(IDKhachHang, MaHoaDon);
                                    KhachHang();
                                    LamMoi();
                                    break;
                                case 1:// bán vé
                                    CapNhatDiemBanVe(IDKhachHang, MaHoaDon);
                                    KhachHang();
                                    LamMoi();
                                    break;
                                case 2://cafe
                                    CapNhatDiemCaFe(IDKhachHang, MaHoaDon);
                                    KhachHang();
                                    LamMoi();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Khách lẻ không áp dụng điểm tích lũy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Điểm cập nhật phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtMaHoaDon.Select();
                LamMoi();
                MessageBox.Show("Vui lòng nhập mã hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CapNhatDiemCaFe(string IDKhachHang, string MaHoaDon)
        {

            if (DAO_TimKiem.CapNhatIDKhachHangCaFe(IDKhachHang, MaHoaDon) == true)
            {
                int IDNhanVien = frmDangNhap.NguoiDung.Id;
                double DiemCong = double.Parse(txtDiem.Text.ToString());
                if (DAO_TimKiem.LichSuCapNhatDiem(IDKhachHang, IDNhanVien.ToString(), "Cập nhật điểm tích lũy: Nhà hàng - Cafe", DiemCong.ToString()) == true)
                {
                    DAO_ChiTietHoaDonChinh.CongDiemTichLuy(IDKhachHang, DiemCong.ToString());
                    MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void CapNhatDiemBanHangLe(string IDKhachHang, string MaHoaDon)
        {

            if (DAO_TimKiem.CapNhatIDKhachHangBanHangLe(IDKhachHang, MaHoaDon) == true)
            {
                int IDNhanVien = frmDangNhap.NguoiDung.Id;
                double DiemCong = double.Parse(txtDiem.Text.ToString());
                if (DAO_TimKiem.LichSuCapNhatDiem(IDKhachHang, IDNhanVien.ToString(), "Cập nhật điểm tích lũy: Bán hàng lẻ", DiemCong.ToString()) == true)
                {
                    DAO_ChiTietHoaDonChinh.CongDiemTichLuy(IDKhachHang, DiemCong.ToString());
                    MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void CapNhatDiemBanVe(string IDKhachHang, string MaHoaDon)
        {

            if (DAO_TimKiem.CapNhatIDKhachHangBanVe(IDKhachHang, MaHoaDon) == true)
            {
                int IDNhanVien = frmDangNhap.NguoiDung.Id;
                double DiemCong = double.Parse(txtDiem.Text.ToString());
                if (DAO_TimKiem.LichSuCapNhatDiem(IDKhachHang, IDNhanVien.ToString(), "Cập nhật điểm tích lũy: Bán vé", DiemCong.ToString()) == true)
                {
                    DAO_ChiTietHoaDonChinh.CongDiemTichLuy(IDKhachHang, DiemCong.ToString());
                    MessageBox.Show("Cập nhật điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void txtMaHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int Length = txtMaHoaDon.Text.Length;
                switch (Length)
                {
                    case 14:
                        cmbLoaiHoaDon.SelectedIndex = 2;
                        TimKiem();
                        break;
                    case 15:
                        cmbLoaiHoaDon.SelectedIndex = 0;
                        TimKiem();
                        break;
                    case 16:
                        cmbLoaiHoaDon.SelectedIndex = 1;
                        TimKiem();
                        break;
                    default:
                        cmbLoaiHoaDon.SelectedIndex = 0;
                        TimKiem();
                        break;
                }
            }
        }
    }
}