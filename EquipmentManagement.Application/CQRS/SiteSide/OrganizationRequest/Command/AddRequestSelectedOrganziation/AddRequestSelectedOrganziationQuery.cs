namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AddRequestSelectedOrganziation;

public record AddRequestSelectedOrganziationQuery(
    ulong RequestId , 
    List<ulong> organizationChartIds) : 
    IRequest<bool>;
