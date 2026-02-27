using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeTransactionCommandRepository: ICommandGenericRepository<Domain.Entities.Employee.EmployeeTransaction>
{

}
