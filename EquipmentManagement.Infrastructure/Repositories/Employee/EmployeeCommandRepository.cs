using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Infrastructure.Repositories.Employee;

public class EmployeeCommandRepository : CommandGenericRepository<Domain.Entities.Employee.Employee>, IEmployeeCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}