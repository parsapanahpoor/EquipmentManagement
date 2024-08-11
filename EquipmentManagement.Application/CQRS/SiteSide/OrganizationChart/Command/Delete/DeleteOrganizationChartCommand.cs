using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Edit;
using NPOI.Util;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Delete;

public record DeleteOrganizationChartCommand(ulong OrganizationChartId) : 
    IRequest<bool>;
