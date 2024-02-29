using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Olimp.Data.Seeds;

public interface IDbSeed
{
    Task SeedAsync(IdentityDbContext dbContext);
}