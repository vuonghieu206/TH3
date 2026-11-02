BÀI THỰC HÀNH 5 - ASP.NET CORE MVC .NET 10
===========================================

1. YÊU CẦU
- Cài Visual Studio 2026 hoặc .NET 10 SDK.
- Có kết nối Internet lần đầu để tải các gói NuGet và thư viện Bootstrap/jQuery từ CDN.

2. MỞ PROJECT
- Giải nén file ZIP.
- Mở file BaiThucHanh5_Net10.csproj bằng Visual Studio.
- Chờ Visual Studio Restore NuGet Packages.
- Nhấn F5 hoặc Ctrl + F5 để chạy.

Có thể chạy bằng Terminal tại thư mục chứa file .csproj:
    dotnet restore
    dotnet run

3. DATABASE
- Project dùng SQLite, file school.db được tự tạo trong thư mục chạy ứng dụng.
- Database và dữ liệu mẫu được tạo tự động ở lần chạy đầu tiên.
- Không cần tự tạo database hoặc sửa connection string.
- Muốn tạo lại dữ liệu mẫu từ đầu: tắt ứng dụng, xóa file school.db rồi chạy lại.

4. CHỨC NĂNG THEO ĐỀ
- Đồng bộ: MajorViewComponent hiển thị danh sách chuyên ngành; bấm vào chuyên ngành sẽ gửi mid đến LearnerController.Index.
- Không đồng bộ: chọn chuyên ngành ở combobox; jQuery AJAX gọi LearnerByMajorID và nạp PartialView LearnerTable vào divcontent.
- Có thêm chức năng Create, Edit, Delete để quản lý Learner.

5. CẤU TRÚC QUAN TRỌNG
- Models: Learner.cs, Major.cs
- Data: SchoolContext.cs, DbInitializer.cs
- ViewComponents: MajorViewComponent.cs
- Controllers: LearnerController.cs
- Views/Shared/Components/Major/RenderMajor.cshtml
- Views/Learner/Index.cshtml
- Views/Learner/LearnerTable.cshtml

LƯU Ý
- TargetFramework đã đặt là net10.0.
- Nếu máy chưa cài .NET 10 SDK thì cần cài SDK .NET 10 trước khi chạy.
