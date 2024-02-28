using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;

namespace Olimp;

public class OlympicsController : Controller
{
    private readonly ApplicationDbContext _context;

    public OlympicsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Olympics
    public async Task<IActionResult> Index()
    {
        return _context.Olimps != null ? 
            View(await _context.Olimps.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Olimps'  is null.");
    }

    // GET: Olympics/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Olimps == null)
        {
            return NotFound();
        }

        var olimp = await _context.Olimps
            .FirstOrDefaultAsync(m => m.Id == id);
        if (olimp == null)
        {
            return NotFound();
        }

        return View(olimp);
    }

    // GET: Olympics/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Olympics/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Year,Description,Map")] Data.Olimp olimp)
    {
        if (ModelState.IsValid)
        {
            olimp.Id = Guid.NewGuid();
            _context.Add(olimp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(olimp);
    }

    // GET: Olympics/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Olimps == null)
        {
            return NotFound();
        }

        var olimp = await _context.Olimps.FindAsync(id);
        if (olimp == null)
        {
            return NotFound();
        }
        return View(olimp);
    }

    // POST: Olympics/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Year,Description,Map")] Data.Olimp olimp)
    {
        if (id != olimp.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(olimp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OlimpExists(olimp.Id))
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
        return View(olimp);
    }

    // GET: Olympics/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Olimps == null)
        {
            return NotFound();
        }

        var olimp = await _context.Olimps
            .FirstOrDefaultAsync(m => m.Id == id);
        if (olimp == null)
        {
            return NotFound();
        }

        return View(olimp);
    }

    // POST: Olympics/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Olimps == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Olimps'  is null.");
        }
        var olimp = await _context.Olimps.FindAsync(id);
        if (olimp != null)
        {
            _context.Olimps.Remove(olimp);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OlimpExists(Guid id)
    {
        return (_context.Olimps?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}