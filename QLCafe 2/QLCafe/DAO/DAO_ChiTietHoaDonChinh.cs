﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_ChiTietHoaDonChinh
    {
        public static bool CongDiemTichLuy(string IDKhachHang, string DiemTichLuy)
        {
            CongLichSu(IDKhachHang, DiemTichLuy);
            string sTruyVan = string.Format(@"UPDATE [GPM_KhachHang] SET DiemTichLuy = DiemTichLuy +  '{0}' WHERE ID = {1}", DiemTichLuy.ToString(), IDKhachHang);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);

        }
        public static bool TruDiemTichLuy(string IDKhachHang, string DiemTichLuy)
        {
            TruLichSu(IDKhachHang, DiemTichLuy);
            string sTruyVan = string.Format(@"UPDATE [GPM_KhachHang] SET DiemTichLuy = DiemTichLuy - '{0}' WHERE ID = '{1}'", DiemTichLuy, IDKhachHang);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);

        }
        public static bool TruLichSu(string IDKhachHang, string DiemTichLuy)
        {
            string DiemCu = DAO_Setting.LayDiemHienTai(IDKhachHang).ToString();
            string sTruyVan = string.Format(@"INSERT INTO [GPM_LichSuQuyDoiDiem]([IDKhachHang],[SoDiemCu],[SoDiemMoi],[NoiDung],[Ngay],[HinhThuc]) VALUES('{0}','{1}','{2}',N'Thanh toán Nhà hàng - Cafe',getdate(),N'Trừ')", IDKhachHang, DiemCu, (double.Parse(DiemCu) - double.Parse(DiemTichLuy)).ToString());
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CongLichSu(string IDKhachHang, string DiemTichLuy)
        {
            string DiemCu = DAO_Setting.LayDiemHienTai(IDKhachHang).ToString();
            string sTruyVan = string.Format(@"INSERT INTO [GPM_LichSuQuyDoiDiem]([IDKhachHang],[SoDiemCu],[SoDiemMoi],[NoiDung],[Ngay],[HinhThuc]) VALUES('{0}','{1}','{2}',N'Thanh toán Nhà hàng - Cafe',getdate(),N'Cộng')", IDKhachHang, DiemCu, (double.Parse(DiemCu) + double.Parse(DiemTichLuy)).ToString());
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool ThemChiTietHoaDonChinh(int IDHoaDon, int IDHangHoa, int SL, double DonGia, double ThanhTien, int IDBan, string MaHangHoa, int IDDonViTinh, float TrongLuong)
        {
            string sTruyVan = string.Format(@"INSERT INTO CF_ChiTietHoaDon(IDHoaDon,IDHangHoa,SoLuong,DonGia,ThanhTien,IDBan,MaHangHoa,IDDonViTinh,TrongLuong) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", IDHoaDon, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, TrongLuong);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool XoaChiTietHoaDonTemp(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"DELETE FROM [CF_ChiTietHoaDon_Temp] WHERE IDHoaDon = {0}", IDHoaDon);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatChiTietGio(int IDHoaDon, int IDBan)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_ChiTietGio] SET [ThanhToan] = 1 WHERE [TrangThai] = 1 AND [IDHoaDon] = {0} AND [IDBan] = {1}", IDHoaDon, IDBan);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatHoaDonChinh(int IDHoaDon, int IDBan, int IDNhanVien, string KhachThanhToan, string TienThua, string KhachCanTra, string HinhThucGiamGia, string GiamGia, string IDKhachHang, string DiemQuiDoi, string GiamGiaDiem, string GiamGiaHoaDon, string TongGiamGia, string TyLeGiamGia)
        {
            //lấy mã hóa đơn
            string CompuMaHoaDon = @"SELECT 
                                          REPLICATE('0', 5 - LEN((count(ID) + 1))) + 
                                          CAST((count(ID) + 1) AS varchar) + '-' + 
                                          FORMAT(GETDATE() , 'ddMMyy')
                                          as 'Mã Hóa Đơn'  
                                          from CF_HoaDon 
                                          where MaHoaDon is not null AND DATEDIFF(dd,NgayBan, GetDate()) = 0";
            string MaHoaDon = "";
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(CompuMaHoaDon);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                MaHoaDon = dr["Mã Hóa Đơn"].ToString();
            }

            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [MaHoaDon] = '{14}',[TyLeGiamGia] = '{13}', [TongGiamGia] = '{12}',[GiamGiaHoaDon] = '{11}',[GiamGiaDiem] = '{10}',[DiemQuiDoi] = '{9}',[IDKhachHang] = '{8}',[GiamGia] = '{7}',[HinhThucGiamGia] = N'{6}',[KhachCanTra] = '{5}',[TrangThai] = 1, [GioRa] = getdate(), [IDNhanVien] = {0},[KhachThanhToan] = '{1}', [TienThua] = '{2}' WHERE [ID] = {3} AND [IDBan] = {4}", IDNhanVien, KhachThanhToan, TienThua, IDHoaDon, IDBan, KhachCanTra, HinhThucGiamGia, GiamGia, IDKhachHang, DiemQuiDoi, GiamGiaDiem, GiamGiaHoaDon, TongGiamGia, TyLeGiamGia, MaHoaDon.ToString());
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatHoaDonChinhTemp(int IDHoaDon, int IDBan, int IDNhanVien, string KhachThanhToan, string TienThua, string KhachCanTra, string HinhThucGiamGia, string GiamGia, string IDKhachHang, string DiemQuiDoi, string GiamGiaDiem, string GiamGiaHoaDon, string TongGiamGia, string TyLeGiamGia)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [TyLeGiamGia] = '{13}',[TongGiamGia] = '{12}',[GiamGiaHoaDon] = '{11}',[GiamGiaDiem] = '{10}',[DiemQuiDoi] = '{9}',[IDKhachHang] = '{8}',[GiamGia] = '{7}',[HinhThucGiamGia] = N'{6}',[KhachCanTra] = '{5}', [GioRa] = getdate(), [IDNhanVien] = {0},[KhachThanhToan] = '{1}', [TienThua] = '{2}' WHERE [ID] = {3} AND [IDBan] = {4}", IDNhanVien, KhachThanhToan, TienThua, IDHoaDon, IDBan, KhachCanTra, HinhThucGiamGia, GiamGia, IDKhachHang, DiemQuiDoi, GiamGiaDiem, GiamGiaHoaDon, TongGiamGia, TyLeGiamGia);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatChiTietGio_ID(int IDHoaDon, int IDBan, int ID)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_ChiTietGio] SET [ThanhToan] = 1, [IDHoaDon] = {0} WHERE [TrangThai] = 1  AND [IDBan] = {1} AND [ID] = {2}", IDHoaDon, IDBan, ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
      
        public static object ThemMoiHoaDon(int IDBan, int NhanVien, DateTime GioVao)
        {
            object ID = null;
            string sTruyVan = string.Format(@"INSERT INTO CF_HoaDon(IDBan,GioVao,IDNhanVien,GioRa,NgayBan) OUTPUT INSERTED.ID VALUES ('{0}','{1}',{2},getdate(),getdate())", IDBan, GioVao.ToString("yyyy-MM-dd hh:mm:ss tt"), NhanVien);
            SqlConnection conn = new SqlConnection();
            DAO_ConnectSQL connect = new DAO_ConnectSQL();
            conn = connect.Connect();
            SqlCommand cm = new SqlCommand(sTruyVan, conn);
            ID = cm.ExecuteScalar();
            return ID;
        }

        public static DateTime LayGioVao(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT GioVao FROM [CF_HoaDon] WHERE [ID] = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return DateTime.Parse(dr["GioVao"].ToString());
            }
            else
                return DateTime.Now;
        }
        public static bool CapNhatTongTienHoaDonChinh(int IDHoaDon, int IDBan, double TongTien)
        {
            string CompuMaHoaDon = @"SELECT 
                                          REPLICATE('0', 5 - LEN((count(ID) + 1))) + 
                                          CAST((count(ID) + 1) AS varchar) + '-' + 
                                          FORMAT(GETDATE() , 'ddMMyy')
                                          as 'Mã Hóa Đơn'  
                                          from CF_HoaDon 
                                          where MaHoaDon is not null  AND DATEDIFF(dd,NgayBan, GetDate()) = 0";
            string MaHoaDon = "";
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(CompuMaHoaDon);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                MaHoaDon = dr["Mã Hóa Đơn"].ToString();
            }

            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [MaHoaDon] = N'" + MaHoaDon + "',[TrangThai] = 1,  [TongTien] = {0},[KhachThanhToan] = '{1}',KhachCanTra = '{5}', [TienThua] = '{2}' WHERE [ID] = {3} AND [IDBan] = {4}", TongTien, TongTien, TongTien, IDHoaDon, IDBan, TongTien);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatTienGioHoaDonChinh(int IDHoaDon, int IDBan, double TienGio)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [TrangThai] = 1,  [TienGio] = {0} WHERE [ID] = {1} AND [IDBan] = {2}", TienGio, IDHoaDon, IDBan);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool KiemTraHangHoa(int IDHoaDon, int IDHangHoa, int IDBan, float TrongLuong)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon] WHERE IDBan = {0} AND IDHangHoa = {1} AND [IDHoaDon] = {2} AND TrongLuong = '{3}'", IDBan, IDHangHoa, IDHoaDon, TrongLuong);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CapNhatChiTietHoaDon(int IDHoaDon, int SL, double ThanhTien, int IDHangHoa, int IDBan)
        {
            string sTruyVan = string.Format(@"UPDATE CF_ChiTietHoaDon SET [SoLuong] =  SoLuong + {0}, [ThanhTien] = [ThanhTien] + {1} WHERE [IDHoaDon] = {2} AND [IDHangHoa] = {3} AND [IDBan] = {4}", SL, ThanhTien, IDHoaDon, IDHangHoa, IDBan);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
