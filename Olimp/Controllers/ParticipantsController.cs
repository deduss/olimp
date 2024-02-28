using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;

namespace Olimp.Controllers;

public class ParticipantsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ParticipantsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Participants
    public async Task<IActionResult> Index()
    {
        return _context.Participants != null ? 
            View(await _context.Participants.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Participants'  is null.");
    }

    // GET: Participants/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Participants == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants
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
        return View();
    }

    // POST: Participants/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Number,FirstName,SurName,LastName,Gender,Role")] Participant participant)
    {
        if (ModelState.IsValid)
        {
            participant.Id = Guid.NewGuid();
            participant.CreationDate = DateTime.UtcNow;
            _context.Add(participant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(participant);
    }

    // GET: Participants/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Participants == null)
        {
            return NotFound();
        }

        var participant = await _context.Participants.FindAsync(id);
        if (participant == null)
        {
            return NotFound();
        }
        return View(participant);
    }

    // POST: Participants/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,FirstName,SurName,LastName,Gender,Role")] Participant participant)
    {
        if (id != participant.Id)
        {
            return NotFound();
        }

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
        var participant = await _context.Participants.FindAsync(id);
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
}