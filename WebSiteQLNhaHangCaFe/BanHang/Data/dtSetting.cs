using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Web;
using System.Management;
using System.IO;
using System.Collections; 

namespace BanHang.Data
{
    public class dtSetting
    {
        public static int getKeyCode()
        {
            //PhysicalAddress address = GetMacAddress();
            string sx = GetHardDiskSerialNo();

            string strAddress = sx + "GPM";
            string sha1Address = GetSHA1HashData(strAddress);

            DataTable da = getData_Setting();
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                string macAddress = dr["KeyKichHoatCaFe"].ToString();
                if (macAddress.CompareTo(sha1Address) == 0)
                    return 1;
            }
            return -1;
        }
        public static string GetHardDiskSerialNo()
        {
            string drive = "C";
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
            disk.Get();
            return disk["VolumeSerialNumber"].ToString();
        }
        public static int setKeyCode(string Key, string user)
        {
            //PhysicalAddress address = GetMacAddress();
            string sx = GetHardDiskSerialNo();

            string strAddress = sx + "GPM";

            if (Key.CompareTo("gpm6868") == 0)
            {
                string sha1Address = GetSHA1HashData(strAddress);
                setData_Setting(sha1Address, user);
                return 1;
            }
            return -1;

        }
        public static void setData_Setting(string KeyKichHoatCaFe, string NguoiKichHoatCaFe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [Setting] SET [KeyKichHoatCaFe] = @KeyKichHoatCaFe, [NguoiKichHoatCaFe] = @NguoiKichHoatCaFe";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@KeyKichHoatCaFe", KeyKichHoatCaFe);
                        myCommand.Parameters.AddWithValue("@NguoiKichHoatCaFe", NguoiKichHoatCaFe);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public static DataTable getData_Setting()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    con.Open();
                    string cmdText = "SELECT KeyKichHoatCaFe FROM [Setting]";
                    using (SqlCommand command = new SqlCommand(cmdText, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        //public static string GetSHA1HashData(string data)
        //{
        //    //create new instance of md5
        //    SHA1 sha1 = SHA1.Create();

        //    byte[] hashData = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + 123));

        //    System.Text.StringBuilder returnValue = new System.Text.StringBuilder();

        //    for (int i = 0; i < hashData.Length; i++)
        //    {
        //        returnValue.Append(hashData[i].ToString("x"));
        //    }

        //    return returnValue.ToString();
        //}
       
        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static void CapNhatKho(string IDNguyenLieu, string TrongLuong, string IDChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [CF_TonKho] SET [TrongLuong] = @TrongLuong WHERE [IDNguyenLieu] = @IDNguyenLieu AND [IDChiNhanh] = @IDChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNguyenLieu", IDNguyenLieu);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public static float SoLuong_TonKho(string IDNguyenLieu,string IDChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT TrongLuong FROM [CF_TonKho] WHERE [IDNguyenLieu] = '" + IDNguyenLieu + "' AND [IDChiNhanh] = '" + IDChiNhanh + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["TrongLuong"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static void TruTonKho(string IDNguyenLieu, string TrongLuong, string IDChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [CF_TonKho] SET [TrongLuong] = [TrongLuong] - @TrongLuong WHERE [IDNguyenLieu] = @IDNguyenLieu AND [IDChiNhanh] = @IDChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNguyenLieu", IDNguyenLieu);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public static void CongTonKho(string IDNguyenLieu, string TrongLuong, string IDChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [CF_TonKho] SET [TrongLuong] = [TrongLuong] + @TrongLuong WHERE [IDNguyenLieu] = @IDNguyenLieu AND [IDChiNhanh] = @IDChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNguyenLieu", IDNguyenLieu);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
      
        public static string LayIDNguyenLieu(string MaNguyenLieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [CF_NguyenLieu] WHERE [MaNguyenLieu] = N'" + MaNguyenLieu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        string ID = dr["ID"].ToString().Trim();
                        return ID;
                    }
                    return null;
                }
            }
        }
     
        public static float GiaMua(string IDNguyenLieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT GiaMua FROM [CF_NguyenLieu] WHERE [ID] = '" + IDNguyenLieu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaMua"].ToString());
                    }
                    else return -1;
                }
            }
        }
      
        public static string convertDauSangKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
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

        public static int tinhSoNgay(int thang, int nam)
        {
            if (thang == 1 || thang == 3 || thang == 5 || thang == 7 || thang == 8 || thang == 10 || thang == 12)
                return 31;
            if (thang == 4 || thang == 6 || thang == 9 || thang == 11)
                return 30;

            if (nam % 4 == 0 && nam % 100 != 0 || nam % 400 == 0)
                return 29;
            else return 28;
        }
    }
}