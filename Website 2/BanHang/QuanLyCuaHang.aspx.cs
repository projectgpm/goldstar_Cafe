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
            da.Columns.Add("TongTien", typeof(float));
            da.Columns.Add("GiamGia", typeof(float));
            da.Columns.Add("PhuThu", typeof(float));
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
            float GiamGia = 0;
            float PhuThu = 0;
            float LoiNhuan = 0;
            try
            {
                if (data.Rows.Count != 0)
                {
                    TongTien = float.Parse(data.Rows[0]["TongTien"].ToString());
                    GiamGia = float.Parse(data.Rows[0]["TongGiamGia"].ToString());
                    PhuThu = float.Parse(data.Rows[0]["TienPhuThu"].ToString());
                    LoiNhuan = (TongTien + PhuThu) - GiamGia;
                }
            }
            catch (Exception) { }


            da.Rows.Add(SoLuongBan, TongTien, GiamGia, PhuThu, LoiNhuan);

            gridDanhSachBan.DataSource = da;
            gridDanhSachBan.DataBind();
        }
    }
}