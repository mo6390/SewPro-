using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using SewPro.Data; // تأكد من استيراد السياق الخاص بقاعدة البيانات

namespace SewPro.Controllers
{
    public class TailorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TailorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // صفحة عرض الخياطين
        public IActionResult Index()
        {
            var tailors = _context.Tailors.ToList();
            return View(tailors);
        }

        // صفحة إنشاء خياط جديد
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tailor tailor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tailor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tailor);
        }

        // إجراء حذف خياط
        public async Task<IActionResult> Delete(int id)
        {
            var tailor = await _context.Tailors.FindAsync(id);
            if (tailor == null)
            {
                return NotFound();
            }

            _context.Tailors.Remove(tailor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
