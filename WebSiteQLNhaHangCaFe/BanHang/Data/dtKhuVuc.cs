using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtKhuVuc
    {
        public void Sua(string ID, string GhiChu, string TenKhuVuc, string TyLe, string IDChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_KhuVuc] SET [IDChiNhanh] = @IDChiNhanh,[TyLe] = @TyLe,[GhiChu] = @GhiChu,[TenKhuVuc] = @TenKhuVuc, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TenKhuVuc", TenKhuVuc);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.Parameters.AddWithValue("@TyLe", TyLe);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void Xoa(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_KhuVuc] SET [DAXOA] = 1 WHERE [ID] = @ID";
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
        public void Them(string MaKhuVuc, string TenKhuVuc, string TyLe, string IDChiNhanh, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [CF_KhuVuc] ([MaKhuVuc],[TenKhuVuc],[TyLe],[IDChiNhanh], [NgayCapNhat],[GhiChu]) VALUES (@MaKhuVuc,@TenKhuVuc,@TyLe,@IDChiNhanh, getdate(),@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.Parameters.AddWithValue("@TyLe", TyLe);
                        myCommand.Parameters.AddWithValue("@MaKhuVuc", MaKhuVuc);
                        myCommand.Parameters.AddWithValue("@TenKhuVuc", TenKhuVuc);
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

        public DataTable DanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [CF_KhuVuc] WHERE DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static string Dem_Max(string IDChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string SoVe;
                string GPM = "000";
                string cmdText = "SELECT * FROM [CF_KhuVuc] WHERE [IDChiNhanh] = '" + IDChiNhanh + "'";
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
    }
}