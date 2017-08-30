using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
   public  class DAO_BanHang
    {
       public static DateTime GioVao_IDBan(int IDBan)
       {
           string sTruyVan = string.Format(@"SELECT GioVao FROM [CF_HoaDon] WHERE IDBan = {0} AND [TrangThai] = 0", IDBan);
           DataTable data = new DataTable();
           data = DataProvider.TruyVanLayDuLieu(sTruyVan);
           if (data.Rows.Count > 0)
           {
               DataRow dr = data.Rows[0];
               return DateTime.Parse(dr["GioVao"].ToString());
           }
           return DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
       }

       public static int IDHoaDon(int IDBan)
       {
           string sTruyVan = string.Format(@"SELECT ID FROM [CF_HoaDon] WHERE IDBan = {0} AND [TrangThai] = 0", IDBan);
           DataTable data = new DataTable();
           data = DataProvider.TruyVanLayDuLieu(sTruyVan);
           if (data.Rows.Count > 0)
           {
               DataRow dr = data.Rows[0];
               return Int32.Parse(dr["ID"].ToString());
           }
           return 0;
       }

       public static bool XoaMonAn(int IDBan, string MaHangHoa, int IDHoaDon)
       {
           string sTruyVan = string.Format(@"DELETE FROM [CF_ChiTietHoaDon] WHERE IDBan = {0} AND [MaHangHoa] = '{1}' AND [IDHoaDon] = '{2}' ", IDBan, MaHangHoa, IDHoaDon);
           return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
       }
    }
}
