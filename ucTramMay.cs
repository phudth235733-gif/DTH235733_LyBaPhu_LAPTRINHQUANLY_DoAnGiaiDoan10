using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QuanLyQuanNet
{
    public partial class ucTramMay : UserControl
    {
        // Giữ nguyên các biến hình ảnh và màu sắc của bạn
        Image imgPcOn = Image.FromFile(@"C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\pic_on.png");
        Image imgPcOff = Image.FromFile(@"C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\pic_off.png");
        Color mauChuCoKhach = Color.Lime;
        Color mauChuTrong = Color.Silver;

        private bool _isOnline = false;

        public string TenMay
        {
            get { return lblTenMay.Text; }
            set { lblTenMay.Text = value; }
        }

        public DateTime ThoiGianBatDau { get; set; }
        public double TienTraTruoc { get; set; } = 0;

        public ucTramMay()
        {
            InitializeComponent();
            SetStatus(false);

            // --- 1. THÊM TÍNH NĂNG "XUYÊN THẤU" CLICK ---
            // Giúp click vào cái hình hay cái chữ trên máy đều nhận lệnh
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += (s, e) => this.InvokeOnClick(this, EventArgs.Empty);
            }
        }

        // --- 2. THÊM HÀM PHÁT SÁNG KHI ĐƯỢC CHỌN ---
        public void SetSelected(bool isSelected)
        {
            if (isSelected)
            {
                // Đổi màu nền thành Xanh dương để báo hiệu đang chọn
                this.BackColor = Color.FromArgb(88, 101, 242);
            }
            else
            {
                // Trả về trong suốt khi bỏ chọn
                this.BackColor = Color.Transparent;
            }
        }
        public void SetStatus(bool isOnline, string timeUsed = "Trống")
        {
            _isOnline = isOnline;

            if (_isOnline)
            {
                // Máy có người chơi
                picPC.Image = imgPcOn;
                lblTrangThai.Text = timeUsed; // Hiện thời gian đã sử dụng
                lblTrangThai.ForeColor = mauChuCoKhach;
                this.BorderStyle = BorderStyle.FixedSingle; // Thêm viền nhẹ
            }
            else
            {
                // Máy trống
                picPC.Image = imgPcOff;
                lblTrangThai.Text = "Trống";
                lblTrangThai.ForeColor = mauChuTrong;
                this.BorderStyle = BorderStyle.None; // Bỏ viền
            }
        }

        // Sự kiện Click cho toàn bộ UserControl
        private void ucTramMay_Click(object sender, EventArgs e)
        {
            // Gọi sự kiện Click của chính UserControl để Form1 có thể bắt được
            this.OnClick(e);
        }
    }
}