using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_NhomKhachHang
    {
        private static DAO_NhomKhachHang instance;

        public static DAO_NhomKhachHang Instance
        {
            get { if (instance == null) instance = new DAO_NhomKhachHang(); return DAO_NhomKhachHang.instance; }
            private set { DAO_NhomKhachHang.instance = value; }
        }
        public List<DTO_NhomKhachHang> listNhomKhachHang()
        {
            List<DTO_NhomKhachHang> tablelist = new List<DTO_NhomKhachHang>();
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_NhomKhachHang]");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_NhomKhachHang table = new DTO_NhomKhachHang(item);
                tablelist.Add(table);
            }
            return tablelist;
        }
    }
}
