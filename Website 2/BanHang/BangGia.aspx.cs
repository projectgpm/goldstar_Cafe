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
    public partial class BangGia : System.Web.UI.Page
    {
        dtBangGia data = new dtBangGia();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtBangGia();
            gridBangGia.DataSource = data.DanhSach();
            gridBangGia.DataBind();
        }

        protected void gridBangGia_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtBangGia();
            data.XoaBangGia(ID);
            e.Cancel = true;
            gridBangGia.CancelEdit();
            LoadGrid();
        }

        protected void gridBangGia_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenBangGia = e.NewValues["TenBangGia"].ToString();
            data = new dtBangGia();
            object ID = data.ThemMoi(TenBangGia);
            if (ID != null)
            {
                dtHangHoa hh = new dtHangHoa();
                DataTable db = hh.LayDanhSachHangHoa();
                foreach (DataRow dr in db.Rows)
                {
                    string IDHangHoa = dr["ID"].ToString();
                    string GiaCu = dr["GiaBan"].ToString();
                    data = new dtBangGia();
                    data.ThemIDHangHoaVaoChiTietGia(IDHangHoa, ID, GiaCu);
                }
            }
            e.Cancel = true;
            gridBangGia.CancelEdit();
            LoadGrid();
        }

        protected void gridBangGia_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenBangGia = e.NewValues["TenBangGia"].ToString();
            data = new dtBangGia();
            data.SuaBangGia(ID, TenBangGia);
            e.Cancel = true;
            gridBangGia.CancelEdit();
            LoadGrid();
        }
    }
}