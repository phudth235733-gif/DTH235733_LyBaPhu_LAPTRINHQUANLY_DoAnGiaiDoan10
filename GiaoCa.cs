using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QuanLyQuanNet
{
    public partial class GiaoCa : UserControl
    {
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";

        private DateTime gioVaoCa;
        private string tenNhanVienDangTruc;

        // Cấu trúc lưu thông tin nhân viên
        class ThongTinNhanVien
        {
            public string HoTen;
            public string DuongDanAnh;
        }

        // ==============================================================
        // DATA NHÂN VIÊN VÀ LINK ẢNH (HARDCODE)
        // ==============================================================
        Dictionary<string, ThongTinNhanVien> dsTaiKhoan = new Dictionary<string, ThongTinNhanVien>()
        {
            { "admin", new ThongTinNhanVien { HoTen = "Sếp Tổng", DuongDanAnh = @"C:\code\lập trình quản lý\QuanLyQuanNet\QuanLyQuanNet\New folder\admin.jpg" } },
            { "nv1", new ThongTinNhanVien { HoTen = "Nguyễn Văn A", DuongDanAnh = @"C:\code\lập trình quản lý\QuanLyQuanNet\QuanLyQuanNet\New folder\nv1.jpg" } },
            { "nv2", new ThongTinNhanVien { HoTen = "Trần Thị B", DuongDanAnh = @"C:\code\lập trình quản lý\QuanLyQuanNet\QuanLyQuanNet\New folder\nv2.jpg" } },
            { "nv3", new ThongTinNhanVien { HoTen = "Lê Văn C", DuongDanAnh = @"C:\code\lập trình quản lý\QuanLyQuanNet\QuanLyQuanNet\New folder\nv3.jpg" } }
        };

        public GiaoCa()
        {
            InitializeComponent();

            // Ép cắm dây điện cho các nút và ComboBox
            cmbGiaoCa.SelectedIndexChanged += cmbGiaoCa_SelectedIndexChanged;
            cmbNhanCa.SelectedIndexChanged += cmbNhanCa_SelectedIndexChanged;
            btnXacNhan.Click += btnXacNhan_Click;
        }

        public void ThietLapThongTin(string nhanVienTruc, DateTime thoiGianVao)
        {
            // Cắt bỏ khoảng trắng dư thừa lúc truyền vào cho chắc ăn
            tenNhanVienDangTruc = nhanVienTruc != null ? nhanVienTruc.Trim() : "";
            gioVaoCa = thoiGianVao;

            LoadThongTinCa();
        }

        private void LoadThongTinCa()
        {
            cmbGiaoCa.Items.Clear();
            string userAutoSelect = "";

            // Nạp danh sách (admin, nv1, nv2...) vào cmbGiaoCa
            foreach (var kvp in dsTaiKhoan)
            {
                cmbGiaoCa.Items.Add(kvp.Key);

                // SO SÁNH TUYỆT ĐỐI: Bỏ qua khoảng trắng 2 đầu để tìm đúng người
                if (kvp.Value.HoTen.Trim() == tenNhanVienDangTruc || kvp.Key.Trim() == tenNhanVienDangTruc)
                {
                    userAutoSelect = kvp.Key;
                }
            }

            // BẮT BUỘC NHẢY ĐÚNG TÊN NGƯỜI ĐANG TRỰC VÀ KHÓA LẠI
            if (!string.IsNullOrEmpty(userAutoSelect))
            {
                cmbGiaoCa.SelectedItem = userAutoSelect; // Dùng SelectedItem thay vì Text
                cmbGiaoCa.Enabled = false; // Khóa chết ComboBox này, cấm đổi người
            }
            else if (cmbGiaoCa.Items.Count > 0)
            {
                cmbGiaoCa.SelectedIndex = 0;
                cmbGiaoCa.Enabled = false;
            }

            // Chạy hàm tính tiền
            TinhToanDoanhThuHeThong();
        }

        // ==============================================================
        // HÀM LOAD ẢNH TỪ Ổ ĐĨA BAO MƯỢT
        // ==============================================================
        private void HienThiAnh(PictureBox pic, string duongDan)
        {
            if (pic == null) return;
            try
            {
                if (File.Exists(duongDan))
                {
                    pic.Image = Image.FromFile(duongDan);
                    pic.SizeMode = PictureBoxSizeMode.Zoom; // Ép ảnh tự thu nhỏ vừa khung
                }
                else
                {
                    pic.Image = null;
                }
            }
            catch
            {
                pic.Image = null;
            }
        }

        // ==============================================================
        // LOGIC CHỌN NGƯỜI GIAO CA -> ĐỔI TÊN & ẢNH
        // ==============================================================
        private void cmbGiaoCa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGiaoCa.SelectedItem == null) return;
            string nguoiGiao = cmbGiaoCa.SelectedItem.ToString();

            if (dsTaiKhoan.ContainsKey(nguoiGiao))
            {
                // Đổ tên và hình lên giao diện
                txtGiaoCa.Text = dsTaiKhoan[nguoiGiao].HoTen;
                HienThiAnh(picGiaoCa, dsTaiKhoan[nguoiGiao].DuongDanAnh);
            }

            // Lọc bỏ Người Giao Ca khỏi danh sách Người Nhận Ca
            string nguoiNhanHienTai = cmbNhanCa.Text;
            cmbNhanCa.Items.Clear();
            foreach (var nv in dsTaiKhoan.Keys)
            {
                if (nv != nguoiGiao)
                {
                    cmbNhanCa.Items.Add(nv);
                }
            }

            if (cmbNhanCa.Items.Contains(nguoiNhanHienTai))
                cmbNhanCa.SelectedItem = nguoiNhanHienTai;
            else if (cmbNhanCa.Items.Count > 0)
                cmbNhanCa.SelectedIndex = 0;
        }

        // ==============================================================
        // LOGIC CHỌN NGƯỜI NHẬN CA -> ĐỔI TÊN & ẢNH
        // ==============================================================
        private void cmbNhanCa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhanCa.SelectedItem == null) return;
            string nguoiNhan = cmbNhanCa.SelectedItem.ToString();

            if (dsTaiKhoan.ContainsKey(nguoiNhan))
            {
                txtNhanCa.Text = dsTaiKhoan[nguoiNhan].HoTen;
                HienThiAnh(picNhanCa, dsTaiKhoan[nguoiNhan].DuongDanAnh);
            }
        }

        // ==============================================================
        // TÍNH TOÁN DOANH THU CA TỪ DATABASE 
        // ==============================================================
        private void TinhToanDoanhThuHeThong()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    string sql = "SELECT ThanhToan, Nguon FROM GiaoDich WHERE ThoiGian >= @VaoCa";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@VaoCa", gioVaoCa);

                    double tongDoanhThu = 0;
                    double tongChuyenKhoan = 0;
                    double tongTienMat = 0;

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            double tien = Convert.ToDouble(r["ThanhToan"]);
                            string nguon = r["Nguon"].ToString();

                            tongDoanhThu += tien;

                            if (nguon.Contains("Chuyển khoản") || nguon.Contains("QR"))
                                tongChuyenKhoan += tien;
                            else
                                tongTienMat += tien;
                        }
                    }

                    txtDTTC.Text = tongDoanhThu.ToString("#,##0");
                    txtChuyenKhoan.Text = tongChuyenKhoan.ToString("#,##0");
                    txtTienMat.Text = tongTienMat.ToString("#,##0");
                }
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi ngầm
            }
        }

        // ==============================================================
        // SỰ KIỆN BẤM NÚT XÁC NHẬN GIAO CA 
        // ==============================================================
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNhanCa.Text))
            {
                MessageBox.Show("Vui lòng chọn người nhận ca!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // GOM CÁC CHECKBOX
            CheckBox[] listCB = { cb1, cb2, cb3, cb4, cb5, cb6, cb7 };
            string trangThaiKiemKe = "";

            foreach (CheckBox cb in listCB)
            {
                if (cb != null && cb.Checked) trangThaiKiemKe += $"[x] {cb.Text}. ";
            }

            string ghiChu = richTextBox1.Text.Trim();

            DialogResult dr = MessageBox.Show($"Xác nhận bàn giao ca từ [{txtGiaoCa.Text}] sang [{txtNhanCa.Text}]?\n\n- Tiền mặt bàn giao: {txtTienMat.Text} VNĐ",
                                              "Xác nhận bàn giao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                    {
                        conn.Open();
                        string sql = @"INSERT INTO LichSuGiaoCa 
                                       (NhanVienGiao, NhanVienNhan, ThoiGianVaoCa, DoanhThuCa, TongTienMat, TrangThaiKiemKe, GhiChuSuCo) 
                                       VALUES (@Giao, @Nhan, @VaoCa, @DoanhThu, @TienMat, @KiemKe, @GhiChu)";

                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Giao", txtGiaoCa.Text); // Lưu luôn họ tên cho đẹp sổ sách
                        cmd.Parameters.AddWithValue("@Nhan", txtNhanCa.Text);
                        cmd.Parameters.AddWithValue("@VaoCa", gioVaoCa);
                        cmd.Parameters.AddWithValue("@DoanhThu", string.IsNullOrEmpty(txtDTTC.Text) ? 0 : double.Parse(txtDTTC.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@TienMat", string.IsNullOrEmpty(txtTienMat.Text) ? 0 : double.Parse(txtTienMat.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@KiemKe", trangThaiKiemKe);
                        cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Bàn giao ca thành công!\nHệ thống sẽ tự động khởi động lại để đổi ca.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu lịch sử giao ca: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e) { }
    }
}