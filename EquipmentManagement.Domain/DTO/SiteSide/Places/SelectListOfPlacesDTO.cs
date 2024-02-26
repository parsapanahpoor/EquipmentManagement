namespace EquipmentManagement.Domain.DTO.SiteSide.Places;

public class SelectListOfPlacesDTO
{
    #region properties

    public ulong PlaceId{ get; set; }

    public string PlaceTitle { get; set; }

    public ulong? ParentId { get; set; }

    #endregion
}
