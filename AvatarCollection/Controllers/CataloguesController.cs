using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvatarCollection.Data;
using AvatarCollectionLibrary;

namespace AvatarCollection.Controllers
{
    public class CataloguesController : Controller
    {
        private readonly DataDbContext _context;

        public CataloguesController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Catalogues
        public async Task<IActionResult> Index()
        {
              return _context.Catalogues != null ? 
                          View(await _context.Catalogues.ToListAsync()) :
                          Problem("Entity set 'DataDbContext.Catalogues'  is null.");
        }

        // GET: Catalogues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catalogues == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .FirstOrDefaultAsync(m => m.CatalogueID == id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return View(catalogue);
        }

        // GET: Catalogues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalogues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogueID")] Catalogue catalogue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogue);
        }

        // GET: Catalogues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catalogues == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue == null)
            {
                return NotFound();
            }
            return View(catalogue);
        }

        // POST: Catalogues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CatalogueID")] Catalogue catalogue)
        {
            if (id != catalogue.CatalogueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogueExists(catalogue.CatalogueID))
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
            return View(catalogue);
        }

        // GET: Catalogues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catalogues == null)
            {
                return NotFound();
            }

            var catalogue = await _context.Catalogues
                .FirstOrDefaultAsync(m => m.CatalogueID == id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return View(catalogue);
        }

        // POST: Catalogues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Catalogues == null)
            {
                return Problem("Entity set 'DataDbContext.Catalogues'  is null.");
            }
            var catalogue = await _context.Catalogues.FindAsync(id);
            if (catalogue != null)
            {
                _context.Catalogues.Remove(catalogue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogueExists(int? id)
        {
          return (_context.Catalogues?.Any(e => e.CatalogueID == id)).GetValueOrDefault();
        }
    }
}
