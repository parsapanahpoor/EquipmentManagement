using EquipmentManagement.Domain.Entities.Users;

namespace EquipmentManagement.Domain.IRepositories.User;

public interface IUserCommandRepository
{
    #region General Methods

    Task AddAsync(Domain.Entities.Users.User user, CancellationToken cancellationToken);

    #endregion
}
