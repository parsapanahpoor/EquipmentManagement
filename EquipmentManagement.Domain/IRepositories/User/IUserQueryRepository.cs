using EquipmentManagement.Domain.DTO.SiteSide.User;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Domain.IRepositories.User;

public interface IUserQueryRepository
{
    #region General Methods

    Task<bool> IsMobileExist(string mobile, CancellationToken cancellation);

    Task<bool> IsUserActive(string mobile , CancellationToken cancellation);

    Task<bool> IsPasswordValid(string mobile, string password, CancellationToken cancellation);

    Task<Domain.Entities.Users.User?> GetUserByMobile(string mobile, CancellationToken cancellation);

    Task<Domain.Entities.Users.User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    #endregion

    #region Site Side

    Task<FilterUsersDTO> FilterUsers(FilterUsersDTO filter, CancellationToken cancellation);

    Task<bool> IsValidNationalIdForUserEditByAdmin(string nationalId, ulong userId, CancellationToken cancellationToken);

    Task<List<Domain.Entities.Users.User>> ListOfUsers(CancellationToken cancellationToken);

    #endregion
}
