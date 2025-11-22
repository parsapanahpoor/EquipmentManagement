using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;

public record EditPlaceOfServiceQueryHandler : IRequestHandler<EditPlaceOfServiceQuery, EditPlaceOfServiceDTO>
{
    #region Ctor

    private readonly IPlaceOfServicesQueryRepository _PlaceOfServicesQueryRepository;

    public EditPlaceOfServiceQueryHandler(IPlaceOfServicesQueryRepository PlaceOfServicesQueryRepository)
    {
        _PlaceOfServicesQueryRepository = PlaceOfServicesQueryRepository;
    }

    #endregion

    public async Task<EditPlaceOfServiceDTO?> Handle(EditPlaceOfServiceQuery request, CancellationToken cancellationToken)
    {
        //Get PlaceOfService 
        var PlaceOfService = await _PlaceOfServicesQueryRepository.GetByIdAsync(cancellationToken , request.PlaceOfServiceId);
        if (PlaceOfService == null) return null;

        return new EditPlaceOfServiceDTO()
        {
            PlaceOfServiceId = PlaceOfService.Id,
            PlaceOfServiceTitle = PlaceOfService.Title,
            ParentId = PlaceOfService.ParentId,
        };
    }
}
