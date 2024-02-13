using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.IRepositories.Role;

public interface IRoleQueryRepository
{
    #region Site Side 

    Task<List<ulong>> GetUserSelectedRoleIdByUserId(ulong userId,
                                                                 CancellationToken cancellation);

    Task<List<SelectListViewModel>> GetSelectRolesList(CancellationToken cancellation);

    #endregion
}
