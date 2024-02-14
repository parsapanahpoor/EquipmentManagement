using EquipmentManagement.Domain.DTO.SiteSide.Role;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Command;

public class EditRoleCommand : IRequest<EditRoleResult>
{
    #region properties

    public string Title { get; set; }

    public string RoleUniqueName { get; set; }

    public List<ulong>? Permissions { get; set; }

    public ulong Id { get; set; }

    #endregion
}
