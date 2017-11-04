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
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using System.Globalization;
using QLCafe.Report;
using DevExpress.XtraReports.UI;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraGrid.Views.Grid;

namespace QLCafe
{
    public partial class frmBanHang : DevExpress.XtraEditors.XtraForm
    {
       
        public frmBanHang()
        {
            InitializeComponent();
        }
        public static int IDBan = 0;
        public static int TabActive = 0;
        public static string NameTabActive = null;
        public static string TenKhuVuc = null;
        //public static DateTime GioVao;
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            timer1.Start();
            KhachHang();
            DanhSachBan();
            // WindowState = FormWindowState.Maximized;
            lblNgay.Text = "Ngày hôm nay: " + DateTime.Now.ToString("dd/MM/yyyy");
            lblTenCongTy.Text = DAO_Setting.TenCongTy();
            lblDiaChi.Text = DAO_Setting.DiaChiCongTy();
            lblDienThoai.Text = DAO_Setting.DienThoaiCongTy();
            txtTongTien.ReadOnly = true;
            //txtTienGio.ReadOnly = true;
            txtKhachCanTra.ReadOnly = true;
            txtTienThoi.ReadOnly = true;
            txtKhachThanhToan.ReadOnly = true;
            txtTenDangNhap.Text = "Nhân viên: " + frmDangNhap.NguoiDung.Tennguoidung;
        }
        public void AddTabControl(string name, string ID, FlowLayoutPanel layout)
        {
            //kiểm tra tabtrung
            bool KT = false;
            foreach (XtraTabPage tabitem in xtraTabControlDanhSach.TabPages)
            {
                if (tabitem.Name == ID)
                {
                    KT = true;
                    xtraTabControlDanhSach.SelectedTabPage = tabitem;
                }
            }
            if (KT == false)
            {
                xtraTabControlDanhSach.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Colibri", 11, System.Drawing.FontStyle.Bold);
                xtraTabControlDanhSach.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Regular); 
                DAO_BanHang.AddTabControll(xtraTabControlDanhSach, name, ID, layout);
                
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
        public void ClearTabControl()
        {
            xtraTabControlDanhSach.TabPages.Clear();
        }
        public void DanhSachBan()
        {
            ClearTabControl();
            string IDChiNhanh = frmDangNhap.NguoiDung.Idchinhanh;
            DataTable dt = BUS_KhuVuc.DanhSachBanTheoKhuVuc(IDChiNhanh);
            if (dt.Rows.Count > 0)
            {
                ThongKe(dt);
            }
            else
            {
                MessageBox.Show("Danh sách bàn trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            foreach (DataRow dr in dt.Rows)
            {
                string TenKhuVucNull = dr["TenKhuVuc"].ToString();
                string IDKhuVucNull = dr["ID"].ToString();
                FlowLayoutPanel layout = new FlowLayoutPanel();
                layout.Dock = DockStyle.Fill;
                layout.AutoScroll = true;
                AddTabControl(TenKhuVucNull, IDKhuVucNull, layout);
                BanKhuVuc(IDKhuVucNull, layout);
            }
            xtraTabControlDanhSach.SelectedTabPageIndex = TabActive;
        }
        public void ThongKe(DataTable tblThongTin)
        {
            DataRow dr11 = tblThongTin.Rows[0];
            btnTrong.Text = "Trống (" + BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 0) + ")";
            btnDatTruoc.Text = "Đã Đặt (" + BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 1) + ")";
            btnDatTruoc.ForeColor = Color.OrangeRed;
            btnDatTruoc.StyleController = null;
            btnDatTruoc.LookAndFeel.UseDefaultLookAndFeel = false;
            btnDatTruoc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            btnCoNguoi.Text = "Có Người (" + BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 2) + ")";
            btnCoNguoi.ForeColor = Color.Red;
            btnCoNguoi.StyleController = null;
            btnCoNguoi.LookAndFeel.UseDefaultLookAndFeel = false;
            btnCoNguoi.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            float SLPhucVu = BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 2);
            float TongSLBan = BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 2) + BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 0) + BUS_BAN.DanhSachThongKe(dr11["IDChiNhanh"].ToString(), 1);
            float TyLePhucVu = SLPhucVu / (float)TongSLBan;
            txtTyLyPhucVu.Text = "Tỷ lệ phục vụ: " + Math.Round(TyLePhucVu, 2) * 100 + "%";
        }
        public void BanKhuVuc(string IDKhuVuc, FlowLayoutPanel layout)
        {
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
                btn.MouseDown += btn_MouseDown;
                btn.Tag = item;
                switch (TrangThai)
                {
                    case 0:
                        layout.Controls.Add(btn);
                        btn.ToolTip = "Bàn trống";
                        break;
                    case 1:
                        btn.ForeColor = Color.OrangeRed;
                        btn.StyleController = null;
                        btn.LookAndFeel.UseDefaultLookAndFeel = false;
                        List<DTO_DatBan> thongtinnguoidat = DAO_DatBan.Instance.LoadTableList(item.Id);
                        foreach (DTO_DatBan dr1 in thongtinnguoidat)
                        {
                            btn.ToolTip = dr1.TenKhachHang + Environment.NewLine + dr1.DienThoai + Environment.NewLine + dr1.GioDat;
                        }
                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                        layout.Controls.Add(btn);
                        break;
                    case 2:
                        btn.ForeColor = Color.Red;
                        btn.StyleController = null;
                        btn.LookAndFeel.UseDefaultLookAndFeel = false;
                        btn.ToolTip = "Bàn có người";
                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                        layout.Controls.Add(btn);
                        break;
                }
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                 IDBan = 0;
                 IDBan = ((sender as SimpleButton).Tag as DTO_BAN).Id;
                 menuBan.ShowPopup(Control.MousePosition);
            }
        }
       
        public void HienThiHoaDon(int IDBan)
        {
            List<DTO_DanhSachMenu> DanhSachHoaDon = DAO_DanhSachMonAn.Instance.GetDanhSachMonAn(DAO_BanHang.IDHoaDon(IDBan));
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridControlCTHD.DataSource = null;
            //gridControlCTHD.Refresh();
            gridControlCTHD.DataSource = DanhSachHoaDon;
            lblTenBan.Text = "Tên bàn: " + DAO_BAN.LenTenBan(IDBan);
            LoadTongTien();
        }
        public void LoadTongTien()
        {
            txtDiemTichLuy.Text = "0";
            txtGiamGia.Text = "0";
            txtTienThoi.Text = "0";
            txtKhachThanhToan.ReadOnly = false;
            //txtGioBatDau.Time = DAO_BanHang.GioBatDauBiDa(IDBan, DAO_BanHang.IDHoaDon(IDBan));
            //txtGioKetThuc.Time = DateTime.Now;
            DateTime GioBatDau = DAO_BanHang.GioBatDauBiDa(IDBan, DAO_BanHang.IDHoaDon(IDBan));
            //DateTime GioKetThuc = DateTime.Parse(txtGioKetThuc.Text.ToString());
           // TimeSpan TongGioChoi = GioKetThuc - GioBatDau;
           //// int TongSoPhut = (int)TongGioChoi.TotalMinutes;
           // int SoGio = TongSoPhut / 60;
//int SoPhut = TongSoPhut % 60;
           // txtTongGioChoi.Text = SoGio + " giờ " + SoPhut + " phút";
           // txtDonGiaGio.Text = DAO_KhuVuc.LayGiaTheoKhuVuc(DAO_BAN.LayIDKhuVuc(IDBan)) + "";
            txtTongTien.Text = DAO_HoaDon.TongTienHoaDon(DAO_BanHang.IDHoaDon(IDBan)).ToString();
           // txtTienGio.Text = DAO_HoaDon.TongTienGio(DAO_BanHang.IDHoaDon(IDBan)).ToString();
            txtKhachCanTra.Text = DAO_HoaDon.KhachCanTra(DAO_BanHang.IDHoaDon(IDBan)) + "";
        }
        private void btn_Click(object sender, EventArgs e)
        {
            IDBan = ((sender as SimpleButton).Tag as DTO_BAN).Id;
            HienThiHoaDon(IDBan);
            txtKhachThanhToan.ReadOnly = false;
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
            frmXoaBan fr = new frmXoaBan();
            fr.ShowDialog();
                //bool KT = DAO_BAN.XoaBanVeMatDinh(IDBan);
                //if (KT == true)
                //{
                //    DAO_HoaDon.XoaDatBan(IDBan);
                //    DAO_DatBan.XoaKhachDat(IDBan);
                //    DanhSachBan();
                //    HienThiHoaDon(IDBan);
                //    //gridControlCTHD.DataSource = null;
                //    //gridControlCTHD.Refresh();
                //    MessageBox.Show("Cập Nhật Thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                   
                //}
                //else
                //{
                //    DanhSachBan();
                //    MessageBox.Show("Cập Nhật Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            
        }
        public void GetXoaBan(int KT, string LyDoXoa, int IDHOADON, int IDBAN)
        {
            if (KT == 1)
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa bàn ?.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    bool KTTrangThai = DAO_BAN.XoaBanVeMatDinh(IDBAN);// cập nhật trạng thái bàn  = 0; bàn trống
                    if (KTTrangThai == true)
                    {
                        if (IDHOADON != 0)
                        {
                            // đã có món
                            DAO_DatBan.XoaKhachDat(IDBAN);
                        }
                        else
                        {
                            //chưa có món ăn, xóa bình thường
                            DAO_DatBan.XoaKhachDat(IDBAN);
                            DAO_HoaDon.XoaDatBan(IDBAN);
                        }
                        
                    }
                    else
                    {
                        DanhSachBan();
                        MessageBox.Show("Xóa bàn thất bại?.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Xóa bàn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DanhSachBan();
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
            // chuyển bàn
            if (DAO_BAN.TrangThaiBan(IDBan) == 2)
            {
                frmChuyenBan fr = new frmChuyenBan();
                fr.MyGetData = new frmChuyenBan.GetKT(GetChuyenBan);
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể chuyển bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetChuyenBan(int KT, int IDBanChuyen, int IDBanNhan, int IDHoaDon)
        {
            if (KT == 1)
            {
                TinhTongTien(IDHoaDon);
                HienThiHoaDon(IDBanNhan);
                DanhSachBan();
                //gridControlCTHD.DataSource = null;
                //gridControlCTHD.Refresh();
                MessageBox.Show("Chuyển bàn thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Chuyển bàn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DanhSachBan();
            }
        }
        public void GetValueGoiMon(int KT, int IDHoaDon)
        {
            if (KT == 1)
            {
                TinhTongTien(IDHoaDon);
                HienThiHoaDon(IDBan);
                DanhSachBan();
                //LoadTongTien();
                //gridControlCTHD.DataSource = null;
                //gridControlCTHD.Refresh();
               // MessageBox.Show("Gọi Món Thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                frmTachBan fr = new frmTachBan();
                fr.MyGetDataTachBan = new frmTachBan.GetKT(GetTachBan);
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể tách bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetTachBan(int KT, int IDHoaDonA, int IDHoaDonB)
        {
            if (KT == 1)
            {
                DanhSachBan();
                TinhTongTien(IDHoaDonA);
                TinhTongTien(IDHoaDonB);
                gridControlCTHD.DataSource = null;
                gridControlCTHD.Refresh();
                MessageBox.Show("Tách bàn thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Tách bàn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DanhSachBan();
            }
        }
        private void barButtonGopBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DAO_BAN.TrangThaiBan(IDBan) == 2)
            {
                frmGopBan fr = new frmGopBan();
                fr.MyGetDataGopBan = new frmGopBan.GetKT(GetGopBan);
                fr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bàn chưa có món ăn. Không thể gộp bàn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetGopBan(int KT, int IDBanA, int IDBanB, int IDHoaDon)
        {
            if (KT == 1)
            {
                DanhSachBan();
                TinhTongTien(IDHoaDon);
                gridControlCTHD.DataSource = null;
                gridControlCTHD.Refresh();
                MessageBox.Show("Gộp bàn thành Công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Gộp bàn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DanhSachBan();
            }
        }
        private void txtKhachThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            float KhachThanhToan = float.Parse(txtKhachThanhToan.EditValue.ToString());
            float KhachCanThanhToan = float.Parse(txtKhachCanTra.EditValue.ToString());
            txtTienThoi.Text = (KhachThanhToan - KhachCanThanhToan).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "Giờ hiện tại: " + DateTime.Now.ToLongTimeString();
        }

        private void gridControlCTHD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridView1.State != DevExpress.XtraGrid.Views.Grid.GridState.Editing)
            {
                if (MessageBox.Show("Bạn muốn xóa món này ra khỏi bàn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    int IDban = IDBan;
                    string ID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[8]).ToString();
                    if (IDban != 0)
                    {
                        int IDHoaDon = DAO_BanHang.IDHoaDon(IDban);
                        if (DAO_BanHang.XoaMonAnTemp(ID) == true)
                        {
                            TinhTongTien(IDHoaDon);
                            HienThiHoaDon(IDban);
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
            double TongTien = 0,TienGio = 0;
            foreach (DTO_ChiTietHoaDon item in danhsach)
            {
                TongTien = TongTien + item.ThanhTien;
            }
            List<DTO_ChiTietGio> DanhSachGio = DAO_DanhSachGioChuaThanhToan.Instance.GetDanhSachGio(IDHoaDon, IDBan);
            foreach (DTO_ChiTietGio item in DanhSachGio)
            {
                TienGio = TienGio + item.ThanhTien;
            }
            DAO_HoaDon.CapNhatTongTien(IDHoaDon, TongTien.ToString(), TongTien.ToString(), TienGio.ToString());
           
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
                float DonGia = float.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[4]).ToString());
                string ID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[8]).ToString();
                string TrangThai = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[7]).ToString();
                int SoLuongCu = DAO_BanHang.SoLuongCu(ID);
                int IDHangHoa = DAO_BanHang.SoLuongCu_LayIDHangHoa(ID);
                int IDDonViTinh = DAO_BanHang.SoLuongCu_LayIDDonViTinh(ID);
                if (TrangThai == "0" && SLMoi > SoLuongCu)
                {
                    //cập nhật tăng số lượng bình thường
                    if (DAO_ChiTietHoaDon.CapNhatSoLuong(IDHoaDon, (SLMoi * DonGia).ToString(), SLMoi.ToString(), MaHangHoa) == true)
                    {
                        TinhTongTien(IDHoaDon);
                        HienThiHoaDon(IDban);
                    }
                }
                else if (TrangThai == "1" && SLMoi > SoLuongCu)
                {
                    //tạo món mới, thuộc hóa đơn này.
                    if (DAO_ChiTietHoaDon.KiemTraCheBien(IDHoaDon, IDHangHoa, IDban) == false)
                    {
                        DAO_GoiMon.CapNhatChiTietHoaDon(IDHoaDon, SLMoi - SoLuongCu, (SLMoi - SoLuongCu) * DonGia, IDHangHoa, IDban);
                    }
                    else
                    {
                        DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon, IDHangHoa, SLMoi - SoLuongCu, DonGia, (SLMoi - SoLuongCu) * DonGia, IDban, MaHangHoa, IDDonViTinh);

                    }

                    TinhTongTien(IDHoaDon);
                    HienThiHoaDon(IDban);
                }
                else if(TrangThai == "0" && SLMoi < SoLuongCu)
                {
                    //cập nhật giảm số lượng
                    if (DAO_ChiTietHoaDon.CapNhatSoLuong(IDHoaDon, (SLMoi * DonGia).ToString(), SLMoi.ToString(), MaHangHoa) == true)
                    {
                        TinhTongTien(IDHoaDon);
                        HienThiHoaDon(IDban);
                    }
                }
                else if (TrangThai == "1" && SLMoi < SoLuongCu)
                {
                    HienThiHoaDon(IDban);
                    MessageBox.Show("Lỗi: Món đã chế biến không thể giảm số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                HienThiHoaDon(IDBan);
            }
        }

        private void barButtonTinhGio_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmTinhGio fr = new frmTinhGio();
            fr.ShowDialog();
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int IDBanHT = IDBan;
            int IDHoaDonHT = DAO_BanHang.IDHoaDon(IDBanHT);
            if (IDBanHT == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (DAO_BanHang.IDHoaDon(IDBanHT) == 0)
            {
                MessageBox.Show("Bàn chưa có hóa đơn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(float.Parse(txtKhachThanhToan.Text.ToString()) < float.Parse(txtKhachCanTra.Text.ToString()))
            {
                txtKhachThanhToan.Focus();
                MessageBox.Show("Khách thanh toán không đủ số tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Thanh Toán", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    //nếu có bàn nào tính giờ mà chưa có giờ kết thúc thì thông báo cho nhấn giờ kết thúc
                    int KT = DAO_BanHang.KiemTraLayIDGioBatDau(IDHoaDonHT, IDBanHT);// kiểm tra xem có giờ kết thúc hay không
                    if (KT == 0)
                    {
                        bool insert = true;
                        List<DTO_ChiTietHoaDon> DanhSachHoaDon = DAO_ChiTietHoaDon.Instance.ChiTietHoaDon(IDHoaDonHT);
                        // đổi trạng thái hóa đơn + thêm vào CTHD chính, xóa tạm + Chi tiết giờ
                        foreach (DTO_ChiTietHoaDon item in DanhSachHoaDon)
                        {
                            //thêm vào chi tiết hóa đơn chính
                            int IDHangHoa = item.IDHangHoa;
                            int SoLuong = item.SoLuong;
                            double DonGia = item.DonGia;
                            double ThanhTien = item.ThanhTien;
                            string MaHangHoa = item.MaHangHoa;
                            int IDDonViTinh = item.IDDonViTinh;
                            //thêm chi tiết hóa đơn chính, - nguyên liệu hàng hóa
                            if (DAO_ChiTietHoaDonChinh.ThemChiTietHoaDonChinh(IDHoaDonHT, IDHangHoa, SoLuong, DonGia, ThanhTien, IDBanHT, MaHangHoa, IDDonViTinh) == false)
                            {
                                insert = false;
                            }
                            else
                            {
                                List<DTO_NguyenLieu> ListNguyenLieu = DAO_NguyenLieu.Instance.LoadNguyenLieu(IDHangHoa);
                                if (ListNguyenLieu.Count > 0)
                                {
                                    foreach (DTO_NguyenLieu itemNL in ListNguyenLieu)
                                    {
                                        double SLTru = (itemNL.TrongLuong * SoLuong);
                                        DAO_Setting.TruTonKho(itemNL.IDNguyenLieu, frmDangNhap.NguoiDung.Idchinhanh, SLTru);
                                        // trừ tồn kho
                                    }
                                }
                            }
                        }
                        if (insert == true)
                        {
                            // xóa chi tiết hóa đơn temp, cập nhật chi tiết giờ thanh toán  = 1,
                            if (DAO_ChiTietHoaDonChinh.XoaChiTietHoaDonTemp(IDHoaDonHT) == true && DAO_ChiTietHoaDonChinh.CapNhatChiTietGio(IDHoaDonHT, IDBanHT) == true)
                            {
                                // cập nhật trạng thái hóa đơn đã thanh toán, đổi trạng thái bàn
                                int IDNhanVien = frmDangNhap.NguoiDung.Id;
                                double KhachThanhToan = double.Parse(txtKhachThanhToan.Text.ToString());
                                double TienThua = double.Parse(txtTienThoi.Text.ToString());
                                string IDKhachHang = "1";
                                if (cmbTenKhachHang.EditValue != null)
                                    IDKhachHang = cmbTenKhachHang.EditValue.ToString();
                                if (DAO_ChiTietHoaDonChinh.CapNhatHoaDonChinh(IDHoaDonHT, IDBanHT, IDNhanVien, KhachThanhToan, TienThua, IDKhachHang, double.Parse(txtDiemTichLuy.Text.ToString()), double.Parse(txtTongTien.Text.ToString()), double.Parse(txtGiamGia.Text.ToString()), double.Parse(txtKhachCanTra.Text.ToString())) == true && DAO.DAO_BAN.XoaBanVeMatDinh(IDBanHT) == true)// thành công
                                {
                                    if (IDKhachHang != "1")
                                    {
                                        double DiemCong = double.Parse(txtKhachCanTra.Text.ToString()) / double.Parse(DAO_Setting.LayTienQuiDoiDiem().ToString());
                                        if (txtDiemTichLuy.Text.ToString() != "0")
                                        {
                                            DAO_ChiTietHoaDonChinh.TruDiemTichLuy(IDKhachHang, double.Parse(txtDiemTichLuy.Text.ToString()));
                                        }
                                        DAO_ChiTietHoaDonChinh.CongDiemTichLuy(IDKhachHang, DiemCong);
                                    }
                                    txtKhachThanhToan.Text = "0";
                                    txtTienThoi.Text = "0";
                                    txtGiamGia.Text = "0";
                                    txtDiemTichLuy.ReadOnly = true;
                                    txtKhachThanhToan.ReadOnly = true;
                                    DanhSachBan();
                                    HienThiHoaDon(IDBanHT);
                                    LamMoiKhachHang();

                                    DAO_ConnectSQL connect = new DAO_ConnectSQL();
                                    rpHoaDon rp = new rpHoaDon();
                                    SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    string NamePrinter = DAO_Setting.LayTenMayInBill();
                                    rp.Parameters["ID"].Value = IDHoaDonHT;
                                    rp.Parameters["ID"].Visible = false;
                                   // rp.ShowPreviewDialog();
                                    rp.Print(NamePrinter);

                                    ////In bill 
                                    //int rp1 = 0, rp2 = 0;
                                    //int ktReporrt = DAO_Setting.KTHoaDon(IDHoaDonHT + "");
                                    //if (ktReporrt == 0) { rp1 = rp2 = 1; }
                                    //else if (ktReporrt == 1) rp1 = 1;
                                    //else rp2 = 1;

                                    //string NamePrinter = DAO_Setting.LayTenMayInBill();
                                    //DAO_ConnectSQL connect = new DAO_ConnectSQL();
                                    //int IDBill = DAO_Setting.ReportBill();
                                    //if (IDBill == 58)
                                    //{
                                    //    if (rp1 == 1 && rp2 == 1)
                                    //    {
                                    //        rpHoaDonBanHang_58 rp = new rpHoaDonBanHang_58();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //    else if (rp1 == 1)
                                    //    {
                                    //        rpHoaDonBanHang_581 rp = new rpHoaDonBanHang_581();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //    else
                                    //    {
                                    //        rpHoaDonBanHang_582 rp = new rpHoaDonBanHang_582();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (rp1 == 1 && rp2 == 1)
                                    //    {
                                    //        rpHoaDonBanHang rp = new rpHoaDonBanHang();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //    else if (rp1 == 1)
                                    //    {
                                    //        rpHoaDonBanHang1 rp = new rpHoaDonBanHang1();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //    else
                                    //    {
                                    //        rpHoaDonBanHang2 rp = new rpHoaDonBanHang2();
                                    //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                    //        rp.Parameters["ID"].Value = IDHoaDonHT;
                                    //        rp.Parameters["ID"].Visible = false;
                                    //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    //        rp.Parameters["strHoaDon"].Visible = false;
                                    //        //rp.ShowPreviewDialog();
                                    //        rp.Print(NamePrinter);
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bàn chưa có giờ kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void LamMoiKhachHang()
        {
            txtMaKhachHang.Text = "...........................................";
            txtDienThoai.Text = "...........................................";
            txtCMND.Text = "...........................................";
            txtDiem.Text = "...........................................";
            KhachHang();
        }
        private void btnTachHoaDon_Click(object sender, EventArgs e)
        {
            int IDBanHT = IDBan;
            if (IDBanHT == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (DAO_BanHang.IDHoaDon(IDBanHT) == 0)
            {
                MessageBox.Show("Bàn chưa có hóa đơn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmTachBill fr = new frmTachBill();
                fr.MyGetData = new frmTachBill.GetString(GetTachBill);
                fr.ShowDialog();
            }
        }

        private void GetTachBill(int KT, int IDHoaDon, int IDBan)
        {
            if (KT == 1)
            {
                HienThiHoaDon(IDBan);
                TinhTongTien(IDHoaDon);
                LoadTongTien();
                KhachHang();
            }
        }

        private void xtraTabControlDanhSach_Click(object sender, EventArgs e)
        {
            TabActive = xtraTabControlDanhSach.SelectedTabPageIndex;
            NameTabActive = xtraTabControlDanhSach.SelectedTabPage.Name;
            TenKhuVuc = xtraTabControlDanhSach.SelectedTabPage.Text;
            //DanhSachBan();
        }

        private void btnKetCa_Click(object sender, EventArgs e)
        {
            frmKetCa fr = new frmKetCa();
            fr.ShowDialog();
        }

        private void btnXoaMonAn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa món này ra khỏi bàn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int IDban = IDBan;
                string ID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[8]).ToString();
                if (IDban != 0)
                {
                    int IDHoaDon = DAO_BanHang.IDHoaDon(IDban);
                    if (DAO_BanHang.XoaMonAnTemp(ID) == true)
                    {
                        TinhTongTien(IDHoaDon);
                        HienThiHoaDon(IDban);
                    }
                    else
                    {
                        MessageBox.Show("Xóa món ăn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cmbTenKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            int IDBanHT = IDBan;
            int IDHoaDonHT = DAO_BanHang.IDHoaDon(IDBanHT);
            if (IDBanHT == 0)
            {
                LamMoiKhachHang();
                txtDiemTichLuy.Text = "0";
                MessageBox.Show("Vui lòng chọn bàn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                txtDiemTichLuy.ReadOnly = false;
                txtDiemTichLuy.Text = "0";
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
        }

        private void txtDiemTichLuy_EditValueChanged(object sender, EventArgs e)
        {

            float SoDiemCanDoi = float.Parse(txtDiemTichLuy.EditValue.ToString());
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

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string TrangThai = View.GetRowCellDisplayText(e.RowHandle, View.Columns["TrangThai"]);
                if (TrangThai == "0")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 224, 192);
                }
            }
        }
    }
}