using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KhachHang : System.Web.UI.Page
    {
        dtKhachHang data = new dtKhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        public void LoadGrid()
        {
            data = new dtKhachHang();
            KhachHangExport.DataSource = data.LayDanhSachKhachHang();
            KhachHangExport.DataBind();
        }

        protected void gridKhachHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"] == null ? "" : e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse(e.NewValues["NgaySinh"] == null ? DateTime.Today.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string MaKhachHang = e.NewValues["MaKhachHang"].ToString();
            data = new dtKhachHang();
            if (data.KiemTraSDTKhachHang_KhacID(ID,DienThoai) == 0)
            {
                if (data.KiemTraMaKhachHang_CapNhat(ID, MaKhachHang) == true)
                {
                    data.SuaThongTinKhachHang(Int32.Parse(ID), IDNhomKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, GhiChu, MaKhachHang);
                    e.Cancel = true;
                    KhachHangExport.CancelEdit();
                    LoadGrid();
                }
                else
                {
                    throw new Exception("Mã khách hàng đã được đăng ký.");
                }
            }
            else
            {
                throw new Exception("Số điện thoại này đã được đăng ký.");
            }
        }

        protected void gridKhachHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"] == null ? "" : e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse(e.NewValues["NgaySinh"] == null ? DateTime.Today.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string MaKhachHang = e.NewValues["MaKhachHang"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (data.KiemTraSDTKhachHang(DienThoai) == 0)
            {
                if (data.KiemTraMaKhachHang(MaKhachHang) == true)
                {
                    data.ThemKhachHang(IDNhomKhachHang, MaKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, GhiChu);
                    e.Cancel = true;
                    KhachHangExport.CancelEdit();
                    LoadGrid();
                }
                else
                {
                    throw new Exception("Mã khách hàng đã được đăng ký.");
                }
            }
            else
            {
                throw new Exception("Số điện thoại này đã được đăng ký.");
            }
        }

        protected void gridKhachHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKhachHang();
            data.XoaKhachHang(Int32.Parse(ID));
            e.Cancel = true;
            KhachHangExport.CancelEdit();
            LoadGrid();
        }

        protected void btnXuatPDF_Click(object sender, EventArgs e)
        {
            //XuatDuLieu.WritePdfToResponse();
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExportKhachHang.WriteXlsToResponse();
        }

        protected void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImportExcel_KhachHang.aspx");
        }
    }
}