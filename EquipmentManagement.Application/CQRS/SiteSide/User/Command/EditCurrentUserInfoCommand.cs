using EquipmentManagement.Domain.DTO.SiteSide.User;
using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Application.CQRS.SiteSide.User.Command;

public class EditCurrentUserInfoCommand : IRequest<UserPanelEditUserInfoResult>
{
    #region properties

    public EditUserInfoDTO UserInfo { get; set; }

    public IFormFile? UserAvatar { get; set; }

    #endregion
}
