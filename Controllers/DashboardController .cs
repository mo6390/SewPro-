using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using SewPro.Data; // استبدل بـ namespace الحقيقي

namespace SewPro.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var totalCustomers = await _context.Customers.CountAsync();
            var newCustomersMonth = await _context.Customers
                .Where(c => c.CreatedAt.Month == DateTime.Now.Month && c.CreatedAt.Year == DateTime.Now.Year)
                .CountAsync();
            var newCustomersYear = await _context.Customers
                .Where(c => c.CreatedAt.Year == DateTime.Now.Year)
                .CountAsync();



            return Json(new
            {
                totalCustomers,
                newCustomersMonth,
                newCustomersYear,
            });
        }
    }
}
