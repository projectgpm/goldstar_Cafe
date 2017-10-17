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

namespace QLCafe
{
    public partial class frmKichHoat : DevExpress.XtraEditors.XtraForm
    {
        public frmKichHoat()
        {
            InitializeComponent();
        }

        private void linkLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gpm.vn/");
        }

        private void frmKichHoat_Load(object sender, EventArgs e)
        {
            txtSoA.Select();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKichHoat_Click(object sender, EventArgs e)
        {
            if (txtSoA.Text != "" && txtSoB.Text != "" && txtSoC.Text != "" && txtSoD.Text != "" && txtSoE.Text != "" && DAO_Setting.IsNumber(txtSoA.Text) == true && DAO_Setting.IsNumber(txtSoB.Text) == true && DAO_Setting.IsNumber(txtSoC.Text) == true && DAO_Setting.IsNumber(txtSoD.Text) == true && DAO_Setting.IsNumber(txtSoE.Text) == true)
            {
                int SoA = Int32.Parse(txtSoA.Text.ToString());
                int SoB = Int32.Parse(txtSoB.Text.ToString());
                int SoC = Int32.Parse(txtSoC.Text.ToString());
                int SoD = Int32.Parse(txtSoD.Text.ToString());
                int SoE = Int32.Parse(txtSoE.Text.ToString());
                double SUM = SoA + SoB + SoC + SoD + SoE;
                int NamHienTai = Int32.Parse(DateTime.Now.ToString("yyyy"));
                if ((SUM + NamHienTai) == 75595)
                {
                    if (DAO_DangNhap.ThemKeyKichHoat(DAO_Setting.GetSHA1HashData("GPMVIETNAM@2017")) == true)
                    {
                        if (MessageBox.Show("Kích hoạt bản quyền thành công.Cảm ơn quý khách đã xử dụng dịch vụ của chúng tôi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Key không chính xác? Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Key không chính xác? Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

    
    }
}