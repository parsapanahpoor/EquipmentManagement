namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeCommandRepository
{
    Task AddAsync(Domain.Entities.Employee.Employee employee, CancellationToken cancellationToken);

    void Update(Domain.Entities.Employee.Employee employee);
}
