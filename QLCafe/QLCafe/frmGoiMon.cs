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
using QLCafe.DAO;
using QLCafe.DTO;
using DevExpress.XtraGrid.Views.Grid;

namespace QLCafe
{
    public partial class frmGoiMon : DevExpress.XtraEditors.XtraForm
    {
        int IDBan = frmBanHang.IDBan;
        DateTime GioVao = frmBanHang.GioVao;
        int IDHoaDon = DAO_BanHang.IDHoaDon(frmBanHang.IDBan);
        List<ChiTietHoaDon> listChiTietHoaDon = new List<ChiTietHoaDon>();

        public delegate void GetKT(int KT, int IDHoaDon);
        public GetKT MyGetData;

        public frmGoiMon()
        {
            InitializeComponent();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa những món đã chọn?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }
        private void frmGoiMon_Load(object sender, EventArgs e)
        {
            listChiTietHoaDon.Clear();
            lblTenBan.Text = DAO_GoiMon.TenBan(IDBan);
            DataTable danhsachhanghoa = BUS_HangHoa.DSHangHoa_Full();
            cmbHangHoa.Properties.DataSource = danhsachhanghoa;
            cmbHangHoa.Properties.ValueMember = "ID";
            cmbHangHoa.Properties.DisplayMember = "TenHangHoa";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "Chọn Hàng Hóa")
            {
                int id = 0;
                object displayValue = cmbHangHoa.EditValue;
                id = Int32.Parse(displayValue.ToString());
                DataTable dt = DAO_HangHoa.DanhSachHangHoa_ID(id);
                ThemMonAn(dt);
                BindGridChiTietHoaDon();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ThemMonAn(DataTable tbThongTin)
        {
            string GioHienTai = GioVao.ToString("hh:mm:ss");
            float TyLeGio = DAO_Gio.LayTyLe(GioHienTai);
            int IDKhuVuc = DAO_BAN.LayIDKhuVuc(IDBan);
            float TyLeKhuVuc = DAO_KhuVuc.LayTyLe(IDKhuVuc);
            int IDHangHoa = Int32.Parse(tbThongTin.Rows[0]["ID"].ToString());
            string MaHangHoa = tbThongTin.Rows[0]["MaHangHoa"].ToString();
            string TenHangHoa = tbThongTin.Rows[0]["TenHangHoa"].ToString();
            string TenDonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
            float GiaBan = float.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
            int IDDonViTinh = Int32.Parse(tbThongTin.Rows[0]["IDDonViTinh"].ToString());
            int idban = IDBan;
            int SL = Int32.Parse(txtSoLuong.Text);
            //Tính phụ thu
            float PhuThuGio = GiaBan * (float)(TyLeGio / 100);
            float PhuThuKhuVuc = GiaBan * (float)(TyLeKhuVuc / 100);
            float DonGiaTong = GiaBan + PhuThuGio + PhuThuKhuVuc;
            //-------------------------------------------
            int KT = 0;
            foreach (ChiTietHoaDon item in listChiTietHoaDon)
            {
                if (item.IDHangHoa == IDHangHoa)
                {
                    KT = 1;
                    item.SoLuong = item.SoLuong + SL;
                    item.ThanhTien = item.SoLuong * item.DonGiaTong;
                    break;
                }
            }
            if (KT == 0)
            {
                listChiTietHoaDon.Add(new ChiTietHoaDon() { IDHangHoa = IDHangHoa, MaHangHoa = MaHangHoa, IDDonViTinh = IDDonViTinh, SoLuong = SL, DonGia = GiaBan, ThanhTien = DonGiaTong * SL, IdBan = idban, TenDonViTinh = TenDonViTinh, TenHangHoa = TenHangHoa, PhuThuGio = PhuThuGio, PhuThuKhuVuc = PhuThuKhuVuc, DonGiaTong = DonGiaTong });
            }
        }
        public void BindGridChiTietHoaDon()
        {
            gridControllHangHoa.DataSource = null;
            gridControllHangHoa.Refresh();
            gridControllHangHoa.DataSource = listChiTietHoaDon;
        }
        public class ChiTietHoaDon
        {
            private float donGiaTong;

            public float DonGiaTong
            {
                get { return donGiaTong; }
                set { donGiaTong = value; }
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


            private string tenHangHoa;

            public string TenHangHoa
            {
                get { return tenHangHoa; }
                set { tenHangHoa = value; }
            }

            private string tenDonViTinh;

            public string TenDonViTinh
            {
                get { return tenDonViTinh; }
                set { tenDonViTinh = value; }
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
            private int idBan;

            public int IdBan
            {
                get { return idBan; }
                set { idBan = value; }
            }
        }

        private void gridViewHangHoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Xóa Món Ăn?", "Thông Báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                GridView view = sender as GridView;
                view.DeleteRow(view.FocusedRowHandle);
                
            }
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            if (listChiTietHoaDon.Count > 0)
            {
                if (IDHoaDon == 0)
                {
                    int IDNhanVien = frmDangNhap.NguoiDung.Id;
                    object ID = DAO_GoiMon.ThemHoaDon(IDBan, IDNhanVien);
                    IDHoaDon = Int32.Parse(ID.ToString());
                    if (ID != null)
                    {
                        foreach (ChiTietHoaDon item in listChiTietHoaDon)
                        {
                            int IDHangHoa = item.IDHangHoa;
                            int SL = item.SoLuong;
                            float DonGia = item.DonGia;
                            float ThanhTien = item.ThanhTien;
                            int IdBan = item.IdBan;
                            string MaHangHoa = item.MaHangHoa;
                            int IDDonViTinh = item.IDDonViTinh;
                            float PhuThuGio = item.PhuThuGio;
                            float PhuThuKhuVuc = item.PhuThuKhuVuc;
                            float GiaTong = item.DonGiaTong;
                            DAO_GoiMon.ThemChiTietHoaDon(ID, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, PhuThuGio, PhuThuKhuVuc, GiaTong);
                        }
                        DAO_BAN.DoiTrangThaiBanCoNguoi(IDBan);
                    }
                }
                else
                {
                    foreach (ChiTietHoaDon item in listChiTietHoaDon)
                    {
                        int IDHangHoa = item.IDHangHoa;
                        int IdBan = item.IdBan;
                        int SL = item.SoLuong;
                        float DonGia = item.DonGia;
                        float ThanhTien = item.ThanhTien;

                        string MaHangHoa = item.MaHangHoa;
                        int IDDonViTinh = item.IDDonViTinh;
                        float PhuThuGio = item.PhuThuGio;
                        float PhuThuKhuVuc = item.PhuThuKhuVuc;
                        float GiaTong = item.DonGiaTong;
                        if (DAO_ChiTietHoaDon.KiemTraHangHoa(IDHoaDon, IDHangHoa, IDBan) == false)
                        {
                            DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, PhuThuGio, PhuThuKhuVuc, GiaTong);
                        }
                        else
                        {
                            DAO_GoiMon.CapNhatChiTietHoaDon(IDHoaDon, SL, ThanhTien, IDHangHoa, IdBan);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Danh sách món ăn rỗng?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (MyGetData != null)
            {
                MyGetData(1, IDHoaDon);
                this.Close();
            }
        }

      
       
       
    }
}