using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace QuanLyQuanNet
{
    public partial class Form1 : Form
    {
        DataService db = new DataService();

        public static Dictionary<int, long> dataDoanhThu = new Dictionary<int, long>();
        public static Dictionary<int, int> dataSoMay = new Dictionary<int, int>();
        public static Dictionary<int, int> dataSoUser = new Dictionary<int, int>();

        // LƯU TIỀN ĂN UỐNG VÀ CỘNG VÀO BẢNG THEO THỜI GIAN THỰC
        public static Dictionary<string, double> tienDichVuMap = new Dictionary<string, double>();

        public static string TaiKhoanHienTai = "";

        ThongKe trangThongKe;
        TrangGiaoDich trangGD;
        private ucKhachHang trangKhachHang;
        private GiaoCa trangGiaoCa;
        private Kho trangKho;

        Form formDangNhap;
        string quyenHan = "";
        string tenNhanVien = "";
        string duongDanAnh = "";

        public Form1(Form frmLogin, string quyen, string hoTen = "", string linkAnh = "")
        {
            InitializeComponent();
            formDangNhap = frmLogin;
            quyenHan = quyen;
            tenNhanVien = string.IsNullOrEmpty(hoTen) ? "Admin" : hoTen;
            duongDanAnh = linkAnh;

            for (int h = 0; h < 24; h++)
            {
                dataDoanhThu[h] = 0;
                dataSoMay[h] = 0;
                dataSoUser[h] = 0;
            }
        }

        private void TuDongSuaLoiDatabase()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;"))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("ALTER TABLE TrangThaiMay ADD COLUMN TienDichVu DOUBLE DEFAULT 0", conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private void KiemTraGhiChuCaTruoc(string tenNV)
        {
            try
            {
                string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    string sql = "SELECT NhanVienGiao, NhanVienNhan, GhiChuSuCo, TrangThaiKiemKe FROM LichSuGiaoCa ORDER BY ID DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            string nguoiGiao = r["NhanVienGiao"].ToString();
                            string nguoiNhan = r["NhanVienNhan"].ToString();
                            string ghiChu = r["GhiChuSuCo"].ToString().Trim();
                            string viecChuaLam = r["TrangThaiKiemKe"].ToString().Trim();

                            if (nguoiNhan.Trim() == tenNV.Trim())
                            {
                                bool coGhiChu = !string.IsNullOrEmpty(ghiChu) && ghiChu != "note các công việc tại đây:";
                                bool coViecChuaLam = !string.IsNullOrEmpty(viecChuaLam);

                                if (coGhiChu || coViecChuaLam)
                                {
                                    string msg = $"Nhân viên ca trước [{nguoiGiao}] có bàn giao cho bạn:\n\n";
                                    if (coViecChuaLam) msg += $"[⚠️ MỤC CHƯA HOÀN THÀNH]:\n{viecChuaLam}\n\n";
                                    if (coGhiChu) msg += $"[📝 LỜI NHẮN / SỰ CỐ]:\n{ghiChu}\n\n";
                                    msg += $"Chúc bạn một ca làm việc thuận lợi!";
                                    MessageBox.Show(msg, "📩 THÔNG BÁO NHẬN CA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TuDongSuaLoiDatabase();
            KiemTraGhiChuCaTruoc(tenNhanVien);

            this.WindowState = FormWindowState.Maximized;
            LoadDanhSachMay();

            btnDangXuat.Click += btnDangXuat_Click;

            Timer timerDongBo = new Timer();
            timerDongBo.Interval = 3000;
            timerDongBo.Tick += (s, ev) => { DongBoTrangThaiMay(); };
            timerDongBo.Start();

            LoadCacMayDangHoatDongTuSQL();

            if (quyenHan == "Nhân Viên")
            {
                btnThongKe.Enabled = false;
                btnGiaoDich.Enabled = false;
                this.Text = $"Phần mềm Quản Lý Quán Net - Trực máy: NHÂN VIÊN ({tenNhanVien})";
            }
            else this.Text = $"Phần mềm Quản Lý Quán Net - Trực máy: CHỦ QUÁN ({tenNhanVien})";
        }

        private void LoadDanhSachMay()
        {
            flpViTriPC.Controls.Clear(); cmbChonPC.Items.Clear();
            for (int i = 1; i <= 30; i++)
            {
                string tenMay = "PC " + i.ToString("00");
                ucTramMay tramMay = new ucTramMay();
                tramMay.Name = "uc" + tenMay.Replace(" ", "");
                tramMay.TenMay = tenMay;
                tramMay.Margin = new Padding(10);
                tramMay.Tag = "Trong"; tramMay.SetStatus(false);
                tramMay.Click += May_Click;
                flpViTriPC.Controls.Add(tramMay); cmbChonPC.Items.Add(tenMay);
            }
            if (cmbChonPC.Items.Count > 0) cmbChonPC.SelectedIndex = 0;
        }

        private void May_Click(object sender, EventArgs e)
        {
            ucTramMay mayDuocChon = sender as ucTramMay;
            if (mayDuocChon != null)
            {
                cmbChonPC.Text = mayDuocChon.TenMay;
                foreach (Control ctrl in flpViTriPC.Controls) { if (ctrl is ucTramMay may) may.SetSelected(false); }
                mayDuocChon.SetSelected(true);
            }
        }

        private void cmbChonPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mayDangChon = cmbChonPC.Text;
            foreach (Control ctrl in flpViTriPC.Controls)
            {
                if (ctrl is ucTramMay may) { if (may.TenMay == mayDangChon) may.SetSelected(true); else may.SetSelected(false); }
            }
        }

        private void CapNhatThongKe()
        {
            int count = 0;
            foreach (Control ctrl in flpViTriPC.Controls) { if (ctrl is ucTramMay may && may.Tag != null && may.Tag.ToString() == "CoKhach") count++; }
            dataSoMay[DateTime.Now.Hour] = count;
        }

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            string mayDangChon = cmbChonPC.Text;
            ucTramMay tramMay = flpViTriPC.Controls["uc" + mayDangChon.Replace(" ", "")] as ucTramMay;
            if (tramMay != null)
            {
                if (tramMay.Tag != null && tramMay.Tag.ToString() == "CoKhach")
                {
                    MessageBox.Show(mayDangChon + " đang có người sử dụng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                }

                try
                {
                    string sql = "UPDATE TrangThaiMay SET TrangThai = 'CoKhach', ThoiGianBatDau = @time, TienTraTruoc = 0, TienDichVu = 0, NguoiDung = 'Khách vãng lai' WHERE TenMay = @tenMay";
                    db.Execute(sql, new MySqlParameter[] { new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@tenMay", mayDangChon) });

                    tienDichVuMap[mayDangChon] = 0;

                    tramMay.ThoiGianBatDau = DateTime.Now; tramMay.TienTraTruoc = 0; tramMay.Tag = "CoKhach"; tramMay.SetStatus(true, "00:00:00");
                    dataSoUser[DateTime.Now.Hour]++; CapNhatThongKe();
                    MessageBox.Show("Đã mở " + mayDangChon + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
            }
        }

        // 👉 ĐÃ SỬA LẠI: HIỂN THỊ BẢNG CHỌN TIỀN MẶT/QR CỰC KỲ XỊN XÒ
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string mayDangChon = cmbChonPC.Text;
            ucTramMay tramMay = flpViTriPC.Controls["uc" + mayDangChon.Replace(" ", "")] as ucTramMay;

            if (tramMay != null && tramMay.Tag.ToString() == "CoKhach")
            {
                double tienDichVuDaNo = tienDichVuMap.ContainsKey(mayDangChon) ? tienDichVuMap[mayDangChon] : 0;

                TimeSpan thoiGianDaChoi = DateTime.Now - tramMay.ThoiGianBatDau;
                int tongSoPhut = (int)Math.Ceiling(thoiGianDaChoi.TotalMinutes);
                double donGiaMotPhut = 10000.0 / 60.0;
                double tongTienGio = tongSoPhut * donGiaMotPhut;
                if (tongTienGio < 2000) tongTienGio = 2000;

                double tongBill = tongTienGio + tienDichVuDaNo;
                double tienCanThu = tongBill - tramMay.TienTraTruoc;

                string hoaDon = $"HÓA ĐƠN THANH TOÁN {mayDangChon}:\n\n";
                hoaDon += $"- Tiền giờ ({tongSoPhut} phút): {tongTienGio:#,##0} VNĐ\n";
                if (tienDichVuDaNo > 0) hoaDon += $"- Tiền dịch vụ ăn uống: {tienDichVuDaNo:#,##0} VNĐ\n";
                hoaDon += $"- Khách đã nạp trước: {tramMay.TienTraTruoc:#,##0} VNĐ\n";
                hoaDon += "--------------------------------------\n";

                if (tienCanThu > 0) hoaDon += $"=> CẦN THU LÚC NÀY: {tienCanThu:#,##0} VNĐ";
                else if (tienCanThu < 0) hoaDon += $"=> TRẢ LẠI TIỀN THỪA: {Math.Abs(tienCanThu):#,##0} VNĐ";
                else hoaDon += $"=> KHÁCH ĐÃ TRẢ VỪA ĐỦ (0 VNĐ)";

                bool thuTienThanhCong = true;
                string hinhThucLuuDB = "Tiền mặt";

                if (tienCanThu > 0)
                {
                    // TẠO BẢNG CHỌN TIỀN MẶT HOẶC MÃ QR CỦA NÍ ĐÂY
                    Form frmChonTT = new Form() { Width = 450, Height = 250, Text = "Phương thức thanh toán", StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false };
                    Label lbl = new Label() { Text = hoaDon, AutoSize = true, Location = new Point(20, 20), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                    Button btnTienMat = new Button() { Text = " Tiền Mặt", Location = new Point(60, 150), Width = 130, Height = 45, Font = new Font("Segoe UI", 10, FontStyle.Bold), DialogResult = DialogResult.Yes, Cursor = Cursors.Hand };
                    Button btnQR = new Button() { Text = " Quét QR", Location = new Point(240, 150), Width = 130, Height = 45, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.SteelBlue, ForeColor = Color.White, DialogResult = DialogResult.No, Cursor = Cursors.Hand };

                    frmChonTT.Controls.Add(lbl); frmChonTT.Controls.Add(btnTienMat); frmChonTT.Controls.Add(btnQR);

                    DialogResult phuongThuc = frmChonTT.ShowDialog();
                    if (phuongThuc == DialogResult.Cancel) return; // Nhân viên tắt bảng đi (hủy thanh toán)

                    if (phuongThuc == DialogResult.No) // NẾU CHỌN QUÉT QR
                    {
                        thuTienThanhCong = HienThiMaQR(tienCanThu, mayDangChon);
                        hinhThucLuuDB = "Chuyển khoản (QR)";
                    }
                }
                else
                {
                    // Nếu khách đã trả đủ hoặc nạp dư, chỉ cần bấm OK
                    if (MessageBox.Show(hoaDon + "\n\nXác nhận tắt máy và lưu giao dịch?", "Thanh toán", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) != DialogResult.OK) return;
                }

                // TIẾN HÀNH LƯU DATABASE & TẮT MÁY
                if (thuTienThanhCong)
                {
                    try
                    {
                        string sql = "INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES (@time, 'Thanh toán Giờ + Dịch Vụ', @user, @phi, @tra, @nguon)";
                        MySqlParameter[] p = { new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@user", mayDangChon), new MySqlParameter("@phi", tongBill), new MySqlParameter("@tra", Math.Max(0, tienCanThu)), new MySqlParameter("@nguon", hinhThucLuuDB) };
                        db.Execute(sql, p);

                        string sqlUpdate = "UPDATE TrangThaiMay SET TrangThai = 'Trong', ThoiGianBatDau = NULL, TienTraTruoc = 0, TienDichVu = 0, NguoiDung = NULL WHERE TenMay = @tenMay";
                        db.Execute(sqlUpdate, new MySqlParameter[] { new MySqlParameter("@tenMay", mayDangChon) });

                        tienDichVuMap[mayDangChon] = 0; // Xóa nợ ăn uống
                        tramMay.Tag = "Trong"; tramMay.TienTraTruoc = 0; tramMay.SetStatus(false);
                        dataDoanhThu[DateTime.Now.Hour] += (long)tongBill; CapNhatThongKe();

                        if (tienCanThu > 0) MessageBox.Show("Giao dịch thành công. Đã in hóa đơn!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi lưu Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private bool HienThiMaQR(double soTien, string tenMay)
        {
            string urlVietQR = $"https://img.vietqr.io/image/970415-107879591328-compact2.jpg?amount={soTien}&addInfo=ThanhToan{tenMay.Replace(" ", "")}&accountName=LY BA PHU";
            Form frmQR = new Form() { Size = new Size(400, 580), Text = "Quét mã QR", StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.FixedDialog };
            PictureBox picQR = new PictureBox() { Size = new Size(360, 420), Location = new Point(10, 10), SizeMode = PictureBoxSizeMode.Zoom };
            Button btnXacNhan = new Button() { Text = "Oki Đã nhận tiền", Size = new Size(200, 50), Location = new Point(90, 450), DialogResult = DialogResult.OK, BackColor = Color.LimeGreen, ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            try { picQR.Load(urlVietQR); } catch { return false; }
            frmQR.Controls.Add(picQR); frmQR.Controls.Add(btnXacNhan);
            return frmQR.ShowDialog() == DialogResult.OK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dgvThongKe.Rows.Clear();
            foreach (Control ctrl in flpViTriPC.Controls)
            {
                if (ctrl is ucTramMay may && may.Tag != null && may.Tag.ToString() == "CoKhach")
                {
                    TimeSpan thoiGianDaChoi = DateTime.Now - may.ThoiGianBatDau;
                    string chuoiThoiGian = string.Format("{0:D2}:{1:D2}:{2:D2}", (int)thoiGianDaChoi.TotalHours, thoiGianDaChoi.Minutes, thoiGianDaChoi.Seconds);
                    may.SetStatus(true, chuoiThoiGian);

                    int phut = (int)Math.Ceiling(thoiGianDaChoi.TotalMinutes);
                    double tienGio = Math.Max(2000, phut * (10000.0 / 60.0));

                    double tienDichVu = tienDichVuMap.ContainsKey(may.TenMay) ? tienDichVuMap[may.TenMay] : 0;
                    double tongBillHienTai = tienGio + tienDichVu;

                    dgvThongKe.Rows.Add(may.TenMay, "Khách vãng lai", may.ThoiGianBatDau.ToString("HH:mm"), chuoiThoiGian, tongBillHienTai.ToString("#,##0"));
                }
            }
        }

        private void MoFormDichVu()
        {
            string mayDangChon = cmbChonPC.Text;
            ucTramMay tramMay = flpViTriPC.Controls["uc" + mayDangChon.Replace(" ", "")] as ucTramMay;
            if (tramMay != null && tramMay.Tag.ToString() == "CoKhach")
            {
                AnTatCaCacTrang();
                frmDichVu formDV = new frmDichVu(mayDangChon);
                formDV.TopLevel = false; formDV.Dock = DockStyle.Fill;
                guna2Panel1.Controls.Add(formDV); formDV.BringToFront(); formDV.Show();
            }
            else MessageBox.Show("Máy chưa mở!");
        }

        private void btnViTriPC_Click(object sender, EventArgs e) { AnTatCaCacTrang(); guna2Panel2.Visible = true; guna2Panel3.Visible = true; flpViTriPC.BringToFront(); }
        private void btnThongKe_Click(object sender, EventArgs e) { AnTatCaCacTrang(); if (trangThongKe == null) { trangThongKe = new ThongKe(); trangThongKe.Dock = DockStyle.Fill; guna2Panel1.Controls.Add(trangThongKe); } trangThongKe.Visible = true; trangThongKe.BringToFront(); }
        private void btnGiaoDich_Click(object sender, EventArgs e) { AnTatCaCacTrang(); if (trangGD == null) { trangGD = new TrangGiaoDich(); trangGD.Dock = DockStyle.Fill; guna2Panel1.Controls.Add(trangGD); } trangGD.Visible = true; trangGD.BringToFront(); }
        private void btnGoiMon_Click(object sender, EventArgs e) => MoFormDichVu();
        private void btnKhachHang_Click(object sender, EventArgs e) { AnTatCaCacTrang(); if (trangKhachHang == null) { trangKhachHang = new ucKhachHang(); trangKhachHang.Dock = DockStyle.Fill; guna2Panel1.Controls.Add(trangKhachHang); } trangKhachHang.Visible = true; trangKhachHang.BringToFront(); }
        private void btnGiaoCa_Click(object sender, EventArgs e) { AnTatCaCacTrang(); if (trangGiaoCa == null) { trangGiaoCa = new GiaoCa(); trangGiaoCa.Dock = DockStyle.Fill; guna2Panel1.Controls.Add(trangGiaoCa); } trangGiaoCa.ThietLapThongTin(tenNhanVien, DateTime.Now.AddHours(-6)); trangGiaoCa.Visible = true; trangGiaoCa.BringToFront(); }
        private void btnKhoHang_Click(object sender, EventArgs e) { AnTatCaCacTrang(); if (trangKho == null) { trangKho = new Kho(); trangKho.Dock = DockStyle.Fill; guna2Panel1.Controls.Add(trangKho); } trangKho.Visible = true; trangKho.BringToFront(); }
        private void btnDichVuMay_Click(object sender, EventArgs e) => MoFormDichVu();

        private void AnTatCaCacTrang()
        {
            if (trangThongKe != null) trangThongKe.Visible = false;
            if (trangGD != null) trangGD.Visible = false;
            if (trangKhachHang != null) trangKhachHang.Visible = false;
            if (trangGiaoCa != null) trangGiaoCa.Visible = false;
            if (trangKho != null) trangKho.Visible = false;
            guna2Panel2.Visible = false; guna2Panel3.Visible = false;
            foreach (Control c in guna2Panel1.Controls) if (c is frmDichVu) c.Dispose();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (formDangNhap != null) formDangNhap.Show();
                this.Hide();
            }
        }

        private void LoadCacMayDangHoatDongTuSQL()
        {
            try
            {
                DataTable dt = db.GetTable("SELECT * FROM TrangThaiMay WHERE TrangThai = 'CoKhach'");
                foreach (DataRow row in dt.Rows)
                {
                    string ten = row["TenMay"].ToString();
                    ucTramMay tram = flpViTriPC.Controls["uc" + ten.Replace(" ", "")] as ucTramMay;
                    if (tram != null)
                    {
                        tram.Tag = "CoKhach";
                        tram.ThoiGianBatDau = Convert.ToDateTime(row["ThoiGianBatDau"]);
                        tram.TienTraTruoc = Convert.ToDouble(row["TienTraTruoc"]);

                        if (dt.Columns.Contains("TienDichVu"))
                            tienDichVuMap[ten] = Convert.ToDouble(row["TienDichVu"]);

                        tram.SetStatus(true, "00:00:00");
                    }
                }
            }
            catch { }
        }

        private void DongBoTrangThaiMay()
        {
            try
            {
                DataTable dt = db.GetTable("SELECT * FROM TrangThaiMay WHERE TrangThai = 'CoKhach'");
                List<string> list = new List<string>();
                foreach (DataRow r in dt.Rows)
                {
                    string ten = r["TenMay"].ToString();
                    list.Add(ten);
                    if (dt.Columns.Contains("TienDichVu"))
                        tienDichVuMap[ten] = Convert.ToDouble(r["TienDichVu"]);
                }

                foreach (Control c in flpViTriPC.Controls)
                {
                    if (c is ucTramMay m)
                    {
                        if (!list.Contains(m.TenMay) && m.Tag.ToString() == "CoKhach") { m.Tag = "Trong"; m.SetStatus(false); }
                    }
                }
            }
            catch { }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); Application.Exit(); }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            string mayDangChon = cmbChonPC.Text;
            ucTramMay tramMay = flpViTriPC.Controls["uc" + mayDangChon.Replace(" ", "")] as ucTramMay;
            if (tramMay != null && tramMay.Tag.ToString() == "CoKhach")
            {
                string tienNhapVao = HienThiFormNhapTien(mayDangChon);
                if (double.TryParse(tienNhapVao, out double soTien))
                {
                    try
                    {
                        string sqlUpdate = "UPDATE TrangThaiMay SET TienTraTruoc = TienTraTruoc + @tien WHERE TenMay = @tenMay";
                        db.Execute(sqlUpdate, new MySqlParameter[] { new MySqlParameter("@tien", soTien), new MySqlParameter("@tenMay", mayDangChon) });

                        tramMay.TienTraTruoc += soTien;
                        string sql = "INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES (@time, @mota, @user, @phi, @tra, @nguon)";
                        MySqlParameter[] p = { new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@mota", "Nạp tiền"), new MySqlParameter("@user", mayDangChon), new MySqlParameter("@phi", soTien), new MySqlParameter("@tra", soTien), new MySqlParameter("@nguon", "Tiền mặt") };
                        db.Execute(sql, p);
                        MessageBox.Show($"Đã nạp {soTien:#,##0} VNĐ vào {mayDangChon}.\nTổng tiền trả trước hiện tại: {tramMay.TienTraTruoc:#,##0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            string mayDangChon = cmbChonPC.Text;
            ucTramMay tramMay = flpViTriPC.Controls["uc" + mayDangChon.Replace(" ", "")] as ucTramMay;
            if (tramMay != null && tramMay.Tag.ToString() == "CoKhach")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn TẮT " + mayDangChon + " khi chưa thanh toán?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        db.Execute("UPDATE TrangThaiMay SET TrangThai = 'Trong', ThoiGianBatDau = NULL, TienTraTruoc = 0, TienDichVu = 0, NguoiDung = NULL WHERE TenMay = @tenMay",
                                   new MySqlParameter[] { new MySqlParameter("@tenMay", mayDangChon) });
                        tienDichVuMap[mayDangChon] = 0;
                        tramMay.Tag = "Trong"; tramMay.SetStatus(false); CapNhatThongKe();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
                }
            }
        }

        private void btnChuyenMay_Click(object sender, EventArgs e)
        {
            string mayCu = cmbChonPC.Text;
            ucTramMay tramMayCu = flpViTriPC.Controls["uc" + mayCu.Replace(" ", "")] as ucTramMay;
            if (tramMayCu != null && tramMayCu.Tag.ToString() == "CoKhach")
            {
                List<string> dsMayTrong = new List<string>();
                foreach (Control ctrl in flpViTriPC.Controls) { if (ctrl is ucTramMay may && may.Tag.ToString() == "Trong") dsMayTrong.Add(may.TenMay); }
                if (dsMayTrong.Count == 0) return;
                string mayMoi = HienThiFormChuyenMay(dsMayTrong);
                if (!string.IsNullOrEmpty(mayMoi))
                {
                    try
                    {
                        double tienDVCu = tienDichVuMap.ContainsKey(mayCu) ? tienDichVuMap[mayCu] : 0;
                        ucTramMay tramMayMoi = flpViTriPC.Controls["uc" + mayMoi.Replace(" ", "")] as ucTramMay;

                        db.Execute("UPDATE TrangThaiMay SET TrangThai = 'CoKhach', ThoiGianBatDau = @time, TienTraTruoc = @tien, TienDichVu = @tienDV, NguoiDung = 'Khách vãng lai' WHERE TenMay = @mayMoi",
                                   new MySqlParameter[] { new MySqlParameter("@time", tramMayCu.ThoiGianBatDau), new MySqlParameter("@tien", tramMayCu.TienTraTruoc), new MySqlParameter("@tienDV", tienDVCu), new MySqlParameter("@mayMoi", mayMoi) });

                        db.Execute("UPDATE TrangThaiMay SET TrangThai = 'Trong', ThoiGianBatDau = NULL, TienTraTruoc = 0, TienDichVu = 0, NguoiDung = NULL WHERE TenMay = @mayCu",
                                   new MySqlParameter[] { new MySqlParameter("@mayCu", mayCu) });

                        tienDichVuMap[mayMoi] = tienDVCu;
                        tienDichVuMap[mayCu] = 0;

                        tramMayMoi.ThoiGianBatDau = tramMayCu.ThoiGianBatDau; tramMayMoi.TienTraTruoc = tramMayCu.TienTraTruoc; tramMayMoi.Tag = "CoKhach";
                        tramMayCu.Tag = "Trong"; tramMayCu.TienTraTruoc = 0; tramMayCu.SetStatus(false);
                        cmbChonPC.Text = mayMoi;
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message); }
                }
            }
        }

        private string HienThiFormNhapTien(string tenMay)
        {
            Form prompt = new Form() { Width = 300, Height = 180, FormBorderStyle = FormBorderStyle.FixedDialog, Text = "Nạp Tiền", StartPosition = FormStartPosition.CenterScreen, TopMost = true };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = $"Nhập số tiền nạp cho {tenMay}:", AutoSize = true };
            TextBox txtTien = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "Nạp tiền", Left = 160, Width = 100, Top = 90, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(txtTien); prompt.Controls.Add(confirmation); prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? txtTien.Text : "";
        }

        private string HienThiFormChuyenMay(List<string> danhSachMayTrong)
        {
            Form prompt = new Form() { Width = 300, Height = 180, FormBorderStyle = FormBorderStyle.FixedDialog, Text = "Chuyển Máy", StartPosition = FormStartPosition.CenterScreen, TopMost = true };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Chọn máy muốn chuyển ĐẾN:", AutoSize = true };
            ComboBox cmb = new ComboBox() { Left = 20, Top = 50, Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            cmb.Items.AddRange(danhSachMayTrong.ToArray()); if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            Button confirmation = new Button() { Text = "Xác nhận", Left = 160, Width = 100, Top = 90, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(cmb); prompt.Controls.Add(confirmation); prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? cmb.Text : "";
        }
    }
}