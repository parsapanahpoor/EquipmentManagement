using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Delete;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Create;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Edit;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.Filter;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.Get;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RequestSelectedChartIds;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.ListOfOrganizationCharts;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AddRequestSelectedOrganziation;

namespace EquipmentManagement.Presentation.Controllers;

public class OrganizationRequestController : SiteBaseController
{
    #region Filter OrganziationRequest 

    public async Task<IActionResult> FilterOrganziationRequest(FilterOrganizationRequestsDto filter,
                                                  CancellationToken cancellation)
    => View(await Mediator.Send(new FilterOrganizationRequestsQuery()
    {
        Filter = filter,
    }, cancellation));

    #endregion

    #region Create Organziation Request

    public IActionResult CreateOrganizationRequest()
        => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrganizationRequest(OrganizationRequestEntryModel createOrganizationRequest,
                                                                  CancellationToken cancellationToken)
    {
        #region Create OrganizationRequest

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new OrganizationRequestCreateCommand(createOrganizationRequest),
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterOrganziationRequest));
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(createOrganizationRequest);
    }

    #endregion

    #region Edit Organziation Request

    public async Task<IActionResult> EditOrganizationRequest(ulong organizationRequestId,
        CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new GetOrganizationRequestQuery(organizationRequestId), cancellationToken);

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditOrganizationRequest(OrganizationRequestEntryModel editOrganizationRequest,
                                                                  CancellationToken cancellationToken)
    {
        #region Create OrganizationRequest

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditOrganizationRequestCommand(editOrganizationRequest),
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterOrganziationRequest));
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(editOrganizationRequest);
    }

    #endregion

    #region Delete OrganizationRequest

    public async Task<IActionResult> DeleteOrganizationRequest(DeleteOrganizationRequestCommand command,
                                                 CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(command, cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion

    #region Filter Request Desicion Marks

    [HttpGet]
    public async Task<IActionResult> FilterRequestDesicionMarks(ulong requestId,
      string? organizationRequestTitle,
      CancellationToken cancellationToken)
    {
        ViewData["selectedOrganizationChartIds"] = await Mediator.Send(new RequestSelectedChartIdsQuery(requestId),
            cancellationToken);
        ViewData["requestId"] = requestId;
        ViewData["organizationRequestTitle"] = organizationRequestTitle;

        var model = string.IsNullOrEmpty(organizationRequestTitle) ?
                    await Mediator.Send(new ListOfOrganizationQuery(), cancellationToken) :
                    await Mediator.Send(new ListOfOrganizationWithTitleQuery(organizationRequestTitle),
                    cancellationToken);

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterRequestDesicionMarks(ulong requestId,
                                                 List<ulong>? Permissions,
                                                 CancellationToken cancellationToken)
    {
        if (Permissions == null || !Permissions.Any())
        {
            TempData[ErrorMessage] = "عنوان اجباری است.";
            return RedirectToAction(nameof(FilterOrganziationRequest));
        }

        #region Update User Organization Request

        var res = await Mediator.Send(new AddRequestSelectedOrganziationQuery(requestId, Permissions),
            cancellationToken);

        if (res)
        {
            TempData[SuccessMessage] = "عنوان سازمانی انتخابی باموفقیت ثبت گردید.";
            return RedirectToAction(nameof(FilterOrganziationRequest));
        }

        #endregion

        ViewData["selectedOrganizationChartIds"] = await Mediator.Send(new RequestSelectedChartIdsQuery(requestId),
            cancellationToken);

        ViewData["requestId"] = requestId;

        return View(await Mediator.Send(new ListOfOrganizationQuery(), cancellationToken));
    }

    #endregion
}
