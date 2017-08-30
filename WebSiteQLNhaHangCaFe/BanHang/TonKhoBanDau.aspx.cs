using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class TonKhoBanDau : System.Web.UI.Page
    {
        dtTonKhoNguyenLieu data = new dtTonKhoNguyenLieu();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtTonKhoNguyenLieu();
            gridDanhSach.DataSource = data.DanhSachTonKhoNguyenLieu();
            gridDanhSach.DataBind();
        }
    }
}