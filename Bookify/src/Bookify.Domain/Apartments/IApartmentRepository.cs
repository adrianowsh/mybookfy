using System;

namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
	Task<Apartment?> GetByIdAsync(string id, CancellationToken cancellationToken);
}
