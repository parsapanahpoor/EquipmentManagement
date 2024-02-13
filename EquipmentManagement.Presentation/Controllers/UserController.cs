using EquipmentManagement.Application.CQRS.SiteSide.ProductCategories.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.CQRS.SiteSide.User.Query;
using EquipmentManagement.Domain.DTO.SiteSide.User;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

public class UserController : SiteBaseController
{
	#region Filter Users

	public async Task<IActionResult> FilterUsers(FilterUsersDTO filter , 
                                                 CancellationToken cancellationToken)
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

    public async Task<IActionResult> EditUser(EditUserQuery userQuery,
                                              CancellationToken cancellation)
    {
        var user = await Mediator.Send(userQuery, cancellation);
        if (user == null) return RedirectToAction(nameof(FilterUsers));

        #region Page Data

        ViewData["Roles"] = await Mediator.Send(new RoleSelectedListQuery(), cancellation);

        #endregion

        return View(user);
    }

    #endregion
}
