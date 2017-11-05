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
using QLCafe.BUS;
using DevExpress.XtraGrid.Views.Grid;

namespace QLCafe
{
    public partial class frmGopBan : DevExpress.XtraEditors.XtraForm
    {
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        string IDChiNhanh = frmDangNhap.NguoiDung.Idchinhanh;
        List<ChiTietHoaDonA1> listChiTietHoaDonA1 = new List<ChiTietHoaDonA1>();
        List<ChiTietHoaDonB1> listChiTietHoaDonB1 = new List<ChiTietHoaDonB1>();

        public delegate void GetKT(int KT, int IDBanA, int IDBanB, int IDHoaDonTinhTong);
        public GetKT MyGetDataGopBan;

        public frmGopBan()
        {
            InitializeComponent();
        }
        private void btnAB2_Click(object sender, EventArgs e)
        {
            ChuyenASangB();
        }
       
        public void ChuyenASangB()
        {
            int IDBanMoi = Int32.Parse(cmbBanB.EditValue.ToString());
            foreach (ChiTietHoaDonA1 item in listChiTietHoaDonA1)
            {
                listChiTietHoaDonB1.Add(new ChiTietHoaDonB1
                {
                    // nếu muốn kt trùng là ở đây
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien = item.SoLuong * item.DonGia,
                    SoLuong = item.SoLuong,
                    TrangThai = item.TrangThai,
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
            listChiTietHoaDonB1.Clear(); 
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            btnAB2.Enabled = true;
            btnThucHien.Enabled = true;
            DanhSachHangHoaB(Int32.Parse(cmbBanB.EditValue.ToString()));
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát. Dữ liệu thay đổi không được lưu lại?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void frmGopBan_Load(object sender, EventArgs e)
        {
            LamMoi();
        }
        public void LamMoi()
        {
            gridViewA.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridViewB.OptionsSelection.EnableAppearanceFocusedRow = false;
            DanhSachHangHoaA();
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
            //danh sách bàn phải có người : 2
            List<DTO_BAN> ban = DAO_ChuyenBan.DanhSachBanTheoKhuVuc(IDKhuVuc, 2, IDBan);
            cmbBanB.Properties.DataSource = ban;
            cmbBanB.Properties.ValueMember = "Id";
            cmbBanB.Properties.DisplayMember = "Tenban";
        }
        public void DanhSachHangHoaB(int IDBanB)
        {
            listChiTietHoaDonB1.Clear();
            // lấy món ăn theo IDBan
            List<DTO_ChiTietHoaDon> DanhSachHoaDonB2 = DAO_ChiTietHoaDon.Instance.ChiTietHoaDon(DAO_BanHang.IDHoaDon(IDBanB));
            List<DTO_DanhSachMenu> DanhSachHoaDonB1 = DAO_DanhSachMonAn.Instance.GetDanhSachMonAn(DAO_BanHang.IDHoaDon(IDBanB));
            foreach (DTO_DanhSachMenu item in DanhSachHoaDonB1)
            {
                listChiTietHoaDonB1.Add(new ChiTietHoaDonB1
                {
                    MaHangHoa = item.MaHangHoa,
                    TenHangHoa = item.TenHangHoa,
                    DonViTinh = item.DonViTinh,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    SoLuong = item.SoLuong,
                    TrangThai = item.TrangThai,
                });
            }
            
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            gridControlB.DataSource = DanhSachHoaDonB1;
        }
        public void DanhSachHangHoaA()
        {
            listChiTietHoaDonA1.Clear();
            // lấy món ăn theo IDBan
             
            List<DTO_ChiTietHoaDon> DanhSachHoaDonA2 = DAO_ChiTietHoaDon.Instance.ChiTietHoaDon(DAO_BanHang.IDHoaDon(IDBan));
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
                    TrangThai = item.TrangThai,
                    SoLuong = item.SoLuong,
                });
            }
            gridControlA.DataSource = null;
            gridControlA.Refresh();
            gridControlA.DataSource = DanhSachHoaDonA1;
        }
        private void btnLamLai_Click(object sender, EventArgs e)
        {
            LamMoi();
            int IDBanMoi = 0;
            if (cmbBanB.EditValue != null)
            {
                IDBanMoi = Int32.Parse(cmbBanB.EditValue.ToString());
                DanhSachHangHoaB(IDBanMoi);
            }
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (listChiTietHoaDonA1.Count > 0 && listChiTietHoaDonB1.Count > 0)
            {
                MessageBox.Show("Bạn chưa gộp bàn. Vui lòng kiểm tra lại?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (listChiTietHoaDonB1.Count > 0)
            {
                int IDBanA = IDBan;
                int IDBanB = Int32.Parse(cmbBanB.EditValue.ToString());
                int IDHoaDonB = DAO_BanHang.IDHoaDon(IDBanB);
                int IDHoaDonA = IDHoaDon;
                // A Chuyển sang B, xóa toàn bộ hóa đơn A, cập nhật hóa đơn B, đưa trạng thái bàn A về null, xóa chi tiết bàn B
                if (DAO_BAN.XoaBanVeMatDinh(IDBanA) == true && DAO_ChuyenBan.XoaChiTietBanCu(IDHoaDonA, IDBanA) == true && DAO_GopBan.XoaHoaDonCu(IDHoaDonA, IDBanA) == true && DAO_ChuyenBan.XoaChiTietBanCu(IDHoaDonB, IDBanB) == true)
                {
                    //Thêm lại chi tiết bàn B,
                    foreach (ChiTietHoaDonB1 item in listChiTietHoaDonB1)
                    {
                        string MaHangHoa = item.MaHangHoa;
                        int IDHangHoa = DAO_Setting.LayIDHangHoa(MaHangHoa);
                        int SL = item.SoLuong;
                        float DonGia = item.DonGia;
                        float ThanhTien = item.ThanhTien;
                        int IDDonViTinh = DAO_Setting.LayIDDonViTinh(MaHangHoa);
                        int TrangThai = item.TrangThai;
                        if (DAO_ChiTietHoaDon.KiemTraHangHoaTrangThai(IDHoaDonB, IDHangHoa, IDBanB, TrangThai) == false)
                        {
                            DAO_GoiMon.ThemChiTietHoaDonTrangThai(IDHoaDonB, IDHangHoa, SL, DonGia, ThanhTien, IDBanB, MaHangHoa, IDDonViTinh, TrangThai); 
                        }
                        else
                        {
                            DAO_GoiMon.CapNhatChiTietHoaDonTrangThai(IDHoaDonB, SL, ThanhTien, IDHangHoa, IDBanB, TrangThai);
                        }
                    }
                    if (MyGetDataGopBan != null)
                    {
                        MyGetDataGopBan(1, IDBanA, IDBanB, IDHoaDonB);
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Gộp bàn thất bại. Vui lòng kiểm tra lại?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------
        public class ChiTietHoaDonA1
        {
            private int trangThai;

            public int TrangThai
            {
                get { return trangThai; }
                set { trangThai = value; }
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
        //--------------------------------------------
        public class ChiTietHoaDonB1
        {
            private int trangThai;

            public int TrangThai
            {
                get { return trangThai; }
                set { trangThai = value; }
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

        private void gridViewA_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
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

        private void gridViewB_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
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