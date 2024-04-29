using EquipmentManagement.Domain.DTO.SiteSide.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public class FilterUserQuery : IRequest<FilterUsersDTO>
{
    #region properties

    public FilterUsersDTO filter { get; set; }

    #endregion
}
