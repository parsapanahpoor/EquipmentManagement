namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeReceiveFoodDeliveryReceiptLogCommandRepository
{
    Task AddAsync(
        Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog employee, 
        CancellationToken cancellationToken);

    void Update(Domain.Entities.Employee.EmployeeReceiveFoodDeliveryReceiptLog employee);

}
