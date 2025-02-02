using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SewPro.Controllers
{
   public class NeckTypeController : Controller
{
    private readonly ApplicationDbContext _context;

    public NeckTypeController(ApplicationDbContext context)
    {
        _context = context;
    }

     public IActionResult Index()
        {
            var neckTypes = _context.NeckTypes.ToList();  // استعلام للحصول على كل الأنواع من قاعدة البيانات
            return View(neckTypes);  // إرجاع الأنواع إلى الفيو
        }
    // GET: NeckType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: NeckType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NeckType model)
    {
        if (ModelState.IsValid)
        {
            // حفظ النوع في قاعدة البيانات
            _context.NeckTypes.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home"); // إعادة توجيه المستخدم بعد الحفظ
        }

        return View(model);
    }
}
}
