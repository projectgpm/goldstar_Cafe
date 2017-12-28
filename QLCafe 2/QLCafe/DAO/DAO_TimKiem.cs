using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_TimKiem
    {
        public static DataTable DanhSachHoaDonCaFeTimThay(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HoaDon] WHERE MaHoaDon = N'{0}' AND IDKhachHang = 1  ", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static bool TimHoaDonCaFe(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT KhachCanTra FROM [CF_HoaDon] WHERE [MaHoaDon] = N'{0}' AND IDKhachHang = 1", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //========================================================================
        public static bool TimHoaDonBanVe(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT KhachCanTra FROM [GPM_BanVe] WHERE [MaHoaDon] = N'{0}' AND IDKhachHang = 1", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DataTable DanhSachHoaDonBanVeTimThay(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_BanVe] WHERE MaHoaDon = N'{0}' AND IDKhachHang = 1  ", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        //===================================================================
        public static bool TimHoaDonBanHangLe(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT KhachCanTra FROM [GPM_HoaDon] WHERE [MaHoaDon] = N'{0}' AND IDKhachHang = 1", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DataTable DanhSachHoaDonBanHangLeTimThay(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_HoaDon] WHERE MaHoaDon = N'{0}' AND IDKhachHang = 1  ", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }

        //========================================================================
        public static bool CapNhatIDKhachHangCaFe(string IDKhachHang, string MaHoaDon)
        {
            string sTruyVan = string.Format(@"UPDATE CF_HoaDon SET [IDKhachHang] = '{0}' WHERE [MaHoaDon] = N'{1}' ", IDKhachHang, MaHoaDon);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool LichSuCapNhatDiem(string IDKhachHang, string IDNhanVien, string NoiDung, string DiemMoi)
        {
            string DiemCu = DAO_Setting.LayDiemHienTai(IDKhachHang).ToString();
            string sTruyVan = string.Format(@"INSERT INTO [CF_NhatKyCapNhatDiemQuiDoi]([IDKhachHang],[SoDiemCu],[SoDiemMoi],[NoiDung],[NgayLap],[IDNhanVien]) " +
                                                                                        "VALUES('{0}','{1}','{2}',N'{3}',getdate(),'{4}')", IDKhachHang, DiemCu, (double.Parse(DiemCu) + double.Parse(DiemMoi)).ToString(), NoiDung, IDNhanVien);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        //======================================================================
        public static bool CapNhatIDKhachHangBanHangLe(string IDKhachHang, string MaHoaDon)
        {
            string sTruyVan = string.Format(@"UPDATE GPM_HoaDon SET [IDKhachHang] = '{0}' WHERE [MaHoaDon] = N'{1}' ", IDKhachHang, MaHoaDon);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        //=======================================================================
        public static bool CapNhatIDKhachHangBanVe(string IDKhachHang, string MaHoaDon)
        {
            string sTruyVan = string.Format(@"UPDATE [dbo].[GPM_BanVe] SET [IDKhachHang] = '{0}' WHERE [MaHoaDon] = N'{1}' ", IDKhachHang, MaHoaDon);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
