namespace Bookify.Domain.Abstractions;

public abstract class Entity<T> where T : class
{
    protected Entity(T id)
    {
        Id = id;
    }

    public T Id { get; init; }
}
