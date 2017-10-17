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
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        List<ChiTietHoaDonA1> listChiTietMonAn = new List<ChiTietHoaDonA1>();
        List<ChiTietHoaDonB1> listChiTietMonAnThanhToan = new List<ChiTietHoaDonB1>();
        List<ChiTietGioA1> listChiTietGio = new List<ChiTietGioA1>();
        List<ChiTietGioB1> listChiTietGioThanhToan = new List<ChiTietGioB1>();

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
            gridViewC.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridViewD.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            lblTenBan.Text = DAO_GoiMon.TenBan(IDBan);
            DanhSachHangHoaA();
            DanhSachGio();
        }

        private void LamMoi()
        {
            DanhSachHangHoaA();
            listChiTietMonAnThanhToan.Clear();
            gridControlB.DataSource = null;
            gridControlB.DataSource = listChiTietMonAnThanhToan;
        }

        public void LamMoiGio()
        {
            DanhSachGio();
            listChiTietGioThanhToan.Clear();
            gridControlD.DataSource = null;
            gridControlD.DataSource = listChiTietGioThanhToan;
        }
        private void DanhSachGio()
        {
            listChiTietGio.Clear();
            List<DTO_ChiTietGio> DanhSachGio = DAO_DanhSachGioChuaThanhToan.Instance.GetDanhSachGio(IDHoaDon, IDBan);
            foreach (DTO_ChiTietGio item in DanhSachGio)
            {
                listChiTietGio.Add(new ChiTietGioA1
                {
                   GioBatDau = item.GioBatDau,
                   GioKetThuc = item.GioKetThuc,
                   TongGioChoi = item.TongGioChoi,
                   DonGia = item.DonGia,
                   ThanhTien = item.ThanhTien,
                   ID = item.ID,
                });
            }
            gridControlC.DataSource = null;
            gridControlC.Refresh();
            gridControlC.DataSource = DanhSachGio;
            if (listChiTietGio.Count > 0)
            {
                btnABGio.Enabled = true;
                btnLamLaiABGio.Enabled = true;
            }
            else
            {
                btnABGio.Enabled = false;
                btnLamLaiABGio.Enabled = false;
            }
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
                int SoLuongA = Int32.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[3]).ToString());
                string TenHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[1]).ToString();
                string MaHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[0]).ToString();
                string DonViTinh = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[2]).ToString();
                float DonGia = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[4]).ToString());
                if (SoLuongA == SoLuong)
                {
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

        public class ChiTietGioA1
        {
            private DateTime gioBatDau;

            public DateTime GioBatDau
            {
                get { return gioBatDau; }
                set { gioBatDau = value; }
            }
            private DateTime gioKetThuc;

            public DateTime GioKetThuc
            {
                get { return gioKetThuc; }
                set { gioKetThuc = value; }
            }
            private string tongGioChoi;

            public string TongGioChoi
            {
                get { return tongGioChoi; }
                set { tongGioChoi = value; }
            }
            private double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private double thanhTien;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int iD;

            public int ID
            {
                get { return iD; }
                set { iD = value; }
            }
        }
        public class ChiTietGioB1
        {
            private DateTime gioBatDau;

            public DateTime GioBatDau
            {
                get { return gioBatDau; }
                set { gioBatDau = value; }
            }
            private DateTime gioKetThuc;

            public DateTime GioKetThuc
            {
                get { return gioKetThuc; }
                set { gioKetThuc = value; }
            }
            private string tongGioChoi;

            public string TongGioChoi
            {
                get { return tongGioChoi; }
                set { tongGioChoi = value; }
            }
            private double donGia;

            public double DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private double thanhTien;

            public double ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int iD;

            public int ID
            {
                get { return iD; }
                set { iD = value; }
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

        private void btnABGio_Click(object sender, EventArgs e)
        {
            if (listChiTietGio.Count > 0)
            {
                DateTime GioBatDau = DateTime.Parse(gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[0]).ToString());
                DateTime GioKetThuc = DateTime.Parse(gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[1]).ToString());
                string TongGioChoi = gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[2]).ToString();
                double DonGia = double.Parse(gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[3]).ToString());
                double ThanhTien = double.Parse(gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[4]).ToString());
                int ID = Int32.Parse(gridViewC.GetRowCellValue(gridViewC.FocusedRowHandle, gridViewC.Columns[5]).ToString());
                int dongHienTai = gridViewC.FocusedRowHandle;
                listChiTietGioThanhToan.Add(new ChiTietGioB1
                {
                    GioBatDau = GioBatDau,
                    GioKetThuc = GioKetThuc,
                    TongGioChoi = TongGioChoi,
                    DonGia = DonGia,
                    ThanhTien = ThanhTien,
                    ID = ID,
                });
                listChiTietGio.RemoveAt(dongHienTai);// xóa dòng hiện tại A1
                gridControlD.DataSource = null;
                gridControlD.DataSource = listChiTietGioThanhToan;
                gridControlC.DataSource = null;
                gridControlC.DataSource = listChiTietGio;
            }
        }
        private void btnLamLaiABGio_Click(object sender, EventArgs e)
        {
            LamMoiGio();
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            int rp1 = 0, rp2 = 0;
            // thanh toán... đưa dữ liệu lai from chính, laod lại món ăn, tiền giờ.OK
            if (MessageBox.Show("Thanh Toán", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (listChiTietMonAnThanhToan.Count > 0 || listChiTietGioThanhToan.Count > 0)
                {
                    bool KT = true;
                    int IDNhanVien = frmDangNhap.NguoiDung.Id;
                    DateTime GioVao = DAO_ChiTietHoaDonChinh.LayGioVao(IDHoaDon);
                    object ID = DAO_ChiTietHoaDonChinh.ThemMoiHoaDon(IDBan, IDNhanVien, GioVao);
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
                        }
                        
                    }
                    if (listChiTietGioThanhToan.Count > 0 && KT == true && ID != null)// thanh toán tiền giờ
                    {
                        double TongTienGio = 0;
                        foreach (ChiTietGioB1 item in listChiTietGioThanhToan)
                        {
                            int id = item.ID;
                            TongTienGio = TongTienGio + item.ThanhTien;
                            //cập nhật tiền giờ
                            if (DAO_ChiTietHoaDonChinh.CapNhatChiTietGio_ID(Int32.Parse(ID.ToString()), IDBan, id) == false)
                                KT = false;
                        }
                        if (KT == true)
                        {
                            rp2 = 1;  // Show Report 2
                            DAO_ChiTietHoaDonChinh.CapNhatTienGioHoaDonChinh(Int32.Parse(ID.ToString()), IDBan, TongTienGio);
                        }
                    }
                    if (KT == true && ID != null)
                    {
                        if (MyGetData != null)
                        {
                            MyGetData(1, IDHoaDon, IDBan);
                            this.Close();
                        }

                        // in hóa đớn, cập nhật hóa đơn
                        DAO_ConnectSQL connect = new DAO_ConnectSQL();
                        string NamePrinter = DAO_Setting.LayTenMayInBill();

                        int IDBill = DAO_Setting.ReportBill();
                        if (IDBill == 58)
                        {
                            if (rp1 == 1 && rp2 == 1)
                            {
                                rpHoaDonBanHang_58 rp = new rpHoaDonBanHang_58();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                            else if (rp1 == 1)
                            {
                                rpHoaDonBanHang_581 rp = new rpHoaDonBanHang_581();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                            else
                            {
                                rpHoaDonBanHang_582 rp = new rpHoaDonBanHang_582();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                        }
                        else
                        {
                            if (rp1 == 1 && rp2 == 1)
                            {
                                rpHoaDonBanHang rp = new rpHoaDonBanHang();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                            else if (rp1 == 1)
                            {
                                rpHoaDonBanHang1 rp = new rpHoaDonBanHang1();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                            else
                            {
                                rpHoaDonBanHang2 rp = new rpHoaDonBanHang2();
                                SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                sqlDataSource.Connection.ConnectionString += connect.ConnectString();

                                rp.Parameters["ID"].Value = ID;
                                rp.Parameters["ID"].Visible = false;
                                rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                rp.Parameters["strHoaDon"].Visible = false;
                                //rp.ShowPreviewDialog();
                                rp.Print(NamePrinter);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi thanh toán. Danh sách hóa đơn trống?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}