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
using QLCafe.Report;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;

namespace QLCafe
{
    public partial class frmTachBill : DevExpress.XtraEditors.XtraForm
    {
        double TongTien = 0;
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        List<ChiTietHoaDonA1> listChiTietMonAn = new List<ChiTietHoaDonA1>();
        List<ChiTietHoaDonB1> listChiTietMonAnThanhToan = new List<ChiTietHoaDonB1>();
        public delegate void GetString(int KT, int IDHoaDon, int IDBan);
        public GetString MyGetData;
        public frmTachBill()
        {
            InitializeComponent();
        }

        private void frmTachBill_Load(object sender, EventArgs e)
        {
            gridViewA.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridViewB.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            lblTenBan.Text = DAO_GoiMon.TenBan(IDBan);
            DanhSachHangHoaA();
            KhachHang();
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
        private void LamMoi()
        {
            TongTien = 0;
            txtTongTien.Text = TongTien.ToString();
            txtKhachCanTra.Text = TongTien.ToString();
            txtDiemTichLuy.Text = "0";
            DanhSachHangHoaA();
            listChiTietMonAnThanhToan.Clear();
            gridControlB.DataSource = null;
            gridControlB.DataSource = listChiTietMonAnThanhToan;
        }
        private void DanhSachHangHoaA()
        {
            listChiTietMonAn.Clear();
            // lấy món ăn theo IDBan
            List<DTO_DanhSachMenu> DanhSachHoaDonA1 = DAO_DanhSachMonAn.Instance.GetDanhSachMonAn(DAO_BanHang.IDHoaDon(IDBan));
            foreach (DTO_DanhSachMenu item in DanhSachHoaDonA1)
            {
                listChiTietMonAn.Add(new ChiTietHoaDonA1
                {
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    SoLuong = item.SoLuong,
                });
            }
            gridControlA.DataSource = null;
            gridControlA.Refresh();
            gridControlA.DataSource = DanhSachHoaDonA1;
            if (listChiTietMonAn.Count > 0)
            {
                btnABMonAn.Enabled = true;
                btnLamLaiABMonAn.Enabled = true;
            }
            else
            {
                btnABMonAn.Enabled = false;
                btnLamLaiABMonAn.Enabled = false;
            }
        }


        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamLaiABMonAn_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnABMonAn_Click(object sender, EventArgs e)
        {
            frmKiemTraTachBan fr = new frmKiemTraTachBan();
            fr.MyGetData = new frmKiemTraTachBan.GetString(GetValue);
            fr.ShowDialog();
        }

        private void GetValue(int KT, int SoLuong)
        {
            if (KT == 1 && listChiTietMonAn.Count > 0)
            {
                txtDiemTichLuy.Text = "0";
                int SoLuongA = Int32.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[3]).ToString());
                string TenHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[1]).ToString();
                string MaHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[0]).ToString();
                string DonViTinh = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[2]).ToString();
                float DonGia = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[4]).ToString());
                if (SoLuongA == SoLuong)
                {
                    TongTien = TongTien + (SoLuongA * DonGia);
                    txtTongTien.Text = TongTien.ToString();
                    txtKhachCanTra.Text = TongTien.ToString();
                    int dongHienTai = gridViewA.FocusedRowHandle;
                    listChiTietMonAnThanhToan.Add(new ChiTietHoaDonB1
                    {
                        MaHangHoa = MaHangHoa,
                        TenHangHoa = TenHangHoa,
                        DonViTinh = DonViTinh,
                        DonGia = DonGia,
                        ThanhTien = SoLuongA * DonGia,
                        
                        SoLuong = SoLuongA,
                    });
                    listChiTietMonAn.RemoveAt(dongHienTai);// xóa dòng hiện tại A1
                }
                else if (SoLuongA < SoLuong)
                {
                    MessageBox.Show("Số lượng thanh toán không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int KiemTra = 0;
                    int SoLuongB = 0;
                    foreach (ChiTietHoaDonA1 item in listChiTietMonAn)
                    {
                        if (item.MaHangHoa == MaHangHoa)
                        {
                            item.SoLuong = item.SoLuong - SoLuong;
                            SoLuongB = SoLuong;
                            item.ThanhTien = item.SoLuong * item.DonGia;
                            KiemTra = 1;
                            break;
                        }
                    }
                    if (KiemTra == 1)
                    {
                        TongTien = TongTien + (SoLuongB * DonGia);
                        txtTongTien.Text = TongTien.ToString();
                        txtKhachCanTra.Text = TongTien.ToString();
                        listChiTietMonAnThanhToan.Add(new ChiTietHoaDonB1
                        {
                            MaHangHoa = MaHangHoa,
                            TenHangHoa = TenHangHoa,
                            DonViTinh = DonViTinh,
                            DonGia = DonGia,
                            ThanhTien = SoLuongB * DonGia,
                            SoLuong = SoLuongB,
                        });
                    }
                }
                gridControlB.DataSource = null;
                gridControlB.DataSource = listChiTietMonAnThanhToan;
                gridControlA.DataSource = null;
                gridControlA.DataSource = listChiTietMonAn;
            }
            else
            {
                MessageBox.Show("Danh sách món ăn không có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public class ChiTietHoaDonA1
        {
            private float thanhTien;

            public float ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }

            private float donGia;

            public float DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private int soLuong;

            public int SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            private string donViTinh;

            public string DonViTinh
            {
                get { return donViTinh; }
                set { donViTinh = value; }
            }
            private string tenHangHoa;

            public string TenHangHoa
            {
                get { return tenHangHoa; }
                set { tenHangHoa = value; }
            }
            private string maHangHoa;

            public string MaHangHoa
            {
                get { return maHangHoa; }
                set { maHangHoa = value; }
            }
            
        }
        public class ChiTietHoaDonB1
        {
            private float thanhTien;

            public float ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }

            private float donGia;

            public float DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private int soLuong;

            public int SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            private string donViTinh;

            public string DonViTinh
            {
                get { return donViTinh; }
                set { donViTinh = value; }
            }
            private string tenHangHoa;

            public string TenHangHoa
            {
                get { return tenHangHoa; }
                set { tenHangHoa = value; }
            }
            private string maHangHoa;

            public string MaHangHoa
            {
                get { return maHangHoa; }
                set { maHangHoa = value; }
            }
            
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            int rp1 = 0, rp2 = 0;
            // thanh toán... đưa dữ liệu lai from chính, load lại món ăn, tiền giờ.OK
            if (float.Parse(txtKhachThanhToan.Text.ToString()) < float.Parse(txtKhachCanTra.Text.ToString()))
            {
                txtKhachThanhToan.Focus();
                MessageBox.Show("Khách thanh toán không đủ số tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Thanh Toán", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (listChiTietMonAnThanhToan.Count > 0)
                {
                    bool KT = true;
                    int IDNhanVien = frmDangNhap.NguoiDung.Id;
                    DateTime GioVao = DAO_ChiTietHoaDonChinh.LayGioVao(IDHoaDon);
                    string IDKhachHang = "1";
                    if (cmbTenKhachHang.EditValue != null)
                        IDKhachHang = cmbTenKhachHang.EditValue.ToString();
                    object ID = DAO_ChiTietHoaDonChinh.ThemMoiHoaDon(IDBan, IDNhanVien, GioVao, IDKhachHang, double.Parse(txtDiemTichLuy.Text.ToString()), double.Parse(txtTongTien.Text.ToString()), double.Parse(txtGiamGia.Text.ToString()), double.Parse(txtKhachCanTra.Text.ToString()), double.Parse(txtKhachThanhToan.Text.ToString()), double.Parse(txtTienThoi.Text.ToString()));
                    if (listChiTietMonAnThanhToan.Count > 0 && KT == true && ID != null)// thanh toán món ăn
                    {
                        double TongTien = 0;
                        foreach (ChiTietHoaDonB1 item in listChiTietMonAnThanhToan)
                        {
                            TongTien = TongTien + item.ThanhTien;
                            string MaHang = item.MaHangHoa;
                            int SoLuong = item.SoLuong;
                            int IDHangHoa = DAO_Setting.LayIDHangHoa(MaHang);
                            double DonGia = item.DonGia;
                            double ThanhTien = SoLuong * DonGia;
                            int IDDonViTinh = DAO_Setting.LayIDDonViTinh(MaHang);
                            //thêm vào chi tiết hóa đơn chính, cập nhật chi tiết hóa đơn củ, if  Sl = nhau xóa hóa đơn củ
                            // kiểm tra thêm chi tiết
                           
                            if (DAO_ChiTietHoaDonChinh.KiemTraHangHoa(Int32.Parse(ID.ToString()), IDHangHoa, IDBan) == false)
                            {
                                DAO_ChiTietHoaDonChinh.ThemChiTietHoaDonChinh(Int32.Parse(ID.ToString()), IDHangHoa, SoLuong, DonGia, ThanhTien, IDBan, MaHang, IDDonViTinh);
                            }
                            else
                            {
                                DAO_ChiTietHoaDonChinh.CapNhatChiTietHoaDon(Int32.Parse(ID.ToString()), SoLuong, SoLuong * DonGia, IDHangHoa, IDBan);
                            }
                            List<DTO_NguyenLieu> ListNguyenLieu = DAO_NguyenLieu.Instance.LoadNguyenLieu(IDHangHoa);// trừ nguyên liệu tồn kho
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
                        if (ID!= null)
                        {
                             DAO_ChuyenBan.XoaChiTietBanCu(IDHoaDon, IDBan);
                             foreach (ChiTietHoaDonA1 item in listChiTietMonAn)
                             {
                                 string MaHang = item.MaHangHoa;
                                 int SoLuong = item.SoLuong;
                                 float DonGia = item.DonGia;
                                 DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon,DAO_Setting.LayIDHangHoa(MaHang),SoLuong,DonGia,DonGia*SoLuong,IDBan,MaHang,DAO_Setting.LayIDDonViTinh(MaHang));
                             }
                             // cập nhật tổng tiền hóa đơn mới lại
                             rp1 = 1; // Show Report 1
                             DAO_ChiTietHoaDonChinh.CapNhatTongTienHoaDonChinh(Int32.Parse(ID.ToString()), IDBan, TongTien);
                            //trừ điểm tích lũy nếu id != 1, cộng lại điểm tích lũy
                             if (IDKhachHang != "1")
                             {
                                 double DiemCong = double.Parse(txtKhachCanTra.Text.ToString()) / double.Parse(DAO_Setting.LayTienQuiDoiDiem().ToString());
                                 if (txtDiemTichLuy.Text.ToString() != "0")
                                 {
                                     DAO_ChiTietHoaDonChinh.TruDiemTichLuy(IDKhachHang, double.Parse(txtDiemTichLuy.Text.ToString()));
                                 }
                                 DAO_ChiTietHoaDonChinh.CongDiemTichLuy(IDKhachHang, DiemCong);
                             }
                        }
                        
                    }
                    if (KT == true && ID != null)
                    {
                        if (MyGetData != null)
                        {
                            MyGetData(1, IDHoaDon, IDBan);
                            LamMoiKhachHang();
                            this.Close();
                        }

                        DAO_ConnectSQL connect = new DAO_ConnectSQL();
                        rpHoaDon rp = new rpHoaDon();
                        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                        string NamePrinter = DAO_Setting.LayTenMayInBill();
                        rp.Parameters["ID"].Value = ID;
                        rp.Parameters["ID"].Visible = false;
                        //rp.ShowPreviewDialog();
                        rp.Print(NamePrinter);

                        // in hóa đớn, cập nhật hóa đơn
                        //DAO_ConnectSQL connect = new DAO_ConnectSQL();
                        //string NamePrinter = DAO_Setting.LayTenMayInBill();

                        //int IDBill = DAO_Setting.ReportBill();
                        //if (IDBill == 58)
                        //{
                        //    if (rp1 == 1 && rp2 == 1)
                        //    {
                        //        rpHoaDonBanHang_58 rp = new rpHoaDonBanHang_58();
                        //        SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                        //        sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                        //        rp.Parameters["ID"].Value = ID;
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

                        //        rp.Parameters["ID"].Value = ID;
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

                        //        rp.Parameters["ID"].Value = ID;
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

                        //        rp.Parameters["ID"].Value = ID;
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

                        //        rp.Parameters["ID"].Value = ID;
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

                        //        rp.Parameters["ID"].Value = ID;
                        //        rp.Parameters["ID"].Visible = false;
                        //        rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                        //        rp.Parameters["strHoaDon"].Visible = false;
                        //        //rp.ShowPreviewDialog();
                        //        rp.Print(NamePrinter);
                        //    }
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi thanh toán. Danh sách hóa đơn trống?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void cmbTenKhachHang_EditValueChanged(object sender, EventArgs e)
        {
            int IDBanHT = IDBan;
            int IDHoaDonHT = DAO_BanHang.IDHoaDon(IDBanHT);
            if (IDBanHT == 0)
            {
                cmbTenKhachHang.Text = "";
                txtDiemTichLuy.Text = "0";
                MessageBox.Show("Vui lòng chọn bàn để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtKhachThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            float KhachThanhToan = float.Parse(txtKhachThanhToan.EditValue.ToString());
            float KhachCanThanhToan = float.Parse(txtKhachCanTra.EditValue.ToString());
            txtTienThoi.Text = (KhachThanhToan - KhachCanThanhToan).ToString();
        }
    }
}