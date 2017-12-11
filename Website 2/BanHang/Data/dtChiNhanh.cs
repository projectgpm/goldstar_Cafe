using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtChiNhanh
    {
        public void ThemNguyenLieuTonKho(string IDNguyenLieu, string IDChiNhanh, string MaNguyenLieu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [CF_TonKho] ([IDNguyenLieu],[IDChiNhanh],[MaNguyenLieu]) VALUES (@IDNguyenLieu,@IDChiNhanh,@MaNguyenLieu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNguyenLieu", IDNguyenLieu);
                        myCommand.Parameters.AddWithValue("@IDChiNhanh", IDChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaNguyenLieu", MaNguyenLieu);
                      
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
        public DataTable DanhSachNguyenLieu()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [CF_NguyenLieu]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void SuaChiNhanh(string ID, string MaChiNhanh, string TenChiNhanh, string DienThoai, string DiaChi, DateTime NgayMo)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_ChiNhanh] SET [MaChiNhanh] = @MaChiNhanh,[TenChiNhanh] = @TenChiNhanh,[DienThoai] = @DienThoai,[DiaChi] = @DiaChi, [NgayCapNhat] = getdate(), [NgayMo] = @NgayMo WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@MaChiNhanh", MaChiNhanh);
                        myCommand.Parameters.AddWithValue("@TenChiNhanh", TenChiNhanh);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@NgayMo", NgayMo);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void XoaChiNhanh(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [CF_ChiNhanh] SET [DaXoa] = 1 WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public object ThemChiNhanh(string MaChiNhanh, string TenChiNhanh, string DienThoai, string DiaChi, DateTime NgayMo)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [CF_ChiNhanh] ([MaChiNhanh],[TenChiNhanh], [DienThoai], [DiaChi], [NgayMo], [NgayCapNhat]) OUTPUT INSERTED.ID  VALUES (@MaChiNhanh,@TenChiNhanh, @DienThoai, @DiaChi, @NgayMo, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaChiNhanh", MaChiNhanh);
                        myCommand.Parameters.AddWithValue("@TenChiNhanh", TenChiNhanh);
                        myCommand.Parameters.AddWithValue("@DienThoai", DienThoai);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@NgayMo", NgayMo);
                        ID = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return ID;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable LayDanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT *  FROM [CF_ChiNhanh] WHERE DAXOA = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static string Dem_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string SoVe;
                string GPM = "000000";
                string cmdText = "SELECT * FROM [CF_ChiNhanh]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 7 - DoDaiHT);
                    SoVe = DoDaiGPM + STTV;
                    return SoVe;
                }
            }
        }
    }
}