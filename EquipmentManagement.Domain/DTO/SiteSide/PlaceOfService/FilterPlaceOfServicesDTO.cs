using EquipmentManagement.Domain.DTO.Common;

namespace EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;

public class FilterPlaceOfServicesDTO : BasePaging<Domain.Entities.PlaceOfService.PlaceOfService>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string? PlaceOfServiceTitle { get; set; }

    #endregion
}

public class FilterPlaceOfServicesForExcelFileDTO
{
    #region properties

    public ulong Id { get; set; }

    public ulong? ParentId { get; set; }

    public string? ParentPlaceOfService { get; set; }

    public string? PlaceOfServiceTitle { get; set; }

    #endregion
}