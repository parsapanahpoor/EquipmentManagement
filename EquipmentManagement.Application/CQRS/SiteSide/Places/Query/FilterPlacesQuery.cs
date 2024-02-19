using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public class FilterPlacesQuery : IRequest<FilterPlacesDTO>
{
    #region properties

    public ulong? ParentId{ get; set; }

    public string? PlaceTitle { get; set; }

    #endregion
}
