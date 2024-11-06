using EquipmentManagement.Domain.Entities.OrganizationRequest;
using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AddDocumentForOrganizationRequest;

public record AddDocumentForOrganizationRequestCommand(
    ulong RequestId,
    RequestType OrganizationRequestType,
    IFormFile? ImageFile,
    string? Description , 
    ulong UserId) : 
    IRequest<bool>;
