using EquipmentManagement.Domain.DTO.SiteSide.Role;
namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record EditRoleQuery (ulong roleId) : IRequest<EditRoleDTO>;
