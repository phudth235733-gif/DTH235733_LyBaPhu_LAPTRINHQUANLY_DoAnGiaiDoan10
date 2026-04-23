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
        private string chuoiKetNoi = "Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;";
        string mayGoiMenu = "";
        double tongTienDonHang = 0;

        // BỘ NHỚ VĨNH CỬU (static): Giữ nguyên món mới kể cả khi tắt form Dịch Vụ chuyển sang form khác!
        public static List<(string Ten, double Gia, string DanhMuc, Image Hinh)> danhSachMonTuThem = new List<(string, double, string, Image)>();

        public frmDichVu(string tenMay = "")
        {
            InitializeComponent();
            mayGoiMenu = tenMay;
            cmbSelectPC.DropDown += cmbSelectPC_DropDown;
            btnThanhToan.Text = "Xác Nhận Order";

            picUpHinh.Click += picUpHinh_Click;
            btnThemMon.Click += btnThemMon_Click;
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            LoadDanhSachPCDangMo();
            if (!string.IsNullOrEmpty(mayGoiMenu))
            {
                if (!cmbSelectPC.Items.Contains(mayGoiMenu)) cmbSelectPC.Items.Add(mayGoiMenu);
                cmbSelectPC.Text = mayGoiMenu;
            }

            // ÉP CỨNG DANH MỤC CHO KHỚP VỚI CÁC NÚT BÊN TRÁI
            cmbDanhMuc.Items.Clear();
            cmbDanhMuc.Items.AddRange(new string[] { "Đồ Uống", "Đồ Ăn", "Đồ Dùng", "Nạp Thẻ", "Combo" });
            cmbDanhMuc.SelectedIndex = 0;

            LoadMenuThucAn("Đồ Uống");
        }

        private void cmbSelectPC_DropDown(object sender, EventArgs e) => LoadDanhSachPCDangMo();

        private void LoadDanhSachPCDangMo()
        {
            try
            {
                string pcDangChon = cmbSelectPC.Text;
                cmbSelectPC.Items.Clear();
                using (MySqlConnection conn = new MySqlConnection(chuoiKetNoi))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT TenMay FROM TrangThaiMay WHERE TrangThai = 'CoKhach' ORDER BY TenMay ASC", conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) cmbSelectPC.Items.Add(reader["TenMay"].ToString());
                    }
                }
                if (cmbSelectPC.Items.Contains(pcDangChon)) cmbSelectPC.Text = pcDangChon;
            }
            catch { }
        }

        // ĐẢM BẢO TÊN DANH MỤC Ở ĐÂY KHỚP 100% VỚI CÁI COMBOBOX Ở TRÊN
        private void btnDoUong_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Uống");
        private void btnDoAn_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Ăn");
        private void btnDoDung_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Dùng");
        private void btnNapThe_Click(object sender, EventArgs e) => LoadMenuThucAn("Nạp Thẻ");
        private void btnComBo_Click_1(object sender, EventArgs e) => LoadMenuThucAn("Combo");

        private void LoadMenuThucAn(string danhMuc)
        {
            flpMenu.Controls.Clear();
            List<(string Ten, double Gia, string TenHinh)> dsMonMacDinh = new List<(string, double, string)>();

            switch (danhMuc)
            {
                case "Đồ Uống":
                    dsMonMacDinh.Add(("Sting Đỏ", 10000, "sting")); dsMonMacDinh.Add(("Bò Húc", 15000, "BoHuc"));
                    dsMonMacDinh.Add(("Pepsi", 10000, "Pepsi")); dsMonMacDinh.Add(("Nước Suối", 5000, "water")); dsMonMacDinh.Add(("Trà Đá", 2000, "TraDa")); break;
                case "Đồ Ăn":
                    dsMonMacDinh.Add(("Mì Xào Trứng", 20000, "MiXao")); dsMonMacDinh.Add(("Cơm Rang", 35000, "ComRang")); dsMonMacDinh.Add(("Xúc Xích", 10000, "XucXich")); break;
                case "Đồ Dùng":
                    dsMonMacDinh.Add(("Khăn Lạnh", 2000, "KhanLanh")); dsMonMacDinh.Add(("Bật Lửa", 5000, "BatLua")); break;
                case "Nạp Thẻ":
                    dsMonMacDinh.Add(("Thẻ 20K", 20000, "The20k")); dsMonMacDinh.Add(("Thẻ 50K", 50000, "The50k")); dsMonMacDinh.Add(("Thẻ 100K", 100000, "The50k")); break;
                case "Combo":
                    dsMonMacDinh.Add(("Mì + Sting", 28000, "ComBo1")); dsMonMacDinh.Add(("Trà đá 4 ống hút", 8000, "ComBo2")); dsMonMacDinh.Add(("Sting 4 tẩy đá", 16000, "ComBo3")); break;
            }

            // 1. VẼ CÁC MÓN MẶC ĐỊNH
            foreach (var mon in dsMonMacDinh)
            {
                Image img = (Image)Properties.Resources.ResourceManager.GetObject(mon.TenHinh);
                flpMenu.Controls.Add(TaoNutMonAn(mon.Ten, mon.Gia, img));
            }

            // 2. VẼ THÊM CÁC MÓN NGƯỜI DÙNG TỰ THÊM VÀO (TỪ BỘ NHỚ VĨNH CỬU)
            foreach (var mon in danhSachMonTuThem)
            {
                if (mon.DanhMuc == danhMuc) // So sánh khớp 100% tên danh mục thì mới vẽ ra
                {
                    flpMenu.Controls.Add(TaoNutMonAn(mon.Ten, mon.Gia, mon.Hinh));
                }
            }
        }

        // HÀM VẼ GIAO DIỆN NÚT MÓN ĂN VÀ GIỎ HÀNG
        private Guna2Button TaoNutMonAn(string ten, double gia, Image hinh)
        {
            Guna2Button btnMon = new Guna2Button()
            {
                Text = ten + "\n" + gia.ToString("#,##0") + "đ",
                Width = 140,
                Height = 160,
                Margin = new Padding(10),
                BorderRadius = 10,
                FillColor = Color.FromArgb(40, 45, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Tag = new { Ten = ten, Gia = gia }
            };

            if (hinh != null)
            {
                btnMon.Image = hinh;
                btnMon.ImageSize = new Size(80, 80);
                btnMon.ImageAlign = HorizontalAlignment.Center;
                btnMon.TextAlign = HorizontalAlignment.Center;
                btnMon.ImageOffset = new Point(0, -20);
                btnMon.TextOffset = new Point(0, 35);
            }

            int sizeNutNho = 30;
            Guna2Button btnPlus = new Guna2Button() { Text = "+", Font = new Font("Arial", 12F, FontStyle.Bold), Width = sizeNutNho, Height = sizeNutNho, BorderRadius = sizeNutNho / 2, FillColor = Color.Transparent, UseTransparentBackground = true, BorderThickness = 1, BorderColor = Color.White, ForeColor = Color.White, Padding = new Padding(0), Visible = false, Location = new Point(btnMon.Width - sizeNutNho - 5, 15), Tag = btnMon };
            Guna2Button btnMinus = new Guna2Button() { Text = "", Width = sizeNutNho, Height = sizeNutNho, BorderRadius = sizeNutNho / 2, FillColor = Color.Transparent, UseTransparentBackground = true, BorderThickness = 1, BorderColor = Color.White, Visible = false, Location = new Point(btnMon.Width - sizeNutNho - 5, btnPlus.Bottom + 5), Tag = btnMon };
            btnMinus.Paint += (senderBtn, eventArgs) => { Graphics g = eventArgs.Graphics; g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; using (Pen p = new Pen(Color.White, 2)) { int tamX = btnMinus.Width / 2; int tamY = btnMinus.Height / 2; g.DrawLine(p, tamX - 6, tamY, tamX + 6, tamY); } };

            btnMon.MouseEnter += (s, e) => { btnPlus.Visible = btnMinus.Visible = true; };
            btnMon.MouseLeave += (s, e) => { Point mousePos = btnMon.PointToClient(Cursor.Position); if (!btnMon.ClientRectangle.Contains(mousePos)) btnPlus.Visible = btnMinus.Visible = false; };

            btnPlus.Click += TangSoLuong_Click; btnMinus.Click += GiamSoLuong_Click; btnMon.DoubleClick += (s, e) => { TangSoLuong_Click(btnPlus, e); };
            btnMon.Controls.Add(btnPlus); btnMon.Controls.Add(btnMinus);

            return btnMon;
        }

        // ==============================================================
        // TÍNH NĂNG THÊM MÓN MỚI (LƯU VÀO BỘ NHỚ)
        // ==============================================================
        private void picUpHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picUpHinh.Image = new Bitmap(open.FileName);
                picUpHinh.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text.Trim();
            string danhMucChon = cmbDanhMuc.Text;

            // Tự động quét tìm ô nhập giá (không cần biết ní đặt tên ID là gì, miễn nó ở đó)
            double giaTien = 0;
            foreach (Control ctrl in guna2GroupBox2.Controls)
            {
                if (ctrl is Guna2TextBox txt && txt != txtTenMon)
                {
                    double.TryParse(txt.Text, out giaTien);
                    break;
                }
            }

            if (string.IsNullOrEmpty(tenMon) || giaTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập đủ Tên món và Giá tiền!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            // LƯU VÀO BỘ NHỚ VĨNH CỬU
            danhSachMonTuThem.Add((tenMon, giaTien, danhMucChon, picUpHinh.Image));

            // Chuyển màn hình sang đúng danh mục đó để hiển thị liền cho nhân viên coi
            LoadMenuThucAn(danhMucChon);

            // Dọn dẹp form để gõ món khác
            txtTenMon.Clear();
            foreach (Control ctrl in guna2GroupBox2.Controls) if (ctrl is Guna2TextBox txt && txt != txtTenMon) txt.Clear();
            picUpHinh.Image = null;

            MessageBox.Show($"Thêm món [{tenMon}] vào danh mục [{danhMucChon}] thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==============================================================
        // XỬ LÝ GIỎ HÀNG & THANH TOÁN
        // ==============================================================
        private void TangSoLuong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbSelectPC.Text)) { MessageBox.Show("Vui lòng chọn Máy (PC) trước khi gọi món!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); cmbSelectPC.Focus(); return; }
            Guna2Button btnPlus = sender as Guna2Button; if (btnPlus == null || btnPlus.Tag == null) return;
            Guna2Button btnMonCha = btnPlus.Tag as Guna2Button; if (btnMonCha == null || btnMonCha.Tag == null) return;

            dynamic data = btnMonCha.Tag; string tenMon = data.Ten; double donGia = data.Gia; bool daTonTai = false;

            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells["colTenMon"].Value != null && row.Cells["colTenMon"].Value.ToString() == tenMon)
                {
                    int soLuongMoi = Convert.ToInt32(row.Cells["colSoLuong"].Value) + 1;
                    row.Cells["colSoLuong"].Value = soLuongMoi; row.Cells["colThanhTien"].Value = soLuongMoi * donGia; daTonTai = true; break;
                }
            }
            if (!daTonTai) dgvDonHang.Rows.Add(tenMon, 1, donGia, donGia);
            TinhTongTien();
        }

        private void GiamSoLuong_Click(object sender, EventArgs e)
        {
            Guna2Button btnMinus = sender as Guna2Button; if (btnMinus == null || btnMinus.Tag == null) return;
            Guna2Button btnMonCha = btnMinus.Tag as Guna2Button; if (btnMonCha == null || btnMonCha.Tag == null) return;
            dynamic data = btnMonCha.Tag; string tenMon = data.Ten; double donGia = data.Gia;

            foreach (DataGridViewRow row in dgvDonHang.Rows)
            {
                if (row.Cells["colTenMon"].Value != null && row.Cells["colTenMon"].Value.ToString() == tenMon)
                {
                    int soLuongCu = Convert.ToInt32(row.Cells["colSoLuong"].Value);
                    if (soLuongCu > 1) { int slMoi = soLuongCu - 1; row.Cells["colSoLuong"].Value = slMoi; row.Cells["colThanhTien"].Value = slMoi * donGia; }
                    else dgvDonHang.Rows.Remove(row);
                    break;
                }
            }
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            tongTienDonHang = 0;
            foreach (DataGridViewRow row in dgvDonHang.Rows)
                if (row.Cells["colThanhTien"].Value != null) tongTienDonHang += Convert.ToDouble(row.Cells["colThanhTien"].Value);
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.Rows.Count > 0 && MessageBox.Show("Hủy toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { dgvDonHang.Rows.Clear(); TinhTongTien(); }
        }

        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {
            string pcThanhToan = cmbSelectPC.Text.Trim();
            if (string.IsNullOrEmpty(pcThanhToan)) { MessageBox.Show("Bạn chưa chọn máy (PC)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (dgvDonHang.Rows.Count == 0 || tongTienDonHang == 0) { MessageBox.Show("Giỏ hàng trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show($"Xác nhận thêm các món này vào hóa đơn của {pcThanhToan}?", "Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string chiTietDon = "Gọi món: ";
                    foreach (DataGridViewRow row in dgvDonHang.Rows)
                        if (row.Cells["colTenMon"].Value != null) chiTietDon += $"{row.Cells["colTenMon"].Value}(x{row.Cells["colSoLuong"].Value}), ";

                    DataService db = new DataService();

                    db.Execute("UPDATE TrangThaiMay SET TienDichVu = TienDichVu + @tien WHERE TenMay = @may",
                                new MySqlParameter[] { new MySqlParameter("@tien", tongTienDonHang), new MySqlParameter("@may", pcThanhToan) });

                    string sql = "INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES (@time, @mota, @user, @phi, 0, 'Dịch Vụ (Đang nợ)')";
                    db.Execute(sql, new MySqlParameter[] {
                        new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@mota", chiTietDon),
                        new MySqlParameter("@user", pcThanhToan), new MySqlParameter("@phi", tongTienDonHang)
                    });

                    MessageBox.Show($"Đã Order thành công cho {pcThanhToan}!\nSố tiền {tongTienDonHang:#,##0}đ đã được cộng vào Bill của khách.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgvDonHang.Rows.Clear();
                    TinhTongTien();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}