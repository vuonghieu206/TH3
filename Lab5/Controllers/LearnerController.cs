using BaiThucHanh5_Net10.Data;
using BaiThucHanh5_Net10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaiThucHanh5_Net10.Controllers;

public class LearnerController(SchoolContext db) : Controller
{
    // Hiển thị tất cả Learner hoặc lọc theo MajorID (phần đồng bộ).
    public async Task<IActionResult> Index(int? mid)
    {
        ViewData["Majors"] = await db.Majors.OrderBy(m => m.MajorName).ToListAsync();
        var learners = db.Learners.Include(l => l.Major).AsQueryable();

        if (mid == null)
            return View(await learners.OrderBy(l => l.LearnerID).ToListAsync());

        return View(await learners.Where(l => l.MajorID == mid)
            .OrderBy(l => l.LearnerID).ToListAsync());
    }

    // Action được gọi bằng AJAX, trả về PartialView chứa bảng Learner.
    public async Task<IActionResult> LearnerByMajorID(int? mid)
    {
        var learners = db.Learners.Include(l => l.Major).AsQueryable();

        if (mid.HasValue)
            learners = learners.Where(l => l.MajorID == mid.Value);

        return PartialView("LearnerTable", await learners.OrderBy(l => l.LearnerID).ToListAsync());
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
