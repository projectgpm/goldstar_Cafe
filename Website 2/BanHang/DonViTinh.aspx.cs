using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonViTinh : System.Web.UI.Page
    {
        dtDonViTinh data = new dtDonViTinh();
        protected void Page_Load(object sender, EventArgs e)
        {
             LoadGrid();
        }
        public void LoadGrid()
        {
            data = new dtDonViTinh();
            gridDonViTinh.DataSource = data.LayDanhSachDonViTinh();
            gridDonViTinh.DataBind();
        }

        protected void gridDonViTinh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
           
            data = new dtDonViTinh();
            string TenDonViTinh = e.NewValues["TenDonViTinh"].ToString();
            string MaDonViTinh = e.NewValues["MaDVT"].ToString();
            if (dtDonViTinh.KiemTra(dtSetting.convertDauSangKhongDau(TenDonViTinh)) == 1)
            {
                data.ThemDonViTinh(dtSetting.convertDauSangKhongDau(TenDonViTinh), MaDonViTinh);
                e.Cancel = true;
                gridDonViTinh.CancelEdit();
                LoadGrid();
            }
            else
            {
                throw new Exception("Lỗi: Tên đơn vị tính đã tồn tại: " + dtSetting.convertDauSangKhongDau(TenDonViTinh));
            }

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Đơn vị tính", "Thêm đơn vị tính: " + TenDonViTinh);
        }

        protected void gridDonViTinh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtDonViTinh();
            data.XoaDonViTinh(Int32.Parse(ID));
            e.Cancel = true;
            gridDonViTinh.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Đơn vị tính", "Xóa đơn vị tính: " + ID);
        }

        protected void gridDonViTinh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            string TenDonViTinh = e.NewValues["TenDonViTinh"].ToString();
            string MaDonViTinh = e.NewValues["MaDVT"].ToString();
            data.SuaThongTinDonViTinh(ID, dtSetting.convertDauSangKhongDau(TenDonViTinh), MaDonViTinh);
            e.Cancel = true;
            gridDonViTinh.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Đơn vị tính", "Cập nhật đơn vị tính");
        }

        protected void gridDonViTinh_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaDVT"] = dtDonViTinh.Dem_Max();
        }
    }
}