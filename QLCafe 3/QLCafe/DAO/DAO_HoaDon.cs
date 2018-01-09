using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_HoaDon
    {
        private static DAO_HoaDon instance;

        public static DAO_HoaDon Instance
        {
            get { if (instance == null) instance = new DAO_HoaDon(); return DAO_HoaDon.instance; }
           private set { DAO_HoaDon.instance = value; }
        }
        private DAO_HoaDon() { }

        /// <summary>
        /// Thành công HoaDonID
        /// Thất bại -1;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetHoaDonByIDBan(int idban)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HoaDon] WHERE IDBan = {0} AND [TrangThai] = 0", idban);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DTO_HoaDon hd = new DTO_HoaDon(data.Rows[0]);
                return hd.ID;
            }

            return -1;
        }

        public static float TongTienHoaDon(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT TongTien FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["TongTien"].ToString());
            }
            return 0;
        }
        public static float LayDiemQuyDoiHoaDon(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT DiemQuiDoi FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["DiemQuiDoi"].ToString());
            }
            return 0;
        }
        public static float LayGiamGiaDiem(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT GiamGiaDiem FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["GiamGiaDiem"].ToString());
            }
            return 0;
        }
        public static float LayGiamGiaHoaDon(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT GiamGiaHoaDon FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["GiamGiaHoaDon"].ToString());
            }
            return 0;
        }
        public static float LayTongGiamGia(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT TongGiamGia FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["TongGiamGia"].ToString());
            }
            return 0;
        }
        public static float LayTyLeGiamGia(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT TyLeGiamGia FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["TyLeGiamGia"].ToString());
            }
            return 0;
        }
        public static string LayHinhThucGiamGia(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT HinhThucGiamGia FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["HinhThucGiamGia"].ToString();
            }
            return "$";
        }

        public static int LaySoInTamTinh(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT LanIn FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LanIn"].ToString());
            }
            return 0;
        }
        public static int LaySoInTamPhaChe(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT LanIn2 FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LanIn2"].ToString());
            }
            return 0;
        }
        public static int LayIDKhachHang(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT IDKhachHang FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["IDKhachHang"].ToString());
            }
            return 1;
        }
        public static float TongTienGio(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT TienGio FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["TienGio"].ToString());
            }
            return 0;
        }
        public static float KhachCanTra(int IDHoaDon)
        {
            string sTruyVan = string.Format(@"SELECT KhachCanTra FROM [CF_HoaDon] WHERE ID = {0} ", IDHoaDon);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["KhachCanTra"].ToString());
            }
            return 0;
        }

        public static void XoaDatBan(int IDBan)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_HoaDon] WHERE IDBan = {0} AND [TrangThai] = 0", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DTO_HoaDon hd = new DTO_HoaDon(data.Rows[0]);

                sTruyVan = string.Format(@"DELETE FROM [CF_ChiTietHoaDon_Temp] WHERE IDHoaDon = {0}", hd.ID);
                DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
                
                sTruyVan = string.Format(@"DELETE FROM [CF_HoaDon] WHERE ID = {0}   AND [TrangThai] = 0", hd.ID);
                DataProvider.TruyVanKhongLayDuLieu(sTruyVan);

                sTruyVan = string.Format(@"DELETE FROM [CF_ChiTietGio] WHERE IDHoaDon = {0} AND [ThanhToan] = 0", hd.ID);
                DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
               
            }
        }
        public static bool CapNhatTongTien(int ID, string TongTien, string KhachCanTra, string TienGio)
        {
            string sTruyVan = string.Format(@"UPDATE CF_HoaDon SET [TongTien] = {0}, [KhachCanTra] =  {1}, [TienGio] = {3} WHERE [ID] = {2} ", TongTien, KhachCanTra, ID, TienGio);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
    }
}
