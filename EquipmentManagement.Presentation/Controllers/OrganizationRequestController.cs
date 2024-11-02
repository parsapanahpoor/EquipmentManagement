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
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RepairRequestDetail;
using EquipmentManagement.Application.Extensions;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Application.CQRS.SiteSide.RepairRequest.Command.NewFolder;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.AbolitionRequestDetail;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AbolitionRequestStateChanger;

namespace EquipmentManagement.Presentation.Controllers;

public class OrganizationRequestController : SiteBaseController
{
    #region Organization Request

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

    #endregion

    #region Repair Request

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

    #region Show Repair Request Detail

    [HttpGet]
    public async Task<IActionResult> ShowRepairRequestDetail(ulong repairRequestId,
        CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new RepairRequestDetailQuery(
            User.GetUserId(),
            repairRequestId
            ), cancellationToken);

        if (model == null)
        {
            TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
            return RedirectToAction("Landing", "Home");
        }
        if (model.ExpertVisitorOpinion.ResponsType == Domain.Entities.OrganizationRequest.ExpertVisitorResponsType.Reject)
        {
            TempData[ErrorMessage] = "امکان مشاهده ی درخواست های رد شده وجود ندارد .";
            return RedirectToAction("Landing", "Home");
        }

        return View(model);
    }

    #endregion

    #region Change Repair Request State 

    [HttpPost]
    public async Task<IActionResult> ChangeRepairRequestState(ulong repairRequestId,
        RepairRequestState requestState,
        bool outSource,
        string description)
    {
        if (string.IsNullOrEmpty(description))
        {
            TempData[ErrorMessage] = "توضیحات اجباری است";
            return RedirectToAction(nameof(ShowRepairRequestDetail),
                new { repairRequestId = repairRequestId });
        }

        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
            return RedirectToAction(nameof(ShowRepairRequestDetail),
                new { repairRequestId = repairRequestId });
        }

        var result = await Mediator.Send(new ChangeRepairRequestStateCommand(
            User.GetUserId(),
            repairRequestId,
            requestState,
            outSource,
            description));

        if (result)
        {
            TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
            return RedirectToAction("Landing", "Home");
        }

        TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
        return RedirectToAction(nameof(ShowRepairRequestDetail),
            new { repairRequestId = repairRequestId });
    }

    #endregion

    #endregion

    #region Abolition Request

    #region Show Abolition Request Detail

    [HttpGet]
    public async Task<IActionResult> ShowAbolitionRequestDetail(ulong abolitionRequestId,
        CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new AbolitionRequestDetailQuery(
            User.GetUserId(),
            abolitionRequestId
            ), cancellationToken);

        if (model == null)
        {
            TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
            return RedirectToAction("Landing", "Home");
        }
        if (model.ExpertVisitorOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Reject)
        {
            TempData[ErrorMessage] = "امکان مشاهده ی درخواست های رد شده وجود ندارد .";
            return RedirectToAction("Landing", "Home");
        }

        return View(model);
    }

    #endregion

    #region Change Abolition Request State 

    [HttpPost]
    public async Task<IActionResult> ChangeAbolitionRequestState(ulong abolitionRequestId,
        AbolitionRequestState requestState,
        string description)
    {
        if (string.IsNullOrEmpty(description))
        {
            TempData[ErrorMessage] = "توضیحات اجباری است";
            return RedirectToAction(nameof(ShowAbolitionRequestDetail),
                new { abolitionRequestId = abolitionRequestId });
        }

        if (!ModelState.IsValid)
        {
            TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
            return RedirectToAction(nameof(ShowAbolitionRequestDetail),
                new { abolitionRequestId = abolitionRequestId });
        }

        var result = await Mediator.Send(new AbolitionRequestStateChangerCommand(
            User.GetUserId(),
            abolitionRequestId,
            requestState,
            description));

        if (result)
        {
            TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
            return RedirectToAction("Landing", "Home");
        }

        TempData[ErrorMessage] = "شما مجوز دیدن این بخش را ندارید.";
        return RedirectToAction(nameof(ShowAbolitionRequestDetail),
            new { abolitionRequestId = abolitionRequestId });
    }

    #endregion

    #endregion
}
