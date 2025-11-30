using EquipmentManagement.Domain.Common;
namespace EquipmentManagement.Domain.Entities.Account;

public class UserRole : BaseEntities<ulong>
{
    #region properties

    public ulong UserId { get; set; }

    public ulong RoleId { get; set; }
    public Role Role { get; set; }

    #endregion
}
