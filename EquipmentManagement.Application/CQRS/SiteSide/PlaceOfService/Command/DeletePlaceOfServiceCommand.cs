namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public record DeletePlaceOfServiceCommand(ulong PlaceOfServiceId) : IRequest<bool>;
