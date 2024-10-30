using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using EquipmentManagement.Domain.IRepositories.ProductLog;

namespace EquipmentManagement.Application.CQRS.SiteSide.ProductLog.Query.FilterProductLogs;

public record FilterProductLogsQueryHandler(
    IProductLogQueryRepository productLogQueryRepository) :
    IRequestHandler<FilterProductLogsQuery, FilterProductLogDTO>
{
    public async Task<FilterProductLogDTO> Handle(
        FilterProductLogsQuery request,
        CancellationToken cancellationToken)
        => await productLogQueryRepository.FilterProductLogs(request.Filter , cancellationToken);
}
