using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Step
{
    public Guid Id { get; set; }
    [DisplayName("Название")]
    public required string Name { get; set; }
    [DisplayName("Тип")]
    public required string Type { get; set; }
    [DisplayName("Описание")]
    public required string Description { get; set; }
    [DisplayName("Типы вычисления")]
    public int EquationType { get; set; }
    [DisplayName("Координата x на карте")]
    public int MapCoordsX { get; set; }
    [DisplayName("Координата y на карте")]
    public int MapCoordsY { get; set; }
    [DisplayName("Олимпиада")]
    public Guid OlimpId { get; set; }
    [DisplayName("Олимпиада")]
    public Olimp Olimp { get; set; } = null!;
}