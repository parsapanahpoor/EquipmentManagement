#region Using

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

#endregion

[Authorize]
public class HomeController : Controller
{
    #region Index

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    #endregion

    #region Panel Landing

    public async Task<IActionResult> Landing()
    {
        return View();
    }

    #endregion
}
