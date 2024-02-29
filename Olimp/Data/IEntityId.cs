namespace Olimp.Data;

public interface IEntityId<T>
{
    T Id { get; set; }
}