using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QuanLyBan : System.Web.UI.Page
    {
        dtBan data = new dtBan();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtBan();
            gridDanhSach.DataSource = data.DanhSach();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtBan();
            if (dtBan.TrangThai(ID) != 0)
            {
                throw new Exception("Lỗi: Bàn này đã có món ăn?");
            }
            else
            {
                data.Xoa(ID);
            }
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenBan = e.NewValues["TenBan"].ToString();
            string IDKhuVuc = e.NewValues["IDKhuVuc"].ToString();
            string MaBan = dtBan.Dem_Max(IDKhuVuc);
            data = new dtBan();
            data.Them(MaBan, TenBan, IDKhuVuc);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenBan = e.NewValues["TenBan"].ToString();
            string IDKhuVuc = e.NewValues["IDKhuVuc"].ToString();
            data = new dtBan();
            data.Sua(ID, TenBan, IDKhuVuc);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }
    }
}