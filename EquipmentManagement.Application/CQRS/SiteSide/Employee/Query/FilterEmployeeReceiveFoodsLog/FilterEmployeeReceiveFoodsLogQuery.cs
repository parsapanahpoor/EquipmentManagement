using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query.FilterEmployeeReceiveFoodsLog;

public record FilterEmployeeReceiveFoodsLogQuery(
    string? FirstName , 
    string? LastName , 
    string? Mobile , 
    string? PersonnelCode) : 
    IRequest<FilterEmployeeReceiveFoodsLogDto>;
