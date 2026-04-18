using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace QuanLyQuanNet
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTaiKhoan.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=QuanLyQuanNet;Uid=root;Pwd=123456;"))
                {
                    conn.Open();
                    string sql = "SELECT * FROM TaiKhoan WHERE Username = @User AND Password = @Pass";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@User", user);
                    cmd.Parameters.AddWithValue("@Pass", pass);

                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read()) // Nếu đọc được tức là đúng tài khoản mật khẩu
                        {
                            string quyen = r["Quyen"].ToString();
                            string hoTen = r["HoTen"].ToString();
                            string linkAnh = r["DuongDanAnh"].ToString();

                            // Bắn dữ liệu sang Form1
                            Form1 mainForm = new Form1(this, quyen, hoTen, linkAnh);
                            mainForm.Show();
                            this.Hide(); // Giấu form Login đi
                        }
                        else
                        {
                            MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DataBase: " + ex.Message);
            }
        }

        // Đảm bảo khi bấm dấu X tắt form đăng nhập thì phần mềm tắt hẳn luôn
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    
}