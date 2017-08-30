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
using QLCafe.BUS;
using QLCafe.DTO;
using DevExpress.SpreadsheetSource.Implementation;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using QLCafe.DAO;
using DevExpress.XtraBars;

namespace QLCafe
{
    public partial class frmBanHang : DevExpress.XtraEditors.XtraForm
    {
       
        public frmBanHang()
        {
            InitializeComponent();
            DanhSachBan();
        }
        public static int IDBan = 0;
        public static DateTime GioVao;
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            timer1.Start();
            KhachHang();
            // WindowState = FormWindowState.Maximized;
            txtTongTien.ReadOnly = true;
            txtGiamGia.ReadOnly = true;
            txtKhachCanTra.ReadOnly = true;
            txtTienThoi.ReadOnly = true;
            txtDiemTichLuy.ReadOnly = true;
            txtKhachThanhToan.ReadOnly = true;
            txtTenDangNhap.Text = "Nhân viên: " + frmDangNhap.NguoiDung.Tennguoidung;
        }
        public  void DanhSachBan()
        {
            IDBan = 0;
            tblTable1.Controls.Clear();
            string IDChiNhanh = frmDangNhap.NguoiDung.Idchinhanh;
            DataTable dt = BUS_KhuVuc.DanhSachBanTheoKhuVuc(IDChiNhanh);
            DataRow dr11 = dt.Rows[0];
            btnTrong.Text = "Trống (" + BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 0) + ")";

            btnDatTruoc.Text = "Đã Đặt (" + BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 1) + ")";
            btnDatTruoc.ForeColor = Color.OrangeRed;
            btnDatTruoc.StyleController = null;
            btnDatTruoc.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDatTruoc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;

            btnCoNguoi.Text = "Có Người (" + BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 2) + ")";
            btnCoNguoi.ForeColor = Color.Red;
            btnCoNguoi.StyleController = null;
            btnCoNguoi.LookAndFeel.UseDefaultLookAndFeel = false;
            btnCoNguoi.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;

            float SLPhucVu = BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 2);
            float TongSLBan = BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 2) + BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 0) + BUS_BAN.DanhSachThongKe(dr11["ID"].ToString(), 1);
            float TyLePhucVu = SLPhucVu / (float)TongSLBan;
            txtTyLyPhucVu.Text = "Tỷ lệ phục vụ: " + Math.Round(TyLePhucVu, 2)*100 + "%";
            foreach (DataRow dr in dt.Rows)
            {
                string TenKhuVuc = dr["TenKhuVuc"].ToString();
                string IDKhuVuc = dr["ID"].ToString();
                List<DTO_BAN> tablelist = DAO_BAN.Instance.LoadTableList(IDKhuVuc);
                foreach (DTO_BAN item in tablelist)
                {
                    int TrangThai = item.Trangthai;
                    string TenBan = item.Tenban;
                    SimpleButton btn = new SimpleButton();
                    btn.Width = 80;
                    btn.Height = 80;
                    btn.Text = TenBan;
                    btn.Click += btn_Click;
                    btn.MouseDown +=btn_MouseDown;
                    btn.Tag = item;
                    switch (TrangThai)
                    {
                        case 0:
                            tblTable1.Controls.Add(btn);
                            btn.ToolTip = "Bàn trống";
                            break;
                        case 1:
                            btn.ForeColor = Color.OrangeRed;
                            btn.StyleController = null;
                            btn.LookAndFeel.UseDefaultLookAndFeel = false;
                            List<DTO_DatBan> thongtinnguoidat = DAO_DatBan.Instance.LoadTableList(item.Id);
                            foreach (DTO_DatBan dr1 in thongtinnguoidat)
                            {
                                btn.ToolTip = dr1.TenKhachHang + Environment.NewLine + dr1.DienThoai +  Environment.NewLine + dr1.GioDat;
                            }
                            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                            tblTable1.Controls.Add(btn);
                            break;
                        case 2:
                            btn.ForeColor = Color.Red;
                            btn.StyleController = null;
                            btn.LookAndFeel.UseDefaultLookAndFeel = false;
                            btn.ToolTip = "Bàn có người ngồi";
                            btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                            tblTable1.Controls.Add(btn);
                            break;
                    }
                }
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
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                 IDBan = 0;
                 IDBan = ((sender as SimpleButton).Tag as DTO_BAN).Id;
                 menuBan.ShowPopup(Control.MousePosition);
                 int TrangThai = ((sender as SimpleButton).Tag as DTO_BAN).Trangthai;
                 if (TrangThai == 2)
                 {
                     GioVao = DAO_BanHang.GioVao_IDBan(IDBan);
                 }
                 else
                 {
                     GioVao = DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                 }
            }
        }
       
        public void HienThiHoaDon(int id)
        {
            List<DTO_DanhSachMenu> DanhSachHoaDon = DAO_DanhSachMonAn.Instance.GetDanhSachMonAn(DAO_BanHang.IDHoaDon(id));
            //List<DTO_ChiTietHoaDon> DanhSachHoaDon = DAO_ChiTietHoaDon.Instance.ChiTietHoaDon(DAO_HoaDon.Instance.GetHoaDonByIDBan(id));
           
            gridControlCTHD.DataSource = null;
            gridControlCTHD.Refresh();
      
            gridControlCTHD.DataSource = DanhSachHoaDon;
            txtTongTien.Text = DAO_HoaDon.TongTienHoaDon(DAO_BanHang.IDHoaDon(id)).ToString();
            txtKhachCanTra.Text = DAO_HoaDon.KhachCanTra(DAO_BanHang.IDHoaDon(id)).ToString();
            
        }
        private void btn_Click(object sender, EventArgs e)
        {
            IDBan = ((sender as SimpleButton).Tag as DTO_BAN).Id;
            HienThiHoaDon(IDBan);
            txtKhachThanhToan.ReadOnly = false;
            //txtGioVao.Text = "Giờ Vào"
        }
       
        
        private void frmBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

     
        private void barButtonDatBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DAO_BAN.TrangThaiBan(IDBan) == 0)
            {
                frmDatBan fr = new frmDatBan();
                fr.MyGetData = new frmDatBan.GetString(GetValue);
                fr.ShowDialog();
            }
            else if (DAO_BAN.TrangThaiBan(IDBan) == 1)
            {
                MessageBox.Show("Bàn đã có người đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Bàn đã có người ngồi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetValue(String str1, String str2,DateTime a)
        {
            string TenKhachHang = str1;
            string DienThoai = str2;
            DateTime GioDat = a;
            bool KT = DAO_BAN.ThemKhachDatBan(TenKhachHang, DienThoai, GioDat, IDBan);
            if (KT == true)
            {
                DAO_BAN.DoiTrangThaiDatBan(IDBan);
                DanhSachBan();
                MessageBox.Show("Đặt bàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DanhSachBan();
                MessageBox.Show("Đặt bàn Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void barButtonXoaBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("Chuyển trạng thái bàn về Trống?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                bool KT = DAO_BAN.XoaBanVeMatDinh(IDBan);
                if (KT == true)
                {
                    DAO_HoaDon.XoaDatBan(IDBan);
                    DAO_DatBan.XoaKhachDat(IDBan);
                    DanhSachBan();
                   
                    gridControlCTHD.DataSource = null;
                    gridControlCTHD.Refresh();
                    MessageBox.Show("Cập Nhật Thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                   
                }
                else
                {
                    DanhSachBan();
                    MessageBox.Show("Cập Nhật Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void barButtonChonMon_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmGoiMon fr = new frmGoiMon();
            fr.MyGetData = new frmGoiMon.GetKT(GetValueGoiMon);
            fr.ShowDialog();
        }

        private void barButtonChuyenBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DAO_BAN.TrangThaiBan(IDBan) == 2)
            {
                frmChuyenBan fr = new frmChuyenBan();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể chuyển bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetValueGoiMon(int KT, int IDHoaDon)
        {
            if (KT == 1)
            {
                DanhSachBan();
                TinhTongTien(IDHoaDon);
                gridControlCTHD.DataSource = null;
                gridControlCTHD.Refresh();
                MessageBox.Show("Gọi Món Thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Gọi Món Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DanhSachBan();
            }
        }
        private void barButtonTachBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DAO_BAN.TrangThaiBan(IDBan) == 2)
            {
                frmChuyenBan fr = new frmChuyenBan();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể tách bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonGopBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DAO_BAN.TrangThaiBan(IDBan) == 2)
            {
                frmTachBan fr = new frmTachBan();
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể gộp bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtKhachThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            float KhachThanhToan = float.Parse(txtKhachThanhToan.EditValue.ToString());
            float KhachCanThanhToan = float.Parse(txtKhachCanTra.EditValue.ToString());
            //if (KhachThanhToan >= KhachCanThanhToan)
            //{
                txtTienThoi.Text = (KhachThanhToan - KhachCanThanhToan).ToString();
           // }
            //else
            //{
            //    MessageBox.Show("Khách thanh toán không đủ số tiền?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void cmbTenKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            txtDiemTichLuy.ReadOnly = false;
            DataTable tblThongTin = DAO_KhachHang.KhachHangID(cmbTenKhachHang.EditValue.ToString());
            if (tblThongTin.Rows.Count > 0)
            {
                DataRow dr = tblThongTin.Rows[0];
                txtMaKhachHang.Text = dr["MaKhachHang"].ToString();
               
                txtDienThoai.Text = dr["DienThoai"].ToString();
                txtCMND.Text = dr["CMND"].ToString();
                txtDiem.Text = dr["DiemTichLuy"].ToString();

            }
        }

        private void txtDiemTichLuy_EditValueChanged(object sender, EventArgs e)
        {

            int SoDiemCanDoi = Int32.Parse(txtDiemTichLuy.EditValue.ToString());
            float DiemTichLuy = DAO_Setting.DiemTichLuy(cmbTenKhachHang.EditValue.ToString());
            if (SoDiemCanDoi <= DiemTichLuy)
            {
                float SoTienDoi = DAO_Setting.LayDiemQuyDoiTien();
                float TongTien = float.Parse(txtTongTien.EditValue.ToString());
                txtGiamGia.Text = (SoTienDoi * SoDiemCanDoi) + "";
                txtKhachCanTra.Text = (TongTien - (SoTienDoi * SoDiemCanDoi)) + "";
                txtKhachThanhToan.Text = "0";
                txtTienThoi.Text = "0";
            }
            else
            {
                txtDiemTichLuy.Text = "0";
                MessageBox.Show("Điểm tích lũy của khách hàng không đủ?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            frmThemKhachHang fr = new frmThemKhachHang();
            fr.KTTrangThai = new frmThemKhachHang.GetKT(GetValueThemKhachHang);
            fr.ShowDialog();
        }
        public void GetValueThemKhachHang(int KT)
        {
            if (KT == 1)
            {
                KhachHang();
                MessageBox.Show("Thêm Thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Thêm không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControlCTHD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridView1.State != DevExpress.XtraGrid.Views.Grid.GridState.Editing)
            {
                if (MessageBox.Show("Bạn muốn xóa món này ra khỏi bàn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    int IDban = IDBan;
                    string MaHangHoa = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                    if (IDban != 0)
                    {
                        int IDHoaDon = DAO_BanHang.IDHoaDon(IDban);
                        if (DAO_BanHang.XoaMonAn(IDban, MaHangHoa, IDHoaDon) == true)
                        {
                            TinhTongTien(IDHoaDon);
                            HienThiHoaDon(IDban);
                            MessageBox.Show("Xóa món ăn thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            MessageBox.Show("Xóa món ăn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        public static void TinhTongTien(int IDHoaDon)
        {
            List<DTO_ChiTietHoaDon> danhsach = DAO_ChiTietHoaDon.Instance.ChiTietHoaDon(IDHoaDon);
            float TongTien = 0;
            foreach (DTO_ChiTietHoaDon item in danhsach)
            {
                TongTien = TongTien + item.ThanhTien;
            }
            DAO_HoaDon.CapNhatTongTien(IDHoaDon, TongTien.ToString(), TongTien.ToString());
        }

        private void btnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa món này ra khỏi bàn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int IDban = IDBan;
                string MaHangHoa = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                if (IDban != 0)
                {
                    int IDHoaDon = DAO_BanHang.IDHoaDon(IDban);
                    if (DAO_BanHang.XoaMonAn(IDban, MaHangHoa, IDHoaDon) == true)
                    {
                        TinhTongTien(IDHoaDon);
                        HienThiHoaDon(IDban);
                        MessageBox.Show("Xóa món ăn thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Xóa món ăn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void gridView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            // Sự kiện này để người ta không chuyển qua dòng khác được khi có lỗi xảy ra nè
            // Nó nhận giá trị e.Valid của gridView1_ValidateRow để ứng xử
            // neu e,Valid =True thì nó cho chuyển qua dòng khác hoặc làm tác vụ khác
            // và ngược lại
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gridView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            string TenHangHoa = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[1]).ToString();
            if (MessageBox.Show("Bạn muốn cập nhật số lượng cho món: " + TenHangHoa + "?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string MaHangHoa = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                int IDban = IDBan;
                int IDHoaDon = DAO_BanHang.IDHoaDon(IDban);
                int SLMoi = Int32.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[3]).ToString());
                float GiaTong =  float.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[7]).ToString());
                if (DAO_ChiTietHoaDon.CapNhatSoLuong(IDHoaDon, (SLMoi * GiaTong).ToString(), SLMoi.ToString(), MaHangHoa) == true)
                {
                    TinhTongTien(IDHoaDon);
                    HienThiHoaDon(IDban);
                    MessageBox.Show("Cập nhật số lượng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    HienThiHoaDon(IDban);
                    MessageBox.Show("Cập nhật số lượng không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
               
        }

       
      
    }
}