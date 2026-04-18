using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace QuanLyQuanNet
{
    public partial class ucKhachHang : UserControl
    {
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";
        private bool isThemMoi = false;

        // CÔNG CỤ TẠO DẤU CHẤM ĐỎ BÁO LỖI
        private ErrorProvider err = new ErrorProvider();

        public ucKhachHang()
        {
            InitializeComponent();

            // Kích hoạt cảm biến real-time báo lỗi màu đỏ
            CaiDatCamBienLoi();

            // Kích hoạt hàm tự sinh Mã KH khi gõ Ngày Sinh
            txtNgaySinh.TextChanged += (s, e) => SinhMaKhachHangTuDong();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            cmbPCThườngDung.Items.Clear();
            cmbPCThườngDung.Items.Add("Chưa rõ");
            for (int i = 1; i <= 30; i++) cmbPCThườngDung.Items.Add("PC " + i.ToString("00"));

            if (cmbTrangThai.Items.Count == 0)
            {
                cmbTrangThai.Items.Add("Hoạt động");
                cmbTrangThai.Items.Add("Khóa");
            }

            DatTrangThaiMacDinh();
            LoadDanhSachHoiVien();
        }

        // ==========================================
        // THUẬT TOÁN TỰ ĐỘNG SINH MÃ KHÁCH HÀNG (BẢN THÔNG MINH)
        // ==========================================
        private void SinhMaKhachHangTuDong()
        {
            if (isThemMoi)
            {
                string cccd = txtCCCD.Text.Trim();
                // Tự động lột bỏ dấu gạch chéo (nếu có) để đếm số lượng cho chuẩn
                string ngaySinh = txtNgaySinh.Text.Trim().Replace("/", "");

                // CCCD phải đủ 12 số VÀ Ngày sinh phải đủ 8 số (vd: 24032004)
                if (cccd.Length == 12 && ngaySinh.Length == 8)
                {
                    string bonSoCuoiCCCD = cccd.Substring(cccd.Length - 4);
                    string haiSoNgaySinh = ngaySinh.Substring(0, 2);
                    txtMaKH.Text = bonSoCuoiCCCD + haiSoNgaySinh;
                }
                else
                {
                    txtMaKH.Clear();
                }
            }
        }

        private void btnHienThiTT_Click(object sender, EventArgs e)
        {
            string maKHTimKiem = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKHTimKiem))
            {
                MessageBox.Show("Vui lòng nhập Mã Khách Hàng để tìm kiếm!", "Nhắc nhở");
                txtMaKH.Focus();
                return;
            }
            LoadThongTinKhachHang(maKHTimKiem);
        }

        private void LoadThongTinKhachHang(string maKH)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    string sql = "SELECT * FROM KhachHang WHERE MaKH = @MaKH";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtUsername.Text = reader["Username"].ToString();
                            txtMaKH.Text = reader["MaKH"].ToString();
                            txtTenKH.Text = reader["TenKH"].ToString();
                            txtSDT.Text = reader["SDT"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtCCCD.Text = reader["CCCD"].ToString();
                            txtDiaChi.Text = reader["DiaChi"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            txtSoDu.Text = Convert.ToDouble(reader["SoDu"]).ToString("#,##0");
                            txtTongGio.Text = reader["TongGioChoi"].ToString();
                            txtTongTien.Text = Convert.ToDouble(reader["TongTienNap"]).ToString("#,##0");

                            if (reader["LanDangNhapCuoi"] != DBNull.Value)
                                txtLanCuoi.Text = Convert.ToDateTime(reader["LanDangNhapCuoi"]).ToString("dd/MM/yyyy HH:mm");
                            else txtLanCuoi.Text = "Chưa đăng nhập";

                            if (reader["NgaySinh"] != DBNull.Value)
                                txtNgaySinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");

                            if (reader["PCThuongDung"] != DBNull.Value)
                                cmbPCThườngDung.Text = reader["PCThuongDung"].ToString();

                            cmbTrangThai.Text = reader["TrangThai"].ToString();

                            string gioiTinh = reader["GioiTinh"].ToString();
                            if (gioiTinh == "Nam") radNam.Checked = true;
                            else if (gioiTinh == "Nữ") radNu.Checked = true;
                            else radKhac.Checked = true;

                            if (reader["LoaiTaiKhoan"].ToString() == "VIP") radVip.Checked = true;
                            else radThuong.Checked = true;

                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy Mã Khách Hàng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DatTrangThaiMacDinh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy thông tin: " + ex.Message);
            }
        }

        private void LoadDanhSachHoiVien()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    string sql = @"SELECT 
                                    MaKH AS 'Mã KH',
                                    Username AS 'Tài Khoản', 
                                    TenKH AS 'Tên Khách Hàng', 
                                    SDT AS 'Số Điện Thoại', 
                                    LoaiTaiKhoan AS 'Loại', 
                                    SoDu AS 'Số Dư', 
                                    TrangThai AS 'Trạng Thái' 
                                   FROM KhachHang 
                                   ORDER BY TenKH ASC";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLichSuGD.AutoGenerateColumns = true;
                    dgvLichSuGD.DataSource = null;
                    dgvLichSuGD.Rows.Clear(); // Xóa sạch rác cũ
                    dgvLichSuGD.DataSource = dt;

                    if (dgvLichSuGD.Columns.Count > 0)
                        dgvLichSuGD.Columns["Số Dư"].DefaultCellStyle.Format = "#,##0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void dgvLichSuGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedMaKH = dgvLichSuGD.Rows[e.RowIndex].Cells["Mã KH"].Value.ToString();
                txtMaKH.Text = selectedMaKH;
                LoadThongTinKhachHang(selectedMaKH);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            MoKhoaNhapLieu(true);
            XoaTrangForm();
            txtCCCD.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng click chọn khách hàng để sửa!", "Nhắc nhở");
                return;
            }
            isThemMoi = false;
            MoKhoaNhapLieu(true);
            txtMaKH.Enabled = false;
            txtUsername.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH)) return;

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn KHÓA tài khoản '{maKH}' không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                ThucThiSQL("UPDATE KhachHang SET TrangThai = 'Khóa' WHERE MaKH = @MaKH", maKH);
                MessageBox.Show("Đã khóa tài khoản thành công!", "Thông báo");
                DatTrangThaiMacDinh();
                XoaTrangForm();
                LoadDanhSachHoiVien();
            }
        }

        // ==============================================================
        // SỰ KIỆN NÚT LƯU - ĐÃ ĐỔI TÊN THÀNH guna2Button1 CHUẨN XÁC
        // ==============================================================
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string ngaySinh = txtNgaySinh.Text.Trim();

            // 1. KIỂM TRA ĐẦU VÀO VÀ VĂNG BẢNG BÁO LỖI NẾU SAI
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || pass.Length < 3)
            {
                MessageBox.Show("Tài khoản hoặc Mật khẩu chưa hợp lệ (Tối thiểu 3 ký tự)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Chưa nhập Tên Khách Hàng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cccd.Length != 12)
            {
                MessageBox.Show("Số CCCD bắt buộc phải đủ 12 số!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 số và bắt đầu bằng số 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xử lý Ngày Sinh thông minh (Gõ 24/03/2004 hay 24032004 đều nhận)
            DateTime dateNgaySinh;
            string[] formats = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };
            if (!DateTime.TryParseExact(ngaySinh, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNgaySinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ!\n\nVui lòng nhập kiểu 24/03/2004 hoặc viết liền 24032004", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Hệ thống chưa tạo được Mã KH.\n\nHãy kiểm tra lại CCCD và Ngày Sinh phải gõ đủ số!", "Lỗi logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. LƯU VÀO DATABASE
            string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");
            string loaiTK = radVip.Checked ? "VIP" : "Thường";
            string trangThai = string.IsNullOrEmpty(cmbTrangThai.Text) ? "Hoạt động" : cmbTrangThai.Text;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    if (isThemMoi)
                    {
                        string sqlCheck = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH OR Username = @User";
                        MySqlCommand cmdCheck = new MySqlCommand(sqlCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@MaKH", maKH);
                        cmdCheck.Parameters.AddWithValue("@User", user);
                        if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Mã KH hoặc Username này đã tồn tại!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string sqlInsert = @"INSERT INTO KhachHang (MaKH, Username, Password, TenKH, SDT, Email, CCCD, DiaChi, GioiTinh, NgaySinh, LoaiTaiKhoan, TrangThai, SoDu, TongGioChoi, TongTienNap, PCThuongDung) 
                                             VALUES (@MaKH, @User, @Pass, @Ten, @SDT, @Email, @CCCD, @DiaChi, @GioiTinh, @NgaySinh, @LoaiTK, @TrangThai, 0, 0, 0, @PCThuongDung)";
                        MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);
                        GanThamSo(cmd, maKH, user, gioiTinh, loaiTK, trangThai, dateNgaySinh);
                        cmd.ExecuteNonQuery();

                        if (loaiTK == "VIP") MessageBox.Show($"Tạo tài khoản VIP '{user}' thành công!\n\n🎉 Tặng khách hàng 1 ly Trà Đá miễn phí!", "Quà Tặng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else MessageBox.Show($"Tạo tài khoản '{user}' thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sqlUpdate = @"UPDATE KhachHang 
                                             SET Password=@Pass, TenKH=@Ten, SDT=@SDT, Email=@Email, CCCD=@CCCD, DiaChi=@DiaChi, 
                                                 GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, LoaiTaiKhoan=@LoaiTK, TrangThai=@TrangThai, PCThuongDung=@PCThuongDung 
                                             WHERE MaKH=@MaKH";
                        MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn);
                        GanThamSo(cmd, maKH, user, gioiTinh, loaiTK, trangThai, dateNgaySinh);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                DatTrangThaiMacDinh();

                // Ép DataGridView tải lại data mới nhất
                LoadDanhSachHoiVien();
                dgvLichSuGD.Refresh();
                dgvLichSuGD.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GanThamSo(MySqlCommand cmd, string maKH, string user, string gioiTinh, string loaiTK, string trangThai, DateTime dateNgaySinh)
        {
            cmd.Parameters.AddWithValue("@MaKH", maKH);
            cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@Pass", txtPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@Ten", txtTenKH.Text.Trim());
            cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text.Trim());
            cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
            cmd.Parameters.AddWithValue("@NgaySinh", dateNgaySinh);
            cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
            cmd.Parameters.AddWithValue("@LoaiTK", loaiTK);
            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
            cmd.Parameters.AddWithValue("@PCThuongDung", cmbPCThườngDung.Text);
        }

        private void ThucThiSQL(string sql, string maKH)
        {
            using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaKH", maKH);
                cmd.ExecuteNonQuery();
            }
        }

        private void MoKhoaNhapLieu(bool moKhoa)
        {
            txtMaKH.Enabled = false;
            txtUsername.Enabled = moKhoa;
            txtPassword.Enabled = moKhoa; txtTenKH.Enabled = moKhoa;
            txtSDT.Enabled = moKhoa; txtCCCD.Enabled = moKhoa;
            txtDiaChi.Enabled = moKhoa; txtEmail.Enabled = moKhoa;
            txtNgaySinh.Enabled = moKhoa;
            cmbPCThườngDung.Enabled = moKhoa; cmbTrangThai.Enabled = moKhoa;

            radNam.Enabled = moKhoa; radNu.Enabled = moKhoa; radKhac.Enabled = moKhoa;
            radVip.Enabled = moKhoa; radThuong.Enabled = moKhoa;

            if (radVip.Parent != null) radVip.Parent.Enabled = moKhoa;
            if (radNam.Parent != null) radNam.Parent.Enabled = moKhoa;

            txtSoDu.Enabled = false; txtTongGio.Enabled = false;
            txtTongTien.Enabled = false; txtLanCuoi.Enabled = false;

            // Bật tắt nút bấm (Lưu ý: nút Lưu của ní giờ là guna2Button1)
            guna2Button1.Enabled = moKhoa;
            btnThem.Enabled = !moKhoa;
            btnSua.Enabled = !moKhoa;
            btnXoa.Enabled = !moKhoa;

            err.Clear();
        }

        private void DatTrangThaiMacDinh()
        {
            MoKhoaNhapLieu(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            err.Clear();
        }

        private void XoaTrangForm()
        {
            txtMaKH.Clear(); txtUsername.Clear();
            txtPassword.Clear(); txtTenKH.Clear();
            txtSDT.Clear(); txtCCCD.Clear(); txtDiaChi.Clear(); txtEmail.Clear(); txtNgaySinh.Clear();
            txtSoDu.Text = "0"; txtTongGio.Text = "0"; txtTongTien.Text = "0"; txtLanCuoi.Clear();
            radThuong.Checked = true; radNam.Checked = true;
            if (cmbTrangThai.Items.Count > 0) cmbTrangThai.SelectedIndex = 0;
            if (cmbPCThườngDung.Items.Count > 0) cmbPCThườngDung.SelectedIndex = 0;
        }

        // ==============================================================
        // BỘ CẢM BIẾN BẮT LỖI REAL-TIME 
        // ==============================================================
        private void HienThiLoiGiaoDien(Control txt, string thongBaoLoi)
        {
            if (!string.IsNullOrEmpty(thongBaoLoi))
            {
                err.SetError(txt, thongBaoLoi);
                txt.ForeColor = Color.Red;
            }
            else
            {
                err.SetError(txt, "");
                txt.ForeColor = Color.Black;
            }
        }

        private void CaiDatCamBienLoi()
        {
            txtCCCD.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
            txtSDT.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };

            txtCCCD.TextChanged += (s, e) => { HienThiLoiGiaoDien(txtCCCD, ""); SinhMaKhachHangTuDong(); };
            txtSDT.TextChanged += (s, e) => { HienThiLoiGiaoDien(txtSDT, ""); };
            txtUsername.TextChanged += (s, e) => { HienThiLoiGiaoDien(txtUsername, ""); };
            txtNgaySinh.TextChanged += (s, e) => { HienThiLoiGiaoDien(txtNgaySinh, ""); SinhMaKhachHangTuDong(); };

            txtCCCD.Leave += (s, e) =>
            {
                if (txtCCCD.Text.Length > 0 && txtCCCD.Text.Length < 12)
                    HienThiLoiGiaoDien(txtCCCD, "Số CCCD không hợp lệ! Vui lòng nhập đủ 12 số.");
            };

            txtSDT.Leave += (s, e) =>
            {
                if (txtSDT.Text.Length > 0 && !Regex.IsMatch(txtSDT.Text, @"^0\d{9}$"))
                    HienThiLoiGiaoDien(txtSDT, "Số điện thoại sai! (Phải đủ 10 số và bắt đầu bằng số 0)");
            };

            txtUsername.Leave += (s, e) =>
            {
                if (isThemMoi && string.IsNullOrEmpty(txtUsername.Text))
                    HienThiLoiGiaoDien(txtUsername, "Username không được để trống!");
            };

            txtNgaySinh.Leave += (s, e) =>
            {
                if (txtNgaySinh.Text.Length > 0)
                {
                    DateTime temp;
                    string[] formats = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };
                    if (!DateTime.TryParseExact(txtNgaySinh.Text.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                        HienThiLoiGiaoDien(txtNgaySinh, "Nhập sai định dạng ngày! (Ví dụ đúng: 02/10/2000 hoặc 02102000)");
                }
            };
        }

        // --- CÁC SỰ KIỆN TRỐNG ---
        private void guna2GroupBox1_Click(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
        private void txtCCCD_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void txtTenKH_TextChanged(object sender, EventArgs e) { }
        private void txtMaKH_TextChanged(object sender, EventArgs e) { }
        private void txtNgaySinh_TextChanged(object sender, EventArgs e) { }
        private void guna2GroupBox2_Click(object sender, EventArgs e) { }
        private void txtSoDu_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void txtTongGio_TextChanged(object sender, EventArgs e) { }
        private void txtLanCuoi_TextChanged(object sender, EventArgs e) { }
        private void cmbPCThườngDung_SelectedIndexChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }

        // HÀM LƯU CŨ BỊ DƯ ĐÃ ĐƯỢC XÓA BỎ
    }
}