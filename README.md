# HỆ THỐNG QUẢN LÝ THIẾT BỊ ONLINE DÀNH CHO DOANH NGHIỆP

## Các chức năng chính:

Hệ thống quản lý thiết bị bao gồm các tính năng chính sau:
-	Quản lý nhân viên
-	Quản lý thiết bị
-	Quản lý việc chuyển giao thiết bị
-	Quản lý việc gửi yêu cầu trả thiết bị
-	Thống kê và báo cáo


## Hướng dẫn chạy


Bước 1: Mở file `Giuakicnpm.sln` lên và edit file `Web.config` ta edit dòng `connectionString` theo như connect string của DB đã tạo vào máy của mình và Databasename là `LibraryManagement`.

Bước 2: Add migrations : 
 - `enable-migrations`
 - `add-migration "init"`
 - `update-database`

Bước 3: Chạy file `db.sql` để thêm dữ liệu cho database bằng SQL Server Management Studio (SSMS)

Bước 4: Save lại và nhấn `Start`để mở chạy Web ASP.NET MVC lên.

## Một số tài khoản (username - password):
- Quản lý: `admin` - `123456`
- Người dùng: `user2` - `123456`
- Người dùng: `user3` - `123456`
- Path Login: `/Account/Login`



