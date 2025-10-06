using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;

namespace EquipmentManagement.Domain.IRepositories.PlaceOfService;

public interface IPlaceOfServicesQueryRepository
{
    #region Site Side 

    Task<FilterPlaceOfServicesDTO> FilterPlaceOfServices(FilterPlaceOfServicesDTO filter, CancellationToken cancellationToken);

    Task<Domain.Entities.PlaceOfService.PlaceOfService> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<List<Domain.Entities.PlaceOfService.PlaceOfService>> GetSubPlaceOfServicesByPlaceOfServiceParentId(ulong parentId,
                                                                         CancellationToken cancellationToken);

    Task<bool> IsExistAny_PlaceOfService_ById(ulong PlaceOfServiceId, CancellationToken cancellationToken);

    Task<List<SelectListOfPlaceOfServicesDTO>> FillSelectListOfPlaceOfServicesDTO(CancellationToken cancellation);

    Task<List<Domain.Entities.PlaceOfService.PlaceOfService>> GetAllOfOlaceOfServices(
        CancellationToken cancellationToken);

    #endregion
}
