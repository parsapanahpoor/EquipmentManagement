using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;
using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Query;
using EquipmentManagement.Application.Security;
using EquipmentManagement.Domain.DTO.SiteSide.PlaceOfService;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
namespace EquipmentManagement.Presentation.Controllers;

public class PlaceOfServicesController : SiteBaseController
{
    #region Filter PlaceOfServices 

    public async Task<IActionResult> FilterPlaceOfServices(FilterPlaceOfServicesDTO filter,
                                                  CancellationToken cancellation)
    {
        var model = await Mediator.Send(new FilterPlaceOfServicesQuery()
        {
            Filter = filter,
        },
        cancellation);

        return View(model);
    }

    #endregion

    #region Create PlaceOfService

    [HttpGet]
    public IActionResult CreatePlaceOfService(ulong? parentId)
    {
        return View(new CreatePlaceOfServiceDTO()
        {
            ParentId = parentId
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePlaceOfService(CreatePlaceOfServiceDTO createPlaceOfService,
                                                 CancellationToken cancellationToken)
    {
        #region Create PlaceOfService

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreatePlaceOfServiceCommand()
            {
                ParentId = createPlaceOfService.ParentId,
                Title = createPlaceOfService.Title,
            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterPlaceOfServices), new { parentId = createPlaceOfService.ParentId });
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(createPlaceOfService);
    }

    #endregion

    #region Edit PlaceOfService

    [PermissionChecker("EditPlaceOfService")]
    [HttpGet]
    public async Task<IActionResult> EditPlaceOfService(EditPlaceOfServiceQuery PlaceOfService,
                                               CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(PlaceOfService, cancellationToken);
        if (model == null) return NotFound();

        return View(model);
    }

    [PermissionChecker("EditPlaceOfService")]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPlaceOfService(EditPlaceOfServiceDTO PlaceOfService,
                                               CancellationToken cancellation)
    {
        #region Edit PlaceOfService 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditPlaceOfServiceCommand()
            {
                PlaceOfServiceId = PlaceOfService.PlaceOfServiceId,
                PlaceOfServiceTitle = PlaceOfService.PlaceOfServiceTitle
            },
            cancellation);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterPlaceOfServices), new { parentId = PlaceOfService.ParentId });
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(PlaceOfService);
    }

    #endregion

    #region Delete PlaceOfService

    public async Task<IActionResult> DeletePlaceOfService(DeletePlaceOfServiceCommand command,
                                                 CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(command, cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
