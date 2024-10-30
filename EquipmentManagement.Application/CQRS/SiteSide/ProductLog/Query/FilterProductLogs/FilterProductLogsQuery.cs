using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductLog.Query.FilterProductLogs;

public record FilterProductLogsQuery(
    FilterProductLogDTO? Filter) :
    IRequest<FilterProductLogDTO>;
