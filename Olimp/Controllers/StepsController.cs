using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;
using Olimp.Models;

namespace Olimp.Controllers;

[Authorize(Roles = Roles.Admin)]
public class StepsController : Controller
{
    private readonly ApplicationDbContext _context;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.ModelState.Remove("Olimp");
        context.ModelState.Remove("Results");
        base.OnActionExecuting(context);
    }

    public StepsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Steps
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Steps.Include(s => s.Olimp);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Steps/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Steps == null)
        {
            return NotFound();
        }

        var step = await _context.Steps
            .Include(s => s.Olimp)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (step == null)
        {
            return NotFound();
        }

        return View(step);
    }

    // GET: Steps/Create
    public IActionResult Create()
    {
        ViewData["OlimpId"] = new SelectList(_context.Olimps, "Id", "Name");
        return View();
    }

    // POST: Steps/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Type,Description,EquationType,MapCoordsX,MapCoordsY,OlimpId")] Step step)
    {
        if (ModelState.IsValid)
        {
            step.Id = Guid.NewGuid();
            _context.Add(step);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OlimpId"] = new SelectList(_context.Olimps, "Id", "Name", step.OlimpId);
        return View(step);
    }

    // GET: Steps/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Steps == null)
        {
            return NotFound();
        }

        var step = await _context.Steps.FindAsync(id);
        if (step == null)
        {
            return NotFound();
        }
        ViewData["OlimpId"] = new SelectList(_context.Olimps, "Id", "Name", step.OlimpId);
        return View(step);
    }

    // POST: Steps/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Description,EquationType,MapCoordsX,MapCoordsY,OlimpId")] Step step)
    {
        if (id != step.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(step);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepExists(step.Id))
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
        ViewData["OlimpId"] = new SelectList(_context.Olimps, "Id", "Name", step.OlimpId);
        return View(step);
    }

    // GET: Steps/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Steps == null)
        {
            return NotFound();
        }

        var step = await _context.Steps
            .Include(s => s.Olimp)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (step == null)
        {
            return NotFound();
        }

        return View(step);
    }

    // POST: Steps/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Steps == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Steps'  is null.");
        }
        var step = await _context.Steps.FindAsync(id);
        if (step != null)
        {
            _context.Steps.Remove(step);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StepExists(Guid id)
    {
        return (_context.Steps?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}