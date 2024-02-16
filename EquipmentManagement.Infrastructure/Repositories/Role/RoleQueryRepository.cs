using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.Role;

public class RoleQueryRepository : QueryGenericRepository<Domain.Entities.Account.Role>, IRoleQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public RoleQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side 

    public async Task<bool> HasUserPermission(ulong userId, string permissionName)
    {
        // get user
        var user = await _context.Users
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(s => s.Id == userId && !s.IsDelete);

        // check user exists
        if (user == null) return false;

        // admin user access any where
        if (user.IsAdmin) return true;

        // get permission from permission list
        var permission = PermissionsList.Permissions.FirstOrDefault(s => s.PermissionUniqueName == permissionName);

        // check permission exists
        if (permission == null) return false;

        // get user roles
        var userRoles = await _context.UserRole
                                      .AsNoTracking()
                                      .Where(s => s.UserId == userId && 
                                             !s.IsDelete)
                                       .ToListAsync();

        // check user has any roles
        if (!userRoles.Any()) return false;

        // get user role Ids list
        var userRoleIds = userRoles.Select(s => s.RoleId).ToList();

        // check user has permission
        var result = await _context.RolePermissions.AnyAsync(s =>
            s.PermissionId == permission.Id && userRoleIds.Contains(s.RoleId) && !s.IsDelete);

        return result;
    }

    public async Task<List<ulong>> GetUserSelectedRoleIdByUserId(ulong userId,
                                                                 CancellationToken cancellation)
    {
        return await _context.UserRole
                             .AsNoTracking()
                             .Where(s => !s.IsDelete &&
                                    s.UserId == userId)
                             .Select(s => s.RoleId)
                             .ToListAsync();
    }

    public async Task<List<SelectListViewModel>> GetSelectRolesList(CancellationToken cancellation)
    {
        return await _context.Role
                             .AsNoTracking()
                             .Where(s => !s.IsDelete)
                             .Select(s => new SelectListViewModel
                             {
                                 Id = s.Id,
                                 Title = s.Title
                             })
                             .ToListAsync();
    }

    public async Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation)
    {
        var query = _context.Role
                           .AsNoTracking()
                           .OrderByDescending(p => p.CreateDate)
                           .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(filter.RoleTitle)))
        {
            query = query.Where(u => u.Title.Contains(filter.RoleTitle));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<bool> IsExistAnyRoleByRoleUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Role
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Title == title);
    }

    public async Task<Domain.Entities.Account.Role?> GetRoleByUniqueTitle(string title, CancellationToken cancellation)
    {
        return await _context.Role
                             .AsNoTracking()
                             .FirstOrDefaultAsync(p => !p.IsDelete &&
                                                  p.RoleUniqueName == title);
    }

    public async Task<List<ulong>> GetRolePermissionsIdByRoleId(ulong roleId, CancellationToken cancellationToken)
    {
        return await _context.RolePermissions
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.RoleId == roleId)
                             .Select(p => p.PermissionId)
                             .ToListAsync();
    }

    #endregion
}
