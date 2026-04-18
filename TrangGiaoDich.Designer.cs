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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGiaoDich = new System.Windows.Forms.DataGridView();
            this.lblTongDoanhThu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThoiGian = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCuocPhi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhToam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguon = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.dgvGiaoDich.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvGiaoDich.Location = new System.Drawing.Point(0, 0);
            this.dgvGiaoDich.Name = "dgvGiaoDich";
            this.dgvGiaoDich.RowHeadersVisible = false;
            this.dgvGiaoDich.RowHeadersWidth = 51;
            this.dgvGiaoDich.RowTemplate.Height = 24;
            this.dgvGiaoDich.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiaoDich.Size = new System.Drawing.Size(728, 556);
            this.dgvGiaoDich.TabIndex = 0;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.BackColor = System.Drawing.Color.Transparent;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.lblTongDoanhThu.Location = new System.Drawing.Point(36, 655);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(146, 23);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "Tống Doanh Thu Đạt Được:";
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
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.colThoiGian.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Format = "#,##0 VNĐ";
            this.colCuocPhi.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCuocPhi.HeaderText = "Cước Phí";
            this.colCuocPhi.MinimumWidth = 6;
            this.colCuocPhi.Name = "colCuocPhi";
            // 
            // colThanhToam
            // 
            this.colThanhToam.DataPropertyName = "ThanhToan";
            dataGridViewCellStyle3.Format = "#,##0 VNĐ";
            this.colThanhToam.DefaultCellStyle = dataGridViewCellStyle3;
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
            // TrangGiaoDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTongDoanhThu);
            this.Controls.Add(this.dgvGiaoDich);
            this.Name = "TrangGiaoDich";
            this.Size = new System.Drawing.Size(728, 778);
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
    }
}
