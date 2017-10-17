using BanHang.Data;
using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class BCTonKho_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idMatHang = Request.QueryString["idMatHang"];
            string strMatHang = "Tất cả mặt hàng";

            if (Int32.Parse(idMatHang) != -1)
            {
                strMatHang = dtBaoCao.TenMatHang(idMatHang);
            }

            rpBCTonKho rp = new rpBCTonKho();
            rp.Parameters["strMatHang"].Value = strMatHang;
            rp.Parameters["IDMatHang"].Value = idMatHang;
            rp.Parameters["strMatHang"].Visible = false;
            rp.Parameters["IDMatHang"].Visible = false;
            reportView.Report = rp;
        }
    }
}