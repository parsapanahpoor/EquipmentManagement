using EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftMealSelected.Command;
using EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Command;
using EquipmentManagement.Application.CQRS.SiteSide.EmployeeShiftSelected.Query;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Domain.DTO.SiteSide.EmployeeShifts;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;


namespace EquipmentManagement.Presentation.Controllers;

public class EmployeeShiftController :
    SiteBaseController
{
    #region Filter EmployeeShift

    [HttpGet]
    public async Task<IActionResult> FilterEmployeeShift(
        FilterEmployeeShiftDTO filter,
        CancellationToken cancellation = default)
    {
        ViewBag.EmployeeId = filter.EmployeeId;
        return View(await Mediator.Send(new FilterEmployeeShiftSelectedQuery()
        {
            EmployeeId = filter.EmployeeId,
        },
        cancellation));
    }

    #endregion

    #region Create EmployeeShift 

    [HttpGet]
    public async Task<IActionResult> CreateEmployeeShift(CreateEmployeeShiftDTO model)
    {
        var dropDownMealPricing = await Mediator.Send(new DropdownMealPricingSelectedListQuery()
        {

        });
        ViewBag.dropDownMealPricing = dropDownMealPricing;
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmployeeShift(CreateEmployeeShiftDTO model,
                                                CancellationToken cancellationToken = default)
    {
        #region Create EmployeeShift 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateEmployeeShiftSelectedCommand()
            {
                Date = DateOnly.FromDateTime(model.Date.ToGregorianDate()),
                EmployeeId = model.EmployeeId,
            },
            cancellationToken);

            if (res.HasValue && res != 0)
            {
                #region Create EmployeeShiftMeal 



                foreach (var item in model.MealPricingIds)
                {
                    var resEmployeeShiftMeal = await Mediator.Send(new CreateEmployeeShiftMealSelectedCommand()
                    {
                        MealPricingId = item,
                        EmployeeShiftSelectedId = res.Value
                    },
                    cancellationToken);
                }





                #endregion
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
                return RedirectToAction(nameof(FilterEmployeeShift), new { EmployeeId = model.EmployeeId });
            }
        }

        #endregion

        var dropDownMealPricing = await Mediator.Send(new DropdownMealPricingSelectedListQuery()
        {

        });
        ViewBag.dropDownMealPricing = dropDownMealPricing;


        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion



    #region Remove EmployeeShift

    public async Task<IActionResult> RemoveEmployeeShift(DeleteEmployeeShiftSelectedCommand command,
                                                CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
