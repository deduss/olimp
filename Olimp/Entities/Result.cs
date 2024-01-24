using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

[PrimaryKey(nameof(Id))]
public class Result
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid StepId { get; set; }
    public required User User { get; set; }
    public required Step Step { get; set; }
}