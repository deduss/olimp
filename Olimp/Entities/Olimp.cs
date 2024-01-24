using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

[PrimaryKey(nameof(Id))]
public class Olimp {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Year { get; set; }
    public required string Description { get; set; }
    public required string Map { get; set; }
}