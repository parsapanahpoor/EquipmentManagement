using EquipmentManagement.Domain.DTO.Common;
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

    #endregion
}
