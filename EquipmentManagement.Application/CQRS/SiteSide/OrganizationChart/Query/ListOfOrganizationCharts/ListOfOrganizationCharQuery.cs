using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.ListOfOrganizationCharts;

public record ListOfOrganizationQuery : IRequest<List<OrganizationChartSelectedForUserDto>>;
