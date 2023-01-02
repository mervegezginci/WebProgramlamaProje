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
    public class YorumlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YorumlarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yorumlar
        public async Task<IActionResult> Index()
        {
            var webContext = _context.yorumlar.Include(y => y.Kitap);
            return View(await webContext.ToListAsync());
        }

        // GET: Yorumlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.yorumlar == null)
            {
                return NotFound();
            }

            var yorumlar = await _context.yorumlar
                .Include(y => y.Kitap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorumlar == null)
            {
                return NotFound();
            }

            return View(yorumlar);
        }

        // GET: Yorumlar/Create
        public IActionResult Create()
        {
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id");
            return View();
        }

        // POST: Yorumlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,Mail,Yorum,KitapId")] Yorumlar yorumlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yorumlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", yorumlar.KitapId);
            return View(yorumlar);
        }

        // GET: Yorumlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.yorumlar == null)
            {
                return NotFound();
            }

            var yorumlar = await _context.yorumlar.FindAsync(id);
            if (yorumlar == null)
            {
                return NotFound();
            }
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", yorumlar.KitapId);
            return View(yorumlar);
        }

        // POST: Yorumlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,Mail,Yorum,KitapId")] Yorumlar yorumlar)
        {
            if (id != yorumlar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yorumlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YorumlarExists(yorumlar.Id))
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
            ViewData["KitapId"] = new SelectList(_context.kitaplar, "Id", "Id", yorumlar.KitapId);
            return View(yorumlar);
        }

        // GET: Yorumlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.yorumlar == null)
            {
                return NotFound();
            }

            var yorumlar = await _context.yorumlar
                .Include(y => y.Kitap)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorumlar == null)
            {
                return NotFound();
            }

            return View(yorumlar);
        }

        // POST: Yorumlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.yorumlar == null)
            {
                return Problem("Entity set 'WebContext.yorumlar'  is null.");
            }
            var yorumlar = await _context.yorumlar.FindAsync(id);
            if (yorumlar != null)
            {
                _context.yorumlar.Remove(yorumlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YorumlarExists(int id)
        {
          return _context.yorumlar.Any(e => e.Id == id);
        }
    }
}
