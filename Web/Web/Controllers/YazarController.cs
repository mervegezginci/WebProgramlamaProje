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
    public class YazarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YazarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yazar
        public async Task<IActionResult> Index()
        {
              return View(await _context.yazarlar.ToListAsync());
        }

        // GET: Yazar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.yazarlar == null)
            {
                return NotFound();
            }

            var yazar = await _context.yazarlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yazar == null)
            {
                return NotFound();
            }

            return View(yazar);
        }

        // GET: Yazar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yazar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad")] Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yazar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yazar);
        }

        // GET: Yazar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.yazarlar == null)
            {
                return NotFound();
            }

            var yazar = await _context.yazarlar.FindAsync(id);
            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        // POST: Yazar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad")] Yazar yazar)
        {
            if (id != yazar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yazar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YazarExists(yazar.Id))
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
            return View(yazar);
        }

        // GET: Yazar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.yazarlar == null)
            {
                return NotFound();
            }

            var yazar = await _context.yazarlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yazar == null)
            {
                return NotFound();
            }

            return View(yazar);
        }

        // POST: Yazar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.yazarlar == null)
            {
                return Problem("Entity set 'WebContext.yazarlar'  is null.");
            }
            var yazar = await _context.yazarlar.FindAsync(id);
            if (yazar != null)
            {
                _context.yazarlar.Remove(yazar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YazarExists(int id)
        {
          return _context.yazarlar.Any(e => e.Id == id);
        }
    }
}
