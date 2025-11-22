using EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;
using EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Query;

using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShiftMeals;
using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;


namespace EquipmentManagement.Presentation.Controllers;

public class EmployeeShiftMealController :
    SiteBaseController
{
    #region Filter EmployeeShiftMeal

    [HttpGet]
    public async Task<IActionResult> FilterEmployeeShiftMeal(
        FilterEmployeeShiftMealDTO filter,
        CancellationToken cancellation = default)
    {
        ViewBag.EmployeeShiftSelectedId = filter.EmployeeShiftSelectedId;
        return View(await Mediator.Send(new FilterEmployeeShiftMealSelectedQuery()
        {
          EmployeeShiftSelectedId=filter.EmployeeShiftSelectedId,
        },
        cancellation));
    }

    #endregion

    #region Create EmployeeShiftMeal 

    [HttpGet]
    public async Task<IActionResult> CreateEmployeeShiftMeal(CreateEmployeeShiftMealDTO model)
    {


        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmployeeShiftMeal(CreateEmployeeShiftMealDTO model,
                                                CancellationToken cancellationToken = default)
    {
        #region Create EmployeeShiftMeal 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateEmployeeShiftMealSelectedCommand()
            {
                Meal = model.Meal,
                EmployeeShiftSelectedId=model.EmployeeShiftSelectedId
            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
                return RedirectToAction(nameof(FilterEmployeeShiftMeal), new { EmployeeShiftSelectedId = model.EmployeeShiftSelectedId });
            }
        }

        #endregion



        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion



    #region Remove EmployeeShiftMeal

    public async Task<IActionResult> RemoveEmployeeShiftMeal(DeleteEmployeeShiftMealSelectedCommand command,
                                                CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
