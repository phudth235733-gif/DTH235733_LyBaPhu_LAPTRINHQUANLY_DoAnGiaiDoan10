-- ==============================================================
-- 1. TẠO DATABASE (Hỗ trợ 100% Tiếng Việt có dấu)
-- ==============================================================
CREATE DATABASE IF NOT EXISTS QuanLyQuanNet CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE QuanLyQuanNet;

-- ==============================================================
-- 2. TẠO BẢNG VÀ BƠM DỮ LIỆU KHÁCH HÀNG (Bản chuẩn đã gộp các cột)
-- ==============================================================
DROP TABLE IF EXISTS KhachHang; -- Xóa bảng cũ bị chắp vá đi để tạo lại cho sạch

CREATE TABLE KhachHang (
    Username VARCHAR(50) PRIMARY KEY,
    MaKH VARCHAR(50) UNIQUE NOT NULL, -- Cột Mã Khách Hàng mới
    Password VARCHAR(50) NOT NULL,
    TenKH VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    SDT VARCHAR(15),
    Email VARCHAR(100),
    CCCD VARCHAR(20),
    DiaChi VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    GioiTinh VARCHAR(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    NgaySinh DATE,
    LoaiTaiKhoan VARCHAR(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    TrangThai VARCHAR(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    SoDu DOUBLE DEFAULT 0,
    TongGioChoi DOUBLE DEFAULT 0,
    TongTienNap DOUBLE DEFAULT 0,
    LanDangNhapCuoi DATETIME NULL,
    PCThuongDung VARCHAR(20) DEFAULT 'Chưa rõ' -- Cột PC Thường dùng
);

-- Bơm 10 tài khoản khách hàng (ĐÃ TÍNH SẴN MÃ KH = 4 số cuối CCCD + 2 số Ngày Sinh)
INSERT INTO KhachHang (Username, MaKH, Password, TenKH, SDT, Email, CCCD, DiaChi, GioiTinh, NgaySinh, LoaiTaiKhoan, TrangThai, SoDu, TongGioChoi, TongTienNap, LanDangNhapCuoi, PCThuongDung) VALUES
('hoivien01', '111101', '123456', 'Nguyễn Văn Tèo', '0901111111', 'teo@gmail.com', '079111111111', 'Hà Nội', 'Nam', '2000-01-01', 'VIP', 'Hoạt động', 100000, 15, 200000, '2026-04-06 08:30:00', 'PC 05'),
('hoivien02', '222210', '123456', 'Trần Thị Nở', '0902222222', 'no@gmail.com', '079222222222', 'HCM', 'Nữ', '1999-05-10', 'Thường', 'Hoạt động', 20000, 5, 50000, '2026-04-05 19:15:00', 'PC 10'),
('hoivien03', '333315', '123456', 'Lê Lợi', '0903333333', 'loi@gmail.com', '079333333333', 'Đà Nẵng', 'Nam', '2001-08-15', 'Thường', 'Hoạt động', 5000, 2, 20000, '2026-04-04 14:00:00', 'PC 01'),
('hoivien04', '444420', '123456', 'Phạm Cự Lượng', '0904444444', 'pham@gmail.com', '079444444444', 'Cần Thơ', 'Nam', '2002-12-20', 'VIP', 'Hoạt động', 500000, 100, 1000000, '2026-04-06 10:45:00', 'PC 08'),
('hoivien05', '555525', '123456', 'Vũ Văn Thanh', '0905555555', 'thanh@gmail.com', '079555555555', 'Hải Phòng', 'Nam', '1998-03-25', 'Thường', 'Hoạt động', 0, 12, 100000, '2026-04-02 20:00:00', 'PC 15'),
('hoivien06', '666607', '123456', 'Hoàng Hoa', '0906666666', 'hoa@gmail.com', '079666666666', 'Nghệ An', 'Nữ', '2003-07-07', 'Thường', 'Hoạt động', 15000, 8, 80000, '2026-04-01 09:30:00', 'PC 03'),
('hoivien07', '777702', '123456', 'Đinh Bộ Lĩnh', '0907777777', 'linh@gmail.com', '079777777777', 'Ninh Bình', 'Nam', '1995-09-02', 'VIP', 'Hoạt động', 250000, 60, 800000, '2026-04-06 13:20:00', 'PC 12'),
('hoivien08', '888811', '123456', 'Lý Thường Kiệt', '0908888888', 'kiet@gmail.com', '079888888888', 'Hà Tĩnh', 'Nam', '1990-11-11', 'VIP', 'Hoạt động', 120000, 45, 500000, '2026-03-28 16:10:00', 'PC 20'),
('hoivien09', '999928', '123456', 'Ngô Quyền', '0909999999', 'quyen@gmail.com', '079999999999', 'Hải Phòng', 'Nam', '1992-02-28', 'Thường', 'Hoạt động', 30000, 10, 150000, '2026-04-03 11:00:00', 'PC 07'),
('hoivien10', '010106', '123456', 'Hai Bà Trưng', '0910101010', 'trung@gmail.com', '079101010101', 'Vĩnh Phúc', 'Nữ', '1996-06-06', 'VIP', 'Hoạt động', 80000, 30, 400000, '2026-04-06 15:00:00', 'PC 25');

-- ==============================================================
-- 3. TẠO BẢNG VÀ BƠM DỮ LIỆU GIAO DỊCH
-- ==============================================================
DROP TABLE IF EXISTS GiaoDich;

CREATE TABLE GiaoDich (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    ThoiGian DATETIME DEFAULT CURRENT_TIMESTAMP,
    MoTa VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    ThanhVien VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
    CuocPhi DOUBLE,
    ThanhToan DOUBLE,
    Nguon VARCHAR(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci
);

INSERT INTO GiaoDich (ThoiGian, MoTa, ThanhVien, CuocPhi, ThanhToan, Nguon) VALUES
('2026-03-31 08:15:00', 'Tiền giờ PC01', 'Khách lẻ', 15000, 15000, 'PC'),
('2026-03-31 10:30:00', 'Sting Đỏ (x2)', 'PC05', 20000, 20000, 'Dịch Vụ'),
('2026-03-31 14:00:00', 'Mì xào trứng', 'PC12', 25000, 25000, 'Dịch Vụ'),
('2026-03-30 09:00:00', 'Nạp thẻ hội viên', 'hoivien01', 100000, 100000, 'Nạp Thẻ'),
('2026-03-30 19:45:00', 'Tiền giờ PC08', 'Khách lẻ', 30000, 30000, 'PC'),
('2026-03-25 12:00:00', 'Combo Mì + Sting', 'PC03', 35000, 35000, 'Dịch Vụ'),
('2026-03-15 16:30:00', 'Tiền giờ PC10', 'Khách lẻ', 40000, 40000, 'PC'),
('2026-02-28 20:00:00', 'Nạp thẻ 50k', 'hoivien02', 50000, 50000, 'Nạp Thẻ'),
('2026-02-14 10:00:00', 'Bò húc', 'PC01', 15000, 15000, 'Dịch Vụ');

-- ==============================================================
-- 4. TẠO BẢNG VÀ BƠM DỮ LIỆU TRẠNG THÁI MÁY
-- ==============================================================
DROP TABLE IF EXISTS TrangThaiMay;

CREATE TABLE TrangThaiMay (
    TenMay VARCHAR(20) PRIMARY KEY,
    TrangThai VARCHAR(20) DEFAULT 'Trong',
    ThoiGianBatDau DATETIME NULL,
    TienTraTruoc DOUBLE DEFAULT 0,
    NguoiDung VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL
);

-- Bơm dữ liệu: PC 05 chạy được 2 tiếng, PC 15 vừa mới chạy. (Đã xóa PC 10 như ý ní)
INSERT INTO TrangThaiMay (TenMay, TrangThai, ThoiGianBatDau, TienTraTruoc, NguoiDung) VALUES 
('PC 05', 'CoKhach', DATE_SUB(NOW(), INTERVAL 2 HOUR), 50000, 'hoivien01'),
('PC 15', 'CoKhach', NOW(), 10000, 'Khách vãng lai');

CREATE TABLE IF NOT EXISTS TaiKhoan (
    Username VARCHAR(50) PRIMARY KEY,
    Password VARCHAR(50),
    Quyen VARCHAR(50),      -- 'Chủ Quán' hoặc 'Nhân Viên'
    HoTen VARCHAR(100),     -- Tên hiển thị ra màn hình
    DuongDanAnh VARCHAR(255) -- Link trỏ tới ổ đĩa
);

-- Nhét 4 tài khoản có sẵn vào DataBase
INSERT IGNORE INTO TaiKhoan (Username, Password, Quyen, HoTen, DuongDanAnh) VALUES
('admin', '123', 'Chủ Quán', 'Sếp Tổng', 'C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\admin.jpg'),
('nv1', 'nv001', 'Nhân Viên', 'Nguyễn Văn A', 'C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\nv1.jpg'),
('nv2', 'nv002', 'Nhân Viên', 'Trần Thị B', 'C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\nv2.jpg'),
('nv3', 'nv003', 'Nhân Viên', 'Lê Văn C', 'C:\Users\MyPC\OneDrive\Pictures\Lập Trình Quản Lý\New folder\nv3.jpg');