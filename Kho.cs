using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyQuanNet
{
    public partial class Kho : UserControl
    {
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";
        private DataService db = new DataService();

        public Kho()
        {
            InitializeComponent();
            this.Load += Kho_Load;

            // ==========================================
            // MÓC DÂY ĐIỆN CHO CÁC NÚT BẤM (SỰ KIỆN)
            // ==========================================
            // 1. Lọc nhanh danh mục
            btnTatCa.Click += (s, e) => LocNhanhDanhMuc("");
            btnPhanCung.Click += (s, e) => LocNhanhDanhMuc("Phần Cứng");
            btnPhuKien.Click += (s, e) => LocNhanhDanhMuc("Phụ Kiện");
            btnManHinh.Click += (s, e) => LocNhanhDanhMuc("Màn Hình");
            btnFood.Click += (s, e) => LocNhanhDanhMuc("Food");
            btnKhac.Click += (s, e) => LocNhanhDanhMuc("Khác");
            btnLoc.Click += btnLoc_Click;

            // 2. Chuyển đổi Panel & Hủy Phiếu
            btnNhapHang.Click += btnNhapHang_Click;
            btnXuatKho.Click += btnXuatKho_Click;
            btnHuyPhieuNhap.Click += btnHuyPhieuNhap_Click;
            btnHuyPhieuXuat.Click += btnHuyPhieuXuat_Click;

            // 3. Lưu phiếu
            btnLuuPhieuNhap.Click += btnLuuPhieuNhap_Click;
            btnLuuPhieuXuat.Click += btnLuuPhieuXuat_Click;

            // 4. Bắt sự kiện Combobox chọn hàng
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            cmbHangXuat.SelectedIndexChanged += cmbHangXuat_SelectedIndexChanged;

            // 5. Bắt sự kiện gõ số lượng/giá để tự tính tổng tiền
            dgvNhapHang.CellValueChanged += Dgv_CellValueChanged;
            dgvXuatHang.CellValueChanged += Dgv_CellValueChanged;
            dgvNhapHang.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
            dgvXuatHang.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;

            // 6. Sự kiện click xóa hàng trong bảng thống kê
            dgvThongKe.CellContentClick += dgvThongKe_CellContentClick;
        }

        private void Kho_Load(object sender, EventArgs e)
        {
            guna2Panel1.Dock = DockStyle.Fill;
            dgvThongKe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            CaiDatCotBang();
            LoadDuLieuKho();
            LoadComboboxes();
            CaiDatBangNhapXuat();

            guna2GroupBox2.Visible = true;
            btnNhapHang_Click(null, null); // Mặc định mở tab Nhập hàng trước
        }

        #region 1. CÀI ĐẶT BẢNG VÀ GIAO DIỆN
        private void CaiDatBangNhapXuat()
        {
            dgvNhapHang.ReadOnly = false;
            dgvXuatHang.ReadOnly = false;

            // BẢNG NHẬP: Bật chế độ dòng trắng để nhập tay đồ mới
            dgvNhapHang.AllowUserToAddRows = true;
            // BẢNG XUẤT: Khóa lại, không cho tự gõ hàng ảo để xuất
            dgvXuatHang.AllowUserToAddRows = false;

            // Mở khóa TOÀN BỘ CÁC CỘT cho bảng nhập
            foreach (DataGridViewColumn col in dgvNhapHang.Columns)
            {
                col.ReadOnly = false;
            }

            // Bảng xuất thì chỉ mở cột Số Lượng
            foreach (DataGridViewColumn col in dgvXuatHang.Columns)
            {
                col.ReadOnly = !col.HeaderText.Contains("Số Lượng");
            }
        }

        private void CaiDatCotBang()
        {
            dgvThongKe.AutoGenerateColumns = false;
            colMaHang.DataPropertyName = "MaHang";
            colTenHang.DataPropertyName = "TenHang";
            colDanhMuc.DataPropertyName = "DanhMuc";
            colNhaCungCap.DataPropertyName = "NhaCungCap";
            colTonKho.DataPropertyName = "TonKho";
            colDVT.DataPropertyName = "DVT";
            colGiaNhap.DataPropertyName = "GiaNhap";
            colGiaNhap.DefaultCellStyle.Format = "#,##0 VNĐ";
            colTrangThai.DataPropertyName = "TrangThai";

            if (dgvThongKe.Columns.Contains("colThaoTac")) dgvThongKe.Columns.Remove("colThaoTac");
            if (!dgvThongKe.Columns.Contains("btnXoaHang"))
            {
                DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn();
                btnXoa.Name = "btnXoaHang";
                btnXoa.HeaderText = "Thao Tác";
                btnXoa.Text = "Xóa";
                btnXoa.UseColumnTextForButtonValue = true;
                btnXoa.FlatStyle = FlatStyle.Flat;

                // Nhuộm chữ Xóa màu xám chìm
                btnXoa.DefaultCellStyle.ForeColor = Color.DarkGray;
                btnXoa.DefaultCellStyle.SelectionForeColor = Color.DarkGray;

                dgvThongKe.Columns.Add(btnXoa);
            }
        }

        private void LoadComboboxes()
        {
            try
            {
                DataTable dtNCC = db.GetTable("SELECT DISTINCT NhaCungCap FROM KhoHang");
                cmbNhaCungCap.Items.Clear(); cmbNhaCungCap.Items.Add("Tất Cả");
                cmbChonNCC.Items.Clear(); cmbChonNCC.Items.Add("Chọn nhà cung cấp...");
                foreach (DataRow r in dtNCC.Rows)
                {
                    cmbNhaCungCap.Items.Add(r["NhaCungCap"].ToString());
                    cmbChonNCC.Items.Add(r["NhaCungCap"].ToString());
                }
                if (cmbNhaCungCap.Items.Count > 0) cmbNhaCungCap.SelectedIndex = 0;
                cmbChonNCC.SelectedIndex = 0;

                DataTable dtHang = db.GetTable("SELECT MaHang, TenHang FROM KhoHang");
                comboBox1.Items.Clear(); comboBox1.Items.Add("Chọn mặt hàng...");
                cmbHangXuat.Items.Clear(); cmbHangXuat.Items.Add("Chọn mặt hàng...");
                foreach (DataRow r in dtHang.Rows)
                {
                    string item = $"{r["MaHang"]} - {r["TenHang"]}";
                    comboBox1.Items.Add(item);
                    cmbHangXuat.Items.Add(item);
                }
                comboBox1.SelectedIndex = 0; cmbHangXuat.SelectedIndex = 0;
            }
            catch { }

            cmbXuatHang.Items.Clear(); cmbXuatHang.Items.Add("Chọn lý do...");
            cmbXuatHang.Items.AddRange(new string[] { "Bán lẻ", "Sử dụng nội bộ", "Hàng lỗi/Hủy", "Trả hàng NCC" });
            cmbXuatHang.SelectedIndex = 0;

            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.AddRange(new string[] { "Tất Cả", "Còn Hàng", "Sắp Hết", "Hết Hàng" });
            guna2ComboBox3.SelectedIndex = 0;
        }
        #endregion

        #region 2. TÍNH TOÁN & CẬP NHẬT THỜI GIAN THỰC (REAL-TIME)
        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.IsCurrentCellDirty) dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv.Columns[e.ColumnIndex].HeaderText.Contains("Số Lượng") || dgv.Columns[e.ColumnIndex].HeaderText.Contains("Giá"))
                {
                    TinhTongTien(dgv);
                }
            }
        }

        private void TinhTongTien(DataGridView dgv)
        {
            double tongTien = 0;
            int colSL = -1, colGia = -1;

            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].HeaderText.Contains("Số Lượng")) colSL = i;
                if (dgv.Columns[i].HeaderText.Contains("Giá")) colGia = i;
            }

            if (colSL != -1 && colGia != -1)
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trắng lúc tính tiền
                    double gia = 0; int sl = 0;
                    double.TryParse(row.Cells[colGia].Value?.ToString(), out gia);
                    int.TryParse(row.Cells[colSL].Value?.ToString(), out sl);
                    tongTien += (gia * sl);
                }
            }

            if (dgv == dgvNhapHang) txtTongThanhToanNhaphang.Text = tongTien.ToString("#,##0") + " VNĐ";
            else if (dgv == dgvXuatHang) txtTongThanhToanXuatHang.Text = tongTien.ToString("#,##0") + " VNĐ";
        }

        private void ThemHangVaoBang(string maHang, DataGridView dgv)
        {
            int colSL = -1;
            for (int i = 0; i < dgv.Columns.Count; i++)
                if (dgv.Columns[i].HeaderText.Contains("Số Lượng")) colSL = i;

            // Kiểm tra xem mặt hàng đã có trong bảng chưa, nếu có thì cộng dồn số lượng
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                int colMa = -1;
                for (int i = 0; i < dgv.Columns.Count; i++) if (dgv.Columns[i].HeaderText.Contains("Mã")) colMa = i;

                if (colMa != -1 && row.Cells[colMa].Value != null && row.Cells[colMa].Value.ToString() == maHang)
                {
                    int slHienTai = 0;
                    int.TryParse(row.Cells[colSL].Value?.ToString(), out slHienTai);
                    row.Cells[colSL].Value = slHienTai + 1;
                    TinhTongTien(dgv);
                    return;
                }
            }

            // Nếu chưa có thì lấy từ DB chèn vào dòng mới
            try
            {
                DataTable dt = db.GetTable($"SELECT * FROM KhoHang WHERE MaHang = '{maHang}'");
                if (dt.Rows.Count > 0)
                {
                    DataRow r = dt.Rows[0];
                    int index = dgv.Rows.Add();
                    DataGridViewRow newRow = dgv.Rows[index];

                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        string head = col.HeaderText;
                        if (head.Contains("Mã")) newRow.Cells[col.Index].Value = r["MaHang"];
                        else if (head.Contains("Tên")) newRow.Cells[col.Index].Value = r["TenHang"];
                        else if (head.Contains("Danh Mục")) newRow.Cells[col.Index].Value = r["DanhMuc"];
                        else if (head.Contains("Nhà Cung Cấp")) newRow.Cells[col.Index].Value = r["NhaCungCap"];
                        else if (head.Contains("ĐVT")) newRow.Cells[col.Index].Value = r["DVT"];
                        else if (head.Contains("Giá")) newRow.Cells[col.Index].Value = r["GiaNhap"];
                        else if (head.Contains("Trạng Thái")) newRow.Cells[col.Index].Value = r["TrangThai"];
                        else if (head.Contains("Số Lượng")) newRow.Cells[col.Index].Value = 1;
                    }
                    TinhTongTien(dgv);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm hàng: " + ex.Message); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex <= 0) return;
            string maHang = comboBox1.SelectedItem.ToString().Split('-')[0].Trim();
            try
            {
                DataTable dt = db.GetTable($"SELECT NhaCungCap FROM KhoHang WHERE MaHang = '{maHang}'");
                if (dt.Rows.Count > 0)
                {
                    string ncc = dt.Rows[0]["NhaCungCap"].ToString();
                    if (!string.IsNullOrEmpty(ncc)) cmbChonNCC.Text = ncc;
                }
            }
            catch { }
            ThemHangVaoBang(maHang, dgvNhapHang);
            comboBox1.SelectedIndex = 0;
        }

        private void cmbHangXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangXuat.SelectedIndex <= 0) return;
            string maHang = cmbHangXuat.SelectedItem.ToString().Split('-')[0].Trim();
            ThemHangVaoBang(maHang, dgvXuatHang);
            cmbHangXuat.SelectedIndex = 0;
        }
        #endregion

        #region 3. THAO TÁC CƠ SỞ DỮ LIỆU (LƯU NHẬP/XUẤT, TÌM KIẾM, XÓA)
        private void btnLuuPhieuNhap_Click(object sender, EventArgs e)
        {
            if (dgvNhapHang.Rows.Count == 0 || (dgvNhapHang.Rows.Count == 1 && dgvNhapHang.Rows[0].IsNewRow))
            {
                MessageBox.Show("Phiếu nhập đang trống!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            if (MessageBox.Show("Xác nhận lưu phiếu nhập này vào Kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                    {
                        conn.Open(); int matHangThanhCong = 0;

                        foreach (DataGridViewRow row in dgvNhapHang.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string maHang = "", tenHang = "", danhMuc = "", ncc = "", dvt = "", trangThai = "Còn Hàng";
                            int soLuong = 0; double giaNhap = 0;

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string head = dgvNhapHang.Columns[cell.ColumnIndex].HeaderText;
                                string val = cell.Value?.ToString().Trim() ?? "";

                                if (head.Contains("Mã")) maHang = val;
                                else if (head.Contains("Tên")) tenHang = val;
                                else if (head.Contains("Danh Mục")) danhMuc = val;
                                else if (head.Contains("Nhà Cung Cấp")) ncc = val;
                                else if (head.Contains("ĐVT")) dvt = val;
                                else if (head.Contains("Trạng Thái")) trangThai = string.IsNullOrEmpty(val) ? "Còn Hàng" : val;
                                else if (head.Contains("Số Lượng")) int.TryParse(val, out soLuong);
                                else if (head.Contains("Giá"))
                                {
                                    string cleanVal = val.Replace("VNĐ", "").Replace(",", "").Replace(".", "").Trim();
                                    double.TryParse(cleanVal, out giaNhap);
                                }
                            }

                            if (!string.IsNullOrEmpty(maHang) && soLuong > 0)
                            {
                                MySqlCommand checkCmd = new MySqlCommand("SELECT COUNT(*) FROM KhoHang WHERE MaHang = @Ma", conn);
                                checkCmd.Parameters.AddWithValue("@Ma", maHang);
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (count > 0)
                                {
                                    // CỘNG DỒN TỒN KHO NẾU ĐÃ CÓ
                                    MySqlCommand cmd = new MySqlCommand("UPDATE KhoHang SET TonKho = TonKho + @SL WHERE MaHang = @Ma", conn);
                                    cmd.Parameters.AddWithValue("@SL", soLuong);
                                    cmd.Parameters.AddWithValue("@Ma", maHang);
                                    if (cmd.ExecuteNonQuery() > 0) matHangThanhCong++;
                                }
                                else
                                {
                                    // THÊM MỚI HOÀN TOÀN
                                    if (string.IsNullOrEmpty(ncc) && cmbChonNCC.SelectedIndex > 0) ncc = cmbChonNCC.Text;
                                    if (string.IsNullOrEmpty(tenHang)) tenHang = "Sản phẩm mới";
                                    if (string.IsNullOrEmpty(danhMuc)) danhMuc = "Khác";
                                    if (string.IsNullOrEmpty(dvt)) dvt = "Cái";

                                    string sqlInsert = @"INSERT INTO KhoHang (MaHang, TenHang, DanhMuc, NhaCungCap, TonKho, DVT, GiaNhap, TrangThai) 
                                                         VALUES (@Ma, @Ten, @DM, @NCC, @SL, @DVT, @Gia, @TT)";
                                    MySqlCommand cmdIn = new MySqlCommand(sqlInsert, conn);
                                    cmdIn.Parameters.AddWithValue("@Ma", maHang);
                                    cmdIn.Parameters.AddWithValue("@Ten", tenHang);
                                    cmdIn.Parameters.AddWithValue("@DM", danhMuc);
                                    cmdIn.Parameters.AddWithValue("@NCC", ncc);
                                    cmdIn.Parameters.AddWithValue("@SL", soLuong);
                                    cmdIn.Parameters.AddWithValue("@DVT", dvt);
                                    cmdIn.Parameters.AddWithValue("@Gia", giaNhap);
                                    cmdIn.Parameters.AddWithValue("@TT", trangThai);

                                    if (cmdIn.ExecuteNonQuery() > 0) matHangThanhCong++;
                                }
                            }
                        }

                        if (matHangThanhCong > 0)
                        {
                            MessageBox.Show($"Thành công! Đã nhập {matHangThanhCong} mã hàng (Bao gồm cập nhật cũ & thêm mới).", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnHuyPhieuNhap_Click(null, null);
                            LoadDuLieuKho();
                            LoadComboboxes();
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
            }
        }

        private void btnLuuPhieuXuat_Click(object sender, EventArgs e)
        {
            if (cmbXuatHang.SelectedIndex <= 0) { MessageBox.Show("Vui lòng chọn Lý do xuất!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (dgvXuatHang.Rows.Count == 0) { MessageBox.Show("Phiếu xuất đang trống!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show($"Xác nhận Xuất Kho với lý do [{cmbXuatHang.Text}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                    {
                        conn.Open(); int matHangThanhCong = 0;
                        foreach (DataGridViewRow row in dgvXuatHang.Rows)
                        {
                            if (row.IsNewRow) continue;
                            string maHang = ""; int soLuong = 0;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (dgvXuatHang.Columns[cell.ColumnIndex].HeaderText.Contains("Mã")) maHang = cell.Value?.ToString();
                                if (dgvXuatHang.Columns[cell.ColumnIndex].HeaderText.Contains("Số Lượng")) int.TryParse(cell.Value?.ToString(), out soLuong);
                            }
                            if (!string.IsNullOrEmpty(maHang) && soLuong > 0)
                            {
                                MySqlCommand cmd = new MySqlCommand("UPDATE KhoHang SET TonKho = TonKho - @SL WHERE MaHang = @Ma AND TonKho >= @SL", conn);
                                cmd.Parameters.AddWithValue("@SL", soLuong); cmd.Parameters.AddWithValue("@Ma", maHang);
                                if (cmd.ExecuteNonQuery() > 0) matHangThanhCong++;
                                else MessageBox.Show($"Mã [{maHang}] không đủ tồn kho!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        if (matHangThanhCong > 0)
                        {
                            MessageBox.Show($"Thành công! Đã xuất {matHangThanhCong} mã hàng.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnHuyPhieuXuat_Click(null, null); LoadDuLieuKho(); LoadComboboxes();
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
            }
        }

        private void dgvThongKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvThongKe.Columns[e.ColumnIndex].Name == "btnXoaHang")
            {
                string maHang = dgvThongKe.Rows[e.RowIndex].Cells["colMaHang"].Value?.ToString();
                string tenHang = dgvThongKe.Rows[e.RowIndex].Cells["colTenHang"].Value?.ToString();

                if (MessageBox.Show($"Xóa mặt hàng [{tenHang}] ra khỏi kho?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                        {
                            conn.Open();
                            MySqlCommand cmd = new MySqlCommand("DELETE FROM KhoHang WHERE MaHang = @Ma", conn);
                            cmd.Parameters.AddWithValue("@Ma", maHang); cmd.ExecuteNonQuery();
                        }
                        LoadDuLieuKho(); LoadComboboxes();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                }
            }
        }

        private void LoadDuLieuKho(string dieuKien = "")
        {
            try
            {
                dgvThongKe.DataSource = null; // Xóa cache
                dgvThongKe.DataSource = db.GetTable("SELECT * FROM KhoHang " + dieuKien);
            }
            catch { }
        }

        private void LocNhanhDanhMuc(string danhMuc) => LoadDuLieuKho(string.IsNullOrEmpty(danhMuc) ? "" : $"WHERE DanhMuc = '{danhMuc}'");

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim(); if (tuKhoa == "Tìm kiếm hàng hóa...") tuKhoa = "";
            string dieuKien = $"WHERE (TenHang LIKE '%{tuKhoa}%' OR MaHang LIKE '%{tuKhoa}%') ";
            if (cmbNhaCungCap.Text != "Tất Cả") dieuKien += $"AND NhaCungCap = '{cmbNhaCungCap.Text}' ";
            if (guna2ComboBox3.Text != "Tất Cả") dieuKien += $"AND TrangThai = '{guna2ComboBox3.Text}' ";
            LoadDuLieuKho(dieuKien);
        }
        #endregion

        #region 4. CÁC NÚT ĐIỀU HƯỚNG VÀ BÁO CÁO
        private void btnNhapHang_Click(object sender, EventArgs e) { btnNhapHang.FillColor = Color.DodgerBlue; btnXuatKho.FillColor = Color.FromArgb(64, 64, 64); guna2Panel3.Visible = false; }
        private void btnXuatKho_Click(object sender, EventArgs e) { btnXuatKho.FillColor = Color.Black; btnNhapHang.FillColor = Color.FromArgb(64, 64, 64); guna2Panel3.Visible = true; guna2Panel3.BringToFront(); }
        private void btnHuyPhieuNhap_Click(object sender, EventArgs e) { cmbChonNCC.SelectedIndex = 0; comboBox1.SelectedIndex = 0; dtpNgayNhap.Value = DateTime.Now; txtTongThanhToanNhaphang.Text = "0 VNĐ"; dgvNhapHang.Rows.Clear(); }
        private void btnHuyPhieuXuat_Click(object sender, EventArgs e) { cmbXuatHang.SelectedIndex = 0; cmbHangXuat.SelectedIndex = 0; dtpNgayXuat.Value = DateTime.Now; txtTongThanhToanXuatHang.Text = "0 VNĐ"; dgvXuatHang.Rows.Clear(); }

        // BẬT BÁO CÁO KHO HÀNG
        private void btnInBaoCaoKho_Click(object sender, EventArgs e)
        {
            frmBaoCaoKho f = new frmBaoCaoKho();
            f.ShowDialog();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        #endregion
    }
}