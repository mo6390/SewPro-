using Microsoft.AspNetCore.Mvc;
using SewPro.Data;
using SewPro.Models;

namespace SewPro.Controllers
{
    public class SleeveShapeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SleeveShapeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SleeveShape
        public IActionResult Index()
        {
            var sleeveShapes = _context.SleeveShapes.ToList();
            return View(sleeveShapes);
        }

        // GET: SleeveShape/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SleeveShape/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SleeveShape model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;

                _context.SleeveShapes.Add(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: SleeveShape/Edit/5
        public IActionResult Edit(int id)
        {
            var sleeveShape = _context.SleeveShapes.Find(id);
            if (sleeveShape == null)
            {
                return NotFound();
            }

            return View(sleeveShape);
        }

        // POST: SleeveShape/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SleeveShape model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var sleeveShape = _context.SleeveShapes.Find(id);
                    if (sleeveShape != null)
                    {
                        sleeveShape.Name = model.Name;
                        sleeveShape.UpdatedAt = DateTime.Now;

                        _context.Update(sleeveShape);
                        _context.SaveChanges();
                    }
                }
                catch
                {
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: SleeveShape/Delete/5
        public IActionResult Delete(int id)
        {
            var sleeveShape = _context.SleeveShapes.Find(id);
            if (sleeveShape == null)
            {
                return NotFound();
            }

            return View(sleeveShape);
        }

        // POST: SleeveShape/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sleeveShape = _context.SleeveShapes.Find(id);
            if (sleeveShape != null)
            {
                _context.SleeveShapes.Remove(sleeveShape);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
