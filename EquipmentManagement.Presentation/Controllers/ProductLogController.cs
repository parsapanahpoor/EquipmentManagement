using EquipmentManagement.Application.CQRS.SiteSide.Product.Query;
using EquipmentManagement.Application.CQRS.SiteSide.ProductLog.Query.FilterProductLogs;
using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagement.Presentation.Controllers;

public class ProductLogController : SiteBaseController
{
    public async Task<IActionResult> FilterProductLog(
        FilterProductLogDTO filter , 
        CancellationToken cancellationToken)
        => View(await Mediator.Send(new FilterProductLogsQuery(filter) , cancellationToken));
}
