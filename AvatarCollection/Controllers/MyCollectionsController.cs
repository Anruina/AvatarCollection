using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvatarCollection.Data;
using AvatarCollectionLibrary;
using System.Security.Claims;

namespace AvatarCollection.Controllers
{
    public class MyCollectionsController : Controller
    {
        private readonly DataDbContext _context;

        private List<Collectable> NewCollection = new List<Collectable>();

        public MyCollectionsController(DataDbContext context)
        {
            _context = context;
        }

        // GET: MyCollections
        public async Task<IActionResult> Index()
        {
              return _context.MyCollections != null ? 
                          View(await _context.MyCollections.ToListAsync()) :
                          Problem("Entity set 'DataDbContext.MyCollections'  is null.");
        }

        // GET: MyCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MyCollections == null)
            {
                return NotFound();
            }

            var myCollection = await _context.MyCollections
                .FirstOrDefaultAsync(m => m.MyCollectionID == id);
            if (myCollection == null)
            {
                return NotFound();
            }

            return View(myCollection);
        }

        // GET: MyCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MyCollectionID")] MyCollection myCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myCollection);
        }

        // GET: MyCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MyCollections == null)
            {
                return NotFound();
            }

            var myCollection = await _context.MyCollections.FindAsync(id);
            if (myCollection == null)
            {
                return NotFound();
            }
            return View(myCollection);
        }

        // POST: MyCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MyCollectionID")] MyCollection myCollection)
        {
            if (id != myCollection.MyCollectionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyCollectionExists(myCollection.MyCollectionID))
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
            return View(myCollection);
        }

        // GET: MyCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MyCollections == null)
            {
                return NotFound();
            }

            var myCollection = await _context.MyCollections
                .FirstOrDefaultAsync(m => m.MyCollectionID == id);
            if (myCollection == null)
            {
                return NotFound();
            }

            return View(myCollection);
        }

        // POST: MyCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MyCollections == null)
            {
                return Problem("Entity set 'DataDbContext.MyCollections'  is null.");
            }
            var myCollection = await _context.MyCollections.FindAsync(id);
            if (myCollection != null)
            {
                _context.MyCollections.Remove(myCollection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyCollectionExists(int id)
        {
          return (_context.MyCollections?.Any(e => e.MyCollectionID == id)).GetValueOrDefault();
        }
        #region Add & Remove collectables from My Collection

        /// <summary>
        /// adds a new collectable to a list
        /// </summary>
        /// <returns></returns>
        public void AddNewCollectable(int id)
        {
            Collectable collectable = _context.Collectables.Where(c => c.Id == id).First();

            if (collectable == null)
                return;

            NewCollection.Add(collectable);
        }


        public void RemoveCollectable(int id)
        {
            Collectable collectable = _context.Collectables.Where(c => c.Id == id).First();

            if (collectable == null)
                return;

            NewCollection.Add(collectable);
        }


        public void SaveCollection()
        {
            MyCollection myCollection = new MyCollection()
            {
                MyCollectionID = 0,
                Collectables = NewCollection,
                Users = _context.Users.Where(u => u.AuthenticationId == User.FindFirstValue(ClaimTypes.NameIdentifier)).First()
            };

            _context.MyCollections.Add(myCollection);
        }
    }
    #endregion
}
