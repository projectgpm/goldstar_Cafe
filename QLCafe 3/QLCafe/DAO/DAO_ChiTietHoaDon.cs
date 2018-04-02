using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_ChiTietHoaDon
    {
        private static DAO_ChiTietHoaDon instance;

        public static DAO_ChiTietHoaDon Instance
        {
            get { if (instance == null) instance = new DAO_ChiTietHoaDon(); return DAO_ChiTietHoaDon.instance; }
            private set { DAO_ChiTietHoaDon.instance = value; }
        }
        private DAO_ChiTietHoaDon() { }
        public List<DTO_ChiTietHoaDon> ChiTietHoaDon(int id)
        {
            List<DTO_ChiTietHoaDon> list = new List<DTO_ChiTietHoaDon>();
            string sTruyVan = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon_Temp] WHERE IDHoaDon = {0} ", id);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_ChiTietHoaDon table = new DTO_ChiTietHoaDon(item);
                list.Add(table);
            }
            return list;
        }

        public static bool KiemTraHangHoa(int IDHoaDon, int IDHangHoa, int IDBan, double TrongLuong)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon_Temp] WHERE IDBan = {0} AND  IDHangHoa = {1} AND [IDHoaDon] = {2} AND TrongLuong = {3}", IDBan, IDHangHoa, IDHoaDon, TrongLuong);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public static bool CapNhatSoLuong(string ThanhTien, string SL, string ID, string GhiChu)
        {
            string sTruyVan = string.Format(@"UPDATE CF_ChiTietHoaDon_Temp SET [ThanhTien] = {0}, [SoLuong] =  {1}, [GhiChu] = N'{3}' WHERE  [ID] = '{2}' ", ThanhTien, SL, ID, GhiChu);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
