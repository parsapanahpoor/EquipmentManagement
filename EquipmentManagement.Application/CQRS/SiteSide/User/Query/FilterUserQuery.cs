using EquipmentManagement.Domain.DTO.SiteSide.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public class FilterUserQuery : IRequest<FilterUsersDTO>
{
    #region properties

    public string? Username { get; set; }

    public string? Mobile { get; set; }

    #endregion
}
