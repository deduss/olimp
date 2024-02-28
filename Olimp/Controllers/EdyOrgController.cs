using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;
using Olimp.Models;

namespace Olimp.Controllers;

[Authorize(Roles = Roles.Admin)]
public class EdyOrgController : Controller
{
    private readonly ApplicationDbContext _context;

    public EdyOrgController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: EdyOrg
    public async Task<IActionResult> Index()
    {
        return _context.EduOrgs != null ? 
            View(await _context.EduOrgs.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.EduOrgs'  is null.");
    }

    // GET: EdyOrg/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.EduOrgs == null)
        {
            return NotFound();
        }

        var eduOrg = await _context.EduOrgs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (eduOrg == null)
        {
            return NotFound();
        }

        return View(eduOrg);
    }

    // GET: EdyOrg/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EdyOrg/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,ShortName,Logo")] EduOrg eduOrg)
    {
        if (ModelState.IsValid)
        {
            eduOrg.Id = Guid.NewGuid();
            _context.Add(eduOrg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(eduOrg);
    }

    // GET: EdyOrg/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.EduOrgs == null)
        {
            return NotFound();
        }

        var eduOrg = await _context.EduOrgs.FindAsync(id);
        if (eduOrg == null)
        {
            return NotFound();
        }
        return View(eduOrg);
    }

    // POST: EdyOrg/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ShortName,Logo")] EduOrg eduOrg)
    {
        if (id != eduOrg.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(eduOrg);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EduOrgExists(eduOrg.Id))
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
        return View(eduOrg);
    }

    // GET: EdyOrg/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.EduOrgs == null)
        {
            return NotFound();
        }

        var eduOrg = await _context.EduOrgs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (eduOrg == null)
        {
            return NotFound();
        }

        return View(eduOrg);
    }

    // POST: EdyOrg/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.EduOrgs == null)
        {
            return Problem("Entity set 'ApplicationDbContext.EduOrgs'  is null.");
        }
        var eduOrg = await _context.EduOrgs.FindAsync(id);
        if (eduOrg != null)
        {
            _context.EduOrgs.Remove(eduOrg);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EduOrgExists(Guid id)
    {
        return (_context.EduOrgs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}