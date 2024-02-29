using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Olimp.Models;

namespace Olimp.Data.Seeds;

public class RolesSeed : IDbSeed
{
    public const string AdminId = "2301D884-221A-4E7D-B509-0113DCC043E1";
    public const string RegistrarId = "7D9B7113-A8F8-4035-99A7-A20DD400F6A3";

    public async Task SeedAsync(IdentityDbContext dbContext)
    {
        await dbContext.UpsertRange(new IdentityRole
            {
                Id = AdminId,
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper()
            }, new IdentityRole
            {
                Id = RegistrarId,
                Name = Roles.Registrar,
                NormalizedName = Roles.Registrar.ToUpper()
            })
            .On(role => role.Id)
            .RunAsync();

        await dbContext.SaveChangesAsync();
    }
}