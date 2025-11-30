using EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Command;
using EquipmentManagement.Application.CQRS.SiteSide.MealPricing.Query;
using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;
using EquipmentManagement.Application.CQRS.SiteSide.Role.Query;
using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;


namespace EquipmentManagement.Presentation.Controllers;

[Authorize(Roles = "FoodReporter")]
public class FoodGroupReportController :
    SiteBaseController
{
    #region Filter Employees

    [HttpGet]
    public async Task<IActionResult> FilterEmployees(
        FilterSelectedEmployeesDto filter, List<ulong> EmployeeIds,
        CancellationToken cancellation = default)
    {
        filter.EmployeeIds = EmployeeIds;
        //TempData["EmployeeIds"] = EmployeeIds;
        //filter.EmployeeIds = EmployeeIds;
        return View(await Mediator.Send(new FilterSelectedEmployeesQuery()
        {
            FirstName = filter.FirstName,
            LastName = filter.LastName,
            PersonnelCode = filter.PersonnelCode,
            Mobile = filter.Mobile,
            EmployeeIds = filter.EmployeeIds,
        },
        cancellation));
    }



    #endregion

    [HttpGet]
    public async Task<IActionResult> Report(
        List<ulong> EmployeeIds,
        CancellationToken cancellation = default)
    {
        var Ids = TempData["EmployeeIds"] as List<ulong>;
        var listEmployee = await Mediator.Send(new ReceiveFoodListReceiptQuery(EmployeeIds));

        await Mediator.Send(new LoadTemplateEmployeeQuery(listEmployee));
        var fileBytes = await Mediator.Send(new GetPDFCustomEmployeeQuery(listEmployee));
    

        // برگردوندن فایل PDF
        return File(fileBytes, "application/pdf", "Report.pdf");

        //    await System.IO.File.WriteAllBytesAsync("path.pdf", fileBytes);


    }

}
