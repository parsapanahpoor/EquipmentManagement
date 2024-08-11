using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Get;

public record GetOrganizationChartQuery(ulong OrganizationChartId) : 
    IRequest<OrganizationChartEntryModel>;
