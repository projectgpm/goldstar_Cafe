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
using QLCafe.DAO;
using QLCafe.DTO;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;

namespace QLCafe
{
    public partial class frmPhaChe : DevExpress.XtraEditors.XtraForm
    {
        public frmPhaChe()
        {
            InitializeComponent();
        }

        private void ckTuDong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTuDong.Checked == true)
            {
                txtSoGiay.Enabled = true;
            }
            else
            {
                txtSoGiay.Enabled = false;
            }
        }

        private void frmPhaChe_Load(object sender, EventArgs e)
        {
            DanhSachMon();
            txtSoGiay.Text = DAO_Setting.ThoiGianPhaChe().ToString();
            lblNgay.Text = "Ngày hôm nay: " + DateTime.Now.ToString("dd/MM/yyyy");
            txtTenDangNhap.Text = "Nhân viên: " + frmDangNhap.NguoiDung.Tennguoidung;
        }

        public void DanhSachMon()
        {
            List<DTO_DanhSachChuaPhaChe> DanhSachMonChuaPhaChe = DAO_PhaChe.Instance.GetDanhSachMonAn();
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridPhaChe.DataSource = null;
            gridPhaChe.DataSource = DanhSachMonChuaPhaChe;
        }
        private void txtSoGiay_EditValueChanged(object sender, EventArgs e)
        {
            if ((txtSoGiay.Value % 2) == 0)
            {
                int SoGiay = Int32.Parse(txtSoGiay.Value.ToString());
                DAO_PhaChe.CapNhatThoiGian(SoGiay);
            }
            else
                MessageBox.Show("Vui lòng nhập số nguyên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            DanhSachMon();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "Giờ hiện tại: " + DateTime.Now.ToLongTimeString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = DAO_Setting.ThoiGianPhaChe() * 1000;
            if (ckTuDong.Checked == true)
            {
                DanhSachMon();
            }
        }

        private void btnDaCheBien_Click(object sender, EventArgs e)
        {
            // ghi lại nhật ký pha chế. đổi trạng thái món
            GridView view = gridView1;
            if (view.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Xác nhận đã chế biến !!!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        if (gridView1.GetSelectedRows()[i] >= 0)
                        {

                            string ID = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[5]).ToString();
                            //đổi trạng thái xong, ghi lại nhật ký chế biến
                            if (DAO_PhaChe.CapNhatTrangThai(ID) == true)
                            {
                                // thành công đổi trạng thái
                                string MaHangHoa = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[0]).ToString();
                                string TenHangHoa = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[1]).ToString();
                                string TenDonViTinh = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[2]).ToString();
                                string TenBan = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[3]).ToString();
                                string SoLuong = gridView1.GetRowCellValue(gridView1.GetSelectedRows()[i], gridView1.Columns[4]).ToString();
                                DAO_PhaChe.ThemLichSuCheBien(MaHangHoa, TenHangHoa, TenDonViTinh, TenBan, SoLuong, frmDangNhap.NguoiDung.Tennguoidung);
                            }
                        }
                    }
                    DanhSachMon();
                }
            }
            else
                MessageBox.Show("Vui lòng chọn món đã chế biến", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

      
    }
}