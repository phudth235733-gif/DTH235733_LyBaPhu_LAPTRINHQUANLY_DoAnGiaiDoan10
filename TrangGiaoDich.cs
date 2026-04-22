using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Bắt buộc phải có cái này để dùng kết nối MySQL

namespace QuanLyQuanNet
{
    public partial class TrangGiaoDich : UserControl
    {
        // Chuỗi kết nối Database (Giống bên trang Thống Kê)
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";

        public TrangGiaoDich()
        {
            InitializeComponent();

            // Cắm dây điện cho nút Lọc
            btnLocGD.Click += btnLocGD_Click;
        }

        // Hàm này chạy khi form vừa mở lên (được gọi từ Form1)
        public void LoadDuLieuGiaoDich()
        {
            // Mặc định khi mở lên: Load toàn bộ dữ liệu (từ năm 2000 đến 3000 để ra hết)
            // Không gõ gì vào ô tìm kiếm thì nó lấy tất cả
            ThucHienLoc("", new DateTime(2000, 1, 1), new DateTime(3000, 1, 1));
        }

        // ==============================================================
        // SỰ KIỆN KHI BẤM NÚT LỌC
        // ==============================================================
        private void btnLocGD_Click(object sender, EventArgs e)
        {
            // 1. Lấy tên PC khách gõ (Xóa khoảng trắng 2 đầu)
            string tuKhoa = txtTimKiem.Text.Trim();

            // 2. Lấy ngày bắt đầu (Mặc định nó lấy 00:00:00)
            DateTime tuNgay = dtpLocTheoTuNgay.Value.Date;

            // 3. Lấy ngày kết thúc (Ép nó tới 23:59:59 của ngày đó để lấy trọn vẹn)
            DateTime denNgay = dtpDenNgayLoc.Value.Date.AddDays(1).AddSeconds(-1);

            // 4. Bắn dữ liệu qua hàm xử lý
            ThucHienLoc(tuKhoa, tuNgay, denNgay);
        }

        // ==============================================================
        // HÀM XỬ LÝ LỌC DATA VÀ TÍNH TỔNG TIỀN TỪ MYSQL
        // ==============================================================
        private void ThucHienLoc(string tuKhoa, DateTime tuNgay, DateTime denNgay)
        {
            double tongTien = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();

                    // Khởi tạo câu lệnh SQL lấy thời gian
                    string sql = @"SELECT * FROM GiaoDich 
                                   WHERE ThoiGian BETWEEN @TuNgay AND @DenNgay ";

                    // Nếu ô tìm kiếm có chữ, thì thêm lệnh LIKE để tìm PC
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        sql += " AND ThanhVien LIKE @TuKhoa ";
                    }

                    // Sắp xếp mới nhất lên đầu
                    sql += " ORDER BY ThoiGian DESC";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.Add("@TuNgay", MySqlDbType.DateTime).Value = tuNgay;
                    cmd.Parameters.Add("@DenNgay", MySqlDbType.DateTime).Value = denNgay;

                    // Nếu có gõ chữ tìm kiếm thì thêm biến @TuKhoa (Tìm chuỗi chứa chữ đó)
                    if (!string.IsNullOrEmpty(tuKhoa))
                    {
                        cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                    }

                    // Đổ dữ liệu vào DataTable
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Tắt tự động tạo cột rác và nhét data vào bảng lưới
                    dgvGiaoDich.AutoGenerateColumns = false;
                    dgvGiaoDich.DataSource = dt;

                    // Vòng lặp tính TỔNG TIỀN của những dòng vừa lọc được
                    foreach (DataRow r in dt.Rows)
                    {
                        tongTien += Convert.ToDouble(r["ThanhToan"]);
                    }

                    // In tổng tiền ra cái ô txtDoanhThuGD siêu xịn của ní
                    txtDoanhThuGD.Text = tongTien.ToString("#,##0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu giao dịch: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}