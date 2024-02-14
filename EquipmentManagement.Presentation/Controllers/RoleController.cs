using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace EquipmentManagement.Presentation.Controllers;

public class RoleController : SiteBaseController
{
    #region Filter Roles

    [HttpGet]
    public async Task<IActionResult> FilterRoles(FilterRolesDTO filter,
                                                 CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterRolesQuery()
        {
            RoleTitle = filter.RoleTitle
        },
        cancellation)
        );
    }

    #endregion

    #region Create Role 

    [HttpGet]
    public IActionResult CreateRole()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleDTO model , 
                                                CancellationToken cancellationToken = default)
    {
        #region Create Role 

        if (ModelState.IsValid)
        {

            TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
            return RedirectToAction(nameof(FilterRoles));
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion

    #region Edit Role



    #endregion

    #region Remove Role



    #endregion
}
