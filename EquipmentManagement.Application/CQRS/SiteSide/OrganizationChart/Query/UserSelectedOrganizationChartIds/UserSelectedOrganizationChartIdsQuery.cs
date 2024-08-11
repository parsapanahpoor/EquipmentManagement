namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.UserSelectedOrganizationChartIds;

public record UserSelectedOrganizationChartIdsQuery(ulong userId) : IRequest<List<ulong>>;
