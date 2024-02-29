using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public required DbSet<Olimp> Olimps { get; set; }
    public required DbSet<EduOrg> EduOrgs { get; set; }
    public required DbSet<Registration> Registrations { get; set; }
    public required DbSet<Participant> Participants { get; set; }
    public required DbSet<Step> Steps { get; set; }
    public required DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}