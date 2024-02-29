using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class EduOrg : IEntityId<Guid>
{
    public Guid Id { get; set; }
    [DisplayName("Название")] public required string Name { get; set; }
    [DisplayName("Аббревиатура")] public required string ShortName { get; set; }
    [DisplayName("Логотип")] public required string Logo { get; set; }
}