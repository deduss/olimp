using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olimp.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSqlite<MyDbContext>(builder.Configuration.GetConnectionString("Sqlite"));
builder.Services.AddPooledDbContextFactory<MyDbContext>(optionsBuilder => 
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test/{id:guid}", async (Guid id, [FromServices] IDbContextFactory<MyDbContext> factory) => {
    await using var dbContext = await factory.CreateDbContextAsync();

    var olimp = await dbContext.Olimps
        .Where(o => o.Id == id)
        .FirstOrDefaultAsync();
    
    return olimp;
});

app.MapPost("/test", async ([FromServices] IDbContextFactory<MyDbContext> factory, [FromBody] Olimp.Entities.Olimp olimp) => {
    await using var dbContext = await factory.CreateDbContextAsync();

    await dbContext.AddAsync(olimp);
    await dbContext.SaveChangesAsync();
});

using (var scope = app.Services.CreateScope())
{
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>();
    await using var db = await dbContextFactory.CreateDbContextAsync();
    await db.Database.MigrateAsync();
}

app.Run();