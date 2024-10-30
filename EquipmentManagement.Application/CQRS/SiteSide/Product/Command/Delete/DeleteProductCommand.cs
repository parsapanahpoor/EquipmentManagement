namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command;

public record DeleteProductCommand(ulong ProductId) : IRequest<bool>;
