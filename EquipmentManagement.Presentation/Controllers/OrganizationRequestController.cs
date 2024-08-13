using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using Microsoft.AspNetCore.Mvc;

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
}
