namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public record DeletePlaceCommand(ulong placeId) : IRequest<bool>;
