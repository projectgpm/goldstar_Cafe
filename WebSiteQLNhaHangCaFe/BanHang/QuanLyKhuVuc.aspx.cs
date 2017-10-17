using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QuanLyKhuVuc : System.Web.UI.Page
    {
        dtKhuVuc data = new dtKhuVuc();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtKhuVuc();
            gridDanhSach.DataSource = data.DanhSach();
            gridDanhSach.DataBind();
         
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenKhuVuc = e.NewValues["TenKhuVuc"].ToString();
            string KyHieu = e.NewValues["KyHieu"].ToString();
            string TyLe = e.NewValues["GiaKhuVuc"].ToString();
            string IDChiNhanh = e.NewValues["IDChiNhanh"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data = new dtKhuVuc();
            data.Sua(ID, GhiChu, TenKhuVuc, TyLe, IDChiNhanh, KyHieu);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Quản lý khu vực", "Cập nhật khu vực: " + ID);
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string IDChiNhanh = e.NewValues["IDChiNhanh"].ToString();
            string KyHieu = e.NewValues["KyHieu"].ToString();
            string MaKhuVuc = dtKhuVuc.Dem_Max(IDChiNhanh);
            string TenKhuVuc = e.NewValues["TenKhuVuc"].ToString();
            string TyLe = e.NewValues["GiaKhuVuc"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            //if (dtKhuVuc.KiemTra(KyHieu) == true)
            //{
            data = new dtKhuVuc();
            data.Them(MaKhuVuc, TenKhuVuc, TyLe, IDChiNhanh, GhiChu, KyHieu);
            //}
            //else
            //{
            //    throw new Exception("Lỗi: Ký hiệu đã tồn tại: ");
            //}
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Quản lý khu vực", "Thêm khu vực: " + TenKhuVuc );
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKhuVuc();
            data.Xoa(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Quản lý khu vực", "Xóa khu vực: " + ID);
        }
    }
}