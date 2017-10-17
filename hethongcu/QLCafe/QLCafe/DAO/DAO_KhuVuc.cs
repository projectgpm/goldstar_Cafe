using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_KhuVuc
    {
        public static DataTable DanhSachKhuVuc(string IDChiNhanh)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_KhuVuc] WHERE IDChiNhanh = {0} ", IDChiNhanh);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }

        public static int LayTyLe(int IDKhuVuc)
        {
            string sTruyVan = string.Format(@"SELECT TyLe FROM [CF_KhuVuc] WHERE [ID] = {0} ", IDKhuVuc);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["TyLe"].ToString());
            }
            return 0;
        }
    }
}
