
using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;

public record EditPlaceOfServiceQuery(ulong PlaceOfServiceId) : IRequest<EditPlaceOfServiceDTO>;
