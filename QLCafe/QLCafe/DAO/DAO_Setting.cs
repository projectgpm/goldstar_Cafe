using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_Setting
    {
        public static float LayDiemQuyDoiTien()
        {
            string sTruyVan = string.Format(@"SELECT SoTienQuyDoi FROM [GPM_Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["SoTienQuyDoi"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static float DiemTichLuy(string IDKhachHang)
        {
            string sTruyVan = string.Format(@"SELECT DiemTichLuy FROM [GPM_KhachHang] WHERE [ID] = {0}", IDKhachHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["DiemTichLuy"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static string GetSHA1HashData(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + 123));
            System.Text.StringBuilder returnValue = new System.Text.StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x"));
            }
            return returnValue.ToString();
        }
        public static string convertDauSangKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
        }

        public static int LayIDHangHoa(string MaHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT ID FROM [CF_HangHoa] WHERE [MaHangHoa] = {0} ", MaHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["ID"].ToString());
            }
            else
                return 0;
        }
        public static float LayGiaBanTheoKhuVuc(int IDBan)
        {
            string sTruyVan = string.Format(@"SELECT [CF_KhuVuc].GiaKhuVuc FROM [CF_Ban],[CF_KhuVuc] WHERE [CF_KhuVuc].ID =  [CF_Ban].IDKhuVuc AND [CF_Ban].ID = {0} ", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["GiaKhuVuc"].ToString());
            }
            else
                return 0;
        }
        public static bool TruTonKho(int IDNguyenLieu, string IDChiNhanh, double TrongLuong)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_TonKho] SET  [TrongLuong] = [TrongLuong] - {0} WHERE IDNguyenLieu = {1} AND [IDChiNhanh] = {2}", TrongLuong, IDNguyenLieu, IDChiNhanh);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static int LayIDDonViTinh(string MaHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT IDDonViTinh FROM [CF_HangHoa] WHERE [MaHangHoa] = {0} ", MaHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["IDDonViTinh"].ToString());
            }
            else
                return 0;
        }

        public static string TenCongTy()
        {
            string sTruyVan = string.Format(@"SELECT CongTy FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["CongTy"].ToString();
            }
            else
                return "";
        }
        public static string DiaChiCongTy()
        {
            string sTruyVan = string.Format(@"SELECT DiaChi FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["DiaChi"].ToString();
            }
            else
                return "";
        }
        public static string TestDuKieu()
        {
            string sTruyVan = string.Format(@"SELECT TestDuLieu FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["TestDuLieu"].ToString();
            }
            else
                return "";
        }
        public static string LayTenMayInBill()
        {
            string sTruyVan = string.Format(@"SELECT MayIn FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["MayIn"].ToString();
            }
            else
                return "";
        }
        public static bool CapNhatMayInBill(string TenMayIn)
        {
            string sTruyVan = string.Format(@"UPDATE [Setting] SET [MayIn] = '{0}' WHERE ID = 1", TenMayIn);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static string DienThoaiCongTy()
        {
            string sTruyVan = string.Format(@"SELECT SDT FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["SDT"].ToString();
            }
            else
                return "";
        }
        public static int ReportBill()
        {
            string sTruyVan = string.Format(@"SELECT ReportBill FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["ReportBill"].ToString());
            }
            else
                return 58;
        }

        public static int KTHoaDon(string IDHoaDon)
        {
            int r1 = 0, r2 = 0;
            
            string sTruyVan1 = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon] WHERE IDHoaDon = '" + IDHoaDon + "'");
            string sTruyVan2 = string.Format(@"SELECT * FROM [CF_ChiTietGio] WHERE IDHoaDon = '" + IDHoaDon + "'");
            DataTable data1 = new DataTable();
            data1 = DataProvider.TruyVanLayDuLieu(sTruyVan1);
            if (data1.Rows.Count > 0)
            {
                r1 = 1;
            }

            DataTable data2 = new DataTable();
            data2 = DataProvider.TruyVanLayDuLieu(sTruyVan2);
            if (data2.Rows.Count > 0)
            {
                r2 = 1;
            }

            if (r1 == 1 && r2 == 1)
                return 0;
            else if (r1 == 1) return 1;
            else return 2;
        }

        public static bool ThemLichSuTruyCap(int IDNhanVien, int IDNhom, string IDChiNhanh, string Menu, string HanhDong)
        {
            string sTruyVan = string.Format(@"INSERT INTO [CF_LichSuTruyCap](IDChiNhanh,IDNhom,IDNguoiDung,Menu,HanhDong,ThoiGian) VALUES ('{0}','{1}','{2}',N'{3}',N'{4}',getdate())", IDChiNhanh, IDNhom, IDNhanVien, Menu, HanhDong);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        //public static float LayDiemQuyDoiTien()
        //{
        //    string sTruyVan = string.Format(@"SELECT SoTienQuyDoi FROM [GPM_Setting] ");
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return float.Parse(dr["SoTienQuyDoi"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public static float LayTienQuyDoiDiem()
        //{
        //    string sTruyVan = string.Format(@"SELECT SoTienTichLuy FROM [GPM_Setting] ");
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return float.Parse(dr["SoTienTichLuy"].ToString());
        //    }
        //    return 0;
        //}

        //public static float DiemTichLuy(string IDKhachHang)
        //{
        //    string sTruyVan = string.Format(@"SELECT DiemTichLuy FROM [GPM_KhachHang] WHERE [ID] = {0}", IDKhachHang);
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return float.Parse(dr["DiemTichLuy"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
    }
}
