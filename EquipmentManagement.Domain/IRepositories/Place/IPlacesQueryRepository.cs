using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
using EquipmentManagement.Domain.DTO.SiteSide.Places;

namespace EquipmentManagement.Domain.IRepositories.Place;

public interface IPlacesQueryRepository
{
    #region Site Side 

    Task<FilterPlacesDTO> FilterPlaces(FilterPlacesDTO filter, CancellationToken cancellationToken);

    Task<Domain.Entities.Places.Place> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<List<Domain.Entities.Places.Place>> GetSubPlacesByPlaceParentId(ulong parentId,
                                                                         CancellationToken cancellationToken);

    Task<bool> IsExistAny_Place_ById(ulong placeId, CancellationToken cancellationToken);

    Task<List<SelectListOfPlacesDTO>> FillSelectListOfPlacesDTO(CancellationToken cancellation);

    #endregion
}
