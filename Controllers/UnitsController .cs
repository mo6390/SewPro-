using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
    public class UnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض جميع الوحدات
        public async Task<IActionResult> Index()
        {
            var units = await Task.FromResult(_context.Units.ToList());
            return View(units);
        }

        // عرض التفاصيل
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var unit = await Task.FromResult(_context.Units.FirstOrDefault(u => u.Id == id));
            if (unit == null)
                return NotFound();

            return View(unit);
        }

        // عرض نموذج الإدخال
        public IActionResult Create()
        {
            return View();
        }

        // معالجة الإدخال وحفظ البيانات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // عرض نموذج التعديل
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var unit = await Task.FromResult(_context.Units.FirstOrDefault(u => u.Id == id));
            if (unit == null)
                return NotFound();

            return View(unit);
        }

        // معالجة التعديل وحفظ البيانات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Unit unit)
        {
            if (id != unit.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // حذف الوحدة
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var unit = await Task.FromResult(_context.Units.FirstOrDefault(u => u.Id == id));
            if (unit == null)
                return NotFound();

            return View(unit);
        }

        // تأكيد الحذف
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await Task.FromResult(_context.Units.FirstOrDefault(u => u.Id == id));
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
