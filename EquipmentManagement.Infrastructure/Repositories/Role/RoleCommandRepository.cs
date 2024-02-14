using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.Role;

public class RoleCommandRepository : CommandGenericRepository<Domain.Entities.Account.Role>, IRoleCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public RoleCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side

    public async Task RemoveUserRolesByUserId(ulong userId, CancellationToken cancellationToken)
    {
        var userSelectedRoles = await _context.UserRole
                                              .AsNoTracking()
                                              .Where(p => p.UserId == userId)
                                              .ToListAsync();

        _context.UserRole.RemoveRange(userSelectedRoles);
    }

    public async Task AddUserSelectedRole(UserRole userRole, CancellationToken cancellationToken)
    {
        await _context.UserRole.AddAsync(userRole);
    }

    #endregion
}
