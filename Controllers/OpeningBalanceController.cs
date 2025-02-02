using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SewPro.Controllers
{
    public class OpeningBalanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpeningBalanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OpeningBalance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OpeningBalance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpeningBalance openingBalance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(openingBalance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(openingBalance);
        }

        // GET: OpeningBalance/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.OpeningBalances.ToListAsync());
        }

        // GET: OpeningBalance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openingBalance = await _context.OpeningBalances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openingBalance == null)
            {
                return NotFound();
            }

            return View(openingBalance);
        }
    }
}
