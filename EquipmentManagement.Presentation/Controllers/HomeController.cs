#region Using

using EquipmentManagement.Application.CQRS.SiteSide.Dashboard.Query.DashboardInfo;
using EquipmentManagement.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

#endregion

[Authorize]
public class HomeController : SiteBaseController
{
    #region Index

    [AllowAnonymous]
    public IActionResult Index()
        => View();

    #endregion

    #region Index

    [AllowAnonymous]
    public IActionResult EquipmentLanding()
        => View();

    #endregion

    #region Panel Landing

    public async Task<IActionResult> Landing(CancellationToken cancellation)
        => View(await Mediator.Send(new DashboardInfoQuery(User.GetUserId()),
            cancellation));

    #endregion
}
