using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Identity.Data;
using Web.Models;

namespace Web.Controllers
{
    public class KitapYazarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitapYazarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KitapYazar
        public async Task<IActionResult> Index()
        {
            var webContext = _context.kitapYazarlar.Include(k => k.Kitap);
            return View(await webContext.ToListAsync());
        }

        // GET: KitapYazar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kitapYazarlar == null)
            {
                return NotFound();
            }

            var kitapYazar = await _context.kitapYazarlar
                .Include(k => k.Kitap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitapYazar == null)
            {
                return NotFound();
            }

            return View(kitapYazar);
        }

        // GET: KitapYazar/Create
        public IActionResult Create()
        {
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "KitapAd");
            ViewData["YazarId"] = new SelectList(_context.yazarlar, "Id", "AdSoyad");
            return View();
        }

        // POST: KitapYazar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KitapId,YazarId")] KitapYazar kitapYazar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitapYazar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", kitapYazar.KitapId);
            return View(kitapYazar);
        }

        // GET: KitapYazar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kitapYazarlar == null)
            {
                return NotFound();
            }

            var kitapYazar = await _context.kitapYazarlar.FindAsync(id);
            if (kitapYazar == null)
            {
                return NotFound();
            }
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", kitapYazar.KitapId);
            return View(kitapYazar);
        }

        // POST: KitapYazar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KitapId,YazarId")] KitapYazar kitapYazar)
        {
            if (id != kitapYazar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitapYazar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapYazarExists(kitapYazar.Id))
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
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", kitapYazar.KitapId);
            return View(kitapYazar);
        }

        // GET: KitapYazar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kitapYazarlar == null)
            {
                return NotFound();
            }

            var kitapYazar = await _context.kitapYazarlar
                .Include(k => k.Kitap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitapYazar == null)
            {
                return NotFound();
            }

            return View(kitapYazar);
        }

        // POST: KitapYazar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kitapYazarlar == null)
            {
                return Problem("Entity set 'WebContext.kitapYazarlar'  is null.");
            }
            var kitapYazar = await _context.kitapYazarlar.FindAsync(id);
            if (kitapYazar != null)
            {
                _context.kitapYazarlar.Remove(kitapYazar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapYazarExists(int id)
        {
          return _context.kitapYazarlar.Any(e => e.Id == id);
        }
    }
}
