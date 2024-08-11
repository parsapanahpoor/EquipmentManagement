using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Edit;

public record EditOrganizationChartCommand : 
    OrganizationChartEntryModel , 
    IRequest<bool>;
