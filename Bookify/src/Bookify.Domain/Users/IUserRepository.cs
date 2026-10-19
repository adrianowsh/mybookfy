using System;

namespace Bookify.Domain.Users;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken);
	void Add(User user);
}
