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
using QLCafe.BUS;
using QLCafe.DTO;

namespace QLCafe
{
    public partial class frmChuyenBan : DevExpress.XtraEditors.XtraForm
    {
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        string IDChiNhanh = frmDangNhap.NguoiDung.Idchinhanh;
        public delegate void GetKT(int KT, int IDBanChuyen, int IDBanNhan,int IDHoaDonMoi);
        public GetKT MyGetData;
        List<ChiTietHoaDonA1> listChiTietHoaDonA1 = new List<ChiTietHoaDonA1>();
        List<ChiTietHoaDonB1> listChiTietHoaDonB1 = new List<ChiTietHoaDonB1>();
        public frmChuyenBan()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát. Dữ liệu thay đổi không được lưu lại?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void frmChuyenBan_Load(object sender, EventArgs e)
        {
            DanhSachHangHoaA();

            gridViewA.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridViewB.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            cmbBanA.Properties.NullText = DAO_ChuyenBan.LayTenBan(IDBan);
            int IDkhuVuc = DAO_ChuyenBan.LayIDKhuVuc(IDBan);
            cmbKhuVucA.Properties.NullText = DAO_ChuyenBan.LayTenKhuVuc(IDkhuVuc).ToString();
            DataTable dt = BUS_KhuVuc.DanhSachBanTheoKhuVuc(IDChiNhanh);
            cmbKhuVucB.Properties.DataSource = dt;
            cmbKhuVucB.Properties.ValueMember = "ID";
            cmbKhuVucB.Properties.DisplayMember = "TenKhuVuc";
           
        }
        public void DanhSachBanTheoKhuVuc(int IDKhuVuc)
        {
            //danh sách bàn phải trống
            List<DTO_BAN> ban = DAO_ChuyenBan.DanhSachBanTheoKhuVuc(IDKhuVuc,0,IDBan);
            cmbBanB.Properties.DataSource = ban;
            cmbBanB.Properties.ValueMember = "Id";
            cmbBanB.Properties.DisplayMember = "Tenban";
        }

        private void cmbKhuVucB_EditValueChanged(object sender, EventArgs e)
        {
            cmbBanB.Enabled = true;
            btnAB2.Enabled = false;
            btnThucHien.Enabled = false;
            int IDKhuVuc = Int32.Parse(cmbKhuVucB.EditValue.ToString());
            DanhSachBanTheoKhuVuc(IDKhuVuc);
            listChiTietHoaDonB1.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
        }

        private void cmbBanB_EditValueChanged(object sender, EventArgs e)
        {
            DanhSachHangHoaA();
            listChiTietHoaDonB1.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            btnAB2.Enabled = true;
            btnThucHien.Enabled = true;
        }

        private void btnAB2_Click(object sender, EventArgs e)
        {
            ChuyenASangB();
        }
        public void ChuyenASangB()
        {
            int IDBanMoi = Int32.Parse(cmbBanB.EditValue.ToString());
            int IDKhuVuc = DAO_BAN.LayIDKhuVuc(IDBanMoi);
            foreach (ChiTietHoaDonA1 item in listChiTietHoaDonA1)
            {
                listChiTietHoaDonB1.Add(new ChiTietHoaDonB1
                {
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien =  item.ThanhTien,
                    SoLuong = item.SoLuong,
                });
            }
            listChiTietHoaDonA1.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            gridControlB.DataSource = listChiTietHoaDonB1;

            
            gridControlA.DataSource = null;
            gridControlA.Refresh();
            gridControlA.DataSource = listChiTietHoaDonA1;
        }

        public void DanhSachHangHoaA()
        {
            listChiTietHoaDonA1.Clear();
            List<DTO_DanhSachMenu> DanhSachHoaDonA1 = DAO_DanhSachMonAn.Instance.GetDanhSachMonAn(DAO_BanHang.IDHoaDon(IDBan));
            foreach (DTO_DanhSachMenu item in DanhSachHoaDonA1)
            {
                listChiTietHoaDonA1.Add(new ChiTietHoaDonA1
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
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            int IDBanChuyen = IDBan;
            int IDBanNhan = Int32.Parse(cmbBanB.EditValue.ToString());
            int IDNhanVien = frmDangNhap.NguoiDung.Id;
            // xóa chi tiết bàn A, thêm vào chi tiết bàn B, đổi IDBan trong hóa đơn, đổi trạng thái bàn.
            if (listChiTietHoaDonB1.Count > 0)
            {
                if (DAO_ChuyenBan.XoaChiTietBanCu(IDHoaDon, IDBanChuyen) == true && DAO_BAN.DoiTrangThaiBanCoNguoi(IDBanNhan) == true && DAO_BAN.XoaBanVeMatDinh(IDBanChuyen) == true && DAO_ChuyenBan.CapNhatHoaDon(IDHoaDon, IDBanNhan) == true)// xóa chi tiết hóa đơn củ
                {
                    foreach (ChiTietHoaDonB1 item in listChiTietHoaDonB1)
                    {
                        string MaHang = item.MaHangHoa;
                        int SoLuong = item.SoLuong;
                        int IdBan = IDBanNhan;
                        float DonGia = item.DonGia;
                        float ThanhTien = item.ThanhTien;
                        int IDHangHoa = DAO_Setting.LayIDHangHoa(MaHang);
                        int IDDonViTinh = DAO_Setting.LayIDDonViTinh(MaHang);
                        DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon, IDHangHoa, SoLuong, DonGia, ThanhTien, IdBan, MaHang, IDDonViTinh); // thêm chi tiết hóa đơn mới
                    }
                    if (MyGetData != null)
                    {
                        MyGetData(1, IDBanChuyen, IDBanNhan, IDHoaDon);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Danh sách món ăn rỗng. Vui lòng kiểm tra lại?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //----------------------------------------------------
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
        //----------------------------------------------------
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
        //----------------------------------------------------
   
        private void btnLamLai_Click(object sender, EventArgs e)
        {
            DanhSachHangHoaA();
            listChiTietHoaDonB1.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
        }
    }
}