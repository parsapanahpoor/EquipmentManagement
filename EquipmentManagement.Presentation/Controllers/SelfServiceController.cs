using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Command.ReceiveFoodDeliveryReceipt;
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
        }
        catch (Exception ex)
        {
            TempData[ErrorMessage] = ex.Message ;
        }

        return View(model);
    }
}
