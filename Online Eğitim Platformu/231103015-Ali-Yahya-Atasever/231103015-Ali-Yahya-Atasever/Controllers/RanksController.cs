﻿using System;
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
    public class RanksController : Controller
    {
        private readonly EgitimContext _context;

        public RanksController(EgitimContext context)
        {
            _context = context;
        }

        // GET: Ranks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ranks.ToListAsync());
        }

        // GET: Ranks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // GET: Ranks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Rank rank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rank);
        }

        // GET: Ranks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks.FindAsync(id);
            if (rank == null)
            {
                return NotFound();
            }
            return View(rank);
        }

        // POST: Ranks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Rank rank)
        {
            if (id != rank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankExists(rank.Id))
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
            return View(rank);
        }

        // GET: Ranks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // POST: Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rank = await _context.Ranks.FindAsync(id);
            if (rank != null)
            {
                _context.Ranks.Remove(rank);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankExists(int id)
        {
            return _context.Ranks.Any(e => e.Id == id);
        }
    }
}
