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
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HoaDon] WHERE MaHoaDon = {0} AND IDNhanVien = 1  ", MaHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static bool TimHoaDonCaFe(string MaHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT KhachCanTra FROM [CF_HoaDon] WHERE [MaHoaDon] = '{0}' AND IDNhanVien = 1", MaHoaDon);
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
    }
}
