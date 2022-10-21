
CREATE TABLE THUONGHIEU
(
	MaTH INT IDENTITY(1,1),
	TenThuongHieu NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_TH PRIMARY KEY(MaTH)
)
GO

CREATE TABLE LOAIGIAY
(
	MaLoaiGiay INT IDENTITY(1,1),
	TenLoaiGiay NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_CD PRIMARY KEY(MaLoaiGiay)
)
GO

CREATE TABLE GIAY
(
	MaGiay INT IDENTITY(1,1),
	TenGiay NVARCHAR(100) NOT NULL,
	GiaBan MONEY CHECK (GiaBan>=0),
	Size int,
	MoTa NTEXT,
	AnhGiay VARCHAR(50),
	NgayCapNhat SMALLDATETIME,
	SoLuongBan INT CHECK(SoLuongBan>0),
	MaTH INT,
	MaLoaiGiay INT,
	CONSTRAINT PK_S PRIMARY KEY(MaGiay)
)
GO

CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1),
	DaThanhToan BIT DEFAULT(0),
	TinhTrangGiaoHang INT DEFAULT(1),
	NgayDat SMALLDATETIME,
	NgayGiao SMALLDATETIME,
	MaKH INT,
	CONSTRAINT PK_DDH PRIMARY KEY(MaDonHang)
)
GO

CREATE TABLE CHITIETDATHANG
(
	MaDonHang INT,
	MaGiay INT,
	SoLuong INT CHECK(SoLuong>0),
	DonGia DECIMAL(9,2) CHECK(DonGia>=0),
	CONSTRAINT PK_CTDH PRIMARY KEY(MaDonHang,MaGiay)
)
GO

CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen NVARCHAR(50) NOT NULL,
	TaiKhoan VARCHAR(15) UNIQUE,
	MatKhau VARCHAR(15) NOT NULL,
	Email VARCHAR(50) UNIQUE,
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	NgaySinh SMALLDATETIME,
	CONSTRAINT PK_Kh PRIMARY KEY(MaKH)
) 
GO

CREATE TABLE ADMIN
(
	MaAdmin INT IDENTITY(1,1),
	HoTen NVARCHAR(50),
	DiaChi NVARCHAR(50),
	DienThoai VARCHAR(10),
	TenDN VARCHAR(15),
	MatKhau VARCHAR(15),
	NgaySinh SMALLDATETIME,
	Email VARCHAR(50),
	Quyen Int Default 1,
	CONSTRAINT PK_AM PRIMARY KEY(MaAdmin)
)
GO

ALTER TABLE GIAY ADD CONSTRAINT FK_G_TH FOREIGN KEY (MaTH) REFERENCES THUONGHIEU(MaTH)
ALTER TABLE GIAY ADD CONSTRAINT FK_Giay_LOAIGIAY FOREIGN KEY (MaLoaiGiay) REFERENCES LOAIGIAY(MaLoaiGiay)
ALTER TABLE DONDATHANG ADD CONSTRAINT FK_DDH_KH FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_CTDH_DDH FOREIGN KEY (MaDonHang) REFERENCES DONDATHANG(MaDonHang)
ALTER TABLE CHITIETDATHANG ADD CONSTRAINT FK_CTDH_S FOREIGN KEY (MaGiay) REFERENCES GIAY(MaGiay)
GO

SET IDENTITY_INSERT [dbo].[ADMIN] ON 

INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES (1, N'Đỗ Đăng Khoa', N'Thủ Dầu Một', N'0988936592', N'dangkhoa', N'123456', CAST(N'1982-02-10T00:00:00' AS SmallDateTime), N'dangkhoa@tdmu.edu.vn', 1)
INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES (2, N'Võ Nguyễn Nhật Minh', N'Phú Hòa', N'012344444', N'minhvnn', N'12345', CAST(N'1990-01-02T00:00:00' AS SmallDateTime), N'minhvnn@tdmu.edu.vn', 2)
INSERT [dbo].[ADMIN] ([MaAdmin], [HoTen], [DiaChi], [DienThoai], [TenDN], [MatKhau], [NgaySinh], [Email], [Quyen]) VALUES (3, N'Võ Nguyễn Thái Hoàng', N'Phú Hòa', N'012344444', N'hoangvnt', N'12345', CAST(N'1995-04-10T00:00:00' AS SmallDateTime), N'hoangvnt@tdmu.edu.vn', 2)
SET IDENTITY_INSERT [dbo].[ADMIN] OFF
GO

INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (1, 16, 1, CAST(1000000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 1, 1, CAST(1000000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 2, 1, CAST(1100000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 3, 1, CAST(800000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 9, 1, CAST(399000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 10, 1, CAST(400000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 13, 1, CAST(899000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (2, 23, 1, CAST(499000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (3, 20, 1, CAST(300000.00 AS Decimal(9, 2)))
INSERT [dbo].[CHITIETDATHANG] ([MaDonHang], [MaGiay], [SoLuong], [DonGia]) VALUES (4, 16, 1, CAST(1000000.00 AS Decimal(9, 2)))
GO
SET IDENTITY_INSERT [dbo].[DONDATHANG] ON 

INSERT [dbo].[DONDATHANG] ([MaDonHang], [DaThanhToan], [TinhTrangGiaoHang], [NgayDat], [NgayGiao], [MaKH]) VALUES (1, 0, 1, CAST(N'2021-06-19T19:43:00' AS SmallDateTime), CAST(N'2021-06-25T00:00:00' AS SmallDateTime), 16)
INSERT [dbo].[DONDATHANG] ([MaDonHang], [DaThanhToan], [TinhTrangGiaoHang], [NgayDat], [NgayGiao], [MaKH]) VALUES (2, 0, 1, CAST(N'2021-06-23T19:25:00' AS SmallDateTime), CAST(N'2022-02-23T00:00:00' AS SmallDateTime), 16)
INSERT [dbo].[DONDATHANG] ([MaDonHang], [DaThanhToan], [TinhTrangGiaoHang], [NgayDat], [NgayGiao], [MaKH]) VALUES (3, 0, 1, CAST(N'2021-07-06T15:38:00' AS SmallDateTime), CAST(N'2021-07-07T00:00:00' AS SmallDateTime), 16)
INSERT [dbo].[DONDATHANG] ([MaDonHang], [DaThanhToan], [TinhTrangGiaoHang], [NgayDat], [NgayGiao], [MaKH]) VALUES (4, 0, 1, CAST(N'2021-07-13T14:43:00' AS SmallDateTime), CAST(N'2021-07-13T00:00:00' AS SmallDateTime), 16)
SET IDENTITY_INSERT [dbo].[DONDATHANG] OFF
GO
SET IDENTITY_INSERT [dbo].[GIAY] ON 

INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (1, N'Nike White', 1000000.0000, 40, N'Nhẹ nhàng', N'1.jpg', CAST(N'2020-04-12T00:00:00' AS SmallDateTime), 6, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (2, N'Nike White Version 2', 1100000.0000, 38, N'Đẹp thật sự', N'2.jpg', CAST(N'2021-01-01T00:00:00' AS SmallDateTime), 6, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (3, N'Nike Jordan', 800000.0000, 40, N'Dễ phối đồ', N'3.jpg', CAST(N'2020-02-01T00:00:00' AS SmallDateTime), 4, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (4, N'Nike Jordan Yellow', 900000.0000, 41, N'Dễ Phối đồ', N'4.jpg', CAST(N'2021-02-02T00:00:00' AS SmallDateTime), 2, 1, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (5, N'Jordan Blue', 500000.0000, 39, N'Đẹp thật sự', N'5.jpg', CAST(N'2021-05-02T00:00:00' AS SmallDateTime), 2, 1, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (6, N'Jordan', 700000.0000, 40, N'Đẹp', N'6.jpg', CAST(N'2020-12-05T00:00:00' AS SmallDateTime), 1, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (7, N'Nike Line Red', 499000.0000, 41, N'Nhẹ , thoải mái', N'7.jpg', CAST(N'2020-08-12T00:00:00' AS SmallDateTime), 6, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (8, N'Nike New', 399000.0000, 39, N'Nhẹ nhàng, thoải mái', N'8.jpg', CAST(N'2020-12-09T00:00:00' AS SmallDateTime), 3, 1, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (9, N'Adidas Old', 399000.0000, 40, N'Đẹp, thoải mái', N'9.jpg', CAST(N'2020-10-20T00:00:00' AS SmallDateTime), 5, 2, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (10, N'Yezzy', 400000.0000, 35, N'Đẹp', N'10.jpg', CAST(N'2021-04-03T00:00:00' AS SmallDateTime), 3, 2, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (11, N'Adidas Custom', 300000.0000, 36, N'Đẹp', N'11.jpg', CAST(N'2021-03-05T00:00:00' AS SmallDateTime), 1, 2, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (12, N'Adidas Custom 2', 500000.0000, 38, N'Thoải mái', N'12.jpg', CAST(N'2021-05-05T00:00:00' AS SmallDateTime), 1, 2, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (13, N'Puma', 899000.0000, 40, N'Nhẹ nhàng', N'13.jpg', CAST(N'2021-05-08T00:00:00' AS SmallDateTime), 3, 3, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (14, N'Puma White', 499000.0000, 41, N'Nhẹ nhàng, thoải mái', N'14.jpg', CAST(N'2021-05-05T00:00:00' AS SmallDateTime), 2, 3, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (15, N'Puma Black', 399000.0000, 34, N'Nhẹ nhàng', N'15.jpg', CAST(N'2020-09-09T00:00:00' AS SmallDateTime), 5, 3, 3)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (16, N'Nike Air Max 90', 1000000.0000, 42, N'Nhẹ nhàng', N'16.jpg', CAST(N'2021-05-06T00:00:00' AS SmallDateTime), 10, 3, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (17, N'Air Max Custom', 500000.0000, 40, N'Đẹp, thoải mái', N'17.jpg', CAST(N'2021-02-03T00:00:00' AS SmallDateTime), 5, 3, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (18, N'Vans', 899000.0000, 39, N'Dễ phối đồ', N'18.jpg', CAST(N'2020-05-09T00:00:00' AS SmallDateTime), 3, 5, 2)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (19, N'Vans Caro', 599000.0000, 40, N'Đẹp thật sử', N'19.jpg', CAST(N'2021-02-03T00:00:00' AS SmallDateTime), 3, 5, 2)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (20, N'Converse White', 300000.0000, 39, N'Đẹp', N'20.jpg', CAST(N'2021-05-08T00:00:00' AS SmallDateTime), 4, 4, 2)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (21, N'Balenciaga White', 899000.0000, 40, N'Đẹp', N'21.jpg', CAST(N'2021-01-02T00:00:00' AS SmallDateTime), 3, 6, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (22, N'Balenciaga Black', 500000.0000, 41, N'Nhẹ Nhàng', N'22.jpg', CAST(N'2021-03-03T00:00:00' AS SmallDateTime), 3, 6, 2)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (23, N'Balenciaga Custom', 499000.0000, 40, N'Đẹp', N'23.jpg', CAST(N'2020-03-02T00:00:00' AS SmallDateTime), 3, 6, 1)
INSERT [dbo].[GIAY] ([MaGiay], [TenGiay], [GiaBan], [Size], [MoTa], [AnhGiay], [NgayCapNhat], [SoLuongBan], [MaTH], [MaLoaiGiay]) VALUES (33, N'a', 2.0000, 40, N'Đẹp, chật lượng
', N'4.jpg', CAST(N'2021-07-28T00:00:00' AS SmallDateTime), 1, 2, 2)
SET IDENTITY_INSERT [dbo].[GIAY] OFF
GO
SET IDENTITY_INSERT [dbo].[KHACHHANG] ON 

INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (1, N'Nguyễn Văn A', N'nguyenva', N'12345', N'nguyenvana@tdmu.edu.vn', N'Phú Hòa', N'0988936592', CAST(N'1982-02-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (2, N'Nguyễn Tiến Luân', N'thang', N'123456', N'ntluan@hcmuns.edu.vn', N'Quận 6', N'Chua có', CAST(N'1981-03-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (3, N'Đặng Quốc Hòa', N'dqhoa', N'hoa', N'dqhoa@hcmuns.edu.vn', N'Sư Vạn Hạnh', N'Chua có', CAST(N'1980-04-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (4, N'Ngô Ngọc Ngân', N'nnngan', N'ngan', N'nnngan@hcmuns.edu.vn', N'Khu chung cư', N'0918544699', CAST(N'1982-02-19T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (5, N'Đỗ Quỳnh Hương', N'dqhuong', N'huong', N'dqhuong@hcmuns.edu.vn', N'Cống Quỳnh', N'0908123456', CAST(N'1985-06-13T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (6, N'Trần Thị Thu Trang', N'ttttrang', N'trang', N'ttrang@yahoo.com', N'Nơ Trang Long', N'Chua có', CAST(N'1990-12-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (7, N'Nguyễn Thiên Thanh', N'ntthanh', N'thanh', N'ntthanh@t3h.hcmuns.edu.vn', N'Hai Bà Trưng', N'0908320111', CAST(N'1986-12-12T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (8, N'Trần Thị Hải Yến', N'tthyen', N'yen', N'tthyan@vol.vnn.vn', N'Trần Hưng Đạo', N'8111111   ', CAST(N'1988-02-07T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (9, N'Nguyễn Thị Thanh Mai', N'nttmai', N'mai', NULL, N'Trần Quang Diệu', N'81111222', CAST(N'1992-06-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (10, N'Nguyễn Thành Danh', N'ntdanh', N'danh', N'ntdanh@yahoo.com', N'Cộng Hòa', N'8103751', CAST(N'1978-07-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (11, N'Phạm Thị Nga', N'ptnga', N'nga', N'ptnhung@hcmuns.edu.vn', N'Q6 - Tp.HCM', N'0831564512', CAST(N'1986-03-20T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (12, N'Lê Doanh Doanh', N'lddoanh', N'doanh', N'lddoanh@yahoo.com', N'2Bis Hùng Vương', N'07077865', CAST(N'1982-02-11T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (13, N'Đòan Ngọc Minh Tâm', N'dnmtam', N'tam', N'ndmtam@yahoo.com', N'2 Đinh Tiên Hòang', N'0909092222', CAST(N'1982-10-21T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (14, N'Trần Khải Nhi', N'tknhi', N'nhi', N'tknhi@yahoo.com', N'3 Bùi Hữu Nghĩa', N'0909095555', CAST(N'1982-11-24T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (15, N'Nguyễn Văn A', N'nva', N'123456', N'nva@gmail.com', N'Bình Dương', N'123456', CAST(N'2021-06-10T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (16, N'Đỗ Đăng Khoa', N'dangkhoa', N'12345', N'dangkhoa@gmail.com', N'Bình Dương', N'0962730723', CAST(N'2001-11-22T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (17, N'Nguyễn Văn A', N'taikhoanmoi', N'123', N'taikhoanmoi@gmail.com', N'Bình Dương', N'789', CAST(N'2021-07-13T00:00:00' AS SmallDateTime))
INSERT [dbo].[KHACHHANG] ([MaKH], [HoTen], [TaiKhoan], [MatKhau], [Email], [DiaChi], [DienThoai], [NgaySinh]) VALUES (25, N'Nguyễn Văn B', N'ngvb', N'123', N'ngvb@gmail.com', N'Bình Dương', N'456', CAST(N'2021-07-13T00:00:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[KHACHHANG] OFF
GO
SET IDENTITY_INSERT [dbo].[LOAIGIAY] ON 

INSERT [dbo].[LOAIGIAY] ([MaLoaiGiay], [TenLoaiGiay]) VALUES (1, N'Giày Thể Thao')
INSERT [dbo].[LOAIGIAY] ([MaLoaiGiay], [TenLoaiGiay]) VALUES (2, N'Giày Sneaker')
INSERT [dbo].[LOAIGIAY] ([MaLoaiGiay], [TenLoaiGiay]) VALUES (3, N'Giày Sandal')
SET IDENTITY_INSERT [dbo].[LOAIGIAY] OFF
GO
SET IDENTITY_INSERT [dbo].[THUONGHIEU] ON 

INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (1, N'Nike')
INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (2, N'Adidas')
INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (3, N'Puma')
INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (4, N'Converse')
INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (5, N'Vans')
INSERT [dbo].[THUONGHIEU] ([MaTH], [TenThuongHieu]) VALUES (6, N'Balenciaga')
SET IDENTITY_INSERT [dbo].[THUONGHIEU] OFF
GO