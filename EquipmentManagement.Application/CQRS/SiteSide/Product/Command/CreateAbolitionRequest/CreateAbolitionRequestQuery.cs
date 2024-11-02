using EquipmentManagement.Application.Utilities.Security;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateAbolitionRequest;

public record CreateAbolitionRequestCommand(
    ulong ProductId , 
    ulong ExpertUserId , 
    ulong EmployeeId , 
    string? Description) : 
    IRequest<CreateAbolitionRequestCommandRespons>;

public enum CreateAbolitionRequestCommandRespons
{
    Success ,
    Failure ,
    DosentConfig
}
