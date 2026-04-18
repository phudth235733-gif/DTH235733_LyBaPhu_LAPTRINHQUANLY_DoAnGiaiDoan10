using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyQuanNet
{
    public partial class frmDichVu : Form
    {
        // Thêm chuỗi kết nối để hàm quét Real-time có thể xài
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";
        string mayGoiMenu = "";
        double tongTienDonHang = 0;

        public frmDichVu(string tenMay = "")
        {
            InitializeComponent();
            mayGoiMenu = tenMay;

            // TUYỆT CHIÊU: Tự động gắn sự kiện nạp PC bằng code (Không cần thao tác Design)
            cmbSelectPC.DropDown += cmbSelectPC_DropDown;
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            // 1. Quét ngay 1 vòng xem có máy nào đang bật không
            LoadDanhSachPCDangMo();

            // 2. Nếu nhân viên mở menu từ 1 máy cụ thể thì ép chọn máy đó
            if (!string.IsNullOrEmpty(mayGoiMenu))
            {
                if (!cmbSelectPC.Items.Contains(mayGoiMenu))
                    cmbSelectPC.Items.Add(mayGoiMenu);

                cmbSelectPC.Text = mayGoiMenu;
            }

            // Mặc định load Đồ Uống đầu tiên
            LoadMenuThucAn("Đồ Uống");
        }

        // ==============================================================
        // HÀM MỚI: TỰ ĐỘNG CẬP NHẬT PC THEO THỜI GIAN THỰC KHI BẤM CHỌN
        // ==============================================================
        private void cmbSelectPC_DropDown(object sender, EventArgs e)
        {
            LoadDanhSachPCDangMo(); // Sổ xuống 1 cái là gọi DB nạp máy liền
        }

        private void LoadDanhSachPCDangMo()
        {
            try
            {
                string pcDangChon = cmbSelectPC.Text;
                cmbSelectPC.Items.Clear(); // Tẩy sạch cái list cũ rác

                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    // Lặn xuống kho gom máy đang sáng đèn ("CoKhach") lên
                    string sql = "SELECT TenMay FROM TrangThaiMay WHERE TrangThai = 'CoKhach' ORDER BY TenMay ASC";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbSelectPC.Items.Add(reader["TenMay"].ToString());
                        }
                    }
                }

                // Nhớ lại cái máy đang chọn lúc nãy, khỏi bắt nhân viên chọn lại
                if (cmbSelectPC.Items.Contains(pcDangChon))
                    cmbSelectPC.Text = pcDangChon;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách PC: " + ex.Message);
            }
        }

        // ==========================================
        // 1. SỰ KIỆN CLICK CÁC NÚT DANH MỤC BÊN TRÁI
        // ==========================================
        private void btnDoUong_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Uống");
        private void btnDoAn_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Ăn");
        private void btnDoDung_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Dùng");
        private void btnNapThe_Click(object sender, EventArgs e) => LoadMenuThucAn("Nạp Thẻ");
        private void btnComBo_Click_1(object sender, EventArgs e) => LoadMenuThucAn("Combo");

        // ==========================================
        // 2. HÀM TẠO MÓN ĂN KÈM NÚT CỘNG TRỪ ẨN HIỆN
        // ==========================================
        private void LoadMenuThucAn(string danhMuc)
        {
            flpMenu.Controls.Clear();

            // Tuple lưu: (Tên Món, Giá, Tên_File_Ảnh)
            List<(string Ten, double Gia, string TenHinh)> dsMon = new List<(string, double, string)>();

            switch (danhMuc)
            {
                case "Đồ Uống":
                    dsMon.Add(("Sting Đỏ", 10000, "sting"));
                    dsMon.Add(("Bò Húc", 15000, "BoHuc"));
                    dsMon.Add(("Pepsi", 10000, "Pepsi"));
                    dsMon.Add(("Nước Suối", 5000, "water"));
                    dsMon.Add(("Trà Đá", 2000, "TraDa"));
                    break;

                case "Đồ Ăn":
                    dsMon.Add(("Mì Xào Trứng", 20000, "MiXao"));
                    dsMon.Add(("Cơm Rang", 35000, "ComRang"));
                    dsMon.Add(("Xúc Xích", 10000, "XucXich"));
                    break;

                case "Đồ Dùng":
                    dsMon.Add(("Khăn Lạnh", 2000, "KhanLanh"));
                    dsMon.Add(("Bật Lửa", 5000, "BatLua"));
                    break;

                case "Nạp Thẻ":
                    dsMon.Add(("Thẻ 20K", 20000, "The20k"));
                    dsMon.Add(("Thẻ 50K", 50000, "The50k"));
                    dsMon.Add(("Thẻ 100K", 100000, "The50k"));
                    break;

                case "Combo":
                    dsMon.Add(("Mì + Sting", 28000, "ComBo1"));
                    dsMon.Add(("Trà đá 4 ống hút", 8000, "ComBo2"));
                    dsMon.Add(("Sting 4 tẩy đá", 16000, "ComBo3"));
                    break;
            }

            foreach (var mon in dsMon)
            {
                // --- TẠO NÚT MÓN ĂN CHÍNH ---
                Guna2Button btnMon = new Guna2Button();
                btnMon.Text = mon.Ten + "\n" + mon.Gia.ToString("#,##0") + "đ";
                btnMon.Width = 140;
                btnMon.Height = 160;
                btnMon.Margin = new Padding(10);
                btnMon.BorderRadius = 10;
                btnMon.FillColor = Color.FromArgb(40, 45, 50);
                btnMon.ForeColor = Color.White;
                btnMon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                // Nạp ảnh
                object imgObj = Properties.Resources.ResourceManager.GetObject(mon.TenHinh);
                if (imgObj != null)
                {
                    btnMon.Image = (Image)imgObj;
                    btnMon.ImageSize = new Size(80, 80);
                    btnMon.ImageAlign = HorizontalAlignment.Center;
                    btnMon.TextAlign = HorizontalAlignment.Center;
                    btnMon.ImageOffset = new Point(0, -20);
                    btnMon.TextOffset = new Point(0, 35);
                }
                btnMon.Tag = new { Ten = mon.Ten, Gia = mon.Gia };

                // --- TẠO 2 NÚT CỘNG/TRỪ TRONG SUỐT VIỀN TRẮNG ---
                int sizeNutNho = 30;

                // Nút CỘNG
                Guna2Button btnPlus = new Guna2Button();
                btnPlus.Text = "+";
                btnPlus.Font = new Font("Arial", 12F, FontStyle.Bold);
                btnPlus.Width = sizeNutNho;
                btnPlus.Height = sizeNutNho;
                btnPlus.BorderRadius = sizeNutNho / 2;
                btnPlus.FillColor = Color.Transparent;
                btnPlus.UseTransparentBackground = true;
                btnPlus.BorderThickness = 1;
                btnPlus.BorderColor = Color.White;
                btnPlus.ForeColor = Color.White;
                btnPlus.Padding = new Padding(0);
                btnPlus.TextOffset = new Point(0, 0);
                btnPlus.Visible = false;
                btnPlus.Location = new Point(btnMon.Width - sizeNutNho - 5, 15);

                Guna2Button btnMinus = new Guna2Button();
                btnMinus.Text = "";
                btnMinus.Width = sizeNutNho;
                btnMinus.Height = sizeNutNho;
                btnMinus.BorderRadius = sizeNutNho / 2;
                btnMinus.FillColor = Color.Transparent;
                btnMinus.UseTransparentBackground = true;
                btnMinus.BorderThickness = 1;
                btnMinus.BorderColor = Color.White;
                btnMinus.Visible = false;
                btnMinus.Location = new Point(btnMon.Width - sizeNutNho - 5, btnPlus.Bottom + 5);

                // Vẽ vạch ngang (dấu trừ)
                btnMinus.Paint += (senderBtn, eventArgs) =>
                {
                    Graphics g = eventArgs.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    using (Pen p = new Pen(Color.White, 2))
                    {
                        int tamX = btnMinus.Width / 2;
                        int tamY = btnMinus.Height / 2;
                        g.DrawLine(p, tamX - 6, tamY, tamX + 6, tamY);
                    }
                };

                btnPlus.Tag = btnMon;
                btnMinus.Tag = btnMon;

                // --- SỰ KIỆN ẨN HIỆN ---
                btnMon.MouseEnter += (s, e) => { btnPlus.Visible = btnMinus.Visible = true; };
                btnMon.MouseLeave += (s, e) =>
                {
                    Point mousePos = btnMon.PointToClient(Cursor.Position);
                    if (!btnMon.ClientRectangle.Contains(mousePos))
                    {
                        btnPlus.Visible = btnMinus.Visible = false;
                    }
                };

                // --- SỰ KIỆN TĂNG GIẢM ---
                btnPlus.Click += TangSoLuong_Click;
                btnMinus.Click += GiamSoLuong_Click;

                btnMon.DoubleClick += (s, e) => { TangSoLuong_Click(btnPlus, e); };

                btnMon.Controls.Add(btnPlus);
                btnMon.Controls.Add(btnMinus);

                flpMenu.Controls.Add(btnMon);
            }
        }

        // ==========================================
        // 3. XỬ LÝ SỰ KIỆN TĂNG/GIẢM SỐ LƯỢNG
        // ==========================================
        private void TangSoLuong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbSelectPC.Text))
            {
                MessageBox.Show("Vui lòng chọn Máy (PC) trước khi gọi món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSelectPC.Focus();
                return;
            }

            Guna2Button btnPlus = sender as Guna2Button;
            if (btnPlus == null || btnPlus.Tag == null) return;

            Guna2Button btnMonCha = btnPlus.Tag as Guna2Button;
            if (btnMonCha == null || btnMonCha.Tag == null) return;

            dynamic data = btnMonCha.Tag;
            string tenMon = data.Ten;
            double donGia = data.Gia;

            bool daTonTai = false;
            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells["colTenMon"].Value != null && row.Cells["colTenMon"].Value.ToString() == tenMon)
                {
                    int soLuongCu = Convert.ToInt32(row.Cells["colSoLuong"].Value);
                    int soLuongMoi = soLuongCu + 1;

                    row.Cells["colSoLuong"].Value = soLuongMoi;
                    row.Cells["colThanhTien"].Value = soLuongMoi * donGia;

                    daTonTai = true;
                    break;
                }
            }

            if (!daTonTai)
            {
                dgvDonHang.Rows.Add(tenMon, 1, donGia, donGia);
            }

            TinhTongTien();
        }

        private void GiamSoLuong_Click(object sender, EventArgs e)
        {
            Guna2Button btnMinus = sender as Guna2Button;
            if (btnMinus == null || btnMinus.Tag == null) return;

            Guna2Button btnMonCha = btnMinus.Tag as Guna2Button;
            if (btnMonCha == null || btnMonCha.Tag == null) return;

            dynamic data = btnMonCha.Tag;
            string tenMon = data.Ten;
            double donGia = data.Gia;

            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells["colTenMon"].Value != null && row.Cells["colTenMon"].Value.ToString() == tenMon)
                {
                    int soLuongCu = Convert.ToInt32(row.Cells["colSoLuong"].Value);

                    if (soLuongCu > 1)
                    {
                        int soLuongMoi = soLuongCu - 1;
                        row.Cells["colSoLuong"].Value = soLuongMoi;
                        row.Cells["colThanhTien"].Value = soLuongMoi * donGia;
                    }
                    else
                    {
                        dgvDonHang.Rows.Remove(row);
                    }
                    break;
                }
            }

            TinhTongTien();
        }

        // ==========================================
        // 4. TÍNH TIỀN, HỦY VÀ THANH TOÁN (ĐÃ FIX LỖI TÊN MÁY)
        // ==========================================
        private void TinhTongTien()
        {
            tongTienDonHang = 0;
            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells["colThanhTien"].Value != null)
                {
                    tongTienDonHang += Convert.ToDouble(row.Cells["colThanhTien"].Value);
                }
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn hủy toàn bộ giỏ hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvDonHang.Rows.Clear();
                    TinhTongTien();
                }
            }
        }

        // ==========================================
        // 4. TÍNH TIỀN, HỦY VÀ THANH TOÁN (ĐÃ TÍCH HỢP QUÉT QR)
        // ==========================================
        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {
            string pcThanhToan = cmbSelectPC.Text.Trim();

            if (string.IsNullOrEmpty(pcThanhToan))
            {
                MessageBox.Show("Bạn chưa chọn máy (PC) để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSelectPC.Focus();
                return;
            }

            if (dgvDonHang.Rows.Count == 0 || tongTienDonHang == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- TẠO BẢNG CHỌN PHƯƠNG THỨC THANH TOÁN ---
            Form frmChonTT = new Form() { Width = 400, Height = 180, Text = "Phương thức thanh toán", StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false };
            Label lbl = new Label() { Text = $"Thanh toán {tongTienDonHang:#,##0} VNĐ cho {pcThanhToan}. Chọn phương thức:", AutoSize = true, Location = new Point(20, 20), Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            Button btnTienMat = new Button() { Text = "💵 Tiền Mặt", Location = new Point(40, 70), Width = 130, Height = 45, Font = new Font("Segoe UI", 10, FontStyle.Bold), DialogResult = DialogResult.Yes, Cursor = Cursors.Hand };
            Button btnQR = new Button() { Text = "📱 Quét QR", Location = new Point(210, 70), Width = 130, Height = 45, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.SteelBlue, ForeColor = Color.White, DialogResult = DialogResult.No, Cursor = Cursors.Hand };

            frmChonTT.Controls.Add(lbl); frmChonTT.Controls.Add(btnTienMat); frmChonTT.Controls.Add(btnQR);

            DialogResult phuongThuc = frmChonTT.ShowDialog();

            // Nếu nhân viên đóng bảng chọn (không thu tiền nữa) thì thoát
            if (phuongThuc == DialogResult.Cancel) return;

            // Cờ đánh dấu xem đã thu được tiền chưa
            bool thuTienThanhCong = false;
            string hinhThucLuuDB = "Dịch Vụ"; // Chữ để lưu vô cột Nguồn

            if (phuongThuc == DialogResult.Yes)
            {
                // Chọn Tiền mặt -> Coi như đã thu xong
                thuTienThanhCong = true;
            }
            else if (phuongThuc == DialogResult.No)
            {
                // Chọn QR -> Hiện bảng mã QR lên. Chừng nào bấm "Đã nhận tiền" mới báo thành công
                thuTienThanhCong = HienThiMaQR(tongTienDonHang, pcThanhToan);
                hinhThucLuuDB = "Dịch Vụ (Chuyển khoản)"; // Sửa nội dung xíu để dễ Thống Kê sau này
            }

            // --- NẾU ĐÃ THU TIỀN THÀNH CÔNG (Dù là mặt hay QR) THÌ MỚI LƯU DATA ---
            if (thuTienThanhCong)
            {
                try
                {
                    string chiTietDon = "DV Gọi món: ";
                    foreach (DataGridViewRow row in dgvDonHang.Rows)
                    {
                        if (row.Cells["colTenMon"].Value != null)
                        {
                            chiTietDon += $"{row.Cells["colTenMon"].Value}(x{row.Cells["colSoLuong"].Value}), ";
                        }
                    }

                    DataService db = new DataService();
                    string sql = "INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES (@time, @mota, @user, @phi, @tra, @nguon)";
                    MySqlParameter[] p = {
                        new MySqlParameter("@time", DateTime.Now),
                        new MySqlParameter("@mota", chiTietDon),
                        new MySqlParameter("@user", pcThanhToan),
                        new MySqlParameter("@phi", tongTienDonHang),
                        new MySqlParameter("@tra", tongTienDonHang),
                        new MySqlParameter("@nguon", hinhThucLuuDB)
                    };
                    db.Execute(sql, p);

                    MessageBox.Show("Thanh toán thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvDonHang.Rows.Clear();
                    TinhTongTien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Close(); // Đóng form
            }
        }


        // ==============================================================
        // HÀM BỔ TRỢ: TẠO FORM MINI HIỂN THỊ MÃ QR CODE TỰ ĐỘNG
        // ==============================================================
        private bool HienThiMaQR(double soTien, string tenMay)
        {
            // 1. THAY ĐỔI THÔNG TIN TÀI KHOẢNNGÂN HÀNG:
            string maNganHang = "970415"; 
            string soTaiKhoan = "107879591328"; // Gõ 
            string tenChuTaiKhoan = "LÝ BÁ PHÚ"; // 

            // Nội dung chuyển khoản tự động sinh ra (VD: Thanh toan PC05)
            string noiDungCK = "Thanh toan " + tenMay.Replace(" ", "");

            // Link kết nối máy chủ VietQR
            string urlVietQR = $"https://img.vietqr.io/image/{maNganHang}-{soTaiKhoan}-compact2.jpg?amount={soTien}&addInfo={noiDungCK}&accountName={tenChuTaiKhoan}";

            // 2. TỰ ĐỘNG VẼ RA MỘT CÁI BẢNG (FORM) ĐỂ CHỨA MÃ QR
            Form frmQR = new Form()
            {
                Size = new Size(400, 580),
                Text = "Mời khách quét mã QR",
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            PictureBox picQR = new PictureBox()
            {
                Size = new Size(360, 420),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Button btnXacNhan = new Button()
            {
                Text = "✅ Đã nhận được tiền",
                Size = new Size(200, 50),
                Location = new Point(90, 450),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.LimeGreen,
                ForeColor = Color.White,
                DialogResult = DialogResult.OK,
                Cursor = Cursors.Hand
            };

            // Tải ảnh QR từ Internet về
            try
            {
                picQR.Load(urlVietQR);
            }
            catch
            {
                MessageBox.Show("Không thể tải mã QR. Vui lòng kiểm tra lại kết nối mạng!", "Lỗi mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            frmQR.Controls.Add(picQR);
            frmQR.Controls.Add(btnXacNhan);

            // Bảng hiện lên, chừng nào nhân viên bấm nút "Đã nhận được tiền" thì mới trả về TRUE để lưu Database
            return frmQR.ShowDialog() == DialogResult.OK;
        }
    }
}