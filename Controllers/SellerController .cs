using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using SewPro.Data;

namespace SewPro.Controllers
{
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor لتحميل السياق
        public SellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seller/Create
        [HttpPost]
        public IActionResult Create(Seller seller)
        {
            // التحقق من صلاحية النموذج
            if (ModelState.IsValid)
            {
                // إضافة البائع إلى قاعدة البيانات
                _context.Sellers.Add(seller);
                _context.SaveChanges();
                // إعادة التوجيه إلى صفحة الفهرس بعد الحفظ
                return RedirectToAction("Index");
            }

            // إذا كان النموذج غير صالح، إعادة تحميل نفس الصفحة مع عرض الأخطاء
            return View(seller);
        }

        // GET: Seller/Index
        public IActionResult Index()
        {
            // جلب جميع البائعين من قاعدة البيانات
            var sellers = _context.Sellers.ToList();
            return View(sellers);
        }

        // GET: Seller/Delete/{id}
        public IActionResult Delete(int id)
        {
            // البحث عن البائع باستخدام معرّف البائع
            var seller = _context.Sellers.Find(id);
            if (seller != null)
            {
                // حذف البائع من قاعدة البيانات
                _context.Sellers.Remove(seller);
                _context.SaveChanges();
            }

            // إعادة التوجيه إلى صفحة الفهرس بعد الحذف
            return RedirectToAction("Index");
        }
    }
}
