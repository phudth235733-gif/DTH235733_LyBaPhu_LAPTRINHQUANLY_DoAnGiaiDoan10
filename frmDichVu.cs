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

        public frmDichVu(string tenMay = "")
        {
            InitializeComponent();
            mayGoiMenu = tenMay;
            cmbSelectPC.DropDown += cmbSelectPC_DropDown;
            btnThanhToan.Text = "Xác Nhận Order";
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            LoadDanhSachPCDangMo();
            if (!string.IsNullOrEmpty(mayGoiMenu))
            {
                if (!cmbSelectPC.Items.Contains(mayGoiMenu)) cmbSelectPC.Items.Add(mayGoiMenu);
                cmbSelectPC.Text = mayGoiMenu;
            }
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

        private void btnDoUong_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Uống");
        private void btnDoAn_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Ăn");
        private void btnDoDung_Click(object sender, EventArgs e) => LoadMenuThucAn("Đồ Dùng");
        private void btnNapThe_Click(object sender, EventArgs e) => LoadMenuThucAn("Nạp Thẻ");
        private void btnComBo_Click_1(object sender, EventArgs e) => LoadMenuThucAn("Combo");

        private void LoadMenuThucAn(string danhMuc)
        {
            flpMenu.Controls.Clear();
            List<(string Ten, double Gia, string TenHinh)> dsMon = new List<(string, double, string)>();

            switch (danhMuc)
            {
                case "Đồ Uống":
                    dsMon.Add(("Sting Đỏ", 10000, "sting")); dsMon.Add(("Bò Húc", 15000, "BoHuc"));
                    dsMon.Add(("Pepsi", 10000, "Pepsi")); dsMon.Add(("Nước Suối", 5000, "water")); dsMon.Add(("Trà Đá", 2000, "TraDa")); break;
                case "Đồ Ăn":
                    dsMon.Add(("Mì Xào Trứng", 20000, "MiXao")); dsMon.Add(("Cơm Rang", 35000, "ComRang")); dsMon.Add(("Xúc Xích", 10000, "XucXich")); break;
                case "Đồ Dùng":
                    dsMon.Add(("Khăn Lạnh", 2000, "KhanLanh")); dsMon.Add(("Bật Lửa", 5000, "BatLua")); break;
                case "Nạp Thẻ":
                    dsMon.Add(("Thẻ 20K", 20000, "The20k")); dsMon.Add(("Thẻ 50K", 50000, "The50k")); dsMon.Add(("Thẻ 100K", 100000, "The50k")); break;
                case "Combo":
                    dsMon.Add(("Mì + Sting", 28000, "ComBo1")); dsMon.Add(("Trà đá 4 ống hút", 8000, "ComBo2")); dsMon.Add(("Sting 4 tẩy đá", 16000, "ComBo3")); break;
            }

            foreach (var mon in dsMon)
            {
                Guna2Button btnMon = new Guna2Button() { Text = mon.Ten + "\n" + mon.Gia.ToString("#,##0") + "đ", Width = 140, Height = 160, Margin = new Padding(10), BorderRadius = 10, FillColor = Color.FromArgb(40, 45, 50), ForeColor = Color.White, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Tag = new { Ten = mon.Ten, Gia = mon.Gia } };
                object imgObj = Properties.Resources.ResourceManager.GetObject(mon.TenHinh);
                if (imgObj != null) { btnMon.Image = (Image)imgObj; btnMon.ImageSize = new Size(80, 80); btnMon.ImageAlign = HorizontalAlignment.Center; btnMon.TextAlign = HorizontalAlignment.Center; btnMon.ImageOffset = new Point(0, -20); btnMon.TextOffset = new Point(0, 35); }

                int sizeNutNho = 30;
                Guna2Button btnPlus = new Guna2Button() { Text = "+", Font = new Font("Arial", 12F, FontStyle.Bold), Width = sizeNutNho, Height = sizeNutNho, BorderRadius = sizeNutNho / 2, FillColor = Color.Transparent, UseTransparentBackground = true, BorderThickness = 1, BorderColor = Color.White, ForeColor = Color.White, Padding = new Padding(0), Visible = false, Location = new Point(btnMon.Width - sizeNutNho - 5, 15), Tag = btnMon };
                Guna2Button btnMinus = new Guna2Button() { Text = "", Width = sizeNutNho, Height = sizeNutNho, BorderRadius = sizeNutNho / 2, FillColor = Color.Transparent, UseTransparentBackground = true, BorderThickness = 1, BorderColor = Color.White, Visible = false, Location = new Point(btnMon.Width - sizeNutNho - 5, btnPlus.Bottom + 5), Tag = btnMon };
                btnMinus.Paint += (senderBtn, eventArgs) => { Graphics g = eventArgs.Graphics; g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; using (Pen p = new Pen(Color.White, 2)) { int tamX = btnMinus.Width / 2; int tamY = btnMinus.Height / 2; g.DrawLine(p, tamX - 6, tamY, tamX + 6, tamY); } };

                btnMon.MouseEnter += (s, e) => { btnPlus.Visible = btnMinus.Visible = true; };
                btnMon.MouseLeave += (s, e) => { Point mousePos = btnMon.PointToClient(Cursor.Position); if (!btnMon.ClientRectangle.Contains(mousePos)) btnPlus.Visible = btnMinus.Visible = false; };

                btnPlus.Click += TangSoLuong_Click; btnMinus.Click += GiamSoLuong_Click; btnMon.DoubleClick += (s, e) => { TangSoLuong_Click(btnPlus, e); };
                btnMon.Controls.Add(btnPlus); btnMon.Controls.Add(btnMinus); flpMenu.Controls.Add(btnMon);
            }
        }

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

        // ==============================================================
        // ĐÃ XÓA LỆNH THOÁT FORM. CHỈ GHI NỢ VÀ XÓA GIỎ HÀNG
        // ==============================================================
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

                    // CỘNG TIỀN VÀO CỘT TIỀN DỊCH VỤ CỦA MÁY
                    db.Execute("UPDATE TrangThaiMay SET TienDichVu = TienDichVu + @tien WHERE TenMay = @may",
                                new MySqlParameter[] { new MySqlParameter("@tien", tongTienDonHang), new MySqlParameter("@may", pcThanhToan) });

                    // GHI NỢ VÀO SỔ GIAO DỊCH
                    string sql = "INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES (@time, @mota, @user, @phi, 0, 'Dịch Vụ (Đang nợ)')";
                    db.Execute(sql, new MySqlParameter[] {
                        new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@mota", chiTietDon),
                        new MySqlParameter("@user", pcThanhToan), new MySqlParameter("@phi", tongTienDonHang)
                    });

                    MessageBox.Show($"Đã Order thành công cho {pcThanhToan}!\nSố tiền {tongTienDonHang:#,##0}đ đã được cộng vào Bill của khách.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Chỉ xóa giỏ hàng để order tiếp, không đóng form nữa!
                    dgvDonHang.Rows.Clear();
                    TinhTongTien();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi Database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}