using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.Edit;

public record EditOrganizationRequestCommand(
    OrganizationRequestEntryModel data) :
    IRequest<bool>;
