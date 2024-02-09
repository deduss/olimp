using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Result
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid StepId { get; set; }
    public required Participant Participant { get; set; }
    public required Step Step { get; set; }
}