using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_KhachHang
    {
        private static DAO_KhachHang instance;

        public static DAO_KhachHang Instance
        {
            get {if(instance == null) instance = new DAO_KhachHang(); return DAO_KhachHang.instance; }
           private set { DAO_KhachHang.instance = value; }
        }

        public List<DTO_KhachHang> listKhachHang()
        {
            List<DTO_KhachHang> list = new List<DTO_KhachHang>();
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_KhachHang]");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_KhachHang table = new DTO_KhachHang(item);
                list.Add(table);
            }
            return list;
        }
        public static DataTable KhachHangID(string IDKhachHang)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_KhachHang] WHERE [ID] = {0}", IDKhachHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return data;
            }
            else
                return null;
        }

        public static int KiemTraMaKhachHang(string MaKhachHang)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_KHACHHANG] WHERE MaKhachHang = '{0}'", MaKhachHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if(data.Rows.Count == 0)
                return 0;
            return 1;
        }

        public static int KiemTraSDTKhachHang(string SDT)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [GPM_KHACHHANG] WHERE DienThoai =  '{0}'", SDT);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count == 0)
                return 0;
            return 1;
        }
        public static bool ThemKhachHang(int IDNhomKhachHang, string MaKhachHang, string TenKhachHang, DateTime NgaySinh, string CMND, string DiaChi, string DienThoai, string GhiChu)
        {
            string sTruyVan = string.Format(@"INSERT INTO GPM_KHACHHANG([IDNhomKhachHang],[MaKhachHang], [TenKhachHang], [NgaySinh], [CMND], [DiaChi], [DienThoai], [GhiChu]) VALUES ('{0}',N'{1}',N'{2}', N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')", IDNhomKhachHang, MaKhachHang, TenKhachHang, NgaySinh.ToString("yyyy-MM-dd hh:mm:ss"), CMND, DiaChi, DienThoai, GhiChu);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
