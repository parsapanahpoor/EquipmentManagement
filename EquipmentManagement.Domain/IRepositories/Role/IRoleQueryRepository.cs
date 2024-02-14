using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Domain.IRepositories.Role;

public interface IRoleQueryRepository
{
    #region Site Side 

    Task<List<ulong>> GetUserSelectedRoleIdByUserId(ulong userId,
                                                                 CancellationToken cancellation);

    Task<Domain.Entities.Account.Role> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<List<SelectListViewModel>> GetSelectRolesList(CancellationToken cancellation);

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    Task<bool> IsExistAnyRoleByRoleUniqueTitle(string title, CancellationToken cancellationToken);

    Task<Domain.Entities.Account.Role?> GetRoleByUniqueTitle(string title, CancellationToken cancellation);

    Task<List<ulong>> GetRolePermissionsIdByRoleId(ulong roleId, CancellationToken cancellationToken);

    #endregion
}
