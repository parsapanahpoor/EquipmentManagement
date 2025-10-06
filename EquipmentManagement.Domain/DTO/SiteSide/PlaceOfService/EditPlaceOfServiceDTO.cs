namespace EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;

public class EditPlaceOfServiceDTO
{
    #region properties

    public ulong PlaceOfServiceId { get; set; }

    public string? PlaceOfServiceTitle { get; set; }

    public ulong? ParentId { get; set; }

    #endregion
}
