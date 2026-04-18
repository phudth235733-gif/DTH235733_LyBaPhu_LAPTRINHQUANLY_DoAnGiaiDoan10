using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

// LƯU Ý: Nếu báo lỗi ở chữ DataService, hãy chắc chắn bạn đã tạo file DataService.cs
namespace QuanLyQuanNet
{
    public partial class TrangGiaoDich : UserControl
    {
        public TrangGiaoDich()
        {
            InitializeComponent();
        }

        public void LoadDuLieuGiaoDich()
        {
            double tongTien = 0;

            try
            {
                // Gọi kết nối MySQL thông qua DataService
                DataService db = new DataService();

                // Lấy toàn bộ dữ liệu từ bảng GiaoDich, xếp mới nhất lên đầu
                DataTable dt = db.GetTable("SELECT * FROM GiaoDich ORDER BY ID DESC");

                // Tắt chế độ tự tạo cột rác
                dgvGiaoDich.AutoGenerateColumns = false;

                // GÁN DỮ LIỆU VÀO BẢNG: 1 dòng này là đủ đổ 18 dòng ra, KHÔNG dùng Rows.Add nữa
                dgvGiaoDich.DataSource = dt;

                // Vòng lặp này BÂY GIỜ CHỈ DÙNG ĐỂ TÍNH TỔNG TIỀN
                foreach (DataRow r in dt.Rows)
                {
                    tongTien += Convert.ToDouble(r["ThanhToan"]);
                }

                // Cập nhật tổng tiền (Nếu form của bạn có Label này thì bỏ dấu // đi)
                // lblTongDoanhThu.Text = $"Tổng doanh thu:\n{tongTien:#,##0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc tải dữ liệu từ MySQL: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}