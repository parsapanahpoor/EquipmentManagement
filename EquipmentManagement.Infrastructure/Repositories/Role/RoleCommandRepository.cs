using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.Entities.Role;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

    public async Task AddPermissionToRole(RolePermission rolePermission)
    {
        await _context.RolePermissions.AddAsync(rolePermission);
    }

    public async Task RemoveRolePermissions(ulong roleId , List<ulong> rolePermissions , CancellationToken cancellation) 
    {
        //Get Role Permissions
        foreach (var permissionId in rolePermissions)
        {
            var permission = await _context.RolePermissions
                                           .FirstOrDefaultAsync(p => !p.IsDelete &&
                                                                p.RoleId == roleId &&
                                                                p.PermissionId == permissionId);
            if (permission != null)
            {
                _context.RolePermissions.Remove(permission);
            }
        }
    }

    #endregion
}
