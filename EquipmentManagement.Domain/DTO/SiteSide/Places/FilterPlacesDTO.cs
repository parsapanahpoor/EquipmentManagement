using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;

public class FilterPlacesDTO : BasePaging<Domain.Entities.Places.Place>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string? PlaceTitle { get; set; }

    #endregion
}

public class FilterPlacesForExcelFileDTO
{
    #region properties

    public ulong Id { get; set; }

    public ulong? ParentId { get; set; }

    public string? ParentPlace { get; set; }

    public string? PlaceTitle { get; set; }

    #endregion
}