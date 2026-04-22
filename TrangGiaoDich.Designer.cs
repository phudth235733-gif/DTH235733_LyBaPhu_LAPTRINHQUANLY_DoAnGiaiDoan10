namespace QuanLyQuanNet
{
    partial class TrangGiaoDich
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGiaoDich = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCuocPhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhToam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongDoanhThu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtDoanhThuGD = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnLocGD = new Guna.UI2.WinForms.Guna2Button();
            this.dtpDenNgayLoc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpLocTheoTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGiaoDich
            // 
            this.dgvGiaoDich.AllowUserToAddRows = false;
            this.dgvGiaoDich.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGiaoDich.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGiaoDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGiaoDich.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colThoiGian,
            this.colMoTa,
            this.colThanhVien,
            this.colCuocPhi,
            this.colThanhToam,
            this.colNguon});
            this.dgvGiaoDich.Location = new System.Drawing.Point(0, 259);
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.RowHeadersVisible = false;
            this.dgvGiaoDich.RowHeadersWidth = 51;
            this.dgvGiaoDich.RowTemplate.Height = 24;
            this.dgvGiaoDich.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiaoDich.Size = new System.Drawing.Size(1953, 420);
            this.dgvGiaoDich.TabIndex = 0;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID GD";
            this.colID.MinimumWidth = 6;
            this.colID.Name = "colID";
            // 
            // colThoiGian
            // 
            this.colThoiGian.DataPropertyName = "ThoiGian";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            this.colThoiGian.DefaultCellStyle = dataGridViewCellStyle4;
            this.colThoiGian.HeaderText = "Thời gian";
            this.colThoiGian.MinimumWidth = 6;
            this.colThoiGian.Name = "colThoiGian";
            // 
            // colMoTa
            // 
            this.colMoTa.DataPropertyName = "MoTa";
            this.colMoTa.HeaderText = "Mô Tả";
            this.colMoTa.MinimumWidth = 6;
            this.colMoTa.Name = "colMoTa";
            // 
            // colThanhVien
            // 
            this.colThanhVien.DataPropertyName = "ThanhVien";
            this.colThanhVien.HeaderText = "Thành Viên ";
            this.colThanhVien.MinimumWidth = 6;
            this.colThanhVien.Name = "colThanhVien";
            // 
            // colCuocPhi
            // 
            this.colCuocPhi.DataPropertyName = "CuocPhi";
            dataGridViewCellStyle5.Format = "#,##0 VNĐ";
            this.colCuocPhi.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCuocPhi.HeaderText = "Cước Phí";
            this.colCuocPhi.MinimumWidth = 6;
            this.colCuocPhi.Name = "colCuocPhi";
            // 
            // colThanhToam
            // 
            this.colThanhToam.DataPropertyName = "ThanhToan";
            dataGridViewCellStyle6.Format = "#,##0 VNĐ";
            this.colThanhToam.DefaultCellStyle = dataGridViewCellStyle6;
            this.colThanhToam.HeaderText = "Thanh Toán";
            this.colThanhToam.MinimumWidth = 6;
            this.colThanhToam.Name = "colThanhToam";
            // 
            // colNguon
            // 
            this.colNguon.DataPropertyName = "Nguon";
            this.colNguon.HeaderText = "Nguồn";
            this.colNguon.MinimumWidth = 6;
            this.colNguon.Name = "colNguon";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.BackColor = System.Drawing.Color.Transparent;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.lblTongDoanhThu.Location = new System.Drawing.Point(43, 701);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(146, 23);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "Tống Doanh Thu Đạt Được:";
            // 
            // txtDoanhThuGD
            // 
            this.txtDoanhThuGD.BorderRadius = 15;
            this.txtDoanhThuGD.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDoanhThuGD.DefaultText = "";
            this.txtDoanhThuGD.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDoanhThuGD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDoanhThuGD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThuGD.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThuGD.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThuGD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDoanhThuGD.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThuGD.Location = new System.Drawing.Point(205, 686);
            this.txtDoanhThuGD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoanhThuGD.Name = "txtDoanhThuGD";
            this.txtDoanhThuGD.PlaceholderText = "";
            this.txtDoanhThuGD.SelectedText = "";
            this.txtDoanhThuGD.Size = new System.Drawing.Size(172, 38);
            this.txtDoanhThuGD.TabIndex = 4;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.AutoSize = false;
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(671, 41);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(91, 55);
            this.guna2HtmlLabel4.TabIndex = 19;
            this.guna2HtmlLabel4.Text = "Đến Ngày:";
            this.guna2HtmlLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(244, 41);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(191, 55);
            this.guna2HtmlLabel3.TabIndex = 18;
            this.guna2HtmlLabel3.Text = "Lọc Theo Từ Ngày:";
            this.guna2HtmlLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLocGD
            // 
            this.btnLocGD.BorderRadius = 22;
            this.btnLocGD.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLocGD.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLocGD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLocGD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLocGD.FillColor = System.Drawing.Color.Silver;
            this.btnLocGD.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.btnLocGD.ForeColor = System.Drawing.Color.White;
            this.btnLocGD.Image = global::QuanLyQuanNet.Properties.Resources.filter;
            this.btnLocGD.Location = new System.Drawing.Point(968, 151);
            this.btnLocGD.Name = "btnLocGD";
            this.btnLocGD.Size = new System.Drawing.Size(180, 45);
            this.btnLocGD.TabIndex = 17;
            this.btnLocGD.Text = "Lọc";
            // 
            // dtpDenNgayLoc
            // 
            this.dtpDenNgayLoc.Checked = true;
            this.dtpDenNgayLoc.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgayLoc.FillColor = System.Drawing.Color.CadetBlue;
            this.dtpDenNgayLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgayLoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgayLoc.Location = new System.Drawing.Point(784, 43);
            this.dtpDenNgayLoc.MaxDate = new System.DateTime(3199, 11, 23, 0, 0, 0, 0);
            this.dtpDenNgayLoc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgayLoc.Name = "dtpDenNgayLoc";
            this.dtpDenNgayLoc.Size = new System.Drawing.Size(200, 36);
            this.dtpDenNgayLoc.TabIndex = 16;
            this.dtpDenNgayLoc.Value = new System.DateTime(2026, 3, 31, 0, 0, 0, 0);
            // 
            // dtpLocTheoTuNgay
            // 
            this.dtpLocTheoTuNgay.Checked = true;
            this.dtpLocTheoTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpLocTheoTuNgay.FillColor = System.Drawing.Color.CadetBlue;
            this.dtpLocTheoTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpLocTheoTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLocTheoTuNgay.Location = new System.Drawing.Point(430, 43);
            this.dtpLocTheoTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpLocTheoTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpLocTheoTuNgay.Name = "dtpLocTheoTuNgay";
            this.dtpLocTheoTuNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpLocTheoTuNgay.TabIndex = 15;
            this.dtpLocTheoTuNgay.Value = new System.DateTime(2026, 3, 31, 22, 59, 58, 760);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(172, 158);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(191, 55);
            this.guna2HtmlLabel1.TabIndex = 20;
            this.guna2HtmlLabel1.Text = "Tìm Kiếm Giao Dịch Theo PC :\r\n\r\n";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 15;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(383, 158);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(172, 38);
            this.txtTimKiem.TabIndex = 21;
            // 
            // TrangGiaoDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.btnLocGD);
            this.Controls.Add(this.dtpDenNgayLoc);
            this.Controls.Add(this.dtpLocTheoTuNgay);
            this.Controls.Add(this.txtDoanhThuGD);
            this.Controls.Add(this.lblTongDoanhThu);
            this.Controls.Add(this.dgvGiaoDich);
            this.Name = "TrangGiaoDich";
            this.Size = new System.Drawing.Size(1953, 824);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGiaoDich;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTongDoanhThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThoiGian;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCuocPhi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhToam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguon;
        private Guna.UI2.WinForms.Guna2TextBox txtDoanhThuGD;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button btnLocGD;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgayLoc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpLocTheoTuNgay;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
    }
}
