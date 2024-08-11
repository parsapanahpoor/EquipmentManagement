using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Create;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Delete;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Edit;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Get;
using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.GetUsersSelectedChart;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Presentation.HttpManager;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class OrganizationChartController : SiteBaseController
{
    #region Filter OrganizationChart 

    public async Task<IActionResult> FilterOrganizationChart(FilterOrganizationChartsDto filter,
                                                  CancellationToken cancellation)
    => View(await Mediator.Send(new FilterOrganizationChartQuery()
    {
        Filter = filter,
    },cancellation));

    #endregion

    #region Create OrganizationChart

    [HttpGet]
    public IActionResult CreateOrganizationChart(ulong? parentId)
    {
        ViewBag.parentId = parentId;

        return View(new OrganizationChartEntryModel()
        {
            ParentId = parentId
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrganizationChart(OrganizationChartEntryModel createOrganizationChart,
                                                             CancellationToken cancellationToken)
    {
        #region Create OrganizationChart

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new CreateOrganizationChartCommand(createOrganizationChart) ,
            cancellationToken);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterOrganizationChart), new { parentId = createOrganizationChart.ParentId });
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(createOrganizationChart);
    }

    #endregion

    #region Edit OrganizationChart

    [HttpGet]
    public async Task<IActionResult> EditOrganizationChart(ulong organizationChartId,
                                               CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new GetOrganizationChartQuery(organizationChartId),
            cancellationToken);
        if (model == null) return NotFound();

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditOrganizationChart(OrganizationChartEntryModel editOrganizationChart,
                                               CancellationToken cancellation)
    {
        #region Edit OrganizationChart 

        if (ModelState.IsValid)
        {
            var res = await Mediator.Send(new EditOrganizationChartCommand()
            {
                Title = editOrganizationChart.Title,
                ParentId = editOrganizationChart.ParentId,
                Description = editOrganizationChart.Description,
                OrganizationChartId = editOrganizationChart.OrganizationChartId
            },
            cancellation);

            if (res)
            {
                TempData[SuccessMessage] = "عملیات باموفقیت انجام شده است.";
                return RedirectToAction(nameof(FilterOrganizationChart), new { parentId = editOrganizationChart.ParentId });
            }
        }

        #endregion

        TempData[ErrorMessage] = "اطلاعات وارد شده صحیح نمی باشد.";
        return View(editOrganizationChart);
    }

    #endregion

    #region Delete OrganizationChart

    public async Task<IActionResult> DeleteOrganizationChart(DeleteOrganizationChartCommand command,
                                                 CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(command, cancellationToken);
        if (res) return JsonResponseStatus.Success();

        return JsonResponseStatus.Error();
    }

    #endregion

    #region List Of Users Selected Organization Chart

    public async Task<IActionResult> ListOfUserSelectedOrganizationChart(ulong organizationChartId,
        CancellationToken cancellationToken)
        => View(await Mediator.Send(new GetUsersSelectedChartQuery(organizationChartId) , cancellationToken));

    #endregion
}
