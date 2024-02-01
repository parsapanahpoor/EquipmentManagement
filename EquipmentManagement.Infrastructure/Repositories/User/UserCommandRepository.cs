using EquipmentManagement.Domain.IRepositories.User;
namespace EquipmentManagement.Infrastructure.Repositories.User;

public class UserCommandRepository : CommandGenericRepository<Domain.Entities.Users.User>, IUserCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public UserCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}
