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
    public partial class BCTonKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dx = dtBaoCao.NhomMatHang();
                dx.Rows.Add(-1,"Tất cả mặt hàng");
                cmbMatHang.DataSource = dx;
                cmbMatHang.TextField = "TenNguyenLieu";
                cmbMatHang.ValueField = "ID";
                cmbMatHang.DataBind();
                cmbMatHang.SelectedIndex = cmbMatHang.Items.Count;

            }
        }

        protected void btnInBaoCao_Click(object sender, EventArgs e)
        {
            string idMatHang = cmbMatHang.Value + "";

            dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Báo cáo tồn kho", "Xem báo cáo tồn kho");

            popup.ContentUrl = "~/BCTonKho_In.aspx?idMatHang=" + idMatHang;
            popup.ShowOnPageLoad = true;
        }
    }
}