using EquipmentManagement.Domain.DTO.SiteSide.User;
namespace EquipmentManagement.Application.CQRS.SiteSide.User.Query;

public class FillEditCurrentUserInfoQuery : IRequest<EditUserInfoDTO>
{
    public ulong userId { get; set; }
}
