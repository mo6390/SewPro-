using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SewPro.Models;
using Microsoft.Extensions.Logging;
using SewPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SewPro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // عدد العملاء الإجمالي
                var totalCustomers = await _context.Customers.CountAsync();

                // عدد العملاء الجدد في الشهر الأخير
            var newCustomersMonth = await _context.Customers
    .Where(c => c.RegistrationDate.HasValue && c.RegistrationDate >= DateTime.Now.AddMonths(-1))
    .CountAsync();


                // عدد العملاء الجدد في السنة الأخيرة
                var newCustomersYear = await _context.Customers
                    .Where(c => c.RegistrationDate.HasValue && c.RegistrationDate >= DateTime.Now.AddYears(-1))
                    .CountAsync();

                // تمرير البيانات إلى الـ View
                ViewData["TotalCustomers"] = totalCustomers;
                ViewData["NewCustomersMonth"] = newCustomersMonth;
                ViewData["NewCustomersYear"] = newCustomersYear;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ أثناء جلب بيانات العملاء.");
                ViewData["TotalCustomers"] = 0;
                ViewData["NewCustomersMonth"] = 0;
                ViewData["NewCustomersYear"] = 0;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
