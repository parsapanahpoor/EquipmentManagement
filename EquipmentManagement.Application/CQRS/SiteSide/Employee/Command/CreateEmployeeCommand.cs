using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;

public record CreateEmployeeCommand : 
    CreateEmployeeDto , 
    IRequest<bool>;
