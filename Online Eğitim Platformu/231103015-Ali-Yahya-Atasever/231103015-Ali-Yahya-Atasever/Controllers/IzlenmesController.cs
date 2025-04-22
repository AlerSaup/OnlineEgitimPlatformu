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
    public class IzlenmesController : Controller
    {
        private readonly EgitimContext _context;

        public IzlenmesController(EgitimContext context)
        {
            _context = context;
        }

        // GET: Izlenmes
        public async Task<IActionResult> Index()
        {
            var egitimContext = _context.Izlenmes.Include(i => i.Ogrencibilgi).Include(i => i.Video);
            return View(await egitimContext.ToListAsync());
        }

        // GET: Izlenmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izlenme = await _context.Izlenmes
                .Include(i => i.Ogrencibilgi)
                .Include(i => i.Video)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (izlenme == null)
            {
                return NotFound();
            }

            return View(izlenme);
        }

        // GET: Izlenmes/Create
        public IActionResult Create()
        {
            ViewData["OgrencibilgiId"] = new SelectList(_context.Ogrencibilgis, "Id", "Id");
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id");
            return View();
        }

        // POST: Izlenmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OgrencibilgiId,VideoId,Izlenmemiktari")] Izlenme izlenme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(izlenme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgrencibilgiId"] = new SelectList(_context.Ogrencibilgis, "Id", "Id", izlenme.OgrencibilgiId);
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", izlenme.VideoId);
            return View(izlenme);
        }

        // GET: Izlenmes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izlenme = await _context.Izlenmes.FindAsync(id);
            if (izlenme == null)
            {
                return NotFound();
            }
            ViewData["OgrencibilgiId"] = new SelectList(_context.Ogrencibilgis, "Id", "Id", izlenme.OgrencibilgiId);
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", izlenme.VideoId);
            return View(izlenme);
        }

        // POST: Izlenmes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OgrencibilgiId,VideoId,Izlenmemiktari")] Izlenme izlenme)
        {
            if (id != izlenme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izlenme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzlenmeExists(izlenme.Id))
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
            ViewData["OgrencibilgiId"] = new SelectList(_context.Ogrencibilgis, "Id", "Id", izlenme.OgrencibilgiId);
            ViewData["VideoId"] = new SelectList(_context.Videos, "Id", "Id", izlenme.VideoId);
            return View(izlenme);
        }

        // GET: Izlenmes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izlenme = await _context.Izlenmes
                .Include(i => i.Ogrencibilgi)
                .Include(i => i.Video)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (izlenme == null)
            {
                return NotFound();
            }

            return View(izlenme);
        }

        // POST: Izlenmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var izlenme = await _context.Izlenmes.FindAsync(id);
            if (izlenme != null)
            {
                _context.Izlenmes.Remove(izlenme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzlenmeExists(int id)
        {
            return _context.Izlenmes.Any(e => e.Id == id);
        }
    }
}
