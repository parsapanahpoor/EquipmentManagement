using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Create;

public record CreateOrganizationChartCommand(OrganizationChartEntryModel model) : 
    IRequest<bool>;
