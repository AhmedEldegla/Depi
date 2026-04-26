using Depi.Application.Repositories.Identity;
using DEPI.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUserGUID(Guid guid)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Id == guid);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetWithRolesAsync(Guid id)
    {
        return await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByRefreshTokenAsync(string token)
    {
        return await _dbSet.FirstOrDefaultAsync(u => 
            u.RefreshToken == token && 
            u.RefreshTokenExpiry > DateTime.UtcNow);
    }
}