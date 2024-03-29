using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;
using Olimp.Models;

namespace Olimp.Controllers;

[Authorize(Roles = Roles.Registrar)]
public class ResultsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ResultsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.ModelState.Remove("Step");
        context.ModelState.Remove("Participant");
        base.OnActionExecuting(context);
    }

    // GET: Results
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Results
            .Include(r => r.Participant)
            .Include(r => r.Step.Olimp);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Results/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Results == null)
        {
            return NotFound();
        }

        var result = await _context.Results
            .Include(r => r.Participant)
            .Include(r => r.Step)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    // GET: Results/Create
    public IActionResult Create()
    {
        var currentYear = DateTimeOffset.UtcNow.Year;
        var contextParticipants = _context.Participants
            .Where(p => p.CreationDate.Year == currentYear)
            .Where(p => p.Number != null);
        var steps = _context.Steps
            .Include(s => s.Olimp)
            .Where(step => step.Olimp.Year == currentYear);
        ViewData["ParticipantId"] = CustomSelectList.Create(contextParticipants, x => x.Id.ToString(), 
            x => $"{x.FirstName} {x.SurName} {x.LastName} ({x.Number})");
        ViewData["StepId"] = CustomSelectList.Create(steps, x => x.Id.ToString(), 
            x => $"{x.Olimp.Name} ({x.Name})");
        return View();
    }

    // POST: Results/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ParticipantId,StepId,Score")] Result result)
    {
        if (ModelState.IsValid)
        {
            result.Id = Guid.NewGuid();
            _context.Add(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var currentYear = DateTimeOffset.UtcNow.Year;
        var contextParticipants = _context.Participants.Where(p => p.CreationDate.Year == currentYear);
        var steps = _context.Steps
            .Include(s => s.Olimp)
            .Where(step => step.Olimp.Year == currentYear);
        ViewData["ParticipantId"] = CustomSelectList.Create(contextParticipants, x => x.Id.ToString(), 
            x => $"{x.FirstName} {x.SurName} {x.LastName} ({x.Number})");
        ViewData["StepId"] = CustomSelectList.Create(steps, x => x.Id.ToString(), 
            x => $"{x.Olimp.Name} ({x.Name})");
        return View(result);
    }

    // GET: Results/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Results == null)
        {
            return NotFound();
        }
        return View(id.Value);
    }

    // GET: Results/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Results == null)
        {
            return NotFound();
        }

        var result = await _context.Results
            .Include(r => r.Participant)
            .Include(r => r.Step)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (result == null)
        {
            return NotFound();
        }

        return View(result);
    }

    // POST: Results/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Results == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Results'  is null.");
        }
        var result = await _context.Results.FindAsync(id);
        if (result != null)
        {
            _context.Results.Remove(result);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ResultExists(Guid id)
    {
        return (_context.Results?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}