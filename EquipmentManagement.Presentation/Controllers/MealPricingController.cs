using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace EquipmentManagement.Presentation.Controllers;

public class MealPricingController :
    SiteBaseController
{
    #region Filter MealPricing

    [HttpGet]
    public async Task<IActionResult> FilterMealPricing(
        FilterMealPricing filter,
        CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new MealPricingSelectedListQuery()
        {
            MealType=filter.MealType,
            Price=filter.Price,
        },
        cancellation));
    }

    #endregion

    #region Create MealPricing 

    [HttpGet]
    public async Task<IActionResult> CreateMealPricing(CancellationToken cancellationToken = default)
    {


        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMealPricing(CreateMealPricingDTO model,
                                                CancellationToken cancellationToken = default)
    {
        #region Create MealPricing 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateMealPricingCommand()
            {
                MealType = model.MealType,
                Price = model.Price
            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
                return RedirectToAction(nameof(FilterMealPricing));
            }
        }

        #endregion



        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion

    #region Edit MealPricing

    [HttpGet]
    public async Task<IActionResult> EditMealPricing(
        EditMealPricingQuery query,
        CancellationToken cancellation)
    {
        var model = await Mediator.Send(query, cancellation);
        if (model == null) return NotFound();



        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMealPricing(
        EditMealPricingDTO model,
        CancellationToken cancellationToken)
    {
        #region Edit MealPricing 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditMealPricingCommand()
            {
                Id = model.MealPricingId,
                MealType = model.MealType,
                Price = model.Price,

            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد .";
                return RedirectToAction(nameof(FilterMealPricing));
            }
            TempData[ErrorMessage] = "نقش مورد نظر یافت نشد .";
            return RedirectToAction(nameof(FilterMealPricing));
        }

        #endregion



        return View(model);
    }

    #endregion

    #region Remove MealPricing

    public async Task<IActionResult> RemoveMealPricing(DeleteMealPricingCommand command,
                                                CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result)
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
