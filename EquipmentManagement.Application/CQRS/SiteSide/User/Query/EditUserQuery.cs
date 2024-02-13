using EquipmentManagement.Domain.DTO.SiteSide.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public class EditUserQuery : IRequest<EditUserDTO>
{
    #region properties

    public ulong UserId { get; set; }

    #endregion
}
