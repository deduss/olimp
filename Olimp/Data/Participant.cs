using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Participant : IEntityId<Guid>
{
    public Guid Id { get; set; }
    [DisplayName("Номер")] public int? Number { get; set; }
    [DisplayName("Имя")] public required string FirstName { get; set; }
    [DisplayName("Фамилия")] public required string SurName { get; set; }
    [DisplayName("Отчество")] public required string LastName { get; set; }
    [DisplayName("Пол")] public bool Gender { get; set; }
    [DisplayName("ПОО")] public Guid EduOrgId { get; set; }
    [DisplayName("ПОО")] public required EduOrg EduOrg { get; set; }
    public virtual HashSet<Result> Results { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string GetNumberOrRoleName()
    {
        return Number is not null ? Number.Value.ToString() : "Тренер";
    }
}