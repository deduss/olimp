using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

public class MyDbContext : DbContext {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public DbSet<Olimp> Olimps { get; set; }
    public DbSet<EduOrg> EduOrgs { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<Result> Results { get; set; }
}