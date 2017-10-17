using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_GoiMon
    {
        public static string TenBan(int IDBan)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_Ban] WHERE ID = {0} ", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["TenBan"].ToString();
            }
            return null;
        }
        /// <summary>
        /// Thêm hóa đơn return IDHoaDon
        /// </summary>
        /// <param name="IDBan"></param>
        /// <param name="GioVao"></param>
        /// <returns></returns>
        public static object ThemHoaDon(int IDBan, int NhanVien)
        {
            object ID = null;
            string sTruyVan = string.Format(@"INSERT INTO CF_HoaDon(IDBan,GioVao,IDNhanVien) OUTPUT INSERTED.ID VALUES ('{0}',getdate(),{1})", IDBan, NhanVien);
            SqlConnection conn = new SqlConnection();
            DAO_ConnectSQL connect = new DAO_ConnectSQL();
            conn = connect.Connect();
            SqlCommand cm = new SqlCommand(sTruyVan, conn);
            ID = cm.ExecuteScalar();
            return ID;
        }


        public static bool ThemChiTietHoaDon(object IDHoaDon, int IDHangHoa, int SL, float DonGia, float ThanhTien, int IDBan, string MaHangHoa, int IDDonViTinh)
        {
            string sTruyVan = string.Format(@"INSERT INTO CF_ChiTietHoaDon_Temp(IDHoaDon,IDHangHoa,SoLuong,DonGia,ThanhTien,IDBan,MaHangHoa,IDDonViTinh) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", IDHoaDon, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatChiTietHoaDon(int IDHoaDon, int SL, float ThanhTien, int IDHangHoa, int IDBan)
        {
            string sTruyVan = string.Format(@"UPDATE CF_ChiTietHoaDon_Temp SET [SoLuong] =  SoLuong + {0}, [ThanhTien] = [ThanhTien] + {1} WHERE [IDHoaDon] = {2} AND [IDHangHoa] = {3} AND [IDBan] = {4}", SL, ThanhTien, IDHoaDon, IDHangHoa, IDBan);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static DataTable DanhSachMonAnBanChay()
        {
            string sTruyVan = string.Format(@"  SELECT TOP 16 SUM([CF_ChiTietHoaDon].SoLuong) as SL, [CF_ChiTietHoaDon].IDHangHoa,[CF_HangHoa].TenHangHoa  FROM [CF_ChiTietHoaDon],[CF_HangHoa] WHERE [CF_HangHoa].ID =[CF_ChiTietHoaDon].IDHangHoa   GROUP BY [CF_ChiTietHoaDon].IDHangHoa,[CF_HangHoa].TenHangHoa ORDER BY SL DESC");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
    }
}
