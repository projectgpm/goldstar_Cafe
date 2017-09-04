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
    public partial class frmTachBan : DevExpress.XtraEditors.XtraForm
    {
        int IDBan = frmBanHang.IDBan;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        string IDChiNhanh = frmDangNhap.NguoiDung.Idchinhanh;
        List<ChiTietHoaDonA1> listChiTietHoaDonA1 = new List<ChiTietHoaDonA1>();
        List<ChiTietHoaDonA2> listChiTietHoaDonA2 = new List<ChiTietHoaDonA2>();
        List<ChiTietHoaDonB1> listChiTietHoaDonB1 = new List<ChiTietHoaDonB1>();
        List<ChiTietHoaDonB2> listChiTietHoaDonB2 = new List<ChiTietHoaDonB2>();
        public frmTachBan()
        {
            InitializeComponent();
        }

        private void btnAB2_Click(object sender, EventArgs e)
        {
            frmXacNhan fr = new frmXacNhan();
            fr.getdate = new frmXacNhan.GetString(GetValue1);
            fr.ShowDialog();
        }

        private void btnBA1_Click(object sender, EventArgs e)
        {
            frmXacNhan fr = new frmXacNhan();
            fr.getdate = new frmXacNhan.GetString(GetValue2);
            fr.ShowDialog();
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
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn thật sự muốn thoát. Dữ liệu thay đổi không được lưu lại?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void cmbKhuVucB_EditValueChanged(object sender, EventArgs e)
        {
            cmbBanB.Enabled = true;
            btnBA1.Enabled = false;
            btnAB2.Enabled = false;
            btnThucHien.Enabled = false;
            int IDKhuVuc = Int32.Parse(cmbKhuVucB.EditValue.ToString());
            DanhSachBanTheoKhuVuc(IDKhuVuc);
            DanhSachHangHoaA();
            listChiTietHoaDonB1.Clear();
            listChiTietHoaDonB2.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
        }

        private void cmbBanB_EditValueChanged(object sender, EventArgs e)
        {

            listChiTietHoaDonB1.Clear();
            listChiTietHoaDonB2.Clear();
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            LamMoi();
            //btnBA1.Enabled = true;
            //btnAB2.Enabled = true;
            btnThucHien.Enabled = true;
            DanhSachHangHoaB(Int32.Parse(cmbBanB.EditValue.ToString()));
        }
        public void DanhSachHangHoaB(int IDBanB)
        {
            listChiTietHoaDonB1.Clear();
            listChiTietHoaDonB2.Clear();
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
                    GiaTong = item.GiaTong,
                    PhuThuGio = item.PhuThuGio,
                    PhuThuKhuVuc = item.PhuThuKhuVuc,
                    SoLuong = item.SoLuong,
                });
            }
            foreach (DTO_ChiTietHoaDon item in DanhSachHoaDonB2)
            {
                listChiTietHoaDonB2.Add(new ChiTietHoaDonB2
                {
                    IDHoaDon = item.IDHoaDon,
                    IDHangHoa = item.IDHangHoa,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    IDBan = item.IdBan,
                    MaHangHoa = item.MaHangHoa,
                    IDDonViTinh = item.IDDonViTinh,
                    PhuThuGio = item.PhuThuGio,
                    PhuThuKhuVuc = item.PhuThuKhuVuc,
                    GiaTong = item.GiaTong,
                });
            }
            gridControlB.DataSource = null;
            gridControlB.Refresh();
            gridControlB.DataSource = DanhSachHoaDonB1;
        }
        public void GetValue1(int PhuThuTheoGio, int PhuThuTheoKhuVuc)
        {
            ChuyenASangB(PhuThuTheoGio, PhuThuTheoKhuVuc);
        }
        public void GetValue2(int PhuThuTheoGio, int PhuThuTheoKhuVuc)
        {
            ChuyenBSangA(PhuThuTheoGio, PhuThuTheoKhuVuc);
        }
        public void ChuyenASangB(int KTGio, int KTKhuVuc)
        {
            string GioHienTai;
            if (KTGio == 1)
            {
                if (listChiTietHoaDonB1.Count == 0)
                {
                    GioHienTai = DateTime.Now.ToString("hh:mm:ss tt");
                }
                else
                {
                    DateTime GioVao = DAO_BanHang.GioVao_IDBan(Int32.Parse(cmbBanB.EditValue.ToString()));
                    GioHienTai = GioVao.ToString("hh:mm:ss tt");
                }
            }
            else
            {
                DateTime GioVao = DAO_BanHang.GioVao_IDBan(IDBan);
                GioHienTai = GioVao.ToString("hh:mm:ss tt");
            }
            int IDBanMoi = Int32.Parse(cmbBanB.EditValue.ToString());
            float TyLeGio = DAO_Gio.LayTyLe(GioHienTai);
            int IDKhuVuc = DAO_BAN.LayIDKhuVuc(IDBanMoi);
            float TyLeKhuVuc = DAO_KhuVuc.LayTyLe(IDKhuVuc);

            float GiaTheoGio = 0, GiaTheoKhuVuc = 0;
            if (listChiTietHoaDonA1.Count > 1)
            {
                string TenHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[1]).ToString();
                string MaHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[0]).ToString();
                string DonViTinh = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[2]).ToString();
                float DonGia = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[4]).ToString());
                float PhuThuGio = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[5]).ToString());
                float PhuThuKhuVuc = float.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[6]).ToString());
                int SoLuong = Int32.Parse(gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[3]).ToString());
                int IDHoaDon = DAO_BanHang.IDHoaDon(IDBan);
                int dongHienTai = gridViewA.FocusedRowHandle;
                if (KTGio == 1)
                {
                    GiaTheoGio = DonGia * (float)(TyLeGio / 100);
                }
                else
                {
                    GiaTheoGio = PhuThuGio;
                }
                if (KTKhuVuc == 1)
                {
                    GiaTheoKhuVuc = DonGia * (float)(TyLeKhuVuc / 100);
                }
                else
                {
                    GiaTheoKhuVuc = PhuThuKhuVuc;
                }
                float DonGiaTong = DonGia + GiaTheoGio + GiaTheoKhuVuc;
                 listChiTietHoaDonB1.Add(new ChiTietHoaDonB1
                    {
                        // nếu muốn kt trùng là ở đây
                        MaHangHoa = MaHangHoa,
                        TenHangHoa = TenHangHoa,
                        DonViTinh = DonViTinh,
                        DonGia = DonGia,
                        ThanhTien = SoLuong * DonGiaTong,
                        GiaTong = DonGiaTong,
                        PhuThuGio = GiaTheoGio,
                        PhuThuKhuVuc = GiaTheoKhuVuc,
                        SoLuong = SoLuong,
                    });
                listChiTietHoaDonA1.RemoveAt(dongHienTai);
                gridControlB.DataSource = null;
                gridControlB.Refresh();
                gridControlB.DataSource = listChiTietHoaDonB1;

                gridControlA.DataSource = null;
                gridControlA.Refresh();
                gridControlA.DataSource = listChiTietHoaDonA1;
            }
            else
            {
                MessageBox.Show("Bạn đang chọn trạng thái tách bàn? Danh sách món ăn còn lại phải lớn hơn hoặc bằng 1.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if (listChiTietHoaDonA2.Count > 1)
            //{
            //    foreach (ChiTietHoaDonA2 item in listChiTietHoaDonA2)
            //    {
            //        float DonGiaTong = item.DonGia + GiaTheoGio + GiaTheoKhuVuc;
            //        listChiTietHoaDonB2.Add(new ChiTietHoaDonB2
            //        {
            //            IDHoaDon = item.IDHoaDon,
            //            IDHangHoa = item.IDHangHoa,
            //            SoLuong = item.SoLuong,
            //            DonGia = item.DonGia,
            //            ThanhTien = item.SoLuong * DonGiaTong,
            //            IDBan = IDBanMoi,
            //            MaHangHoa = item.MaHangHoa,
            //            IDDonViTinh = item.IDDonViTinh,
            //            PhuThuGio = GiaTheoGio,
            //            PhuThuKhuVuc = GiaTheoKhuVuc,
            //            GiaTong = DonGiaTong,
            //        });
            //    }
            //    listChiTietHoaDonA2.Clear();
            //    gridControlA.DataSource = null;
            //    gridControlA.Refresh();
            //    gridControlA.DataSource = listChiTietHoaDonA1;
            //}
        }
        public void ChuyenBSangA(int KTGio, int KTKhuVuc)
        {
            string GioHienTai;
            if (KTGio == 1)
            {
                DateTime GioVao = DAO_BanHang.GioVao_IDBan(IDBan);
                GioHienTai = GioVao.ToString("hh:mm:ss");
            }
            else
            {
                DateTime GioVao = DAO_BanHang.GioVao_IDBan(Int32.Parse(cmbBanB.EditValue.ToString()));
                GioHienTai = GioVao.ToString("hh:mm:ss");
            }
            int IDBanMoi = IDBan;
            float TyLeGio = DAO_Gio.LayTyLe(GioHienTai);
            int IDKhuVuc = DAO_BAN.LayIDKhuVuc(IDBanMoi);
            float TyLeKhuVuc = DAO_KhuVuc.LayTyLe(IDKhuVuc);

            float GiaTheoGio = 0, GiaTheoKhuVuc = 0;
            if (listChiTietHoaDonB1.Count > 1)
            {
                string TenHangHoa = gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[1]).ToString();
                string MaHangHoa = gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[0]).ToString();
                string DonViTinh = gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[2]).ToString();
                float DonGia = float.Parse(gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[4]).ToString());
                float PhuThuGio = float.Parse(gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[5]).ToString());
                float PhuThuKhuVuc = float.Parse(gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[6]).ToString());
                int SoLuong = Int32.Parse(gridViewB.GetRowCellValue(gridViewB.FocusedRowHandle, gridViewB.Columns[3]).ToString());
                int IDHoaDon = DAO_BanHang.IDHoaDon(IDBan);
                int dongHienTai = gridViewB.FocusedRowHandle;
                if (KTGio == 1)
                {
                    GiaTheoGio = DonGia * (float)(TyLeGio / 100);
                }
                else
                {
                    GiaTheoGio = PhuThuGio;
                }
                if (KTKhuVuc == 1)
                {
                    GiaTheoKhuVuc = DonGia * (float)(TyLeKhuVuc / 100);
                }
                else
                {
                    GiaTheoKhuVuc = PhuThuKhuVuc;
                }
                float DonGiaTong = DonGia + GiaTheoGio + GiaTheoKhuVuc;
                listChiTietHoaDonA1.Add(new ChiTietHoaDonA1
                {
                    // nếu muốn kt trùng là ở đây
                    MaHangHoa = MaHangHoa,
                    TenHangHoa = TenHangHoa,
                    DonViTinh = DonViTinh,
                    DonGia = DonGia,
                    ThanhTien = SoLuong * DonGiaTong,
                    GiaTong = DonGiaTong,
                    PhuThuGio = GiaTheoGio,
                    PhuThuKhuVuc = GiaTheoKhuVuc,
                    SoLuong = SoLuong,
                });
                listChiTietHoaDonB1.RemoveAt(dongHienTai);
                gridControlA.DataSource = null;
                gridControlA.Refresh();
                gridControlA.DataSource = listChiTietHoaDonA1;

                gridControlB.DataSource = null;
                gridControlB.Refresh();
                gridControlB.DataSource = listChiTietHoaDonB1;
            }
            else
            {
                MessageBox.Show("Bạn đang chọn trạng thái tách bàn? Danh sách món ăn còn lại phải lơn hơn hoặc bằng 1.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void DanhSachBanTheoKhuVuc(int IDKhuVuc)
        {
            //danh sách bàn tất cả
            List<DTO_BAN> ban = DAO_ChuyenBan.DanhSachBanTheoKhuVuc(IDKhuVuc, -1, IDBan);
            cmbBanB.Properties.DataSource = ban;
            cmbBanB.Properties.ValueMember = "Id";
            cmbBanB.Properties.DisplayMember = "Tenban";
        }
        private void frmTachBan_Load(object sender, EventArgs e)
        {
            LamMoi();
        }
        public void LamMoi()
        {
            DanhSachHangHoaA();
            cmbBanA.Properties.NullText = DAO_ChuyenBan.LayTenBan(IDBan);
            int IDkhuVuc = DAO_ChuyenBan.LayIDKhuVuc(IDBan);
            cmbKhuVucA.Properties.NullText = DAO_ChuyenBan.LayTenKhuVuc(IDkhuVuc).ToString();
            DataTable dt = BUS_KhuVuc.DanhSachBanTheoKhuVuc(IDChiNhanh);
            cmbKhuVucB.Properties.DataSource = dt;
            cmbKhuVucB.Properties.ValueMember = "ID";
            cmbKhuVucB.Properties.DisplayMember = "TenKhuVuc";
        }
        public void DanhSachHangHoaA()
        {
            listChiTietHoaDonA1.Clear();
            listChiTietHoaDonA2.Clear();
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
                    GiaTong = item.GiaTong,
                    PhuThuGio = item.PhuThuGio,
                    PhuThuKhuVuc = item.PhuThuKhuVuc,
                    SoLuong = item.SoLuong,
                });
            }
            foreach (DTO_ChiTietHoaDon item in DanhSachHoaDonA2)
            {
                listChiTietHoaDonA2.Add(new ChiTietHoaDonA2
                {
                    IDHoaDon = item.IDHoaDon,
                    IDHangHoa = item.IDHangHoa,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien,
                    IDBan = item.IdBan,
                    MaHangHoa = item.MaHangHoa,
                    IDDonViTinh = item.IDDonViTinh,
                    PhuThuGio = item.PhuThuGio,
                    PhuThuKhuVuc = item.PhuThuKhuVuc,
                    GiaTong = item.GiaTong,
                });
            }
            gridControlA.DataSource = null;
            gridControlA.Refresh();
            gridControlA.DataSource = DanhSachHoaDonA1;
        }
        public class ChiTietHoaDonA1
        {
            private float giaTong;

            public float GiaTong
            {
                get { return giaTong; }
                set { giaTong = value; }
            }
            private float phuThuKhuVuc;

            public float PhuThuKhuVuc
            {
                get { return phuThuKhuVuc; }
                set { phuThuKhuVuc = value; }
            }
            private float phuThuGio;

            public float PhuThuGio
            {
                get { return phuThuGio; }
                set { phuThuGio = value; }
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
        public class ChiTietHoaDonA2
        {
            private int iDHoaDon;

            public int IDHoaDon
            {
                get { return iDHoaDon; }
                set { iDHoaDon = value; }
            }
            private int iDHangHoa;

            public int IDHangHoa
            {
                get { return iDHangHoa; }
                set { iDHangHoa = value; }
            }
            private int soLuong;

            public int SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            private float donGia;

            public float DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private float thanhTien;

            public float ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int iDBan;

            public int IDBan
            {
                get { return iDBan; }
                set { iDBan = value; }
            }
            private string maHangHoa;

            public string MaHangHoa
            {
                get { return maHangHoa; }
                set { maHangHoa = value; }
            }
            private int iDDonViTinh;

            public int IDDonViTinh
            {
                get { return iDDonViTinh; }
                set { iDDonViTinh = value; }
            }
            private float phuThuGio;

            public float PhuThuGio
            {
                get { return phuThuGio; }
                set { phuThuGio = value; }
            }
            private float phuThuKhuVuc;

            public float PhuThuKhuVuc
            {
                get { return phuThuKhuVuc; }
                set { phuThuKhuVuc = value; }
            }
            private float giaTong;

            public float GiaTong
            {
                get { return giaTong; }
                set { giaTong = value; }
            }
        }
        public class ChiTietHoaDonB1
        {
            private float giaTong;

            public float GiaTong
            {
                get { return giaTong; }
                set { giaTong = value; }
            }
            private float phuThuKhuVuc;

            public float PhuThuKhuVuc
            {
                get { return phuThuKhuVuc; }
                set { phuThuKhuVuc = value; }
            }
            private float phuThuGio;

            public float PhuThuGio
            {
                get { return phuThuGio; }
                set { phuThuGio = value; }
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
        public class ChiTietHoaDonB2
        {
            private int iDHoaDon;

            public int IDHoaDon
            {
                get { return iDHoaDon; }
                set { iDHoaDon = value; }
            }
            private int iDHangHoa;

            public int IDHangHoa
            {
                get { return iDHangHoa; }
                set { iDHangHoa = value; }
            }
            private int soLuong;

            public int SoLuong
            {
                get { return soLuong; }
                set { soLuong = value; }
            }
            private float donGia;

            public float DonGia
            {
                get { return donGia; }
                set { donGia = value; }
            }
            private float thanhTien;

            public float ThanhTien
            {
                get { return thanhTien; }
                set { thanhTien = value; }
            }
            private int iDBan;

            public int IDBan
            {
                get { return iDBan; }
                set { iDBan = value; }
            }
            private string maHangHoa;

            public string MaHangHoa
            {
                get { return maHangHoa; }
                set { maHangHoa = value; }
            }
            private int iDDonViTinh;

            public int IDDonViTinh
            {
                get { return iDDonViTinh; }
                set { iDDonViTinh = value; }
            }
            private float phuThuGio;

            public float PhuThuGio
            {
                get { return phuThuGio; }
                set { phuThuGio = value; }
            }
            private float phuThuKhuVuc;

            public float PhuThuKhuVuc
            {
                get { return phuThuKhuVuc; }
                set { phuThuKhuVuc = value; }
            }
            private float giaTong;

            public float GiaTong
            {
                get { return giaTong; }
                set { giaTong = value; }
            }
        }


        private void gridControlA_Click(object sender, EventArgs e)
        {
            if (cmbBanB.EditValue != null)
            {
                btnBA1.Enabled = false;
                btnAB2.Enabled = true;
                //string TenHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[1]).ToString();
                //string MaHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[0]).ToString();
                //int IDHoaDon = DAO_BanHang.IDHoaDon(IDBan);
            }
        }

        private void gridControlB_Click(object sender, EventArgs e)
        {
            if (cmbBanB.EditValue != null)
            {
                btnBA1.Enabled = true;
                btnAB2.Enabled = false;
                //string TenHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[1]).ToString();
                //string MaHangHoa = gridViewA.GetRowCellValue(gridViewA.FocusedRowHandle, gridViewA.Columns[0]).ToString();
                //int IDBanMoi = Int32.Parse(cmbBanB.EditValue.ToString());
                //int IDHoaDon = DAO_BanHang.IDHoaDon(IDBanMoi);
            }
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            // lấy lại dữ liệu A và B, xóa chi tiết A, thêm lại A, Thêm B + Hóa Đơn Mới(if đã tồn tại thêm chèn vào)
        }
    }
}