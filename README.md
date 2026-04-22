# QuanLyQuanNet

Ứng dụng quản lý quán net bằng WinForms (.NET Framework 4.8) với MySQL làm cơ sở dữ liệu.

## Giới thiệu

`QuanLyQuanNet` là phần mềm quản lý quán net bao gồm:
- Đăng nhập bằng tài khoản MySQL
- Quản lý trạng thái máy và giao dịch khách hàng
- Quản lý kho hàng và dịch vụ
- Quản lý khách hàng và giao ca
- Báo cáo và thống kê doanh thu

## Yêu cầu

- Windows
- Visual Studio (phiên bản hỗ trợ .NET Framework 4.8)
- .NET Framework 4.8
- MySQL Server
- MySQL Workbench hoặc công cụ tương đương

## Cấu hình cơ sở dữ liệu

1. Mở MySQL Workbench hoặc MySQL client.
2. Import file `QLQN.sql` trong thư mục gốc để tạo database `QuanLyQuanNet` và các bảng cần thiết.
3. Chuỗi kết nối mặc định trong mã nguồn:

   - `Server=localhost; Database=QuanLyQuanNet; Uid=root; Pwd=123456;`

4. Nếu tài khoản hoặc mật khẩu MySQL khác, hãy sửa lại trong các file sau:

   - `DataService.cs`
   - `frmDangNhap.cs`
   - `Form1.cs`

## Cài đặt và chạy

1. Mở `QuanLyQuanNet.sln` bằng Visual Studio.
2. Chạy lệnh Restore NuGet packages nếu cần (Visual Studio sẽ tự động restore khi build nếu thiết lập đúng).
3. Build project.
4. Chạy ứng dụng. Màn hình đầu tiên sẽ hiển thị form đăng nhập (`frmDangNhap`).

## Đăng nhập

- Ứng dụng sử dụng bảng `TaiKhoan` trong database `QuanLyQuanNet`.
- Nếu chưa có tài khoản, tạo bằng cách thêm bản ghi vào bảng `TaiKhoan`.
- Mặc định, tài khoản root MySQL được dùng để kết nối cơ sở dữ liệu; tài khoản phần mềm nằm trong bảng `TaiKhoan`.

## Chức năng chính

### 1. Đăng nhập

- Nhập `Username` và `Password` từ bảng `TaiKhoan`.
- Nếu đúng, ứng dụng mở Form chính (`Form1`).

### 2. Quản lý máy

- Màn hình chính chứa danh sách máy PC.
- Chọn máy và bấm `Bắt đầu` để chuyển máy sang trạng thái có khách.
- Bấm `Thanh toán` để tính tiền theo thời gian và dịch vụ.
- Trạng thái máy được lưu vào bảng `TrangThaiMay`.

### 3. Quản lý kho hàng

- Form `Kho` để quản lý vật tư, hàng hóa.
- Nhập xuất tồn kho, cập nhật số lượng, giá nhập.

### 4. Quản lý dịch vụ

- Form `frmDichVu` để quản lý dịch vụ ăn uống và các dịch vụ kèm theo.
- Dịch vụ có thể được cộng vào tiền khách hàng.

### 5. Quản lý khách hàng

- Control `ucKhachHang` dùng để quản lý thông tin khách hàng nếu cần lưu trữ riêng.

### 6. Giao ca

- Control `GiaoCa` giúp ghi nhận giao ca giữa nhân viên.
- Lưu lại người giao, người nhận, ghi chú và trạng thái kiểm kê.

### 7. Báo cáo và thống kê

- `ThongKe` hiển thị biểu đồ doanh thu, số máy và số khách theo giờ.
- `frmBaoCaoKho` để in ấn báo cáo kho hàng.

## Lưu ý

- Nếu ứng dụng không kết nối được database, kiểm tra:
  - MySQL Server đã chạy chưa
  - Chuỗi kết nối chính xác
  - Database `QuanLyQuanNet` đã tồn tại
- Mã nguồn hiện tại dùng kết nối cố định tới `localhost`.
- Khuyến cáo thay đổi `Uid` và `Pwd` phù hợp với môi trường.

## Cấu trúc thư mục chính

- `Program.cs`: điểm vào của ứng dụng, mở form đăng nhập.
- `frmDangNhap.cs`: form đăng nhập.
- `Form1.cs`: form chính sau khi đăng nhập.
- `DataService.cs`: lớp quản lý kết nối và truy vấn MySQL.
- `QLQN.sql`: file script tạo database.
- `frmDichVu.cs`, `Kho.cs`, `GiaoCa.cs`, `ThongKe.cs`, `frmBaoCaoKho.cs`: các chức năng chính.

---

Nếu cần hướng dẫn chi tiết hơn cho một chức năng cụ thể, hãy cho biết chức năng muốn mô tả.
