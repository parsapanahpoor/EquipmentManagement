using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record RoleSelectedListQuery : IRequest<List<SelectListViewModel>>
{
}
