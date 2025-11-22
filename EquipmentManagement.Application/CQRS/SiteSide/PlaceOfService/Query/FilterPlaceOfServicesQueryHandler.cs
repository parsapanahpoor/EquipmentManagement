using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;

public record FilterPlaceOfServicesQueryHandler : IRequestHandler<FilterPlaceOfServicesQuery, FilterPlaceOfServicesDTO>
{
    #region Ctor

    private readonly IPlaceOfServicesQueryRepository _PlaceOfServicesQueryRepository;

    public FilterPlaceOfServicesQueryHandler(IPlaceOfServicesQueryRepository PlaceOfServicesQueryRepository)
    {
        _PlaceOfServicesQueryRepository = PlaceOfServicesQueryRepository;
    }

    #endregion

    public async Task<FilterPlaceOfServicesDTO> Handle(FilterPlaceOfServicesQuery request, CancellationToken cancellationToken)
    {
        return await _PlaceOfServicesQueryRepository.FilterPlaceOfServices(request.Filter , cancellationToken);
    }
}
