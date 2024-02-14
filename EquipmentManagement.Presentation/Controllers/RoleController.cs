using EquipmentManagement.Application.CQRS.SiteSide.Role.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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
        ViewData["Permissions"] = PermissionsList.Permissions.Where(s => !s.IsDelete).ToList();

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleDTO model,
                                                CancellationToken cancellationToken = default)
    {
        #region Create Role 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateRoleCommand()
            {
                Permissions = model.Permissions,
                RoleUniqueName = model.RoleUniqueName,
                Title = model.Title
            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
                return RedirectToAction(nameof(FilterRoles));
            }
        }

        #endregion

        ViewData["Permissions"] = PermissionsList.Permissions.Where(s => !s.IsDelete).ToList();

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion

    #region Edit Role

    [HttpGet]
    public async Task<IActionResult> EditRole(EditRoleQuery query , 
                                              CancellationToken cancellation)
    {
        var model = await Mediator.Send(query , cancellation);
        if (model == null)return NotFound();

        ViewData["Permissions"] = PermissionsList.Permissions.Where(s => !s.IsDelete).ToList();

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditRoleDTO model , CancellationToken cancellationToken)
    {
        #region Edit Role 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditRoleCommand()
            {
                Id = model.Id,
                Permissions = model.Permissions,
                RoleUniqueName = model.RoleUniqueName,
                Title = model.Title
            },
            cancellationToken);

            switch (res)
            {
                case EditRoleResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد .";
                    return RedirectToAction(nameof(FilterRoles));

                case EditRoleResult.RoleNotFound:
                    TempData[ErrorMessage] = "نقش مورد نظر یافت نشد .";
                    return RedirectToAction(nameof(FilterRoles));

                case EditRoleResult.UniqueNameExists:
                    TempData[WarningMessage] = "نام یکتا از قبل موجود است .";
                    break;
            }
        }

        #endregion

        ViewData["Permissions"] = PermissionsList.Permissions.Where(s => !s.IsDelete).ToList();
        return View(model);
    }

    #endregion

    #region Remove Role

    public async Task<IActionResult> RemoveRole(DeleteRoleCommand command , 
                                                CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
