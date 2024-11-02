using EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateAbolitionRequest;
using EquipmentManagement.Application.Utilities.Security;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateRepairRequest;

public record CreateRepairRequestCommand(
    ulong ProductId , 
    ulong ExpertUserId , 
    ulong EmployeeId , 
    string? Description) : 
    IRequest<CreateRepairRequestCommandRespons>;

public enum CreateRepairRequestCommandRespons
{
    Success ,
    Failure ,
    DosentConfig
}