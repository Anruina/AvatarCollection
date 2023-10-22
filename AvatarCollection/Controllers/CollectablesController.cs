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
    public class CollectablesController : Controller
    {
        private readonly DataDbContext _context;

        public CollectablesController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Collectables
        public async Task<IActionResult> Index()
        {
              return _context.Collectables != null ? 
                          View(await _context.Collectables.ToListAsync()) :
                          Problem("Entity set 'DataDbContext.Collectables'  is null.");
        }

        // GET: Collectables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collectables == null)
            {
                return NotFound();
            }

            var collectable = await _context.Collectables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectable == null)
            {
                return NotFound();
            }

            return View(collectable);
        }

        // GET: Collectables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collectables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollectableID,Name,Description,Worth,Price,Releasedate,Category,OperatingSystem,Platform,ComicEdition,Comic,Novel,BlueRay,DVD,PVC,FunkoPop,Id")] Collectable collectable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collectable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collectable);
        }

        // GET: Collectables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collectables == null)
            {
                return NotFound();
            }

            var collectable = await _context.Collectables.FindAsync(id);
            if (collectable == null)
            {
                return NotFound();
            }
            return View(collectable);
        }

        // POST: Collectables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollectableID,Name,Description,Worth,Price,Releasedate,Category,OperatingSystem,Platform,ComicEdition,Comic,Novel,BlueRay,DVD,PVC,FunkoPop,Id")] Collectable collectable)
        {
            if (id != collectable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collectable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectableExists(collectable.Id))
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
            return View(collectable);
        }

        // GET: Collectables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collectables == null)
            {
                return NotFound();
            }

            var collectable = await _context.Collectables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collectable == null)
            {
                return NotFound();
            }

            return View(collectable);
        }

        // POST: Collectables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collectables == null)
            {
                return Problem("Entity set 'DataDbContext.Collectables'  is null.");
            }
            var collectable = await _context.Collectables.FindAsync(id);
            if (collectable != null)
            {
                _context.Collectables.Remove(collectable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectableExists(int id)
        {
          return (_context.Collectables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
