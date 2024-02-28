using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;

namespace Olimp
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Results.Include(r => r.Participant).Include(r => r.Step);
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
            var contextParticipants = _context.Participants.Where(p => p.CreationDate.Year == DateTimeOffset.UtcNow.Year);
            ViewData["ParticipantId"] = new SelectList(contextParticipants, "Id", "Number");
            ViewData["StepId"] = new SelectList(_context.Steps, "Id", "Id");
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
            var contextParticipants = _context.Participants.Where(p => p.CreationDate.Year == DateTimeOffset.UtcNow.Year);
            ViewData["ParticipantId"] = new SelectList(contextParticipants, "Id", "Number", result.ParticipantId);
            ViewData["StepId"] = new SelectList(_context.Steps, "Id", "Id", result.StepId);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Results == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var contextParticipants = _context.Participants.Where(p => p.CreationDate.Year == DateTimeOffset.UtcNow.Year);
            ViewData["ParticipantId"] = new SelectList(contextParticipants, "Id", "Number", result.ParticipantId);
            ViewData["StepId"] = new SelectList(_context.Steps, "Id", "Id", result.StepId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ParticipantId,StepId,Score")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            var contextParticipants = _context.Participants.Where(p => p.CreationDate.Year == DateTimeOffset.UtcNow.Year);
            ViewData["ParticipantId"] = new SelectList(contextParticipants, "Id", "Number", result.ParticipantId);
            ViewData["StepId"] = new SelectList(_context.Steps, "Id", "Id", result.StepId);
            return View(result);
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
}
