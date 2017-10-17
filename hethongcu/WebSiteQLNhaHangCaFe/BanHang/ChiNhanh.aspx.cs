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
    public partial class ChiNhanh : System.Web.UI.Page
    {
        dtChiNhanh data = new dtChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtChiNhanh();
            gridChiNhanh.DataSource = data.LayDanhSach();
            gridChiNhanh.DataBind();
        }

        protected void gridChiNhanh_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaChiNhanh"] = dtChiNhanh.Dem_Max();
        }

        protected void gridChiNhanh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtChiNhanh();
            data.XoaChiNhanh(ID);
            e.Cancel = true;
            gridChiNhanh.CancelEdit();
            LoadGrid();
        }

        protected void gridChiNhanh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string MaChiNhanh = e.NewValues["MaChiNhanh"].ToString();
            string TenChiNhanh = e.NewValues["TenChiNhanh"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            DateTime NgayMo = DateTime.Parse(e.NewValues["NgayMo"].ToString());

            data = new dtChiNhanh();

            object ID =  data.ThemChiNhanh(MaChiNhanh, TenChiNhanh, DienThoai, DiaChi, NgayMo);
            if (ID != null)
            {
                data = new dtChiNhanh();
                DataTable dt = data.DanhSachNguyenLieu();
                foreach (DataRow dr in dt.Rows)
                {
                    string IDNguyenLieu = dr["ID"].ToString();
                    string MaNguyenLieu = dr["MaNguyenLieu"].ToString();
                    data = new dtChiNhanh();
                    data.ThemNguyenLieuTonKho(IDNguyenLieu, ID.ToString(), MaNguyenLieu);
                }
            }
            e.Cancel = true;
            gridChiNhanh.CancelEdit();
            LoadGrid();
        }

        protected void gridChiNhanh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string MaChiNhanh = e.NewValues["MaChiNhanh"].ToString();
            string TenChiNhanh = e.NewValues["TenChiNhanh"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            DateTime NgayMo = DateTime.Parse(e.NewValues["NgayMo"].ToString());
            data = new dtChiNhanh();
            data.SuaChiNhanh(ID, MaChiNhanh, TenChiNhanh, DienThoai, DiaChi, NgayMo);
            e.Cancel = true;
            gridChiNhanh.CancelEdit();
            LoadGrid();
        }
    }
}