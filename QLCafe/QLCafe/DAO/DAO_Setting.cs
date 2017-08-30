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

        public static float LayTienQuyDoiDiem()
        {
            string sTruyVan = string.Format(@"SELECT SoTienTichLuy FROM [GPM_Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return float.Parse(dr["SoTienTichLuy"].ToString());
            }
            return 0;
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
    }
}
