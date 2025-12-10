using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;
using EquipmentManagement.Domain.Entities.MealPricing;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class SelfServiceController : SiteBaseController
{
    [HttpGet]
    public async Task<IActionResult> ReceiveFoodDeliveryReceipt()
    {
        var dropDownMealPricing = await Mediator.Send(new DropdownMealPricingSelectedListQuery()
        {

        });
        ViewBag.dropDownMealPricing = dropDownMealPricing;
        return View();

    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ReceiveFoodDeliveryReceipt(
        ReceiveFoodDeliveryReceiptDto model,
        CancellationToken cancellationToken)
    {
        try
        {
            
            var result = await Mediator.Send(
                new ReceiveFoodDeliveryReceiptCommand(model),
                cancellationToken);
            if (result.Status)
                return RedirectToAction(nameof(ReceiveFoodReceipt), new { mobile = result.Mobile, MealPricingId = model.MealPricingId });
        }
        catch (Exception ex)
        {
            TempData[ErrorMessage] = ex.Message;
        }
        var dropDownMealPricing = await Mediator.Send(new DropdownMealPricingSelectedListQuery()
        {

        });
        ViewBag.dropDownMealPricing = dropDownMealPricing;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ReceiveFoodReceipt(
        string mobile, ulong MealPricingId,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(ReceiveFoodDeliveryReceipt));
        var mealPricing = await Mediator.Send(new EditMealPricingQuery(MealPricingId), cancellationToken);
        ViewBag.MealType = mealPricing.MealType;
        ViewBag.MealPrice = mealPricing.Price;


        var result = await Mediator.Send(new ReceiveFoodReceiptQuery(mobile, MealPricingId), cancellationToken);
        return View(result);
    }

}
