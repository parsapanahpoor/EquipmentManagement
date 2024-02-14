namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Command;

public record DeleteRoleCommand(ulong roleId) : IRequest<bool>;
