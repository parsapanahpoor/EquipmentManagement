using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Infrastructure.Repositories.Employee;

public class EmployeeReceiveFoodDeliveryReceiptLogCommandRepository : 
    CommandGenericRepository<Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog>,
    IEmployeeReceiveFoodDeliveryReceiptLogCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public EmployeeReceiveFoodDeliveryReceiptLogCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task AddAsync(
        Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog employee, 
        CancellationToken cancellationToken)
    {
        await _context.EmployeeReceiveFoodDeliveryReceiptLogs.AddAsync(employee, cancellationToken);
    }
}