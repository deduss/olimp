using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Registration : IEntityId<Guid>
{
    public Guid Id { get; set; }

    public Guid OlimpId { get; set; }
    public required Olimp Olimp { get; set; }

    public Guid UserId { get; set; }
    public required Participant Participant { get; set; }

    public Guid EduOrgId { get; set; }
    public required EduOrg EduOrg { get; set; }
}