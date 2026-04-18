namespace QuanLyQuanNet
{
    partial class ThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.cbmThangNay = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnXuatBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.btnXuatExcel = new Guna.UI2.WinForms.Guna2Button();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtDoanhThu = new Guna.UI2.WinForms.Guna2TextBox();
            this.chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnDoanhThu = new Guna.UI2.WinForms.Guna2Button();
            this.btnPCHD = new Guna.UI2.WinForms.Guna2Button();
            this.btnMember = new Guna.UI2.WinForms.Guna2Button();
            this.dgvThongKeChiTiet = new System.Windows.Forms.DataGridView();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.Controls.Add(this.cbmThangNay);
            this.guna2Panel1.Controls.Add(this.btnLoc);
            this.guna2Panel1.Controls.Add(this.btnXuatBaoCao);
            this.guna2Panel1.Controls.Add(this.btnXuatExcel);
            this.guna2Panel1.Controls.Add(this.dtpDenNgay);
            this.guna2Panel1.Controls.Add(this.dtpTuNgay);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Controls.Add(this.txtDoanhThu);
            this.guna2Panel1.Controls.Add(this.chartThongKe);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(927, 530);
            this.guna2Panel1.TabIndex = 0;
            // 
            // cbmThangNay
            // 
            this.cbmThangNay.BackColor = System.Drawing.Color.Transparent;
            this.cbmThangNay.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbmThangNay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmThangNay.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbmThangNay.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbmThangNay.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbmThangNay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbmThangNay.ItemHeight = 30;
            this.cbmThangNay.Location = new System.Drawing.Point(110, 90);
            this.cbmThangNay.Name = "cbmThangNay";
            this.cbmThangNay.Size = new System.Drawing.Size(140, 36);
            this.cbmThangNay.TabIndex = 10;
            this.cbmThangNay.SelectedIndexChanged += new System.EventHandler(this.cbmThangNay_SelectedIndexChanged);
            // 
            // btnLoc
            // 
            this.btnLoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(406, 143);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(180, 45);
            this.btnLoc.TabIndex = 9;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatBaoCao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuatBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(709, 23);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(180, 45);
            this.btnXuatBaoCao.TabIndex = 8;
            this.btnXuatBaoCao.Text = "Xuất Báo Cáo";
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(475, 23);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(180, 45);
            this.btnXuatExcel.TabIndex = 7;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(573, 90);
            this.dtpDenNgay.MaxDate = new System.DateTime(3199, 11, 23, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpDenNgay.TabIndex = 6;
            this.dtpDenNgay.Value = new System.DateTime(2026, 3, 31, 0, 0, 0, 0);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(352, 90);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpTuNgay.TabIndex = 5;
            this.dtpTuNgay.Value = new System.DateTime(2026, 3, 31, 22, 59, 58, 760);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(28, 150);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(149, 18);
            this.guna2HtmlLabel1.TabIndex = 4;
            this.guna2HtmlLabel1.Text = "Doanh Thu Đã Đạt Được:";
            // 
            // txtDoanhThu
            // 
            this.txtDoanhThu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDoanhThu.DefaultText = "";
            this.txtDoanhThu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDoanhThu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDoanhThu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDoanhThu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDoanhThu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDoanhThu.Location = new System.Drawing.Point(172, 150);
            this.txtDoanhThu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoanhThu.Name = "txtDoanhThu";
            this.txtDoanhThu.PlaceholderText = "";
            this.txtDoanhThu.SelectedText = "";
            this.txtDoanhThu.Size = new System.Drawing.Size(172, 38);
            this.txtDoanhThu.TabIndex = 3;
            // 
            // chartThongKe
            // 
            this.chartThongKe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartThongKe.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartThongKe.Legends.Add(legend2);
            this.chartThongKe.Location = new System.Drawing.Point(3, 219);
            this.chartThongKe.Name = "chartThongKe";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartThongKe.Series.Add(series2);
            this.chartThongKe.Size = new System.Drawing.Size(914, 311);
            this.chartThongKe.TabIndex = 0;
            this.chartThongKe.Text = "chart1";
            // 
            // btnDoanhThu
            // 
            this.btnDoanhThu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDoanhThu.BorderRadius = 15;
            this.btnDoanhThu.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDoanhThu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDoanhThu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDoanhThu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDoanhThu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDoanhThu.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDoanhThu.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.btnDoanhThu.ForeColor = System.Drawing.Color.White;
            this.btnDoanhThu.Location = new System.Drawing.Point(85, 637);
            this.btnDoanhThu.Name = "btnDoanhThu";
            this.btnDoanhThu.Size = new System.Drawing.Size(180, 45);
            this.btnDoanhThu.TabIndex = 1;
            this.btnDoanhThu.Text = "Doanh Thu Hôm Nay";
            // 
            // btnPCHD
            // 
            this.btnPCHD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPCHD.BorderRadius = 15;
            this.btnPCHD.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnPCHD.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPCHD.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPCHD.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPCHD.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPCHD.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPCHD.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.btnPCHD.ForeColor = System.Drawing.Color.White;
            this.btnPCHD.Location = new System.Drawing.Point(314, 637);
            this.btnPCHD.Name = "btnPCHD";
            this.btnPCHD.Size = new System.Drawing.Size(180, 45);
            this.btnPCHD.TabIndex = 2;
            this.btnPCHD.Text = "PC Đang Hoạt Động";
            // 
            // btnMember
            // 
            this.btnMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMember.BorderRadius = 15;
            this.btnMember.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMember.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMember.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMember.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMember.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMember.FillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnMember.Font = new System.Drawing.Font("Bahnschrift Condensed", 10.2F);
            this.btnMember.ForeColor = System.Drawing.Color.White;
            this.btnMember.Location = new System.Drawing.Point(606, 637);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(180, 45);
            this.btnMember.TabIndex = 3;
            this.btnMember.Text = "Thành Viên Mới";
            // 
            // dgvThongKeChiTiet
            // 
            this.dgvThongKeChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeChiTiet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvThongKeChiTiet.Location = new System.Drawing.Point(0, 749);
            this.dgvThongKeChiTiet.Name = "dgvThongKeChiTiet";
            this.dgvThongKeChiTiet.RowHeadersWidth = 51;
            this.dgvThongKeChiTiet.RowTemplate.Height = 24;
            this.dgvThongKeChiTiet.Size = new System.Drawing.Size(920, 47);
            this.dgvThongKeChiTiet.TabIndex = 2;
            this.dgvThongKeChiTiet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThongKeChiTiet_CellContentClick);
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // ThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDoanhThu);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.btnPCHD);
            this.Controls.Add(this.btnMember);
            this.Controls.Add(this.dgvThongKeChiTiet);
            this.Name = "ThongKe";
            this.Size = new System.Drawing.Size(920, 796);
            this.Load += new System.EventHandler(this.ThongKe_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeChiTiet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnDoanhThu;
        private Guna.UI2.WinForms.Guna2Button btnPCHD;
        private Guna.UI2.WinForms.Guna2Button btnMember;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
        private System.Windows.Forms.DataGridView dgvThongKeChiTiet;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtDoanhThu;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private Guna.UI2.WinForms.Guna2Button btnXuatBaoCao;
        private Guna.UI2.WinForms.Guna2Button btnXuatExcel;
        private Guna.UI2.WinForms.Guna2Button btnLoc;
        private Guna.UI2.WinForms.Guna2ComboBox cbmThangNay;
    }
}
