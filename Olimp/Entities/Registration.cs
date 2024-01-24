using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

[PrimaryKey(nameof(Id))]
public class Registration {
    public Guid Id { get; set; }

    public Guid OlimpId { get; set; }
    public required Olimp Olimp { get; set; }
    
    public Guid UserId { get; set; }
    public required User User { get; set; }

    public Guid EduOrgId { get; set; }
    public required EduOrg EduOrg { get; set; }
}