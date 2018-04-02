﻿using System;
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
        /// <summary>
        /// lấy giá bán tự chọn
        /// </summary>
        /// <param name="IDHangHoa"></param>
        /// <param name="IDBangGia"></param>
        /// <returns></returns>
        public static double LayGiaBanTuChon(string IDNguyenLieu)
        {
            string sTruyVan = string.Format(@"SELECT GiaBan FROM [CF_NguyenLieu] WHERE ID = {0}", IDNguyenLieu);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["GiaBan"].ToString());
            }
            return -1;
        }
        /// <summary>
        /// lấy giá bán theo bảng giá
        /// </summary>
        /// <param name="IDBan"></param>
        /// <returns></returns>
        public static double LayGiaBan(int IDHangHoa, int IDBangGia)
        {
            string sTruyVan = string.Format(@"SELECT GiaMoi FROM [CF_ChiTietBangGia] WHERE IDBangGia = {0} AND IDHangHoa = {1}", IDBangGia, IDHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["GiaMoi"].ToString());
            }
            return -1;
        }
        /// <summary>
        /// lấy tên bàn để hiển thị
        /// </summary>
        /// <param name="IDBan"></param>
        /// <returns></returns>
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
        /// lấy IDKhuVuc để tính giá áp dụng
        /// </summary>
        /// <param name="IDBan"></param>
        /// <returns></returns>
        public static int LayIDKhuVuc(int IDBan)
        {
            string sTruyVan = string.Format(@"SELECT IDKhuVuc FROM [CF_Ban] WHERE ID = {0} ", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["IDKhuVuc"].ToString());
            }
            return 0;
        }
        /// <summary>
        /// lấy IDbanGia
        /// </summary>
        /// <param name="IDKhuVuc"></param>
        /// <returns></returns>
        public static int LayIDBanGia(int IDBan)
        {
            
            string sTruyVan = string.Format(@"SELECT IDKhuVuc FROM [CF_Ban] WHERE ID = {0} ", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                sTruyVan = string.Format(@"SELECT IDBangGia FROM [CF_KhuVuc] WHERE ID = {0} ", dr["IDKhuVuc"].ToString());
                data = DataProvider.TruyVanLayDuLieu(sTruyVan);
                if (data.Rows.Count > 0)
                {
                    DataRow dr1 = data.Rows[0];
                    return Int32.Parse(dr1["IDBangGia"].ToString());
                }
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// lấy id máy in món ăn thuong
        /// </summary>
        /// <param name="IDBan"></param>
        /// <returns></returns>
        public static int LayIDMayInHangHoa(int IDHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT IDNhomHang FROM [CF_HangHoa] WHERE ID = {0} ", IDHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                sTruyVan = string.Format(@"SELECT IDMayIn FROM [CF_NhomHangHoa] WHERE ID = {0} ", dr["IDNhomHang"].ToString());
                data = DataProvider.TruyVanLayDuLieu(sTruyVan);
                if (data.Rows.Count > 0)
                {
                    DataRow dr1 = data.Rows[0];
                    return Int32.Parse(dr1["IDMayIn"].ToString());
                }
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDHangHoa"></param>
        /// <returns></returns>
        public static int LayIDMayInNguyenLieu(int IDNguyenLieu)
        {
            string sTruyVan = string.Format(@"SELECT IDNhomHang FROM [CF_NguyenLieu] WHERE ID = {0} ", IDNguyenLieu);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                sTruyVan = string.Format(@"SELECT IDMayIn FROM [CF_NhomHangHoa] WHERE ID = {0} ", dr["IDNhomHang"].ToString());
                data = DataProvider.TruyVanLayDuLieu(sTruyVan);
                if (data.Rows.Count > 0)
                {
                    DataRow dr1 = data.Rows[0];
                    return Int32.Parse(dr1["IDMayIn"].ToString());
                }
                return 0;
            }
            return 0;
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
            string sTruyVan = string.Format(@"INSERT INTO CF_HoaDon(IDBan,GioVao,IDNhanVien,NgayBan) OUTPUT INSERTED.ID VALUES ('{0}',getdate(),{1},getdate())", IDBan, NhanVien);
            SqlConnection conn = new SqlConnection();
            DAO_ConnectSQL connect = new DAO_ConnectSQL();
            conn = connect.Connect();
            SqlCommand cm = new SqlCommand(sTruyVan, conn);
            ID = cm.ExecuteScalar();
            return ID;
        }


        public static bool ThemChiTietHoaDon(object IDHoaDon, int IDHangHoa, int SL, double DonGia, double ThanhTien, int IDBan, string MaHangHoa, int IDDonViTinh, double TrongLuong, int IDMayIn, int InPhaChe)
        {
            string sTruyVan = string.Format(@"INSERT INTO CF_ChiTietHoaDon_Temp(IDHoaDon,IDHangHoa,SoLuong,DonGia,ThanhTien,IDBan,MaHangHoa,IDDonViTinh,TrongLuong,IDMayIn,InPhaChe) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", IDHoaDon, IDHangHoa, SL, DonGia, ThanhTien, IDBan, MaHangHoa, IDDonViTinh, TrongLuong, IDMayIn, InPhaChe);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatChiTietHoaDon(int IDHoaDon, int SL, double ThanhTien, int IDHangHoa, int IDBan, int InPhaChe)
        {
            string sTruyVan = string.Format(@"UPDATE CF_ChiTietHoaDon_Temp SET [InPhaChe] = InPhaChe + {5},[SoLuong] =  SoLuong + {0}, [ThanhTien] = [ThanhTien] + {1} WHERE [IDHoaDon] = {2} AND [IDHangHoa] = {3} AND [IDBan] = {4}", SL, ThanhTien, IDHoaDon, IDHangHoa, IDBan, InPhaChe);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static DataTable DanhSachMonAnBanChay()
        {
            string sTruyVan = string.Format(@"  SELECT TOP 16 SUM([CF_ChiTietHoaDon].SoLuong) as SL, [CF_ChiTietHoaDon].IDHangHoa,[CF_HangHoa].TenHangHoa  FROM [CF_ChiTietHoaDon],[CF_HangHoa] WHERE [CF_ChiTietHoaDon].TrongLuong = 0 AND [CF_HangHoa].ID = [CF_ChiTietHoaDon].IDHangHoa   GROUP BY [CF_ChiTietHoaDon].IDHangHoa,[CF_HangHoa].TenHangHoa ORDER BY SL DESC");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            return data;
        }
    }
}
