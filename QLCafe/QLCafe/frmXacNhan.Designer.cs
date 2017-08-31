namespace QLCafe
{
    partial class frmXacNhan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXacNhan));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnHuyBo = new DevExpress.XtraEditors.SimpleButton();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.checkKhuVuc = new DevExpress.XtraEditors.CheckEdit();
            this.checkGio = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkKhuVuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkGio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnHuyBo);
            this.panelControl1.Controls.Add(this.btnXacNhan);
            this.panelControl1.Controls.Add(this.checkKhuVuc);
            this.panelControl1.Controls.Add(this.checkGio);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(432, 124);
            this.panelControl1.TabIndex = 1;
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyBo.Appearance.Options.UseFont = true;
            this.btnHuyBo.Image = ((System.Drawing.Image)(resources.GetObject("btnHuyBo.Image")));
            this.btnHuyBo.Location = new System.Drawing.Point(224, 69);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(99, 36);
            this.btnHuyBo.TabIndex = 2;
            this.btnHuyBo.Text = "Hủy Bỏ";
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Appearance.Options.UseFont = true;
            this.btnXacNhan.Image = ((System.Drawing.Image)(resources.GetObject("btnXacNhan.Image")));
            this.btnXacNhan.Location = new System.Drawing.Point(80, 69);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(116, 36);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // checkKhuVuc
            // 
            this.checkKhuVuc.Location = new System.Drawing.Point(205, 27);
            this.checkKhuVuc.Name = "checkKhuVuc";
            this.checkKhuVuc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkKhuVuc.Properties.Appearance.Options.UseFont = true;
            this.checkKhuVuc.Properties.Caption = "Tính lại phụ thu theo khu vực";
            this.checkKhuVuc.Size = new System.Drawing.Size(214, 22);
            this.checkKhuVuc.TabIndex = 1;
            // 
            // checkGio
            // 
            this.checkGio.Location = new System.Drawing.Point(12, 27);
            this.checkGio.Name = "checkGio";
            this.checkGio.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkGio.Properties.Appearance.Options.UseFont = true;
            this.checkGio.Properties.Caption = "Tính lại phụ thu theo giờ";
            this.checkGio.Size = new System.Drawing.Size(187, 22);
            this.checkGio.TabIndex = 1;
            // 
            // frmXacNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 124);
            this.Controls.Add(this.panelControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmXacNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MÀN HÌNH XÁC NHẬN";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkKhuVuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkGio.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit checkKhuVuc;
        private DevExpress.XtraEditors.CheckEdit checkGio;
        private DevExpress.XtraEditors.SimpleButton btnHuyBo;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;

    }
}