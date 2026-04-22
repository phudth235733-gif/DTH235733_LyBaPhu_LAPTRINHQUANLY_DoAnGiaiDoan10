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

        class ThongTinNhanVien
        {
            public string HoTen;
            public string DuongDanAnh;
        }

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
            cmbGiaoCa.SelectedIndexChanged += cmbGiaoCa_SelectedIndexChanged;
            cmbNhanCa.SelectedIndexChanged += cmbNhanCa_SelectedIndexChanged;
            btnXacNhan.Click += btnXacNhan_Click;
        }

        public void ThietLapThongTin(string nhanVienTruc, DateTime thoiGianVao)
        {
            tenNhanVienDangTruc = nhanVienTruc != null ? nhanVienTruc.Trim() : "";
            gioVaoCa = thoiGianVao;
            LoadThongTinCa();
        }

        private void LoadThongTinCa()
        {
            cmbGiaoCa.Items.Clear();
            string userAutoSelect = "";

            foreach (var kvp in dsTaiKhoan)
            {
                cmbGiaoCa.Items.Add(kvp.Key);
                if (kvp.Value.HoTen.Trim() == tenNhanVienDangTruc || kvp.Key.Trim() == tenNhanVienDangTruc)
                {
                    userAutoSelect = kvp.Key;
                }
            }

            if (!string.IsNullOrEmpty(userAutoSelect))
            {
                cmbGiaoCa.SelectedItem = userAutoSelect;
                cmbGiaoCa.Enabled = false;
            }
            else if (cmbGiaoCa.Items.Count > 0)
            {
                cmbGiaoCa.SelectedIndex = 0;
                cmbGiaoCa.Enabled = false;
            }
            TinhToanDoanhThuHeThong();
        }

        private void HienThiAnh(PictureBox pic, string duongDan)
        {
            if (pic == null) return;
            try { if (File.Exists(duongDan)) { pic.Image = Image.FromFile(duongDan); pic.SizeMode = PictureBoxSizeMode.Zoom; } else pic.Image = null; }
            catch { pic.Image = null; }
        }

        private void cmbGiaoCa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGiaoCa.SelectedItem == null) return;
            string nguoiGiao = cmbGiaoCa.SelectedItem.ToString();

            if (dsTaiKhoan.ContainsKey(nguoiGiao))
            {
                txtGiaoCa.Text = dsTaiKhoan[nguoiGiao].HoTen;
                HienThiAnh(picGiaoCa, dsTaiKhoan[nguoiGiao].DuongDanAnh);
            }

            string nguoiNhanHienTai = cmbNhanCa.Text;
            cmbNhanCa.Items.Clear();
            foreach (var nv in dsTaiKhoan.Keys)
            {
                if (nv != nguoiGiao) cmbNhanCa.Items.Add(nv);
            }

            if (cmbNhanCa.Items.Contains(nguoiNhanHienTai)) cmbNhanCa.SelectedItem = nguoiNhanHienTai;
            else if (cmbNhanCa.Items.Count > 0) cmbNhanCa.SelectedIndex = 0;
        }

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

                    double tongDoanhThu = 0, tongChuyenKhoan = 0, tongTienMat = 0;

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            double tien = Convert.ToDouble(r["ThanhToan"]);
                            string nguon = r["Nguon"].ToString();
                            tongDoanhThu += tien;
                            if (nguon.Contains("Chuyển khoản") || nguon.Contains("QR")) tongChuyenKhoan += tien;
                            else tongTienMat += tien;
                        }
                    }
                    txtDTTC.Text = tongDoanhThu.ToString("#,##0");
                    txtChuyenKhoan.Text = tongChuyenKhoan.ToString("#,##0");
                    txtTienMat.Text = tongTienMat.ToString("#,##0");
                }
            }
            catch { }
        }

        // 👉 ĐÃ SỬA: CHỈ QUÉT LẤY NHỮNG MỤC CHƯA ĐƯỢC CHECK (Để báo cáo ca sau)
        private void QuetCheckBox(Control parent, ref string viecChuaLam)
        {
            foreach (Control c in parent.Controls)
            {
                // Dấu ! nghĩa là "Chưa Check"
                if (c is Guna.UI2.WinForms.Guna2CheckBox cb && !cb.Checked)
                {
                    viecChuaLam += $"- {cb.Text}\n";
                }
                if (c.HasChildren)
                {
                    QuetCheckBox(c, ref viecChuaLam);
                }
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNhanCa.Text))
            {
                MessageBox.Show("Vui lòng chọn người nhận ca!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThaiKiemKe = "";
            QuetCheckBox(this, ref trangThaiKiemKe); // Nhặt mấy mục chưa làm vào đây

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
                        cmd.Parameters.AddWithValue("@Giao", txtGiaoCa.Text);
                        cmd.Parameters.AddWithValue("@Nhan", txtNhanCa.Text); // Lưu Họ tên người nhận ca
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