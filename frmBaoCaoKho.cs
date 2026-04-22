using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;

namespace QuanLyQuanNet
{
    public partial class frmBaoCaoKho : Form
    {
        DataService db = new DataService();

        public frmBaoCaoKho()
        {
            InitializeComponent();
        }

        private void frmBaoCaoKho_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Múc dữ liệu thật từ Database MySQL lên
                string sql = "SELECT MaHang, TenHang, DanhMuc, TonKho, DVT, GiaNhap FROM KhoHang";
                DataTable dtLichSu = db.GetTable(sql);

                // 2. Dọn dẹp cái máy in (ReportViewer) cho sạch sẽ trước khi in
                reportViewer1.LocalReport.DataSources.Clear();

                // 3. Đổ mực vào máy in. 
                // ⚠️ CHÚ Ý: Chữ "DataSetKhoHang" PHẢI TRÙNG KHỚP 100% với tên Dataset ní đã đặt trong lúc thiết kế file rptKhoHang.rdlc
                ReportDataSource rds = new ReportDataSource("DataSetKhoHang", dtLichSu);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // 4. Bỏ tờ giấy (file thiết kế .rdlc) vào máy in
                reportViewer1.LocalReport.ReportPath = "rptKhoHang.rdlc";

                // 5. Cài đặt hiển thị bản in rõ nét 100%
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

                // 6. Ra lệnh xuất bản
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}