using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.Entities.Role;

namespace EquipmentManagement.Domain.IRepositories.Role;

public interface IRoleCommandRepository
{
    #region Site Side

    Task RemoveUserRolesByUserId(ulong userId, CancellationToken cancellationToken);

    Task AddUserSelectedRole(UserRole userRole, CancellationToken cancellationToken);

    Task AddAsync(Domain.Entities.Account.Role role , CancellationToken cancellationToken);

    void Update(Domain.Entities.Account.Role role);

    Task AddPermissionToRole(RolePermission rolePermission);

    Task RemoveRolePermissions(ulong roleId, List<ulong> rolePermissions, CancellationToken cancellation);

    #endregion
}
