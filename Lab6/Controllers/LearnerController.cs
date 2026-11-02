using BaiThucHanh5_Net10.Data;
using BaiThucHanh5_Net10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaiThucHanh5_Net10.Controllers;

public class LearnerController(SchoolContext db) : Controller
{
    private const int PageSize = 3;

    // Phân trang dữ liệu và lọc theo MajorID (đồng bộ).
    public async Task<IActionResult> Index(int? mid, int pageIndex = 1)
    {
        pageIndex = pageIndex < 1 ? 1 : pageIndex;
        ViewData["Majors"] = await db.Majors.OrderBy(m => m.MajorName).ToListAsync();

        var learners = db.Learners.Include(l => l.Major).AsQueryable();
        if (mid != null)
            learners = learners.Where(l => l.MajorID == mid.Value);

        var totalPages = (int)Math.Ceiling(await learners.CountAsync() / (double)PageSize);
        ViewBag.pageNum = Math.Max(totalPages, 1);
        ViewBag.mid = mid;

        var result = await learners.OrderBy(l => l.LearnerID)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return View(result);
    }

    // Lọc + tìm kiếm + phân trang, được gọi bằng AJAX.
    public async Task<IActionResult> LearnerFilter(int? mid, string? keyword, int pageIndex = 1)
    {
        pageIndex = pageIndex < 1 ? 1 : pageIndex;

        var learners = db.Learners.Include(l => l.Major).AsQueryable();

        if (mid != null)
            learners = learners.Where(l => l.MajorID == mid.Value);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var key = keyword.Trim().ToLower();
            learners = learners.Where(l =>
                l.FirstName.ToLower().Contains(key) ||
                l.LastName.ToLower().Contains(key));
        }

        var totalPages = (int)Math.Ceiling(await learners.CountAsync() / (double)PageSize);
        ViewBag.pageNum = Math.Max(totalPages, 1);
        ViewBag.mid = mid;
        ViewBag.keyword = keyword;

        var result = await learners.OrderBy(l => l.LearnerID)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return PartialView("LearnerTable", result);
    }

    public async Task<IActionResult> Create()
    {
        await LoadMajors();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Learner learner)
    {
        if (!ModelState.IsValid)
        {
            await LoadMajors(learner.MajorID);
            return View(learner);
        }

        db.Learners.Add(learner);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var learner = await db.Learners.FindAsync(id);
        if (learner == null) return NotFound();

        await LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Learner learner)
    {
        if (id != learner.LearnerID) return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadMajors(learner.MajorID);
            return View(learner);
        }

        db.Update(learner);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var learner = await db.Learners.Include(l => l.Major)
            .FirstOrDefaultAsync(l => l.LearnerID == id);
        if (learner == null) return NotFound();
        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var learner = await db.Learners.FindAsync(id);
        if (learner != null)
        {
            db.Learners.Remove(learner);
            await db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadMajors(int? selected = null)
    {
        ViewBag.MajorID = new SelectList(
            await db.Majors.OrderBy(m => m.MajorName).ToListAsync(),
            "MajorID", "MajorName", selected);
    }
}
