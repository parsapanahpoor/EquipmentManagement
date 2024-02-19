using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;

namespace EquipmentManagement.Domain.IRepositories.Place;

public interface IPlacesQueryRepository
{
    #region Site Side 

    Task<FilterPlacesDTO> FilterPlaces(FilterPlacesDTO filter, CancellationToken cancellationToken);

    Task<Domain.Entities.Places.Place> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<List<Domain.Entities.Places.Place>> GetSubPlacesByPlaceParentId(ulong parentId,
                                                                         CancellationToken cancellationToken);

    #endregion
}
