using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Olimp.Models;

namespace Olimp.Data.Seeds;

public class UsersSeed : IDbSeed
{
    private readonly IConfiguration _configuration;
    public UsersSeed(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public const string AdminId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";
    public const string RegistrarId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F8";

    private static string PassGenerate(IdentityUser user, string password)
    {
        var passHash = new PasswordHasher<IdentityUser>();
        return passHash.HashPassword(user, password);
    }

    public async Task SeedAsync(IdentityDbContext dbContext)
    {
        var adminCredentials = _configuration.GetSection("Admin").Get<UserCredentialsConfig>() 
                               ?? throw new Exception("Specify admin credentials in config");
        var admin = new IdentityUser
        {
            Id = AdminId,
            UserName = adminCredentials.Email,
            NormalizedUserName = adminCredentials.Email.ToUpper(),
            Email = adminCredentials.Email,
            NormalizedEmail = adminCredentials.Email.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D")
        };
        admin.PasswordHash = PassGenerate(admin, adminCredentials.Password);
        
        var registrarCredentials = _configuration.GetSection("Registrar").Get<UserCredentialsConfig>() 
                                   ?? throw new Exception("Specify registrar credentials in config");
        var registrar = new IdentityUser
        {
            Id = RegistrarId,
            UserName = registrarCredentials.Email,
            NormalizedUserName = registrarCredentials.Email.ToUpper(),
            Email = registrarCredentials.Email,
            NormalizedEmail = registrarCredentials.Email.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D")
        };
        registrar.PasswordHash = PassGenerate(registrar, registrarCredentials.Password);

        await dbContext.UpsertRange(admin, registrar)
            .On(user => user.Id)
            .RunAsync();

        await dbContext.SaveChangesAsync();
    }
}