using EquipmentManagement.Application.CQRS.SiteSide.EmployeeTransaction.Query;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace EquipmentManagement.Presentation.Controllers;

public class EmployeeTransactionController :
    SiteBaseController
{
    #region FilterEmployeeTransaction

    [HttpGet]
    public async Task<IActionResult> FilterEmployeeTransaction(
        FilterEmployeeTransaction filter,
        CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new EmployeeTransactionSelectedListQuery()
        {
          EmployeeId=filter.EmployeeId,
          EmployeeName=filter.EmployeeName
          
        },
        cancellation));
    }

    #endregion


    #region RemoveEmployeeTransaction

    //public async Task<IActionResult> RemoveMealPricing(DeleteMealPricingCommand command,
    //                                            CancellationToken cancellationToken)
    //{
    //    var result = await Mediator.Send(command, cancellationToken);
    //    if (result)
    //        return JsonResponseStatus.Success();

    //    return JsonResponseStatus.Error();
    //}

    #endregion
}
