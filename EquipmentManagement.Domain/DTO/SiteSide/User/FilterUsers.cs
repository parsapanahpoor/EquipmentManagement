using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.User;

public class FilterUsersDTO : BasePaging<Entities.Users.User>
{
    #region properties

    public string? Username { get; set; }

    public string? Mobile { get; set; }

    #endregion
}
