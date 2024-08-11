namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.AddUserSelectedOrganziation;

public record AddUserSelectedOrganziationQuery(ulong userId , 
    List<ulong> organizationChartIds) : IRequest<bool>;
