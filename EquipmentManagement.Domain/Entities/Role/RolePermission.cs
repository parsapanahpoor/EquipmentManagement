namespace EquipmentManagement.Domain.Entities.Role;

public sealed class RolePermission : BaseEntities<ulong>
{
    #region properties

    public ulong RoleId { get; set; }

    public ulong PermissionId { get; set; }

    #endregion 
}
