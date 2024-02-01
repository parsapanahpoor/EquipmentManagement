using EquipmentManagement.Domain.IRepositories.User;
using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Infrastructure.Repositories.User;

public class UserQueryRepository : QueryGenericRepository<Domain.Entities.Users.User>, IUserQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public UserQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region General Methods

    public async Task<bool> IsMobileExist(string mobile, CancellationToken cancellation)
    {
        return await _context.Users
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Mobile == mobile);
    }

    public async Task<bool> IsUserActive(string mobile, CancellationToken cancellation)
    {
        return await _context.Users 
                             .AsNoTracking() 
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Mobile == mobile &&
                                       p.IsActive);
    }

    public async Task<bool> IsPasswordValid(string mobile, string password, CancellationToken cancellation)
    {
        return await _context.Users
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Mobile == mobile &&
                                       p.Password == password);
    }

    public async Task<Domain.Entities.Users.User?> GetUserByMobile(string mobile , CancellationToken cancellation)
    {
        return await _context.Users
                             .AsNoTracking()
                             .FirstOrDefaultAsync(p => !p.IsDelete &&
                                                  p.Mobile == mobile);
    }

    #endregion
}
