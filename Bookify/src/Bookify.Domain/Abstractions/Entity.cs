namespace Bookify.Domain.Abstractions;

public abstract class Entity<T> where T : class
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity(T id)
    {
        Id = id;
    }
    public T Id { get; init; }
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
