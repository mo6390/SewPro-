using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // لاستخدام ToListAsync و FirstOrDefaultAsync
using Microsoft.EntityFrameworkCore.Storage; // للتعامل مع DbUpdateConcurrencyException

namespace SewPro.Controllers
{
    public class ItemGroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemGroup
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemGroups.ToListAsync());
        }

        // GET: ItemGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemGroup = await _context.ItemGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            return View(itemGroup);
        }

        // GET: ItemGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]  // يتم التحقق من عدم وجود تكرار
        public async Task<IActionResult> Create([Bind("Id,ArabicName,EnglishName,Notes")] ItemGroup itemGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemGroup);
        }

        // GET: ItemGroup/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemGroup = await _context.ItemGroups.FindAsync(id);
            if (itemGroup == null)
            {
                return NotFound();
            }
            return View(itemGroup);
        }

        // POST: ItemGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArabicName,EnglishName,Notes")] ItemGroup itemGroup)
        {
            if (id != itemGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemGroupExists(itemGroup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemGroup);
        }

        // GET: ItemGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemGroup = await _context.ItemGroups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            return View(itemGroup);
        }

        // POST: ItemGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemGroup = await _context.ItemGroups.FindAsync(id);
            _context.ItemGroups.Remove(itemGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemGroupExists(int id)
        {
            return _context.ItemGroups.Any(e => e.Id == id);
        }
    }
}
