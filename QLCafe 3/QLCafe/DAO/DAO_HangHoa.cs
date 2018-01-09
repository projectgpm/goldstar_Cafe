using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_HangHoa
    {
        public static DataTable DanhSachHangHoa(string IDNhomHang)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HangHoa] WHERE [IDNhomHang] = {0}", IDNhomHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }

        public static DataTable DanhSachHangHoa_Full()
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HangHoa] WHERE DaXoa = 0 ORDER BY TenHangHoa ASC");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static DataTable DanhSachHangHoaTimKiem(string TenHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HangHoa] WHERE TenHangHoa LIKE N'%" + TenHangHoa + "%' AND DaXoa = 0 ORDER BY TenHangHoa ASC");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static DataTable DanhSachTuChon()
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_NguyenLieu] WHERE DaXoa = 0 AND TrangThai = 1");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static DataTable DanhSachHangHoa_ID(int IDHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT [CF_HangHoa].*,[CF_DonViTinh].TenDonViTinh FROM [CF_HangHoa],[CF_DonViTinh] WHERE [CF_DonViTinh].ID = [CF_HangHoa].IDDonViTinh   AND [CF_HangHoa].ID =  {0}", IDHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        public static DataTable DanhSachTuChon_ID(int IDnguyenLieu)
        {
            string sTruyVan = string.Format(@"SELECT [CF_NguyenLieu].*,[CF_DonViTinh].TenDonViTinh FROM [CF_NguyenLieu],[CF_DonViTinh] WHERE [CF_DonViTinh].ID = [CF_NguyenLieu].IDDonViTinh   AND [CF_NguyenLieu].ID =  {0}", IDnguyenLieu);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
        private static DAO_HangHoa instance;

        internal static DAO_HangHoa Instance
        {
            get { if (instance == null) instance = new DAO_HangHoa(); return DAO_HangHoa.instance; }
             private set { DAO_HangHoa.instance = value; }
        }
        public DAO_HangHoa() { }
        public List<DTO_HangHoa> DanhSachHangHoaID(int IDNhomHang)
        {
            List<DTO_HangHoa> tablelist = new List<DTO_HangHoa>();
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HangHoa] WHERE [IDNhomHang] = {0}", IDNhomHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            foreach (DataRow item in data.Rows)
            {
                DTO_HangHoa table = new DTO_HangHoa(item);
                tablelist.Add(table);
            }
            return tablelist;
        }
    }
}
