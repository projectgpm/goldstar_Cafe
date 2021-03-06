﻿using System;
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
            DanhSachMonAnBanChay();
            listChiTietHoaDon.Clear();
            lblTenBan.Text = DAO_GoiMon.TenBan(IDBan);
            DataTable danhsachtuchon = DAO_HangHoa.DanhSachTuChon();
            cmbHangHoaTuChon.Properties.DataSource = danhsachtuchon;
            cmbHangHoaTuChon.Properties.ValueMember = "ID";
            cmbHangHoaTuChon.Properties.DisplayMember = "TenNguyenLieu";
        }
        public void DanhSachMonAnBanChay()
        {
            tblTableMonAn.Controls.Clear();
            if (txtTimKiem.Text == "")
            {
                DataTable db = BUS_HangHoa.DSHangHoa_Full();
                if (db.Rows.Count > 0)
                {
                    foreach (DataRow dr in db.Rows)
                    {
                        SimpleButton btn = new SimpleButton();
                        btn.Width = 108;
                        btn.Height = 60;
                        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                        btn.Text = dr["TenHangHoa"].ToString();
                        btn.Click += btn_Click;
                        btn.Tag = dr["ID"].ToString();
                        btn.ForeColor = Color.Black;
                        btn.StyleController = null;
                        btn.LookAndFeel.UseDefaultLookAndFeel = false;
                        tblTableMonAn.Controls.Add(btn);
                    }
                }
            }
            else
            {
                DataTable db = BUS_HangHoa.DSHangHoaTimKiem(txtTimKiem.Text.ToString());
                if (db.Rows.Count > 0)
                {
                    foreach (DataRow dr in db.Rows)
                    {
                        SimpleButton btn = new SimpleButton();
                        btn.Width = 108;
                        btn.Height = 60;
                        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                        btn.Text = dr["TenHangHoa"].ToString();
                        btn.Click += btn_Click;
                        btn.Tag = dr["ID"].ToString();
                        btn.ForeColor = Color.Black;
                        btn.StyleController = null;
                        btn.LookAndFeel.UseDefaultLookAndFeel = false;
                        tblTableMonAn.Controls.Add(btn);
                    }
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string ID = (sender as SimpleButton).Tag.ToString();
            DataTable dt = DAO_HangHoa.DanhSachHangHoa_ID(Int32.Parse(ID));
            ThemMonAn(dt,0);
            BindGridChiTietHoaDon();
        }
        public void ThemMonAn(DataTable tbThongTin, double TrongLuong)
        {
            int IDBangGia = DAO_GoiMon.LayIDBanGia(IDBan);
            int IDHangHoa = Int32.Parse(tbThongTin.Rows[0]["ID"].ToString());
            string MaHangHoa = tbThongTin.Rows[0]["MaHangHoa"].ToString();
            string TenHangHoa = tbThongTin.Rows[0]["TenHangHoa"].ToString();
            string TenDonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
            double GiaBan = double.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
            if (IDBangGia != 0)
            {
                GiaBan = DAO_GoiMon.LayGiaBan(IDHangHoa, IDBangGia);
            }
            int IDMayIn;
            if (TrongLuong > 0)
            {
                // tự chọn
                IDMayIn = DAO_GoiMon.LayIDMayInNguyenLieu(IDHangHoa);
            }
            else
            {
                //k tự chọn
                IDMayIn = DAO_GoiMon.LayIDMayInHangHoa(IDHangHoa);
            }
            int IDDonViTinh = Int32.Parse(tbThongTin.Rows[0]["IDDonViTinh"].ToString());
            int idban = IDBan;
            int SL = 1;
            //-------------------------------------------
            int KT = 0;
            foreach (ChiTietHoaDon item in listChiTietHoaDon)
            {
                if (item.IDHangHoa == IDHangHoa)
                {
                    KT = 1;
                    item.SoLuong = item.SoLuong + SL;
                    item.ThanhTien = item.SoLuong * item.DonGia;
                    break;
                }
            }
            if (KT == 0)
            {
                listChiTietHoaDon.Add(new ChiTietHoaDon()
                {
                    IDHangHoa = IDHangHoa,
                    MaHangHoa = MaHangHoa,
                    IDDonViTinh = IDDonViTinh,
                    SoLuong = SL,
                    DonGia = GiaBan,
                    ThanhTien = GiaBan * SL,
                    IdBan = idban,
                    TenDonViTinh = TenDonViTinh,
                    TenHangHoa = TenHangHoa,
                    TrongLuong = TrongLuong,
                    IdMayIn = IDMayIn
                });
            }
        }
        public void BindGridChiTietHoaDon()
        {
            txtTrongLuongTuChon.Text = "0";
            gridViewHangHoa.OptionsSelection.EnableAppearanceFocusedRow = false;// Ẩn dòng đầu...
            gridControllHangHoa.DataSource = null;
            gridControllHangHoa.Refresh();
            gridControllHangHoa.DataSource = listChiTietHoaDon;
        }
        private void gridViewHangHoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Xóa Món Ăn?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
                            double DonGia = item.DonGia;
                            double ThanhTien = item.ThanhTien;
                            int IdBan = item.IdBan;
                            string MaHangHoa = item.MaHangHoa;
                            int IDDonViTinh = item.IDDonViTinh;
                            double TrongLuong = item.TrongLuong;
                            int IDMayIn = item.IdMayIn;
                            DAO_GoiMon.ThemChiTietHoaDon(ID, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, TrongLuong, IDMayIn,0);
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
                        double DonGia = item.DonGia;
                        double ThanhTien = item.ThanhTien;
                        string MaHangHoa = item.MaHangHoa;
                        int IDDonViTinh = item.IDDonViTinh;
                        double TrongLuong = item.TrongLuong;
                        int IDMayIn = item.IdMayIn;
                        if (DAO_ChiTietHoaDon.KiemTraHangHoa(IDHoaDon, IDHangHoa, IDBan, TrongLuong) == false)
                        {
                            DAO_GoiMon.ThemChiTietHoaDon(IDHoaDon, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, TrongLuong, IDMayIn,0);
                        }
                        else
                        {
                            DAO_GoiMon.CapNhatChiTietHoaDon(IDHoaDon, SL, ThanhTien, IDHangHoa, IdBan,0);
                        }
                    }
                }
                if (MyGetData != null)
                {
                    MyGetData(1, IDHoaDon);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Danh sách món ăn rỗng?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        //------------------------------------------
        public class ChiTietHoaDon
        {
            private int idMayIn;

            public int IdMayIn
            {
                get { return idMayIn; }
                set { idMayIn = value; }
            }
            private double trongLuong;

            public double TrongLuong
            {
                get { return trongLuong; }
                set { trongLuong = value; }
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

        private void btnLuuTrongLuong_Click(object sender, EventArgs e)
        {

            if (cmbHangHoaTuChon.Text != "Chọn Hàng Hóa")
            {
                if (txtTrongLuongTuChon.Text == "0")
                {
                    txtTrongLuongTuChon.Focus();
                    MessageBox.Show("Vui lòng nhập trọng lượng?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int id = 0;
                    object displayValue = cmbHangHoaTuChon.EditValue;
                    id = Int32.Parse(displayValue.ToString());
                    DataTable dt = DAO_HangHoa.DanhSachTuChon_ID(id);
                    int IDHangHoa = Int32.Parse(dt.Rows[0]["ID"].ToString());
                    string MaHangHoa = dt.Rows[0]["MaNguyenLieu"].ToString();
                    string TenHangHoa = dt.Rows[0]["TenNguyenLieu"].ToString();
                    string TenDonViTinh = dt.Rows[0]["TenDonViTinh"].ToString();
                    double GiaBan = double.Parse(txtDonGiaBan.Text.ToString());
                    int IDDonViTinh = Int32.Parse(dt.Rows[0]["IDDonViTinh"].ToString());
                    int idban = IDBan;
                    int SL = 1;
                    double TrongLuong = double.Parse(txtTrongLuongTuChon.Text.ToString());
                    //-------------------------------------------
                    int IDMayIn;
                    if (TrongLuong > 0)
                    {
                        // tự chọn
                        IDMayIn = DAO_GoiMon.LayIDMayInNguyenLieu(IDHangHoa);
                    }
                    else
                    {
                        //k tự chọn
                        IDMayIn = DAO_GoiMon.LayIDMayInHangHoa(IDHangHoa);
                    }
                    int KT = 0;
                    foreach (ChiTietHoaDon item in listChiTietHoaDon)
                    {
                        if (item.IDHangHoa == IDHangHoa && item.TrongLuong == TrongLuong)
                        {
                            KT = 1;
                            item.SoLuong = item.SoLuong + SL;
                            item.ThanhTien = item.SoLuong * (item.TrongLuong * item.DonGia);
                            break;
                        }
                    }
                    if (KT == 0)
                    {
                        listChiTietHoaDon.Add(new ChiTietHoaDon()
                        {
                            IDHangHoa = IDHangHoa,
                            MaHangHoa = MaHangHoa,
                            IDDonViTinh = IDDonViTinh,
                            SoLuong = SL,
                            DonGia = GiaBan,
                            ThanhTien =  (TrongLuong * GiaBan) * SL,
                            IdBan = idban,
                            TenDonViTinh = TenDonViTinh,
                            TenHangHoa = TenHangHoa,
                            TrongLuong = TrongLuong,
                            IdMayIn = IDMayIn,
                        });
                    }
                    BindGridChiTietHoaDon();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn tự chọn?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHangHoaTuChon_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbHangHoaTuChon.Text != "Chọn Hàng Hóa")
            {
                string IDHangHoa = cmbHangHoaTuChon.EditValue.ToString();
                double GiaBan = DAO_GoiMon.LayGiaBanTuChon(IDHangHoa);
                txtDonGiaBan.Text = GiaBan.ToString();
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            DanhSachMonAnBanChay();
        }

      
    }
}