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
    public class KitapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kitap
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.kitaplar.Include(k => k.dil).Include(k => k.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kitap/Details/5
        KitapYorum kitap_yorum = new KitapYorum();
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kitap = await _context.kitaplar
                .Include(k => k.dil)
                .Include(k => k.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            //ViewData["Kategori"] = _context.Kategori.Where(i => i.Id == kitap.KategoriId).Select(p=>p.KategoriAd).FirstOrDefault();
            //ViewData["Dil"] = _context.Dil.Where(i => i.Id == kitap.DilId).Select(p=>p.DilId).FirstOrDefault();
            kitap_yorum.Kitap= _context.kitaplar.Where(x => x.Id == id).FirstOrDefault();
            kitap_yorum.yorum = _context.yorumlar.Where(x => x.KitapId == id).ToList();
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap_yorum);
        }

        // GET: Kitap/Create
        public IActionResult Create()
        {
            ViewData["DilId"] = new SelectList(_context.diller, "Id", "DilId");
            ViewData["KategoriId"] = new SelectList(_context.kategoriler, "Id", "KategoriAd");
            return View();
        }

        // POST: Kitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KitapAdi,sayfaSayisi,basimYili,konu,KategoriId,DilId")] Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kitap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DilId"] = new SelectList(_context.diller, "Id", "DilId", kitap.DilId);
            ViewData["KategoriId"] = new SelectList(_context.kategoriler, "Id", "KategoriAd", kitap.KategoriId);
            return View(kitap);
        }

        // GET: Kitap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.kitaplar.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }
            ViewData["DilId"] = new SelectList(_context.diller, "Id", "DilId", kitap.DilId);
            ViewData["KategoriId"] = new SelectList(_context.kategoriler, "Id", "KategoriAd", kitap.KategoriId);
            return View(kitap);
        }

        // POST: Kitap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,KitapAdi,sayfaSayisi,basimYili,konu,KategoriId,DilId")] Kitap kitap)
        {
            if (id != kitap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kitap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitapExists(kitap.Id))
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
            ViewData["DilId"] = new SelectList(_context.diller, "Id", "DilId", kitap.DilId);
            ViewData["KategoriId"] = new SelectList(_context.kategoriler, "Id", "KategoriAd", kitap.KategoriId);
            return View(kitap);
        }

        // GET: Kitap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kitap = await _context.kitaplar
                .Include(k => k.dil)
                .Include(k => k.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }

            return View(kitap);
        }

        // POST: Kitap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kitap = await _context.kitaplar.FindAsync(id);
            _context.kitaplar.Remove(kitap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitapExists(int id)
        {
            return _context.kitaplar.Any(e => e.Id == id);
        }
    }
}
