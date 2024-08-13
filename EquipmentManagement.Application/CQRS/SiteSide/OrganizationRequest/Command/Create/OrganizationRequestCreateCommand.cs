using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Create;

public record OrganizationRequestCreateCommand(OrganizationRequestEntryModel data) :
    IRequest<bool>;
