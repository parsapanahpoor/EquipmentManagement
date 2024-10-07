using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public record FilterPlacesForExcelFileQueryHandler :
    IRequestHandler<FilterPlacesForExcelFileQuery, List<FilterPlacesForExcelFileDTO>>
{
    #region Ctor

    private readonly IPlacesQueryRepository _placesQueryRepository;

    public FilterPlacesForExcelFileQueryHandler(IPlacesQueryRepository placesQueryRepository)
    {
        _placesQueryRepository = placesQueryRepository;
    }

    #endregion

    public async Task<List<FilterPlacesForExcelFileDTO>> Handle(
        FilterPlacesForExcelFileQuery request, 
        CancellationToken cancellationToken)
    {
        return await _placesQueryRepository.FilterPlacesForExcelFile(cancellationToken);
    }
}
