using Microsoft.EntityFrameworkCore;

namespace Olimp.Entities;

[PrimaryKey(nameof(Id))]
public class User
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public required string FirstName { get; set; }
    public required string SurName { get; set; }
    public required string LastName { get; set; }
    public bool Gender { get; set; }
    public bool Role { get; set; }
}