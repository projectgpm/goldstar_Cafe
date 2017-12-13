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
        

        public delegate void GetString(int KT, int IDHoaDon, int IDBan);
        public GetString MyGetData;
        public frmTachBill()
        {
            InitializeComponent();
        }

        private void frmTachBill_Load(object sender, EventArgs e)
        {
            //gridViewA.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridViewB.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            //gridViewC.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
           
            lblTenBan.Text = DAO_GoiMon.TenBan(IDBan);
            DanhSachHangHoaA();
           
        }

        private void LamMoi()
        {
            DanhSachHangHoaA();
            listChiTietMonAnThanhToan.Clear();
            gridControlB.DataSource = null;
            gridControlB.DataSource = listChiTietMonAnThanhToan;
        }

       
        private void DanhSachHangHoaA()
        {
            listChiTietMonAn.Clear();
            // lấy món ăn theo IDBan
            List<DTO_DanhSachMenu> MonAnThuong = DAO_DanhSachMonAn.Instance.GetMonAnThuong(DAO_BanHang.IDHoaDon(IDBan));
            List<DTO_DanhSachMenu> MonAnTuChon = DAO_DanhSachMonAn.Instance.GetMonAnTuChon(DAO_BanHang.IDHoaDon(IDBan));
            foreach (DTO_DanhSachMenu item in MonAnThuong)
            {
                listChiTietMonAn.Add(new ChiTietHoaDonA1
                {
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    TrongLuong = item.TrongLuong,
                    SoLuong = item.SoLuong,
                    InPhaChe = item.InPhaChe,
                });
            }
            foreach (DTO_DanhSachMenu item in MonAnTuChon)
            {
                listChiTietMonAn.Add(new ChiTietHoaDonA1
                {
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    TrongLuong = item.TrongLuong,
                    SoLuong = item.SoLuong,
                    InPhaChe = item.InPhaChe,
                });
            }

            gridControlA.DataSource = null;
            gridControlA.Refresh();
            gridControlA.DataSource = listChiTietMonAn;
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
                float TrongLuong = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[6]).ToString());
                if (SoLuongA == SoLuong)
                {
                    int dongHienTai = gridViewA.FocusedRowHandle;
                    listChiTietMonAnThanhToan.Add(new ChiTietHoaDonB1
                    {
                        MaHangHoa = MaHangHoa,
                        TenHangHoa = TenHangHoa,
                        DonViTinh = DonViTinh,
                        DonGia = DonGia,
                        TrongLuong = TrongLuong,
                        ThanhTien = TrongLuong > 0 ? (SoLuongA * (TrongLuong * DonGia)) : SoLuongA * DonGia,
                        SoLuong = SoLuongA,
                        InPhaChe = SoLuong,
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
                            item.InPhaChe = item.InPhaChe - SoLuong;
                            SoLuongB = SoLuong;
                            item.ThanhTien = TrongLuong > 0 ? (item.SoLuong * (TrongLuong * item.DonGia)) : item.SoLuong * item.DonGia;
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
                            ThanhTien = TrongLuong > 0 ? (SoLuongB * (TrongLuong * DonGia)) : SoLuongB * DonGia,
                            SoLuong = SoLuongB,
                            InPhaChe = SoLuongB,
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
            private int inPhaChe;

            public int InPhaChe
            {
                get { return inPhaChe; }
                set { inPhaChe = value; }
            }
            private float trongLuong;

            public float TrongLuong
            {
                get { return trongLuong; }
                set { trongLuong = value; }
            }
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
            private int inPhaChe;

            public int InPhaChe
            {
                get { return inPhaChe; }
                set { inPhaChe = value; }
            }
            private float trongLuong;

            public float TrongLuong
            {
                get { return trongLuong; }
                set { trongLuong = value; }
            }
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
            // thanh toán... đưa dữ liệu lai from chính, laod lại món ăn, tiền giờ.OK
            if (MessageBox.Show("Thanh Toán", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                if (listChiTietMonAnThanhToan.Count > 0 )
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
                            double TrongLuong = item.TrongLuong;
                            string MaHang = item.MaHangHoa;
                            int SoLuong = item.SoLuong;
                            int IDHangHoa = TrongLuong > 0 ? DAO_Setting.LayIDHangHoaTuChon(MaHang) : DAO_Setting.LayIDHangHoa(MaHang);
                            double DonGia = item.DonGia;
                            double ThanhTien = TrongLuong > 0 ? (SoLuong * (TrongLuong * DonGia)) : (SoLuong * DonGia);
                            int IDDonViTinh = TrongLuong > 0 ? DAO_Setting.LayIDDonViTinhTuChon(MaHang) : DAO_Setting.LayIDDonViTinh(MaHang);

                            //thêm vào chi tiết hóa đơn chính, cập nhật chi tiết hóa đơn củ, if  Sl = nhau xóa hóa đơn củ
                            // kiểm tra thêm chi tiết
                            if (DAO_ChiTietHoaDonChinh.KiemTraHangHoa(Int32.Parse(ID.ToString()), IDHangHoa, IDBan, float.Parse(TrongLuong.ToString())) == false)
                            {
                                DAO_ChiTietHoaDonChinh.ThemChiTietHoaDonChinh(Int32.Parse(ID.ToString()), IDHangHoa, SoLuong, DonGia, ThanhTien, IDBan, MaHang, IDDonViTinh, float.Parse(TrongLuong.ToString()));
                            }
                            else
                            {
                                DAO_ChiTietHoaDonChinh.CapNhatChiTietHoaDon(Int32.Parse(ID.ToString()), SoLuong, ThanhTien, IDHangHoa, IDBan);
                            }
                            if (TrongLuong == 0)
                            {
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
                            else if (TrongLuong > 0)
                            {
                                DAO_Setting.TruTonKho(IDHangHoa, frmDangNhap.NguoiDung.Idchinhanh, SoLuong * TrongLuong);
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
                                 float TrongLuong = item.TrongLuong;
                                 float ThanhTien = TrongLuong > 0 ? (SoLuong * (TrongLuong * DonGia)) : (SoLuong * DonGia);
                                 int IDHangHoa = TrongLuong > 0 ? DAO_Setting.LayIDHangHoaTuChon(MaHang) : DAO_Setting.LayIDHangHoa(MaHang);
                                 int IDDonViTinh = TrongLuong > 0 ? DAO_Setting.LayIDDonViTinhTuChon(MaHang) : DAO_Setting.LayIDDonViTinh(MaHang);
                                 int IDMayIn = TrongLuong > 0 ? DAO_GoiMon.LayIDMayInNguyenLieu(IDHangHoa) : DAO_GoiMon.LayIDMayInHangHoa(IDHangHoa);
                                 DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon, IDHangHoa, SoLuong, DonGia, ThanhTien, IDBan, MaHang, IDDonViTinh, TrongLuong, IDMayIn,item.InPhaChe);
                             }

                             DAO_ChiTietHoaDonChinh.CapNhatTongTienHoaDonChinh(Int32.Parse(ID.ToString()), IDBan, TongTien);
                        }
                        
                    }
                    
                    if (KT == true && ID != null)
                    {
                        if (MyGetData != null)
                        {
                            MyGetData(1, IDHoaDon, IDBan);
                            this.Close();
                        }

                        if (MessageBox.Show("In hóa đơn", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            // In 1 máy in bán hàng.
                            DAO_ConnectSQL connect = new DAO_ConnectSQL();

                            //DAO_Setting.CapNhatBillInTemp(IDHoaDonHT + "");
                            //int SoLanIn = DAO_Setting.LaySoLanInTemp(IDHoaDonHT + "");
                            string sx = DAO_Setting.GetHardDiskSerialNo();
                            string strAddress = sx + "GPM";
                            string sha1Address = DAO_Setting.GetSHA1HashData(strAddress);

                            string NamePrinter = DAO_Setting.LayTenMayInBill(sha1Address);
                            if (NamePrinter != "")
                            {
                                int KhoGiay = DAO_Setting.ReportBill(sha1Address);
                                if (KhoGiay == 58)
                                {
                                    rpHoaDonBanHang_581 rp = new rpHoaDonBanHang_581();
                                    SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    sqlDataSource.Connection.ConnectionString += connect.ConnectString();
                                    rp.Parameters["ID"].Value = ID;
                                    rp.Parameters["ID"].Visible = false;
                                    rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    rp.Parameters["strHoaDon"].Visible = false;
                                    rp.Print(NamePrinter);
                                }
                                else
                                {
                                    rpHoaDonBanHang1 rp = new rpHoaDonBanHang1();
                                    SqlDataSource sqlDataSource = rp.DataSource as SqlDataSource;
                                    sqlDataSource.Connection.ConnectionString += connect.ConnectString();
                                    rp.Parameters["ID"].Value = ID;
                                    rp.Parameters["ID"].Visible = false;
                                    rp.Parameters["strHoaDon"].Value = "HÓA ĐƠN THANH TOÁN";
                                    rp.Parameters["strHoaDon"].Visible = false;
                                    rp.Print(NamePrinter);
                                }
                            }
                            else
                                MessageBox.Show("Lỗi: Chưa có máy in hóa đơn?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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