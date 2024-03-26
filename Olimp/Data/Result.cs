using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Result : IEntityId<Guid>
{
    public Guid Id { get; set; }
    [DisplayName("Номер участника")] public Guid ParticipantId { get; set; }
    [DisplayName("Номер участка")] public Guid StepId { get; set; }
    [DisplayName("Номер участника")] public Participant Participant { get; set; } = null!;
    [DisplayName("Номер участка")] public Step Step { get; set; } = null!;
    [DisplayName("Результат")] public required decimal Score { get; set; }
}