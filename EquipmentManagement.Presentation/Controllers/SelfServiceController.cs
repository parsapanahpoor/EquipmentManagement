using EquipmentManagement.Application.CQRS.SiteSide.Employee.Command.Transactions;
using EquipmentManagement.Application.CQRS.SiteSide.EmployeeTransaction.Command;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.Entities.MealPricing;
using Microsoft.AspNetCore.Mvc;
using SSP1126.PcPos.BaseClasses;
using SSP1126.PcPos.Infrastructure;
using System.Threading;

namespace EquipmentManagement.Presentation.Controllers;

public class SelfServiceController : SiteBaseController
{

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateEmployeeTransactionDto request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // مثال: ذخیره در دیتابیس
        var result = await Mediator.Send(
                    new CreateEmployeeTransactionCommand()
                    {
                        Amount = request.Amount,
                        Description = request.Description,
                        EmployeeId = request.EmployeeId,
                        Paid = true,
                    },
                    ct);
        if (result.Item1)
        {

            return Ok(new { success = true, id = result.Item2 });
        }
        return BadRequest(ModelState);
    }

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
            //   PcPosFactory pcPosFactory=new PcPosFactory();
            //   pcPosFactory.SetLan("");

            //   pcPosFactory.Initialization(SSP1126.PcPos.Infrastructure.ResponseLanguage.Persian,3000, AsyncType.Async);

            //var res=   pcPosFactory.PaymentServiceSendData(,);
            //   res.
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
