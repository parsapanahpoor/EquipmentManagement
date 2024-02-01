using EquipmentManagement.Domain.DTO.SiteSide.Account;
namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Command;

public record RegisterUserCommand : IRequest<RegisterUserResult>
{
    public string Password { get; set; }
    public string Mobile { get; set; }
}
