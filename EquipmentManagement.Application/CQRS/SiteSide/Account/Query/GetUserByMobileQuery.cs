namespace EquipmentManagement.Application.CQRS.SiteSide.Account.Query;

public class GetUserByMobileQuery : IRequest<Domain.Entities.Users.User>
{
    public string Mobile { get; set; }
}
