using EquipmentManagement.Application.CQRS.SiteSide.Employee.Query.FilterEmployeeReceiveFoodsLog;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using Microsoft.AspNetCore.Mvc;


namespace EquipmentManagement.Presentation.Controllers;

public class EmployeeReceiveFoodLogController : SiteBaseController
{
    #region  FilterEmployeeReceiveFoodLog

    [HttpGet]
    public async Task<IActionResult> FilterEmployeeReceiveFoodLog(
        FilterEmployeeReceiveFoodsLogDto filter,
        CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(
            new FilterEmployeeReceiveFoodsLogQuery(
            filter.FirstName , 
            filter.LastName, 
            filter.Mobile, 
            filter.PersonnelCode),
        cancellation));
    }

    #endregion
}
