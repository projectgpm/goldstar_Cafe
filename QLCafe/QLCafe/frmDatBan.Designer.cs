namespace QLCafe
{
    partial class frmDatBan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatBan));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.txtDienThoai = new DevExpress.XtraEditors.TextEdit();
            this.txtTenKhachHang = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSoNguoi = new DevExpress.XtraEditors.TextEdit();
            this.timeGioDat = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKhachHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNguoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioDat.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioDat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(11, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(135, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên Khách Hàng(*):";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(12, 66);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(94, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Điện Thoại(*):";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(12, 131);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 18);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Giờ Đặt(*):";
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(93, 176);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(106, 35);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu Lại";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Appearance.Options.UseFont = true;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(217, 176);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(84, 35);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.EditValue = "";
            this.txtDienThoai.Location = new System.Drawing.Point(177, 57);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDienThoai.Properties.Appearance.Options.UseFont = true;
            this.txtDienThoai.Properties.NullText = "Nhập số điện thoại";
            this.txtDienThoai.Size = new System.Drawing.Size(205, 24);
            this.txtDienThoai.TabIndex = 3;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.EditValue = "";
            this.txtTenKhachHang.Location = new System.Drawing.Point(177, 23);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenKhachHang.Properties.Appearance.Options.UseFont = true;
            this.txtTenKhachHang.Properties.NullText = "Nhập tên khách hàng";
            this.txtTenKhachHang.Size = new System.Drawing.Size(205, 24);
            this.txtTenKhachHang.TabIndex = 3;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(11, 96);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(122, 18);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Tổng Số Người(*):";
            // 
            // txtSoNguoi
            // 
            this.txtSoNguoi.EditValue = "";
            this.txtSoNguoi.Location = new System.Drawing.Point(177, 89);
            this.txtSoNguoi.Name = "txtSoNguoi";
            this.txtSoNguoi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoNguoi.Properties.Appearance.Options.UseFont = true;
            this.txtSoNguoi.Properties.NullText = "Số người ngồi trong bàn";
            this.txtSoNguoi.Size = new System.Drawing.Size(205, 24);
            this.txtSoNguoi.TabIndex = 3;
            // 
            // timeGioDat
            // 
            this.timeGioDat.EditValue = null;
            this.timeGioDat.Location = new System.Drawing.Point(177, 121);
            this.timeGioDat.Name = "timeGioDat";
            this.timeGioDat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeGioDat.Properties.Appearance.Options.UseFont = true;
            this.timeGioDat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeGioDat.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeGioDat.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.timeGioDat.Properties.DisplayFormat.FormatString = "dd/MM/yyyy H:mm";
            this.timeGioDat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.timeGioDat.Properties.EditFormat.FormatString = "dd/MM/yyyy H:mm";
            this.timeGioDat.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.timeGioDat.Properties.Mask.EditMask = "dd/MM/yyyy H:mm";
            this.timeGioDat.Properties.TimeEditWidth = 1;
            this.timeGioDat.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.timeGioDat.Size = new System.Drawing.Size(206, 22);
            this.timeGioDat.TabIndex = 5;
            // 
            // frmDatBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 225);
            this.Controls.Add(this.timeGioDat);
            this.Controls.Add(this.txtTenKhachHang);
            this.Controls.Add(this.txtSoNguoi);
            this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THÔNG TIN KHÁCH ĐẶT BÀN";
            this.Load += new System.EventHandler(this.frmDatBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKhachHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoNguoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioDat.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeGioDat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.TextEdit txtDienThoai;
        private DevExpress.XtraEditors.TextEdit txtTenKhachHang;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtSoNguoi;
        private DevExpress.XtraEditors.DateEdit timeGioDat;

    }
}