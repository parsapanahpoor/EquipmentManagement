using EquipmentManagement.Domain.DTO.SiteSide.Places;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public record EditPlaceQuery(ulong placeId) : IRequest<EditPlaceDTO>;
