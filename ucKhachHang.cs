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
        private ErrorProvider err = new ErrorProvider();

        public ucKhachHang()
        {
            InitializeComponent();

            CaiDatCamBienLoi();
            txtNgaySinh.TextChanged += (s, e) => SinhMaKhachHangTuDong();
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            btnHienThiTT.Click += btnHienThiTT_Click;
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

        private void btnHienThiTT_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa)) tuKhoa = txtMaKH.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập Tên, CCCD hoặc Mã KH vào ô Tìm Kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiem.Focus();
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM KhachHang WHERE MaKH = @TuKhoa OR CCCD = @TuKhoa OR TenKH LIKE @TuKhoaLike LIMIT 1";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TuKhoa", tuKhoa);
                    cmd.Parameters.AddWithValue("@TuKhoaLike", "%" + tuKhoa + "%");

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

                            if (reader["NgaySinh"] != DBNull.Value)
                                txtNgaySinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");

                            string gioiTinh = reader["GioiTinh"].ToString();
                            if (gioiTinh == "Nam") radNam.Checked = true;
                            else if (gioiTinh == "Nữ") radNu.Checked = true;
                            else radKhac.Checked = true;

                            txtSoDu.Text = Convert.ToDouble(reader["SoDu"]).ToString("#,##0");
                            txtTongGio.Text = reader["TongGioChoi"].ToString();
                            txtTongTien.Text = Convert.ToDouble(reader["TongTienNap"]).ToString("#,##0");

                            if (reader["LoaiTaiKhoan"].ToString() == "VIP") radVip.Checked = true;
                            else radThuong.Checked = true;

                            if (reader["LanDangNhapCuoi"] != DBNull.Value)
                                txtLanCuoi.Text = Convert.ToDateTime(reader["LanDangNhapCuoi"]).ToString("dd/MM/yyyy HH:mm");
                            else txtLanCuoi.Text = "Chưa đăng nhập";

                            if (reader["PCThuongDung"] != DBNull.Value)
                                cmbPCThườngDung.Text = reader["PCThuongDung"].ToString();

                            cmbTrangThai.Text = reader["TrangThai"].ToString();

                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
                        }
                        else MessageBox.Show("Không tìm thấy thông tin khách hàng khớp với từ khóa trên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị thông tin: " + ex.Message); }
        }

        // 👉 ĐÃ FIX: XÓA CACHE BẢNG TRƯỚC KHI NẠP LẠI (TÌM KIẾM)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    string sql = @"SELECT MaKH AS 'Mã KH', Username AS 'Tài Khoản', TenKH AS 'Tên Khách Hàng', SDT AS 'Số Điện Thoại', LoaiTaiKhoan AS 'Loại', SoDu AS 'Số Dư', TrangThai AS 'Trạng Thái' 
                                   FROM KhachHang WHERE TenKH LIKE @TuKhoa OR MaKH LIKE @TuKhoa OR CCCD LIKE @TuKhoa ORDER BY TenKH ASC";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLichSuGD.DataSource = null; // Ép xóa cache cũ
                    dgvLichSuGD.DataSource = dt;   // Nạp cái mới

                    if (dgvLichSuGD.Columns.Count > 0 && dgvLichSuGD.Columns.Contains("Số Dư"))
                        dgvLichSuGD.Columns["Số Dư"].DefaultCellStyle.Format = "#,##0 VNĐ";
                }
            }
            catch { }
        }

        // 👉 ĐÃ FIX: XÓA CACHE BẢNG TRƯỚC KHI NẠP LẠI (LOAD MẶC ĐỊNH)
        private void LoadDanhSachHoiVien()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    string sql = @"SELECT MaKH AS 'Mã KH', Username AS 'Tài Khoản', TenKH AS 'Tên Khách Hàng', SDT AS 'Số Điện Thoại', LoaiTaiKhoan AS 'Loại', SoDu AS 'Số Dư', TrangThai AS 'Trạng Thái' 
                                   FROM KhachHang ORDER BY TenKH ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLichSuGD.DataSource = null; // Ép xóa cache cũ
                    dgvLichSuGD.DataSource = dt;   // Nạp cái mới

                    if (dgvLichSuGD.Columns.Count > 0 && dgvLichSuGD.Columns.Contains("Số Dư"))
                        dgvLichSuGD.Columns["Số Dư"].DefaultCellStyle.Format = "#,##0 VNĐ";
                }
            }
            catch { }
        }

        private void dgvLichSuGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maKH = dgvLichSuGD.Rows[e.RowIndex].Cells["Mã KH"].Value.ToString();
                txtMaKH.Text = maKH;
                btnHienThiTT_Click(null, null);
            }
        }

        private void SinhMaKhachHangTuDong()
        {
            if (isThemMoi)
            {
                string cccd = txtCCCD.Text.Trim();
                string ngaySinh = txtNgaySinh.Text.Trim().Replace("/", "");
                if (cccd.Length == 12 && ngaySinh.Length == 8)
                    txtMaKH.Text = cccd.Substring(8) + ngaySinh.Substring(0, 2);
                else txtMaKH.Clear();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThemMoi = true;
            MoKhoaNhapLieu(true);
            XoaTrangForm();
            txtSoDu.ReadOnly = false;
            txtCCCD.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;

            isThemMoi = false;
            MoKhoaNhapLieu(true);

            txtMaKH.Enabled = false;
            txtUsername.Enabled = false;
            txtSoDu.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            if (string.IsNullOrEmpty(maKH)) return;
            if (MessageBox.Show($"Khóa tài khoản {maKH}?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE KhachHang SET TrangThai = 'Khóa' WHERE MaKH = @MaKH", conn);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.ExecuteNonQuery();
                }
                LoadDanhSachHoiVien();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string ngaySinh = txtNgaySinh.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || pass.Length < 3)
            {
                MessageBox.Show("Tài khoản hoặc Mật khẩu chưa hợp lệ (Tối thiểu 3 ký tự)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (string.IsNullOrEmpty(ten) || cccd.Length != 12 || !Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Vui lòng điền đúng Tên, Số điện thoại (10 số) và CCCD (12 số)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            DateTime dateNgaySinh;
            string[] formats = { "dd/MM/yyyy", "ddMMyyyy", "d/M/yyyy" };
            if (!DateTime.TryParseExact(ngaySinh, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateNgaySinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            string gioiTinh = radNam.Checked ? "Nam" : (radNu.Checked ? "Nữ" : "Khác");
            string loaiTK = radVip.Checked ? "VIP" : "Thường";
            string trangThai = string.IsNullOrEmpty(cmbTrangThai.Text) ? "Hoạt động" : cmbTrangThai.Text;

            double soDuCapNhat = 0;
            if (!string.IsNullOrEmpty(txtSoDu.Text))
            {
                string tien = txtSoDu.Text.Replace(",", "").Replace(".", "").Replace("VNĐ", "").Trim();
                double.TryParse(tien, out soDuCapNhat);
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    if (isThemMoi)
                    {
                        string sqlCheck = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH OR Username = @User";
                        MySqlCommand cmdCheck = new MySqlCommand(sqlCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@MaKH", maKH); cmdCheck.Parameters.AddWithValue("@User", user);
                        if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
                        {
                            MessageBox.Show("Mã KH hoặc Username này đã tồn tại!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }

                        string sqlInsert = @"INSERT INTO KhachHang (MaKH, Username, Password, TenKH, SDT, Email, CCCD, DiaChi, GioiTinh, NgaySinh, LoaiTaiKhoan, TrangThai, SoDu, TongGioChoi, TongTienNap, PCThuongDung) 
                                             VALUES (@MaKH, @User, @Pass, @Ten, @SDT, @Email, @CCCD, @DiaChi, @GioiTinh, @NgaySinh, @LoaiTK, @TrangThai, @SoDuCapNhat, 0, @SoDuCapNhat, @PCThuongDung)";
                        MySqlCommand cmd = new MySqlCommand(sqlInsert, conn);
                        GanThamSo(cmd, maKH, user, pass, ten, sdt, txtEmail.Text.Trim(), cccd, txtDiaChi.Text.Trim(), gioiTinh, loaiTK, trangThai, dateNgaySinh);
                        cmd.Parameters.AddWithValue("@SoDuCapNhat", soDuCapNhat);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Tạo tài khoản '{user}' thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sqlUpdate = @"UPDATE KhachHang 
                                             SET Password=@Pass, TenKH=@Ten, SDT=@SDT, Email=@Email, CCCD=@CCCD, DiaChi=@DiaChi, 
                                                 GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, LoaiTaiKhoan=@LoaiTK, TrangThai=@TrangThai, 
                                                 PCThuongDung=@PCThuongDung, SoDu=@SoDuCapNhat 
                                             WHERE MaKH=@MaKH";
                        MySqlCommand cmd = new MySqlCommand(sqlUpdate, conn);
                        GanThamSo(cmd, maKH, user, pass, ten, sdt, txtEmail.Text.Trim(), cccd, txtDiaChi.Text.Trim(), gioiTinh, loaiTK, trangThai, dateNgaySinh);
                        cmd.Parameters.AddWithValue("@SoDuCapNhat", soDuCapNhat);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật thông tin (và Số Dư) thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // 👉 ĐÃ FIX: Tự động tải lại bảng mượt mà
                DatTrangThaiMacDinh();

                // Nếu đang tìm kiếm thì ép nó tìm lại đúng chữ đó để cập nhật bảng
                if (!string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    txtTimKiem_TextChanged(null, null);
                }
                else
                {
                    LoadDanhSachHoiVien();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
        }

        private void GanThamSo(MySqlCommand cmd, string maKH, string user, string pass, string ten, string sdt, string email, string cccd, string diaChi, string gioiTinh, string loaiTK, string trangThai, DateTime dateNgaySinh)
        {
            cmd.Parameters.AddWithValue("@MaKH", maKH); cmd.Parameters.AddWithValue("@User", user);
            cmd.Parameters.AddWithValue("@Pass", pass); cmd.Parameters.AddWithValue("@Ten", ten);
            cmd.Parameters.AddWithValue("@SDT", sdt); cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@CCCD", cccd); cmd.Parameters.AddWithValue("@DiaChi", diaChi);
            cmd.Parameters.AddWithValue("@NgaySinh", dateNgaySinh); cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
            cmd.Parameters.AddWithValue("@LoaiTK", loaiTK); cmd.Parameters.AddWithValue("@TrangThai", trangThai);
            cmd.Parameters.AddWithValue("@PCThuongDung", cmbPCThườngDung.Text);
        }

        private void MoKhoaNhapLieu(bool mo)
        {
            txtUsername.Enabled = mo; txtPassword.Enabled = mo; txtTenKH.Enabled = mo;
            txtSDT.Enabled = mo; txtCCCD.Enabled = mo; txtDiaChi.Enabled = mo;
            txtEmail.Enabled = mo; txtNgaySinh.Enabled = mo; cmbTrangThai.Enabled = mo;
            radNam.Enabled = mo; radNu.Enabled = mo; radVip.Enabled = mo; radThuong.Enabled = mo;
            guna2Button1.Enabled = mo; btnThem.Enabled = !mo; btnSua.Enabled = !mo; btnXoa.Enabled = !mo;
        }

        private void DatTrangThaiMacDinh()
        {
            MoKhoaNhapLieu(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtSoDu.ReadOnly = true;
            err.Clear();
        }

        private void XoaTrangForm()
        {
            txtMaKH.Clear(); txtUsername.Clear(); txtPassword.Clear(); txtTenKH.Clear();
            txtSDT.Clear(); txtCCCD.Clear(); txtDiaChi.Clear(); txtEmail.Clear(); txtNgaySinh.Clear();
            txtSoDu.Text = "0";
        }

        private void CaiDatCamBienLoi()
        {
            txtCCCD.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
            txtSDT.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
            txtSoDu.KeyPress += (s, e) => { if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true; };
        }

        // Sự kiện rỗng tránh lỗi
        private void guna2GroupBox1_Click(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtDiaChi_TextChanged(object sender, EventArgs e) { }
        private void txtCCCD_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void txtTenKH_TextChanged(object sender, EventArgs e) { }
        private void txtMaKH_TextChanged(object sender, EventArgs e) { }
        private void guna2GroupBox2_Click(object sender, EventArgs e) { }
        private void txtSoDu_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtTongTien_TextChanged(object sender, EventArgs e) { }
        private void txtTongGio_TextChanged(object sender, EventArgs e) { }
        private void txtLanCuoi_TextChanged(object sender, EventArgs e) { }
        private void cmbPCThườngDung_SelectedIndexChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}