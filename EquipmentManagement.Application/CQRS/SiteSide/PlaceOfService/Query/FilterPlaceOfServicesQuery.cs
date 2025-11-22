using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;

public class FilterPlaceOfServicesQuery : IRequest<FilterPlaceOfServicesDTO>
{
    #region properties

    public FilterPlaceOfServicesDTO? Filter { get; set; }

    #endregion
}
