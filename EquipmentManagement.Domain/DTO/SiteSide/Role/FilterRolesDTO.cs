using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.Role;

public class FilterRolesDTO : BasePaging<Entities.Account.Role>
{
    #region properties

    public string? RoleTitle { get; set; }

    #endregion
}
