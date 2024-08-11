using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.ListOfOrganizationCharts;

public record ListOfOrganizationWithTitleQuery(string title) : IRequest<List<OrganizationChartSelectedForUserDto>?>;
