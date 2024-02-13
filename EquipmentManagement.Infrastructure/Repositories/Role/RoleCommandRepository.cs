using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Infrastructure.Repositories.Role;

public class RoleCommandRepository : CommandGenericRepository<Domain.Entities.Account.Role>, IRoleCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public RoleCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}
