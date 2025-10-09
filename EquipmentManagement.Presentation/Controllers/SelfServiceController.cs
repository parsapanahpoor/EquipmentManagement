using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodReceipt;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class SelfServiceController : SiteBaseController
{
    [HttpGet]
    public IActionResult ReceiveFoodDeliveryReceipt()
        => View();

    [HttpPost , ValidateAntiForgeryToken]
    public async Task<IActionResult> ReceiveFoodDeliveryReceipt(
        ReceiveFoodDeliveryReceiptDto model, 
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(
                new ReceiveFoodDeliveryReceiptCommand(model),
                cancellationToken);

            return RedirectToAction(nameof(ReceiveFoodReceipt) , new { mobile = model.Mobile });
        }
        catch (Exception ex)
        {
            TempData[ErrorMessage] = ex.Message ;
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ReceiveFoodReceipt(
        string mobile , 
        CancellationToken cancellationToken = default)
        => View(await Mediator.Send(new ReceiveFoodReceiptQuery(mobile) , cancellationToken));
}
