using BaiThucHanh5_Net10.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiThucHanh5_Net10.ViewComponents;

public class MajorViewComponent(SchoolContext db) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var majors = await db.Majors.OrderBy(m => m.MajorName).ToListAsync();
        return View("RenderMajor", majors);
    }
}
