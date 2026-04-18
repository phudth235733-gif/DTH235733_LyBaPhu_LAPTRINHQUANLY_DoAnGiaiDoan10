namespace QuanLyQuanNet
{
    partial class ucTramMay
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
            this.picPC = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblTenMay = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPC)).BeginInit();
            this.SuspendLayout();
            // 
            // picPC
            // 
            this.picPC.ImageRotate = 0F;
            this.picPC.Location = new System.Drawing.Point(3, 0);
            this.picPC.Name = "picPC";
            this.picPC.Size = new System.Drawing.Size(147, 118);
            this.picPC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPC.TabIndex = 0;
            this.picPC.TabStop = false;
            // 
            // lblTenMay
            // 
            this.lblTenMay.Location = new System.Drawing.Point(4, 121);
            this.lblTenMay.Name = "lblTenMay";
            this.lblTenMay.Size = new System.Drawing.Size(127, 23);
            this.lblTenMay.TabIndex = 1;
            this.lblTenMay.Text = "label1";
            this.lblTenMay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(4, 146);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(127, 23);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "label2";
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucTramMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(66)))), ((int)(((byte)(73)))));
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblTenMay);
            this.Controls.Add(this.picPC);
            this.Name = "ucTramMay";
            this.Size = new System.Drawing.Size(150, 180);
            ((System.ComponentModel.ISupportInitialize)(this.picPC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picPC;
        private System.Windows.Forms.Label lblTenMay;
        private System.Windows.Forms.Label lblTrangThai;
    }
}
