namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RequestSelectedChartIds;

public record RequestSelectedChartIdsQuery(
    ulong requestId) : 
    IRequest<List<ulong>>;
