using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class IndustryController : Controller
{
    private readonly ApplicationDbContext _context;

    public IndustryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // عرض قائمة الصناعات
    public async Task<IActionResult> Index()
    {
        var industries = await _context.Industries.ToListAsync();
        return View(industries);
    }

    // عرض صفحة إضافة صناعة جديدة
    public IActionResult Create()
    {
        return View();
    }

    // إضافة صناعة جديدة
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Industry industry)
    {
        if (ModelState.IsValid)
        {
            _context.Industries.Add(industry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(industry);
    }

    // عرض تفاصيل صناعة
    public async Task<IActionResult> Details(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null)
        {
            return NotFound();
        }
        return View(industry);
    }

    // عرض صفحة التعديل
    public async Task<IActionResult> Edit(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null)
        {
            return NotFound();
        }
        return View(industry);
    }

    // تعديل صناعة
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Industry industry)
    {
        if (id != industry.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(industry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(industry);
    }

    // عرض صفحة الحذف
    public async Task<IActionResult> Delete(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry == null)
        {
            return NotFound();
        }
        return View(industry);
    }

    // تأكيد الحذف
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var industry = await _context.Industries.FindAsync(id);
        if (industry != null)
        {
            _context.Industries.Remove(industry);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
   
   }
   
   
}
