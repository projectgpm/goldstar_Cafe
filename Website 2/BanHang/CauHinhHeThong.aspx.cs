using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class CauHinhHeThong : System.Web.UI.Page
    {
        dtSetting data = new dtSetting();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtSetting();
            gridDanhSach.DataSource = data.CauHinhHeThong();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string CongTy = e.NewValues["CongTy"].ToString();
            string DiaChi = e.NewValues["DiaChi"].ToString();
            string SDT = e.NewValues["SDT"].ToString();
            string SoTienTichLuy = e.NewValues["SoTienTichLuy"].ToString();
            string PhuThuCaFe = e.NewValues["PhuThuCaFe"].ToString();
            string SoTienQuyDoi = e.NewValues["SoTienQuyDoi"].ToString();

            string PhuThuCaFe_PhanTram = e.NewValues["PhuThuCaFe_PhanTram"].ToString();
            string PhuThuCaFe_ApDung = e.NewValues["PhuThuCaFe_ApDung"].ToString();

            data = new dtSetting();
            data.CapNhatCauHinh(CongTy, DiaChi, SDT, SoTienTichLuy, SoTienQuyDoi, PhuThuCaFe, PhuThuCaFe_PhanTram, PhuThuCaFe_ApDung);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }
    }
}