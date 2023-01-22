using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListFinalFinal.Data;

namespace ToDoListFinalFinal.Controllers
{
    public class jobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IdentityUser Userr;

        public jobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: jobs
        public async Task<IActionResult> Index()
        {
              return View(await _context.jobs.ToListAsync());
        }

        // GET: jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.jobs == null)
            {
                return NotFound();
            }

            var job = await _context.jobs
                .FirstOrDefaultAsync(m => m.id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,priority,state,datecreated,description")] job job)
        {
            IdentityUser idu = await _context.Users.FirstOrDefaultAsync(m => m.UserName == User.Identity.Name);
            job.owner = idu;

            //if (ModelState.IsValid)
            //{
                job.datecreated = DateTime.Now;
                job.state = 0;
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(job);
        }

        // GET: jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.jobs == null)
            {
                return NotFound();
            }

            var job = await _context.jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,priority,state,datecreated,description")] job job)
        {
            if (id != job.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jobExists(job.id))
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
            return View(job);
        }

        // GET: jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.jobs == null)
            {
                return NotFound();
            }

            var job = await _context.jobs
                .FirstOrDefaultAsync(m => m.id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.jobs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.jobs'  is null.");
            }
            var job = await _context.jobs.FindAsync(id);
            if (job != null)
            {
                _context.jobs.Remove(job);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> stateUpdate(int? id)
        {
            if (id == null || _context.jobs == null)
            {
                return NotFound();
            }

            var job = await _context.jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            int i = job.state;
            if (i == 0)
            {
                i = 1;
            }
            else if (i == 1)
            {
                i = 2;
            }
            else if (i == 2)
            {
                i = 0;
            }
            job.state = i;
            //return View(jobs);
            try
            {
                _context.Update(job);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!jobExists(job.id))
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
        private bool jobExists(int id)
        {
          return _context.jobs.Any(e => e.id == id);
        }
    }
}
