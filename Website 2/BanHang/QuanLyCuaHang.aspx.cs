using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QuanLyCuaHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable da = new DataTable();
            da.Columns.Add("SoBanSuDung", typeof(String));
            da.Columns.Add("TongVon", typeof(float));
            da.Columns.Add("DoanhThu", typeof(float));
            da.Columns.Add("GiamGia", typeof(float));
            da.Columns.Add("TienHienTai", typeof(float));
            da.Columns.Add("LoiNhuan", typeof(float));

            DateTime date = DateTime.Now;
            string ngayBD = ""; string ngayKT = "";
            ngayBD = date.ToString("yyyy-MM-dd ");
            ngayKT = date.ToString("yyyy-MM-dd ");
            ngayBD = ngayBD + "00:00:0.000";
            ngayKT = ngayKT + "23:59:59.999";

            int SoLuongBan = dtQuanLyCuaHang.SoLuongBan();
            DataTable data = dtQuanLyCuaHang.TongTienHienTai(ngayBD, ngayKT);
            float TongTien = 0;
            float KhachCanTra = 0;
            float TongGiaMua = 0;
            try
            {
                if (data.Rows.Count != 0)
                {
                    TongTien = float.Parse(data.Rows[0]["TongTien"].ToString());
                    KhachCanTra = float.Parse(data.Rows[0]["KhachCanTra"].ToString());
                    TongGiaMua = dtQuanLyCuaHang.TongTienVonHienTai(ngayBD, ngayKT);
                }
            }
            catch (Exception) { }


            da.Rows.Add(SoLuongBan, TongGiaMua, TongTien, TongTien - KhachCanTra, KhachCanTra, KhachCanTra - TongGiaMua);

            gridDanhSachBan.DataSource = da;
            gridDanhSachBan.DataBind();
        }
    }
}