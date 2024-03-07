using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;
using Olimp.Models;

namespace Olimp.Controllers;

[Authorize(Roles = Roles.Registrar)]
public class ParticipantsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ParticipantsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.ModelState.Remove("EduOrg");
        base.OnActionExecuting(context);
    }

    // GET: Participants
    public async Task<IActionResult> Index()
    {
        if (_context.Participants != null)
            return View(await _context.Participants.Include(p => p.EduOrg).ToListAsync());
        else
            return Problem("Entity set 'ApplicationDbContext.Participants'  is null.");
    }

    // GET: Participants/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Participants == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants
            .Include(p => p.EduOrg)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (participant == null)
        {
            return NotFound();
        }

        return View(participant);
    }

    // GET: Participants/Create
    public IActionResult Create()
    {
        ViewBag.EduOrgId = new SelectList(_context.EduOrgs, "Id", "Name");
        return View();
    }

    // POST: Participants/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Number,FirstName,SurName,LastName,Gender,Role,EduOrgId")] Participant participant)
    {
        await EnsureNumberDoesntExist(participant.Number, null);
        if (ModelState.IsValid)
        {
            participant.Id = Guid.NewGuid();
            participant.CreationDate = DateTime.UtcNow;
            _context.Add(participant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.EduOrgId = new SelectList(_context.EduOrgs, "Id", "Name");
        return View(participant);
    }

    // GET: Participants/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Participants == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants
            .Include(p => p.EduOrg)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (participant == null)
        {
            return NotFound();
        }
        ViewBag.EduOrgId = new SelectList(_context.EduOrgs, "Id", "Name");
        return View(participant);
    }

    // POST: Participants/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,FirstName,SurName,LastName,Gender,Role,EduOrgId")] Participant participant)
    {
        if (id != participant.Id)
        {
            return NotFound();
        }

        await EnsureNumberDoesntExist(participant.Number, id);
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(participant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(participant.Id))
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
        ViewBag.EduOrgId = new SelectList(_context.EduOrgs, "Id", "Name");
        return View(participant);
    }

    // GET: Participants/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Participants == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants
            .Include(p => p.EduOrg)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (participant == null)
        {
            return NotFound();
        }

        return View(participant);
    }

    // POST: Participants/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Participants == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Participants'  is null.");
        }
        var participant = await _context.Participants
            .Include(p => p.EduOrg)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (participant != null)
        {
            _context.Participants.Remove(participant);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ParticipantExists(Guid id)
    {
        return (_context.Participants?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    private async Task EnsureNumberDoesntExist(int? number, Guid? id)
    {
        if (number is null)
        {
            return;
        }
        
        var year = DateTime.UtcNow.Year;
        var numberAlreadyExistsRequest = _context.Participants
            .Where(p => p.Number == number && p.CreationDate.Year == year);

        if (id is not null)
        {
            numberAlreadyExistsRequest = numberAlreadyExistsRequest
                .Where(participant => participant.Id != id);
        }

        if (await numberAlreadyExistsRequest.AnyAsync())
        {
            ModelState.AddModelError(nameof(Participant.Number), "Участник с таким номером уже существует");
        }
    }
}