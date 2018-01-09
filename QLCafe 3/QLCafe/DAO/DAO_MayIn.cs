using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_MayIn
    {
        private static DAO_MayIn instance;

        public static DAO_MayIn Instance
        {
            get { if (instance == null) instance = new DAO_MayIn(); return DAO_MayIn.instance; }
            private set { DAO_MayIn.instance = value; }
        }
        public DataTable DanhSach(string IDChiNhanh)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_MayIn] WHERE IDChiNhanh = {0}", IDChiNhanh);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static int KiemTraDanhSachMonCanIn(string IDHoaDon, string IDMayIn)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon_Temp] WHERE IDHoaDon = {0} AND [IDMayIn] = {1} AND [InPhaChe] != [SoLuong]", IDHoaDon, IDMayIn);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //public static float LayGiaTheoKhuVuc(int IDKhuVuc)
        //{
        //    string sTruyVan = string.Format(@"SELECT GiaKhuVuc FROM [CF_KhuVuc] WHERE [ID] = {0} ", IDKhuVuc);
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return float.Parse(dr["GiaKhuVuc"].ToString());
        //    }
        //    return 0;
        //}
        public List<DTO_MayIn> DanhSachCaBan(string IDChiNhanh)
        {
            List<DTO_MayIn> tablelist = new List<DTO_MayIn>();
            string sTruyVan = string.Format(@"SELECT * FROM [CF_MayIn] WHERE IDChiNhanh = {0}", IDChiNhanh);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_MayIn table = new DTO_MayIn(item);
                tablelist.Add(table);
            }
            return tablelist;
        }
    }
}
