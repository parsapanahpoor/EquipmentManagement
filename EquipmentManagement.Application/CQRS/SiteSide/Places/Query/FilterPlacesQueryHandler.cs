using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public record FilterPlacesQueryHandler : IRequestHandler<FilterPlacesQuery, FilterPlacesDTO>
{
    #region Ctor

    private readonly IPlacesQueryRepository _placesQueryRepository;

    public FilterPlacesQueryHandler(IPlacesQueryRepository placesQueryRepository)
    {
        _placesQueryRepository = placesQueryRepository;
    }

    #endregion

    public async Task<FilterPlacesDTO> Handle(FilterPlacesQuery request, CancellationToken cancellationToken)
    {
        return await _placesQueryRepository.FilterPlaces(new FilterPlacesDTO()
        {
            ParentId = request.ParentId,
            PlaceTitle = request.PlaceTitle
        },
        cancellationToken);
    }
}
