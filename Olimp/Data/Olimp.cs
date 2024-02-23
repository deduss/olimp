using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Olimp {
    public Guid Id { get; set; }
    [DisplayName("Название")]
    public required string Name { get; set; }
    [DisplayName("Год")]
    public int Year { get; set; }
    [DisplayName("Описание")]
    public required string Description { get; set; }
    [DisplayName("Карта")]
    public required string Map { get; set; }
}