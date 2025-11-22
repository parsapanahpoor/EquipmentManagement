using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;

public record EditEmployeeQuery(ulong Id) :
    IRequest<EditEmployeeDto>;
