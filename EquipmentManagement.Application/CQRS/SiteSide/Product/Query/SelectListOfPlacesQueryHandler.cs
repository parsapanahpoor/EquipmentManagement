using EquipmentManagement.Domain.DTO.SiteSide.Places;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Query;

public record SelectListOfPlacesQueryHandler : IRequestHandler<SelectListOfPlacesQuery, List<SelectListOfPlacesDTO>>
{
    #region Ctor

    private readonly IPlacesQueryRepository _placesQueryRepository;

    public SelectListOfPlacesQueryHandler(IPlacesQueryRepository placesQueryRepository)
    {
        _placesQueryRepository = placesQueryRepository;
    }

    #endregion

    public async Task<List<SelectListOfPlacesDTO>> Handle(SelectListOfPlacesQuery request, CancellationToken cancellationToken)
    {
        return await _placesQueryRepository.FillSelectListOfPlacesDTO(cancellationToken);
    }
}
