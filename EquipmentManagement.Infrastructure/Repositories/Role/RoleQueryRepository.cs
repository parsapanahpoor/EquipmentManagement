using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.IRepositories.Role;
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

    public async Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter , CancellationToken cancellation)
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

    public async Task<bool> IsExistAnyRoleByRoleUniqueTitle(string title , CancellationToken cancellationToken)
    {
        return await _context.Role
                             .AsNoTracking()
                             .AnyAsync(p => !p.IsDelete &&
                                       p.Title == title);
    }

    public async Task<Domain.Entities.Account.Role?> GetRoleByUniqueTitle(string title  , CancellationToken cancellation)
    {
        return await _context.Role
                             .AsNoTracking()
                             .FirstOrDefaultAsync(p => !p.IsDelete &&
                                                  p.RoleUniqueName == title);
    }

    #endregion
}
