using EquipmentManagement.Domain.Entities.Account;

namespace EquipmentManagement.Domain.IRepositories.Role;

public interface IRoleCommandRepository
{
    #region Site Side

    Task RemoveUserRolesByUserId(ulong userId, CancellationToken cancellationToken);

    Task AddUserSelectedRole(UserRole userRole, CancellationToken cancellationToken);

    #endregion
}
