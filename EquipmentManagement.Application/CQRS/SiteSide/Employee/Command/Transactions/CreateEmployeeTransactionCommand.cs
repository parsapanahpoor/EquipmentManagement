using EquipmentManagement.Domain.DTO.SiteSide.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Command.Transactions;

public record CreateEmployeeTransactionCommand : CreateEmployeeTransactionDto,


    IRequest<(bool,ulong)>;
