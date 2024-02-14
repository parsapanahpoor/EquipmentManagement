using EquipmentManagement.Domain.DTO.SiteSide.Role;
namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public class FilterRolesQuery : IRequest<FilterRolesDTO>
{
    #region properties

    public string? RoleTitle { get; set; }

    #endregion
}
