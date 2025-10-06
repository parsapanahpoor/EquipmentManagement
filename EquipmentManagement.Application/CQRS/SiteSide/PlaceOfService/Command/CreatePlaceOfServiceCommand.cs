namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public class CreatePlaceOfServiceCommand : IRequest<bool>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string? Title { get; set; }

    #endregion
}
