using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;
namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;

public record GetListOfPlaceOfServiceQuery : 
    IRequest<List<Domain.Entities.PlaceOfService.PlaceOfService>>;
