using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

public class MyDbContext : DbContext {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public DbSet<Olimp> Olimps { get; set; }
    public DbSet<EduOrg> EduOrgs { get; set; }
    public DbSet<Registration> Registrations { get; set; }
}
[PrimaryKey(nameof(Id))]
public class Olimp {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Year { get; set; }
    public required string Description { get; set; }
    public required string Map { get; set; }
}
[PrimaryKey(nameof(Id))]
public class EduOrg {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Logo { get; set; }
}
[PrimaryKey(nameof(Id))]
public class Registration {
    public Guid Id { get; set; }

    public Guid OlimpId { get; set; }
    public required Olimp Olimp { get; set; }

    public Guid EduOrgId { get; set; }
    public required EduOrg EduOrg { get; set; }
}