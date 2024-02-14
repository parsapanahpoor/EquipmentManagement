namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public record DeleteUserCommand(ulong userId) : IRequest<bool>;
