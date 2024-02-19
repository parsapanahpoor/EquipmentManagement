namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public class EditPlaceCommand : IRequest<bool>
{
    #region proerpties

    public ulong PlaceId{ get; set; }

    public string PlaceTitle { get; set; }

    #endregion
}
