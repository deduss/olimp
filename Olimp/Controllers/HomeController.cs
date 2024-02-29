using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olimp.Data;
using Olimp.Models;

namespace Olimp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public HomeController(ILogger<HomeController> logger, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IActionResult> Index()
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        var olimp = await dbContext.Olimps
            .Include(olimp => olimp.Steps)
            .ThenInclude(step => step.Results)
            .ThenInclude(result => result.Participant)
            .ThenInclude(participant => participant.EduOrg)
            .OrderByDescending(olimp => olimp.Year)
            .FirstOrDefaultAsync();
        return View(olimp);
    }

    [AnyRoleAuthorize(Roles.Admin, Roles.Registrar)]
    public IActionResult Info()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}