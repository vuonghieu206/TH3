BÀI THỰC HÀNH 5 - ASP.NET CORE MVC .NET 10
===========================================

PROJECT GỒM CÁC CHỨC NĂNG
1. Hiển thị danh sách Learner.
2. Lọc Learner theo Major.
3. Phân trang dữ liệu (3 Learner/trang).
4. Lọc + tìm kiếm theo tên + phân trang kết hợp bằng AJAX.
5. Thêm, sửa, xóa Learner.
6. SQLite tự tạo database và dữ liệu mẫu.

CÁCH CHẠY
- Cài .NET 10 SDK và Visual Studio 2026 (hoặc IDE hỗ trợ .NET 10).
- Giải nén ZIP, mở BaiThucHanh5_Net10.csproj.
- Chờ Restore NuGet Packages.
- Nhấn F5 hoặc Ctrl+F5.
- Có thể chạy terminal: dotnet restore rồi dotnet run.

DATABASE
- Dùng SQLite, file school.db tự tạo ở lần chạy đầu.
- Muốn tạo lại dữ liệu mẫu: dừng ứng dụng, xóa school.db rồi chạy lại.

PHẦN MỚI THEO ĐỀ
- LearnerController.Index: phân trang dữ liệu đồng bộ, pageSize = 3.
- LearnerController.LearnerFilter: kết hợp lọc MajorID, tìm kiếm keyword và phân trang.
- Views/Learner/Index.cshtml: form tìm kiếm và script AJAX.
- Views/Learner/LearnerTable.cshtml: bảng dữ liệu và thanh phân trang AJAX.
