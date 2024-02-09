using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class EduOrg {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Logo { get; set; }
}