using Microsoft.EntityFrameworkCore;

namespace Olimp.Data;

[PrimaryKey(nameof(Id))]
public class Step
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Description { get; set; }
    public int EquationType { get; set; }
    public int MapCoordsX { get; set; }
    public int MapCoordsY { get; set; }
    public Guid OlimpId { get; set; }
    public required Olimp Olimp { get; set; }
}