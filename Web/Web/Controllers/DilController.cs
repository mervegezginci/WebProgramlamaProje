﻿using System;
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
    public class DilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DilController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dil
        public async Task<IActionResult> Index()
        {
              return View(await _context.diller.ToListAsync());
        }

        // GET: Dil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.diller == null)
            {
                return NotFound();
            }

            var dil = await _context.diller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dil == null)
            {
                return NotFound();
            }

            return View(dil);
        }

        // GET: Dil/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DilId")] Dil dil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dil);
        }

        // GET: Dil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.diller == null)
            {
                return NotFound();
            }

            var dil = await _context.diller.FindAsync(id);
            if (dil == null)
            {
                return NotFound();
            }
            return View(dil);
        }

        // POST: Dil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DilId")] Dil dil)
        {
            if (id != dil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DilExists(dil.Id))
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
            return View(dil);
        }

        // GET: Dil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.diller == null)
            {
                return NotFound();
            }

            var dil = await _context.diller
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dil == null)
            {
                return NotFound();
            }

            return View(dil);
        }

        // POST: Dil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.diller == null)
            {
                return Problem("Entity set 'WebContext.Dil'  is null.");
            }
            var dil = await _context.diller.FindAsync(id);
            if (dil != null)
            {
                _context.diller.Remove(dil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DilExists(int id)
        {
          return _context.diller.Any(e => e.Id == id);
        }
    }
}
