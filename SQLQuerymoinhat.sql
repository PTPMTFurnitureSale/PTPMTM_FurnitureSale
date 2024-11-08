tui gửi lại cái database tui sửa rồi
-- Tạo cơ sở dữ liệu
CREATE DATABASE QuanLyBanHang COLLATE Vietnamese_CI_AS;
GO


USE QuanLyBanHang;
GO

-- Bảng nhân viên
CREATE TABLE nhan_vien (
    id_nhan_vien NVARCHAR(36) PRIMARY KEY,
    ten_nhan_vien NVARCHAR(200),
    tai_khoan NVARCHAR(200),
    mat_khau NVARCHAR(200),
    ngay_sinh DATE,
    dia_chi NVARCHAR(200),
    dien_thoai NVARCHAR(20),
    chuc_vu NVARCHAR(200),
    id_loai_nhan_vien NVARCHAR(36) -- Chỉnh sửa kiểu dữ liệu cho đồng nhất
);

-- Bảng loại nhân viên
CREATE TABLE loai_nhan_vien (
    id_loai_nhan_vien NVARCHAR(36) PRIMARY KEY,
    ten_loai_nhan_vien NVARCHAR(200)
);

-- Bảng khách hàng
CREATE TABLE khach_hang (
    id_khach_hang NVARCHAR(36) PRIMARY KEY,
    ten_khach_hang NVARCHAR(200),
    ngay_sinh DATE,
    dia_chi NVARCHAR(200),
    email NVARCHAR(200),
    dien_thoai NVARCHAR(20),
    tai_khoan NVARCHAR(200),
    mat_khau NVARCHAR(200),
	dia_chi_giao_hang NVARCHAR(200) DEFAULT 'Không'

);

-- Bảng sản phẩm
CREATE TABLE san_pham (
    id_san_pham NVARCHAR(36) PRIMARY KEY,
    ten_san_pham NVARCHAR(200),
    don_gia FLOAT,
    diem FLOAT,
    hinh NVARCHAR(500),
    mo_ta NVARCHAR(500),
    id_loai NVARCHAR(36), -- Chỉnh sửa kiểu dữ liệu cho đồng nhất
	

);

-- Bảng loại sản phẩm
CREATE TABLE loai_san_pham (
    id_loai NVARCHAR(36) PRIMARY KEY,
    ten_loai NVARCHAR(200),
    hinh NVARCHAR(200)
);

-- Bảng hóa đơn
CREATE TABLE hoa_don (
    id_hoa_don NVARCHAR(36) PRIMARY KEY,
    id_khach_hang NVARCHAR(36),
    ngay_dat DATE,
    ngay_giao_hang DATE,
    tinh_trang_thanh_toan INT,
    phuong_thuc_thanh_toan INT,
    tinh_trang_giao_hang INT,
    tong_tien FLOAT,
    FOREIGN KEY (id_khach_hang) REFERENCES khach_hang(id_khach_hang) -- Khóa ngoại
);

-- Bảng phản hồi
CREATE TABLE phan_hoi (
    id_phan_hoi NVARCHAR(36) PRIMARY KEY,
    noi_dung_phan_hoi NVARCHAR(500),
    ngay_phan_hoi DATE,
    id_khach_hang NVARCHAR(36),
    id_san_pham NVARCHAR(36),
    FOREIGN KEY (id_khach_hang) REFERENCES khach_hang(id_khach_hang), -- Khóa ngoại
    FOREIGN KEY (id_san_pham) REFERENCES san_pham(id_san_pham) -- Khóa ngoại
);

-- Bảng đánh giá
CREATE TABLE danh_gia (
    id_danh_gia NVARCHAR(36) PRIMARY KEY,
    noi_dung_danh_gia NVARCHAR(200),
    diem INT,
    ngay_danh_gia DATE,
    id_khach_hang NVARCHAR(36),
    id_san_pham NVARCHAR(36),
    FOREIGN KEY (id_khach_hang) REFERENCES khach_hang(id_khach_hang), -- Khóa ngoại
    FOREIGN KEY (id_san_pham) REFERENCES san_pham(id_san_pham) -- Khóa ngoại
);

-- Bảng khuyến mãi
CREATE TABLE khuyen_mai (
    id_khuyen_mai NVARCHAR(36) PRIMARY KEY,
    ten_khuyen_mai NVARCHAR(200),
    noi_dung_khuyen_mai NVARCHAR(200),
    phan_tram_khuyen_mai INT,
    id_san_pham NVARCHAR(36),
    FOREIGN KEY (id_san_pham) REFERENCES san_pham(id_san_pham) -- Khóa ngoại
);
-- Bảng chi tiết hóa đơn
CREATE TABLE chi_tiet_hoa_don (
    id_hoa_don NVARCHAR(36),
    id_san_pham NVARCHAR(36),
    so_luong INT,
    don_gia FLOAT,
    thanh_tien FLOAT,
    PRIMARY KEY (id_hoa_don, id_san_pham),  -- Đặt PRIMARY KEY cho cặp (id_hoa_don, id_san_pham)
    FOREIGN KEY (id_hoa_don) REFERENCES hoa_don(id_hoa_don), -- Khóa ngoại
    FOREIGN KEY (id_san_pham) REFERENCES san_pham(id_san_pham) -- Khóa ngoại
);

-- Bảng màn hình
CREATE TABLE DM_ManHinh (
    MaManHinh NVARCHAR(36) PRIMARY KEY,
    TenManHinh NVARCHAR(200)
);

-- Bảng người dùng
CREATE TABLE QL_NguoiDung (
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(200),
    HoatDong BIT
);

-- Bảng nhóm người dùng
CREATE TABLE QL_NhomNguoiDung (
    MaNhom NVARCHAR(36) PRIMARY KEY,
    TenNhom NVARCHAR(200),
    GhiChu NVARCHAR(500)
);

-- Bảng liên kết giữa người dùng và nhóm người dùng
CREATE TABLE QL_NguoiDungNhomNguoiDung (
    TenDangNhap NVARCHAR(50),
    MaNhomNguoiDung NVARCHAR(36),
    GhiChu NVARCHAR(500),
    PRIMARY KEY (TenDangNhap, MaNhomNguoiDung),
    FOREIGN KEY (TenDangNhap) REFERENCES QL_NguoiDung(TenDangNhap),
    FOREIGN KEY (MaNhomNguoiDung) REFERENCES QL_NhomNguoiDung(MaNhom)
);

-- Bảng phân quyền
CREATE TABLE QL_PhanQuyen (
    MaNhomNguoiDung NVARCHAR(36),
    MaManHinh NVARCHAR(36),
    CoQuyen BIT,
    PRIMARY KEY (MaNhomNguoiDung, MaManHinh),
    FOREIGN KEY (MaNhomNguoiDung) REFERENCES QL_NhomNguoiDung(MaNhom),
    FOREIGN KEY (MaManHinh) REFERENCES DM_ManHinh(MaManHinh)
);
GO
INSERT INTO nhan_vien (
    id_nhan_vien, ten_nhan_vien, tai_khoan, mat_khau, ngay_sinh, dia_chi, dien_thoai, chuc_vu, id_loai_nhan_vien
) VALUES 
    (N'NV001', N'Nguyễn Văn An', N'nguyenvana', N'1234', N'1990-01-01', N'123 Đường Trần Hưng Đạo, Quận 1, TP.HCM', N'0123456789', N'Nhân viên bán hàng', N'LNV01'),
    (N'NV002', N'Trần Thị Bích Ngọc', N'tranthibichngoc', N'5678', N'1992-02-02', N'234 Đường Lê Lợi, Quận 1, TP.HCM', N'0987654321', N'Quản lý', N'LNV02'),
    (N'NV003', N'Lê Văn Cường', N'levancuong', N'9101', N'1988-03-03', N'345 Đường Nguyễn Huệ, Quận 1, TP.HCM', N'0123654789', N'Nhân viên kho', N'LNV01'),
    (N'NV004', N'Phạm Thị Duyên', N'phamthiduyen', N'1122', N'1995-04-04', N'456 Đường Pasteur, Quận 3, TP.HCM', N'0981122334', N'Nhân viên bán hàng', N'LNV01'),
    (N'NV005', N'Vũ Văn Đức', N'vuvanduc', N'3344', N'1991-05-05', N'567 Đường Hai Bà Trưng, Quận 3, TP.HCM', N'0922334455', N'Quản lý', N'LNV02'),
    (N'NV006', N'Hoàng Thị Lan', N'hoangthilan', N'5566', N'1994-06-06', N'678 Đường Phan Đình Phùng, Quận Phú Nhuận, TP.HCM', N'0911223344', N'Nhân viên bán hàng', N'LNV01'),
    (N'NV007', N'Đặng Văn Giang', N'dangvangiang', N'7788', N'1989-07-07', N'789 Đường Nguyễn Kiệm, Quận Gò Vấp, TP.HCM', N'0933445566', N'Nhân viên kho', N'LNV01'),
	(N'NV008', N'Bùi Thị Hồng', N'buithihong', N'9900', N'1993-08-08', N'890 Đường Trường Chinh, Quận Tân Bình, TP.HCM', N'0944556677', N'Nhân viên bán hàng', N'LNV01'),
    (N'NV009', N'Đỗ Văn Tâm', N'dovantam', N'1235', N'1996-09-09', N'901 Đường Lý Thái Tổ, Quận 10, TP.HCM', N'0966778899', N'Nhân viên kho', N'LNV01'),
    (N'NV010', N'Ngô Thị Kim', N'ngothikim', N'4567', N'1990-10-10', N'112 Đường Hoàng Văn Thụ, Quận Phú Nhuận, TP.HCM', N'0977889900', N'Quản lý', N'LNV02');

go

INSERT INTO loai_nhan_vien (id_loai_nhan_vien, ten_loai_nhan_vien) VALUES 
    ('LNV01', N'Nhân viên bán hàng'),
    ('LNV02', N'Quản lý ứng dụng Marketing'),
    ('LNV03', N'Chuyên viên tài chính - Thanh toán'),
    ('LNV04', N'Nhân viên vận chuyển'),
    ('LNV05', N'Quản trị website'),
    ('LNV06', N'Chăm sóc khách hàng'),
    ('LNV07', N'Nhân viên kế toán'),
    ('LNV08', N'Kỹ thuật viên'),
    ('LNV09', N'Nhân viên IT'),
    ('LNV10', N'Giám đốc điều hành');

go
INSERT INTO khach_hang (id_khach_hang, ten_khach_hang, ngay_sinh, dia_chi, email, dien_thoai, tai_khoan, mat_khau) VALUES
    (N'KH001', N'Tân Việt Books', N'1980-04-15', N'123 Nguyễn Văn Cừ, Hà Nội', N'contact@tanvietbooks.vn', N'0909123456', N'tanvietbooks', N'password123'),
    (N'KH002', N'YODY', N'1992-08-25', N'456 Trần Hưng Đạo, TP.HCM', N'info@yody.vn', N'0918123456', N'yody.vn', N'yodypassword'),
    (N'KH003', N'Kangaroo Hà Nội', N'1985-05-10', N'789 Lê Lợi, Hà Nội', N'support@kangaroohanoi.vn', N'0932123456', N'kangaroo_hanoi', N'kangaroopass'),
    (N'KH004', N'Viglacera', N'1978-12-05', N'321 Điện Biên Phủ, Đà Nẵng', N'contact@viglacera.vn', N'0987123456', N'viglacera', N'viglacera123'),
    (N'KH005', N'Hồng Hà', N'1990-07-19', N'654 Hai Bà Trưng, TP.HCM', N'sales@vpphonghaonline.com.vn', N'0945123456', N'hongha', N'honghapass'),
    (N'KH006', N'Đức Việt Foods', N'1983-11-02', N'987 Quang Trung, Hà Nội', N'info@ducvietfoods.vn', N'0972123456', N'ducviet', N'ducviet1234'),
    (N'KH007', N'Lug.vn', N'1995-03-22', N'123 Lý Thái Tổ, TP.HCM', N'contact@lug.vn', N'0938123456', N'lugvn', N'lugpassword'),
    (N'KH008', N'Dirty Coins', N'1998-10-30', N'321 Nguyễn Huệ, TP.HCM', N'support@dirtycoins.vn', N'0923123456', N'dirtycoins', N'dirtypass'),
    (N'KH009', N'CLOWNZ', N'2000-01-15', N'456 Võ Thị Sáu, Hà Nội', N'info@clownz.vn', N'0916123456', N'clownz', N'clownzpass'),
    (N'KH010', N'Beemart - Đồ làm bánh', N'1993-09-12', N'789 Cách Mạng Tháng 8, TP.HCM', N'contact@beemart.vn', N'0903123456', N'beemart', N'beemartpass');

go

INSERT INTO san_pham (id_san_pham, ten_san_pham, don_gia, diem, hinh, mo_ta, id_loai) VALUES
('SP001', N'Sofa Băng Phòng Khách Truyền Thống QP115', 15000000, 4.5, N'sofa_qp115.png', 'Sofa băng phong cách truyền thống cho phòng khách', 'L01'),
	('SP002', N'Sofa Băng Bọc Vải Phong Cách Scandinavian', 18000000, 4.7, N'sofa_scandinavian.png', 'Sofa băng bọc vải phong cách Bắc Âu', 'L01'),
    ('SP003', N'Sofa Băng Bọc Vải Siêu Rộng Lewis Extra QP243', 22000000, 4.8, N'sofa_lewis.png', 'Sofa băng siêu rộng cho gia đình', 'L01'),
    ('SP004', N'Ghế Sofa Cao Cấp Shade', 25000000, 4.9, 'sofa_shade.png', N'Ghế sofa cao cấp thiết kế sang trọng', 'L01'),
    ('SP005', N'Ghế đẩu Forma', 3000000, 4.2, 'ghe_dau_forma.png', N'Ghế đẩu nhỏ gọn, tiện lợi', 'L02'),
    ('SP006', N'Ghế phòng chờ Haze', 7500000, 4.3, 'ghe_haze.png', 'NGhế phòng chờ thoải mái', 'L02'),
    ('SP007', N'Mochi Pouffe / Nhiều màu', 2000000, 4.0, 'mochi_pouffe.png', N'Ghế lười đa dạng màu sắc', 'L02'),
    ('SP008', N'Bàn bên bệ', 5000000, 4.1, 'ban_ben.png', N'Bàn nhỏ cho phòng khách', 'L03'),
    ('SP009', N'Đèn áp trần pha lê', 12000000, 4.8, 'den_ap_tran.png', N'Đèn áp trần pha lê cao cấp', 'L04'),
    ('SP010', N'Đèn chùm pha lê phòng khách', 18000000, 4.9, 'den_chum.png', N'Đèn chùm pha lê sang trọng', 'L04'),
    ('SP011', N'Tủ Giày Kệ Ngăn Có Cửa - Elona', 5000000, 4.2, 'tu_giay.png', N'Tủ giày kệ ngăn tiện dụng', 'L05'),
    ('SP012', N'Tủ Kệ Tivi Gỗ OSLO 201', 7000000, 4.3, 'tu_oslo.png', N'Tủ kệ tivi phong cách hiện đại', 'L05'),
    ('SP013', N'Tủ Kệ Tivi Gỗ HOBRO 201', 6500000, 4.4, 'tu_hobro.png', N'Tủ kệ tivi thiết kế tối giản', 'L05'),
    ('SP014', N'Tủ Kệ Tivi Gỗ KOLDING 702', 8000000, 4.5, 'tu_kolding.png', N'Tủ kệ tivi chất lượng cao', 'L05'),
    ('SP015', N'Nhà bếp với đá lát kệ bếp sang trọng', 15000000, 4.8, 'nha_bep.png', N'Thiết kế bếp với đá lát sang trọng', 'L06'),
    ('SP016', N'Tủ Kệ Tivi Gỗ Tràm VLINE 301', 8500000, 4.6, 'tu_vline.png', N'Tủ kệ tivi gỗ tràm', 'L05'),
    ('SP017', N'Đèn tường Studio', 3000000, 4.0, 'den_tuong_studio.png', N'Đèn tường hiện đại phong cách studio', 'L04'),
    ('SP018', N'Đèn tường Wally', 3200000, 4.1, 'den_tuong_wally.png', N'Đèn tường phong cách tối giản', 'L04'),
    ('SP019', N'Mochi Pouffe / Nhiều màu', 2000000, 4.0, 'mochi_pouffe(2).png', N'Ghế lười đa dạng màu sắc', 'L02'),
    ('SP020', N'Bàn Cà Phê Raw Đá Cẩm Thạch Đen', 9500000, 4.7, 'ban_ca_phe.png', N'Bàn cà phê với mặt đá cẩm thạch đen', 'L03'),
	 ('SP021', N'Tủ Giày Đôi 4 Cánh Marcell', 11000000, 4.5, 'tu_giay_marcell.png', N'Tủ giày đôi 4 cánh chất liệu cao cấp', 'L05'),
    ('SP022', N'Tủ Giày – Tủ Trang Trí Gỗ VIENNA 203', 12000000, 4.6, 'tu_giay_vienna.png', N'Tủ giày kết hợp tủ trang trí gỗ phong cách Vienna', 'L05'),
    ('SP023', N'Bàn trà gỗ tự nhiên 5CBT-136', 6000000, 4.3, 'ban_tra_go.png', N'Bàn trà làm từ gỗ tự nhiên, thiết kế đơn giản', 'L03'),
    ('SP024', N'Sofa Da Cao Cấp Prime', 28000000, 4.8, 'sofa_prime.png', N'Sofa da cao cấp cho không gian sang trọng', 'L01'),
    ('SP025', N'Sofa Góc Bọc Nỉ HIỆN ĐẠI', 19000000, 4.5, 'sofa_goc.png', N'Sofa góc bọc nỉ phong cách hiện đại', 'L01'),
    ('SP026', N'Ghế Ăn Loft Bọc Da', 4500000, 4.2, 'ghe_an_loft.png', N'Ghế ăn bọc da phong cách Loft', 'L02'),
    ('SP027', N'Ghế Đẩu Gỗ Tự Nhiên', 2200000, 4.1, 'ghe_dau_go.png', N'Ghế đẩu làm từ gỗ tự nhiên', 'L02'),
    ('SP028', N'Ghế Sofa Cổ Điển Vinta', 30000000, 4.9, 'sofa_vinta.png', N'Ghế sofa phong cách cổ điển', 'L01'),
    ('SP029', N'Tủ Rượu Gỗ Tự Nhiên', 15500000, 4.7, 'tu_ruou.png', N'Tủ rượu làm từ gỗ tự nhiên với thiết kế sang trọng', 'L05'),
    ('SP030', N'Tủ Giày Cửa Lùa Hiện Đại', 9800000, 4.4, 'tu_giay_lua.png', N'Tủ giày cửa lùa tiết kiệm không gian', 'L05'),
    ('SP031', N'Bàn Làm Việc Kiểu Nhật', 5000000, 4.3, 'ban_nhat.png', N'Bàn làm việc thiết kế kiểu Nhật', 'L03'),
    ('SP032', N'Bàn Ăn 6 Ghế Bắc Âu', 14000000, 4.6, 'ban_an_bac_au.png', N'Bàn ăn 6 ghế phong cách Bắc Âu', 'L03'),
    ('SP033', N'Đèn Sàn Ánh Sáng Ấm', 5000000, 4.4, 'den_san.png', N'Đèn sàn với ánh sáng ấm áp', 'L04'),
    ('SP034', N'Đèn LED Trần Nhà Thông Minh', 16000000, 4.8, 'den_led_tran.png', N'Đèn LED trần nhà tích hợp công nghệ thông minh', 'L04'),
    ('SP035', N'Tủ Quần Áo 3 Cánh Đơn Giản', 10500000, 4.5, 'tu_quan_ao.png', N'Tủ quần áo 3 cánh gọn nhẹ và tiện dụng', 'L05'),
    ('SP036', N'Tủ Gương Nhà Tắm Tích Hợp Đèn', 9000000, 4.3, 'tu_guong_tam.png', N'Tủ gương nhà tắm có tích hợp đèn LED', 'L05'),
    ('SP037', N'Bàn Console Cổ Điển', 5500000, 4.2, 'ban_console.png', N'Bàn console phong cách cổ điển', 'L03'),
    ('SP038', N'Tủ Giày Đa Năng Gỗ Sồi', 8000000, 4.4, 'tu_giay_soi.png', N'Tủ giày đa năng làm từ gỗ sồi', 'L05'),
    ('SP039', N'Bàn Gỗ Mặt Kính Cao Cấp', 11500000, 4.7, 'ban_go_kinh.png', N'Bàn gỗ kết hợp mặt kính hiện đại', 'L03'),
    ('SP040', N'Đèn Treo Tường Đồng Vintage', 4000000, 4.3, 'den_treo_dong.png', N'Đèn treo tường làm từ đồng phong cách vintage', 'L04');
		

go

INSERT INTO loai_san_pham (id_loai, ten_loai, hinh) VALUES
    ('L01', N'Sofa', 'sofa.png'),
    ('L02', N'Ghế', 'ghe.png'),
    ('L03', N'Bàn', 'ban.png'),
    ('L04', N'Đèn', 'den.png'),
    ('L05', N'Tủ', 'tu.png'),
    ('L06', N'Nhà bếp', 'nha_bep.png');

go
-- Tạo 10 hóa đơn mẫu cho khách hàng với các ID từ bảng khach_hang đã cung cấp
INSERT INTO hoa_don (id_hoa_don, id_khach_hang, ngay_dat, ngay_giao_hang, tinh_trang_thanh_toan, phuong_thuc_thanh_toan, tinh_trang_giao_hang, tong_tien) VALUES
    ('HD001', 'KH001', '2024-11-01', '2024-11-05', 1, 2, 1, 5000000),
    ('HD002', 'KH002', '2024-11-02', '2024-11-06', 1, 1, 1, 2500000),
    ('HD003', 'KH003', '2024-11-03', '2024-11-07', 0, 2, 0, 7000000),
    ('HD004', 'KH004', '2024-11-04', '2024-11-08', 1, 1, 1, 1200000),
    ('HD005', 'KH005', '2024-11-05', '2024-11-10', 1, 2, 0, 8000000),
    ('HD006', 'KH006', '2024-11-06', '2024-11-11', 0, 1, 1, 4500000),
    ('HD007', 'KH007', '2024-11-07', '2024-11-12', 1, 2, 1, 3000000),
    ('HD008', 'KH008', '2024-11-08', '2024-11-13', 0, 1, 0, 2000000),
    ('HD009', 'KH009', '2024-11-09', '2024-11-14', 1, 2, 1, 6000000),
    ('HD010', 'KH010', '2024-11-10', '2024-11-15', 1, 1, 0, 5500000);

go
-- Tạo 10 phản hồi mẫu với id_hoa_don từ bảng hoa_don
INSERT INTO phan_hoi (id_phan_hoi, noi_dung_phan_hoi, ngay_phan_hoi, id_khach_hang, id_san_pham) 
VALUES
    ('PH001', N'Sản phẩm chất lượng tốt, giao hàng nhanh chóng.', '2024-11-05', 'KH001', 'SP001'),
    ('PH002', N'Hài lòng với sản phẩm, nhưng cần cải thiện dịch vụ hỗ trợ.', '2024-11-06', 'KH002', 'SP002'),
    ('PH003', N'Sản phẩm đúng mô tả, nhân viên nhiệt tình.', '2024-11-07', 'KH003', 'SP003'),
    ('PH004', N'Đóng gói sản phẩm chưa được kỹ càng.', '2024-11-08', 'KH004', 'SP004'),
    ('PH005', N'Rất hài lòng với chất lượng và dịch vụ.', '2024-11-09', 'KH005', 'SP005'),
    ('PH006', N'Sản phẩm đẹp nhưng giao hàng chậm.', '2024-11-10', 'KH006', 'SP006'),
    ('PH007', N'Giá hợp lý, sản phẩm như mong đợi.', '2024-11-11', 'KH007', 'SP007'),
    ('PH008', N'Chất lượng sản phẩm bình thường, cần cải thiện.', '2024-11-12', 'KH008', 'SP008'),
    ('PH009', N'Dịch vụ khách hàng tốt, sản phẩm chất lượng.', '2024-11-13', 'KH009', 'SP009'),
    ('PH010', N'Giao hàng nhanh, đóng gói cẩn thận.', '2024-11-14', 'KH010', 'SP010');

go

-- Tạo 10 đánh giá mẫu cho các sản phẩm từ khách hàng
INSERT INTO danh_gia (id_danh_gia, noi_dung_danh_gia, diem, ngay_danh_gia, id_khach_hang, id_san_pham) VALUES
    ('DG001', N'Sofa rất thoải mái và thiết kế đẹp.', 5, '2024-11-05', 'KH001', 'SP001'),
    ('DG002', N'Bàn cà phê đẹp nhưng cần bảo trì thường xuyên.', 4, '2024-11-06', 'KH002', 'SP002'),
    ('DG003', N'Tủ kệ tivi tiện lợi và đẹp mắt.', 5, '2024-11-07', 'KH003', 'SP003'),
    ('DG004', N'Ghế ngồi thoải mái nhưng chất liệu hơi cứng.', 3, '2024-11-08', 'KH004', 'SP004'),
    ('DG005', N'Bộ bàn ghế rất phù hợp với phòng ăn gia đình.', 5, '2024-11-09', 'KH005', 'SP005'),
    ('DG006', N'Đèn chùm sáng đẹp nhưng giá hơi cao.', 4, '2024-11-10', 'KH006', 'SP006'),
    ('DG007', N'Tủ giày nhỏ gọn, tiết kiệm không gian.', 4, '2024-11-11', 'KH007', 'SP007'),
    ('DG008', N'Ghế đẩu đơn giản và chất lượng tốt.', 3, '2024-11-12', 'KH008', 'SP008'),
    ('DG009', N'Nhà bếp thiết kế đẹp, có không gian rộng.', 5, '2024-11-13', 'KH009', 'SP009'),
    ('DG010', N'Đèn tường tạo không gian ấm cúng cho phòng khách.', 5, '2024-11-14', 'KH010', 'SP010');

go

-- Tạo 10 khuyến mãi mẫu cho các sản phẩm với giá trị khuyến mãi tính bằng VNĐ
INSERT INTO khuyen_mai (id_khuyen_mai, ten_khuyen_mai, noi_dung_khuyen_mai, phan_tram_khuyen_mai, id_san_pham) VALUES
    ('KM001', N'Giảm giá mùa hè', 'Giảm giá cho bộ sofa phòng khách', 10, 'SP001'),
    ('KM002', N'Giảm giá cuối năm', 'Giảm giá cho bàn cà phê', 15, 'SP002'),
    ('KM003', N'Khuyến mãi đầu mùa', 'Giảm giá cho tủ kệ tivi', 20, 'SP003'),
    ('KM004', N'Khuyến mãi dịp lễ', 'Giảm giá cho ghế phòng chờ', 10, 'SP004'),
    ('KM005', N'Giảm giá cho khách hàng mới', 'Giảm giá cho bộ bàn ghế phòng ăn', 15, 'SP005'),
    ('KM006', N'Mua 1 tặng 1', 'Mua đèn chùm phòng khách tặng đèn tường', 25, 'SP006'),
    ('KM007', N'Giảm giá đặc biệt', 'Giảm giá cho tủ giày', 10, 'SP007'),
    ('KM008', N'Khuyến mãi cho sinh viên', 'Giảm giá cho ghế đẩu', 15, 'SP008'),
    ('KM009', N'Giảm giá sản phẩm mới', 'Giảm giá cho nhà bếp và tủ bếp', 20, 'SP009'),
    ('KM010', N'Khuyến mãi cuối tuần', 'Giảm giá cho đèn tường trang trí', 10, 'SP010');

go

-- Tạo 10 chi tiết hóa đơn mẫu cho các hóa đơn đã có
INSERT INTO chi_tiet_hoa_don (id_hoa_don, id_san_pham, so_luong, don_gia, thanh_tien) VALUES
    ('HD001', 'SP001', 1, 5000000, 5000000),  -- Bộ sofa phòng khách
    ('HD002', 'SP002', 1, 2500000, 2500000),  -- Bàn cà phê
    ('HD003', 'SP003', 1, 7000000, 7000000),  -- Tủ kệ tivi
    ('HD004', 'SP004', 1, 1200000, 1200000),  -- Ghế phòng chờ
    ('HD005', 'SP005', 1, 8000000, 8000000),  -- Bộ bàn ghế phòng ăn
    ('HD006', 'SP006', 1, 4500000, 4500000),  -- Đèn chùm phòng khách
    ('HD007', 'SP007', 1, 3000000, 3000000),  -- Tủ giày
    ('HD008', 'SP008', 1, 2000000, 2000000),  -- Ghế đẩu
    ('HD009', 'SP009', 1, 6000000, 6000000),  -- Nhà bếp và tủ bếp
    ('HD010', 'SP010', 1, 5500000, 5500000); -- Đèn tường trang trí

go
INSERT INTO DM_ManHinh (MaManHinh, TenManHinh) VALUES 
    ('MMH001', N'Màn hình chính'),
    ('MMH002', N'Màn hình đăng nhập'),
    ('MMH003', N'Màn hình sản phẩm'),
    ('MMH004', N'Màn hình giỏ hàng'),
    ('MMH005', N'Màn hình thanh toán'),
    ('MMH006', N'Màn hình quản lý tài khoản');

go

INSERT INTO QL_NhomNguoiDung (MaNhom, TenNhom, GhiChu) VALUES 
    ('MN001', N'Quản trị viên', N'Nhóm người dùng có quyền truy cập và điều hành toàn bộ hệ thống'),
    ('MN002', N'Nhân viên', N'Nhóm người dùng có quyền truy cập vào các chức năng quản lý nội bộ'),
    ('MN003', N'Khách hàng', N'Nhóm người dùng chỉ có quyền xem và mua sản phẩm'),
	('MN004', N'Quản lý kho', N'Nhóm người dùng quản lý kho và các sản phẩm trong kho'),
    ('MN005', N'Nhà cung cấp', N'Nhóm người dùng quản lý thông tin cung cấp sản phẩm');

go
INSERT INTO QL_NguoiDung (TenDangNhap, MatKhau, HoatDong) 
VALUES ('user1', '111', 1),
       ('user2', '222', 0),
       ('user3', '333', 1);

go
INSERT INTO QL_NguoiDungNhomNguoiDung (TenDangNhap, MaNhomNguoiDung, GhiChu) VALUES 
    ('user1', 'MN001', N'Người dùng 1 thuộc nhóm Quản trị viên'),
    ('user2', 'MN002', N'Người dùng 2 thuộc nhóm Nhân viên'),
    ('user3', 'MN003', N'Người dùng 3 thuộc nhóm Khách hàng')
   


go

INSERT INTO QL_PhanQuyen (MaNhomNguoiDung, MaManHinh, CoQuyen) VALUES 
    ('MN001', 'MMH001', 1), -- Quản trị viên có quyền truy cập màn hình 1
    ('MN002', 'MMH002', 1), -- Nhân viên có quyền truy cập màn hình 2
    ('MN003', 'MMH003', 0), -- Khách hàng không có quyền truy cập màn hình 3
    ('MN004', 'MMH004', 1), -- Quản lý kho có quyền truy cập màn hình 4
    ('MN005', 'MMH005', 0); -- Nhà cung cấp không có quyền truy cập màn hình 5




	ALTER DATABASE QuanLyBanHang SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE QuanLyBanHang;
	-- Xóa các bảng con có khóa ngoại trước
DROP TABLE IF EXISTS QL_PhanQuyen;
DROP TABLE IF EXISTS QL_NguoiDungNhomNguoiDung;
DROP TABLE IF EXISTS chi_tiet_hoa_don;
DROP TABLE IF EXISTS danh_gia;
DROP TABLE IF EXISTS phan_hoi;
DROP TABLE IF EXISTS khuyen_mai;
DROP TABLE IF EXISTS hoa_don;

-- Xóa các bảng chính không phụ thuộc
DROP TABLE IF EXISTS nhan_vien;
DROP TABLE IF EXISTS loai_nhan_vien;
DROP TABLE IF EXISTS khach_hang;
DROP TABLE IF EXISTS san_pham;
DROP TABLE IF EXISTS loai_san_pham;
DROP TABLE IF EXISTS DM_ManHinh;
DROP TABLE IF EXISTS QL_NguoiDung;
DROP TABLE IF EXISTS QL_NhomNguoiDung;