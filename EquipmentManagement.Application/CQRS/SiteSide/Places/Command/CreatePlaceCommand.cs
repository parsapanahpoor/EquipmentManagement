using EquipmentManagement.Domain.DTO.SiteSide.Places;
using System.Security.Cryptography;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public class CreatePlaceCommand : IRequest<bool>
{
    #region properties

    public ulong? ParentId { get; set; }

    public string Title { get; set; }

    #endregion
}
