using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using SewPro.Data;

namespace SewPro.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrencyController(ApplicationDbContext context)
        {
            _context = context;
        }



  public IActionResult Index()
{
    var currencies = _context.Currencies.ToList();  // الحصول على جميع العملات من قاعدة البيانات
    var model = currencies.Select(c => new CurrencyViewModel
    {
        Id = c.Id,
        MainCurrencyArabic = c.MainCurrencyArabic,
        MainCurrencyEnglish = c.MainCurrencyEnglish,
        SubCurrencyArabic = c.SubCurrencyArabic,
        SubCurrencyEnglish = c.SubCurrencyEnglish,
        SubCurrencyDecimal = c.SubCurrencyDecimal,
        NationalityArabic = c.NationalityArabic,
        NationalityEnglish = c.NationalityEnglish,
        Notes = c.Notes
    }).ToList();  // تحويل Currency إلى CurrencyViewModel

    return View(model);  // إرسال البيانات المحولة إلى الفيو
}

        // GET: Currency/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                // إذا كانت البيانات صالحة، يمكنك إضافة العملة إلى قاعدة البيانات أو معالجتها هنا
                var currency = new Currency
                {
                    MainCurrencyArabic = model.MainCurrencyArabic,
                    MainCurrencyEnglish = model.MainCurrencyEnglish,
                    SubCurrencyArabic = model.SubCurrencyArabic,
                    SubCurrencyEnglish = model.SubCurrencyEnglish,
                    SubCurrencyDecimal = model.SubCurrencyDecimal,
                    NationalityArabic = model.NationalityArabic,
                    NationalityEnglish = model.NationalityEnglish,
                    Notes = model.Notes
                };

                _context.Currencies.Add(currency);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");  // توجيه المستخدم إلى الصفحة الرئيسية أو صفحة أخرى بعد الإضافة
            }

            return View(model);
        }
    }
}
