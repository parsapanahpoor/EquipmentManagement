using EquipmentManagement.Domain.DTO.SiteSide.Account;
namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Query;

public class LoginUserQuery() : IRequest<LoginUserResult>
{
    public string Mobile { get; set; }
    public string Password { get; set; }
}