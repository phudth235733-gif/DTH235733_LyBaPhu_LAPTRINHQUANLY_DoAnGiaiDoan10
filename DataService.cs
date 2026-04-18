using System;
using System.Data;
using MySql.Data.MySqlClient; // KHÁC BIỆT 1: Thư viện của MySQL

namespace QuanLyQuanNet
{
    public class DataService
    {
        // KHÁC BIỆT 2: Chuỗi kết nối của MySQL. 
        // BẠN PHẢI SỬA LẠI Uid (Tài khoản) và Pwd (Mật khẩu) CHO KHỚP VỚI MYSQL WORKBENCH CỦA BẠN.
        // Mặc định tài khoản thường là "root".
        private string strCon = "Server=localhost; Database=QuanLyQuanNet; Uid=root; Pwd=123456;";

        // Hàm 1: Thực thi lệnh INSERT, UPDATE, DELETE
        public void Execute(string query, MySqlParameter[] parameters = null)
        {
            // Dùng MySqlConnection thay vì SqlConnection
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Hàm 2: Lấy dữ liệu (SELECT) đổ vào bảng
        public DataTable GetTable(string query, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(strCon))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}