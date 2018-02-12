using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_PhaChe
    {
        private static DAO_PhaChe instance;

        public static DAO_PhaChe Instance
        {
            get { if (instance == null) instance = new DAO_PhaChe(); return DAO_PhaChe.instance; }
            private set { DAO_PhaChe.instance = value; }
        }

        private DAO_PhaChe() { }

        public List<DTO_DanhSachChuaPhaChe> GetDanhSachMonAn()
        {
            List<DTO_DanhSachChuaPhaChe> listMenu = new List<DTO_DanhSachChuaPhaChe>();
            string sTruyVan = string.Format(@"SELECT [CF_HangHoa].MaHangHoa, [CF_HangHoa].TenHangHoa,[CF_DonViTinh].TenDonViTinh, [CF_ChiTietHoaDon_Temp].SoLuong,[CF_ChiTietHoaDon_Temp].ID,[CF_ChiTietHoaDon_Temp].TrangThai,[CF_Ban].TenBan, REPLACE(REPLACE(REPLACE([CF_ChiTietHoaDon_Temp].TrangThai,0,N'Chưa chế biến') + REPLACE([CF_ChiTietHoaDon_Temp].TrangThai,2,N'Đang chế biến'),N'Chưa chế biến0',N'Chưa chế biến'),N'2Đang chế biến',N'Đang chế biến') AS ThongTinTrangThai FROM [CF_Ban],[CF_ChiTietHoaDon_Temp],[CF_HangHoa],[CF_DonViTinh] WHERE [CF_Ban].ID = [CF_ChiTietHoaDon_Temp].[IDBan] AND [CF_HangHoa].IDDonViTinh = [CF_DonViTinh].ID AND [CF_ChiTietHoaDon_Temp].IDHangHoa = [CF_HangHoa].ID AND [CF_ChiTietHoaDon_Temp].TrangThai != 1");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_DanhSachChuaPhaChe table = new DTO_DanhSachChuaPhaChe(item);
                listMenu.Add(table);
            }
            return listMenu;
        }
        public static bool CapNhatThoiGian(int SoGiay)
        {
            string sTruyVan = string.Format(@"UPDATE [Setting] SET [LamMoiPhaChe] = '{0}'", SoGiay);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatTrangThai(string ID)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_ChiTietHoaDon_Temp] SET [TrangThai] = 1 WHERE ID = '{0}'", ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatTrangThaiDangCheBien(string ID)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_ChiTietHoaDon_Temp] SET [TrangThai] = 2 WHERE ID = '{0}'", ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool ThemLichSuCheBien(string MaHangHoa, string TenHangHoa, string TenDonViTinh, string TenBan, string SoLuong, string TenNhanVien)
        {
            string sTruyVan = string.Format(@"INSERT INTO [CF_LichSuCheBien](MaHangHoa,TenHangHoa,TenDonViTinh,TenBan,SoLuong,TenNhanVien,GioCheBien) VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',getdate())", MaHangHoa, TenHangHoa, TenDonViTinh, TenBan, SoLuong, TenNhanVien);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
