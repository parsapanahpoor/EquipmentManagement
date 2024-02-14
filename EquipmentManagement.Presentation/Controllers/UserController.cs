using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.CQRS.SiteSide.User.Command;
using EquipmentManagement.Application.CQRS.SiteSide.User.Query;
using EquipmentManagement.Domain.DTO.SiteSide.User;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

public class UserController : SiteBaseController
{
	#region Filter Users

	public async Task<IActionResult> FilterUsers(FilterUsersDTO filter , 
                                                 CancellationToken cancellationToken = default)
	{
        FilterUserQuery query = new FilterUserQuery()
        {
            Username = filter.Username,
            Mobile = filter.Mobile,
        };

        return View(await Mediator.Send(query, cancellationToken));
    }

    #endregion

    #region Edit User

    [HttpGet]
    public async Task<IActionResult> EditUser(EditUserQuery userQuery,
                                              CancellationToken cancellation = default)
    {
        var user = await Mediator.Send(userQuery, cancellation);
        if (user == null) return RedirectToAction(nameof(FilterUsers));

        #region Page Data

        ViewData["Roles"] = await Mediator.Send(new RoleSelectedListQuery(), cancellation);

        #endregion

        return View(user);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(EditUserDTO userDTO, 
                                              IFormFile UserAvatar,
                                              CancellationToken cancellation = default)
    {
        #region Page Data

        ViewData["Roles"] = await Mediator.Send(new RoleSelectedListQuery(), cancellation);

        #endregion

        var res = await Mediator.Send(new EdirUserCommand()
        {
            avatar = UserAvatar , 
            Avatar = userDTO.Avatar,
            Id = userDTO.Id,
            IsActive = userDTO.IsActive,
            Mobile = userDTO.Mobile,
            Password = userDTO.Password,
            Username = userDTO.Username,
            UserRoles = userDTO.UserRoles
        } , 
        cancellation);

        switch (res)
        {
            case EditUserResult.DuplicateEmail:
                TempData[ErrorMessage] = "ایمیل وارد شده صحیح نمی باشد .";
                break;

            case EditUserResult.DuplicateMobileNumber:
                TempData[ErrorMessage] = "موبایل وارد شده تکراری می باشد.";
                break;

            case EditUserResult.Success:
                return RedirectToAction(nameof(FilterUsers));
        }

        return View();
    }

    #endregion

    #region Delete User 

    public async Task<IActionResult> DeleteUser(DeleteUserCommand command, 
                                                CancellationToken token = default)
    {
        var result = await Mediator.Send(command, token);
        if (result) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
