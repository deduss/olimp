using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Result : IEntityId<Guid>
{
    public Guid Id { get; set; }
    [DisplayName("Номер участника")] public Guid ParticipantId { get; set; }
    [DisplayName("Номер участка")] public Guid StepId { get; set; }
    [DisplayName("Номер участника")] public required Participant Participant { get; set; }
    [DisplayName("Номер участка")] public required Step Step { get; set; }
    [DisplayName("Результат")] public required decimal Score { get; set; }
}