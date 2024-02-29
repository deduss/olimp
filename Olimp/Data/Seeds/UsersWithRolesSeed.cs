using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Olimp.Data.Seeds;

public class UsersWithRolesSeed : IDbSeed
{
    public async Task SeedAsync(IdentityDbContext dbContext)
    {
        var adminRole = new IdentityUserRole<string>
        {
            RoleId = RolesSeed.AdminId,
            UserId = UsersSeed.AdminId
        };

        var registrarRole = new IdentityUserRole<string>
        {
            RoleId = RolesSeed.RegistrarId,
            UserId = UsersSeed.RegistrarId
        };

        await dbContext.UpsertRange(adminRole, registrarRole)
            .On(role => new { role.RoleId, role.UserId })
            .RunAsync();

        await dbContext.SaveChangesAsync();
    }
}