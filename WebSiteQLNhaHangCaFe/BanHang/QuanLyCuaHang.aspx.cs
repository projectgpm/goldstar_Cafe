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
            da.Columns.Add("SoLuongKhach", typeof(String));
            da.Columns.Add("SoBanSuDung", typeof(String));
            da.Columns.Add("TongTienGio", typeof(float));
            da.Columns.Add("TongTienHang", typeof(float));
            da.Columns.Add("TongTienHienTai", typeof(float));

            DateTime date = DateTime.Now;
            string ngayBD = ""; string ngayKT = "";
            ngayBD = date.ToString("yyyy-MM-dd ");
            ngayKT = date.ToString("yyyy-MM-dd ");
            ngayBD = ngayBD + "00:00:0.000";
            ngayKT = ngayKT + "23:59:59.999";
            int SoLuongKhach = dtQuanLyCuaHang.SoKhachHienTai();
            int SoLuongBan = dtQuanLyCuaHang.SoLuongBan();
            DataTable daTienGio = dtQuanLyCuaHang.TongTienHienTai(ngayBD, ngayKT);
            float TongTien = 0;
            float TongTienGio = 0;
            try{
                if(daTienGio.Rows.Count != 0){
                    TongTien = float.Parse(daTienGio.Rows[0]["TongTien"].ToString());
                    TongTienGio = float.Parse(daTienGio.Rows[0]["TienGio"].ToString());
                }
            }catch(Exception){}


            da.Rows.Add(SoLuongKhach, SoLuongBan,TongTienGio, TongTien, TongTien + TongTienGio);

            gridDanhSachBan.DataSource = da;
            gridDanhSachBan.DataBind();
        }
    }
}