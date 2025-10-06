using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;

public record GetListOfPlaceOfServiceQueryHandler(
    IPlaceOfServicesQueryRepository placeOfServicesQueryRepository) : 
    IRequestHandler<GetListOfPlaceOfServiceQuery, List<Domain.Entities.PlaceOfService.PlaceOfService>>
{

    public async Task<List<Domain.Entities.PlaceOfService.PlaceOfService>> Handle(
        GetListOfPlaceOfServiceQuery request, 
        CancellationToken cancellationToken)
    {
        return await placeOfServicesQueryRepository.GetAllOfOlaceOfServices(cancellationToken);
    }
}