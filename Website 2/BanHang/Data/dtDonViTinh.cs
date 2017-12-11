using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BanHang.Data
{
    public class dtDonViTinh
    {
        public static string Dem_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string SoVe;
                string GPM = "000";
                string cmdText = "SELECT * FROM [CF_DonViTinh]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 4 - DoDaiHT);
                    SoVe = DoDaiGPM + STTV;
                    return SoVe;
                }
            }
        }
        public DataTable LayDanhSachDonViTinh()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [CF_DonViTinh] WHERE DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public static int KiemTra(string TenDonViTinh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [CF_DonViTinh] WHERE [TenDonViTinh] = N'" + TenDonViTinh + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count == 0)
                    {

                        return 1;
                    }
                    else return -1;
                }
            }
        }


        public void ThemDonViTinh(string TenDonViTinh, string MaDVT)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [CF_DonViTinh] ([TenDonViTinh],[MaDVT], [NgayCapNhat]) VALUES (@TenDonViTinh,@MaDVT, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenDonViTinh", TenDonViTinh);
                        myCommand.Parameters.AddWithValue("@MaDVT", MaDVT);
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

        public void XoaDonViTinh(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_DonViTinh] SET [DAXOA] = 1 WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void SuaThongTinDonViTinh(int ID, string TenDonViTinh, string MaDVT)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_DonViTinh] SET [TenDonViTinh] = @TenDonViTinh,[MaDVT] = @MaDVT, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TenDonViTinh", TenDonViTinh);
                        myCommand.Parameters.AddWithValue("@MaDVT", MaDVT);

                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
    }
}