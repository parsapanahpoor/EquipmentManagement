namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public class EditPlaceOfServiceCommand : IRequest<bool>
{
    #region proerpties

    public ulong PlaceOfServiceId{ get; set; }

    public string? PlaceOfServiceTitle { get; set; }

    #endregion
}
