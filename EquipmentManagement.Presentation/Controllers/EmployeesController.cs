using EquipmentManagement.Application.CQRS.SiteSide.Employee.Command;
using EquipmentManagement.Application.CQRS.SiteSide.Employee.Query;
using EquipmentManagement.Application.CQRS.SiteSide.PlaceOfService.Query;
using EquipmentManagement.Application.StaticTools;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace EquipmentManagement.Presentation.Controllers;

public class EmployeesController : 
    SiteBaseController
{
    #region Filter Employees

    [HttpGet]
    public async Task<IActionResult> FilterEmployees(
        FilterEmployeesDto filter,
        CancellationToken cancellation = default)
    {
        return View(await Mediator.Send(new FilterEmployeesQuery()
        {
            FirstName = filter.FirstName,
            LastName = filter.LastName,
            PersonnelCode = filter.PersonnelCode,
            Mobile = filter.Mobile
        },
        cancellation));
    }

    #endregion

    #region Create Employee 

    [HttpGet]
    public async Task<IActionResult> CreateEmployeeAsync(CancellationToken cancellationToken = default)
    {
        ViewData["placeOfServices"] = await Mediator.Send(
            new GetListOfPlaceOfServiceQuery(),
            cancellationToken);

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto model,
                                                CancellationToken cancellationToken = default)
    {
        #region Create Employee 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateEmployeeCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mobile = model.Mobile,
                PersonnelCode = model.PersonnelCode,
                PlaceOfServiceId = model.PlaceOfServiceId,
                CanReceiveFood=model.CanReceiveFood,
                RFId=model.RFId,
            },
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شده است .";
                return RedirectToAction(nameof(FilterEmployees));
            }
        }

        #endregion

        ViewData["placeOfServices"] = await Mediator.Send(
            new GetListOfPlaceOfServiceQuery(),
            cancellationToken);

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View();
    }

    #endregion

    #region Edit Employee

    [HttpGet]
    public async Task<IActionResult> EditEmployee(
        EditEmployeeQuery query,
        CancellationToken cancellation)
    {
        var model = await Mediator.Send(query, cancellation);
        if (model == null) return NotFound();

        ViewData["placeOfServices"] = await Mediator.Send(
            new GetListOfPlaceOfServiceQuery(),
            cancellation);

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditEmployee(
        EditEmployeeDto model, 
        CancellationToken cancellationToken)
    {
        #region Edit Employee 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditEmployeeCommand()
            {
                Id = model.Id,
                PlaceOfServiceId = model.PlaceOfServiceId,
                FirstName = model.FirstName,
                PersonnelCode = model.PersonnelCode,
                LastName = model.LastName,
                Mobile = model.Mobile,
                RFId=model.RFId,
                CanReceiveFood=model.CanReceiveFood
            },
            cancellationToken);

            switch (res)
            {
                case EditEmployeeResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد .";
                    return RedirectToAction(nameof(FilterEmployees));

                case EditEmployeeResult.EmployeeNotFound:
                    TempData[ErrorMessage] = "نقش مورد نظر یافت نشد .";
                    return RedirectToAction(nameof(FilterEmployees));

                case EditEmployeeResult.UniqueNameExists:
                    TempData[WarningMessage] = "نام یکتا از قبل موجود است .";
                    break;
            }
        }

        #endregion

        ViewData["placeOfServices"] = await Mediator.Send(
            new GetListOfPlaceOfServiceQuery(),
            cancellationToken);
        
        return View(model);
    }

    #endregion

    #region Remove Employee

    public async Task<IActionResult> RemoveEmployee(DeleteEmployeeCommand command,
                                                CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        if (result) 
            return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion
}
