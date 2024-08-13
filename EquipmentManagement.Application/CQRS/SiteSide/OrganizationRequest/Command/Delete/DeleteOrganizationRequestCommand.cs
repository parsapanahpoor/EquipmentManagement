namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Delete;

public record DeleteOrganizationRequestCommand(ulong OrganizationRequestId) : 
    IRequest<bool>;
