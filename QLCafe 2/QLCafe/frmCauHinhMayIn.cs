using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLCafe.DTO;
using QLCafe.DAO;
using System.Net;

namespace QLCafe
{
    public partial class frmCauHinhMayIn : DevExpress.XtraEditors.XtraForm
    {
        public frmCauHinhMayIn()
        {
            InitializeComponent();
        }

        private void frmCauHinhMayIn_Load(object sender, EventArgs e)
        {
            DanhSach(frmDangNhap.NguoiDung.Idchinhanh);
            DanhSachMayIn();
        }
        public void DanhSachMayIn()
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbMayIn.Properties.Items.Add(printer.ToString());
            }
        }
        public void DanhSach(string IDChiNhanh)
        {
            List<DTO_MayIn> ds = DAO_MayIn.Instance.DanhSachCaBan(IDChiNhanh);
            gridControlCTHD.DataSource = ds;
        }
        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            if (cmbMayIn.Text != "" && cmbKhoGiay.Text !="")
            {
                if (DAO_Setting.KiemTra(cmbMayIn.Text.ToString(), frmDangNhap.NguoiDung.Idchinhanh) == -1)
                {
                    DAO_Setting.ThemMayIn(cmbMayIn.Text.ToString(), frmDangNhap.NguoiDung.Idchinhanh, cmbKhoGiay.Text.ToString());
                    DanhSach(frmDangNhap.NguoiDung.Idchinhanh);
                }
                else
                    MessageBox.Show("Tên máy in đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Vui lòng chọn máy in và khổ giấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gridControlCTHD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && gridView1.State != DevExpress.XtraGrid.Views.Grid.GridState.Editing)
            {
                if (MessageBox.Show("Bạn muốn xóa máy in này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    string ID = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns[0]).ToString();
                    if (DAO_Setting.XoaMayIn(ID))
                    {
                        DanhSach(frmDangNhap.NguoiDung.Idchinhanh);
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}