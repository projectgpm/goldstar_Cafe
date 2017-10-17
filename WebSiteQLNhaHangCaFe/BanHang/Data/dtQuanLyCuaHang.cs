using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtQuanLyCuaHang
    {
        public static int SoKhachHienTai()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(CF_ChiTietHoaDon_Temp.SoLuong) as Tong FROM CF_HoaDon, CF_ChiTietHoaDon_Temp WHERE CF_HoaDon.ID = CF_ChiTietHoaDon_Temp.IDHoaDon";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        try
                        {
                            return Int32.Parse(tb.Rows[0]["Tong"].ToString());
                        }catch(Exception){return 0;}
                    return 0;
                }
            }
        }

        public static int SoLuongBan()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT COUNT(ID) as Tong FROM CF_Ban WHERE TrangThai > 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        try
                        {
                            return Int32.Parse(tb.Rows[0]["Tong"].ToString());
                        }
                        catch (Exception) { return 0; }
                    return 0;
                }
            }
        }

        public static DataTable TongTienHienTai(string GioBD, string GioKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(TongTien) as TongTien, SUM(TienGio) as TienGio FROM CF_HoaDon WHERE GioVao >= '" + GioBD + "' AND GioVao <= '" + GioKT + "' AND GioRa is not null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}