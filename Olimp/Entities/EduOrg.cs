using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

[PrimaryKey(nameof(Id))]
public class EduOrg {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Logo { get; set; }
}