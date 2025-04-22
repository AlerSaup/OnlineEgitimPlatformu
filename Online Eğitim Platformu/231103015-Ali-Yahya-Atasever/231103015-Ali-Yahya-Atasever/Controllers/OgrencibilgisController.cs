using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _231103015_Ali_Yahya_Atasever.DAL;
using Microsoft.AspNetCore.Authorization;

namespace _231103015_Ali_Yahya_Atasever.Controllers
{
    [Authorize(Roles = "3")]
    public class OgrencibilgisController : Controller
    {
        private readonly EgitimContext _context;

        public OgrencibilgisController(EgitimContext context)
        {
            _context = context;
        }

        // GET: Ogrencibilgis
        public async Task<IActionResult> Index()
        {
            var egitimContext = _context.Ogrencibilgis.Include(o => o.Bolum).Include(o => o.Sinif);
            return View(await egitimContext.ToListAsync());
        }

        // GET: Ogrencibilgis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrencibilgi = await _context.Ogrencibilgis
                .Include(o => o.Bolum)
                .Include(o => o.Sinif)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrencibilgi == null)
            {
                return NotFound();
            }

            return View(ogrencibilgi);
        }

        // GET: Ogrencibilgis/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolums, "Id", "Id");
            ViewData["SinifId"] = new SelectList(_context.Sinifs, "Id", "Id");
            return View();
        }

        // POST: Ogrencibilgis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OkulNo,Ad,Soyad,BolumId,SinifId")] Ogrencibilgi ogrencibilgi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogrencibilgi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolums, "Id", "Id", ogrencibilgi.BolumId);
            ViewData["SinifId"] = new SelectList(_context.Sinifs, "Id", "Id", ogrencibilgi.SinifId);
            return View(ogrencibilgi);
        }

        // GET: Ogrencibilgis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrencibilgi = await _context.Ogrencibilgis.FindAsync(id);
            if (ogrencibilgi == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolums, "Id", "Id", ogrencibilgi.BolumId);
            ViewData["SinifId"] = new SelectList(_context.Sinifs, "Id", "Id", ogrencibilgi.SinifId);
            return View(ogrencibilgi);
        }

        // POST: Ogrencibilgis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OkulNo,Ad,Soyad,BolumId,SinifId")] Ogrencibilgi ogrencibilgi)
        {
            if (id != ogrencibilgi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogrencibilgi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrencibilgiExists(ogrencibilgi.Id))
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
            ViewData["BolumId"] = new SelectList(_context.Bolums, "Id", "Id", ogrencibilgi.BolumId);
            ViewData["SinifId"] = new SelectList(_context.Sinifs, "Id", "Id", ogrencibilgi.SinifId);
            return View(ogrencibilgi);
        }

        // GET: Ogrencibilgis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogrencibilgi = await _context.Ogrencibilgis
                .Include(o => o.Bolum)
                .Include(o => o.Sinif)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrencibilgi == null)
            {
                return NotFound();
            }

            return View(ogrencibilgi);
        }

        // POST: Ogrencibilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogrencibilgi = await _context.Ogrencibilgis.FindAsync(id);
            if (ogrencibilgi != null)
            {
                _context.Ogrencibilgis.Remove(ogrencibilgi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgrencibilgiExists(int id)
        {
            return _context.Ogrencibilgis.Any(e => e.Id == id);
        }
    }
}
