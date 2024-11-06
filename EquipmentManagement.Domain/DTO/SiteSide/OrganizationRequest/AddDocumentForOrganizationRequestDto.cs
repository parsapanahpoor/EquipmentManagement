using EquipmentManagement.Domain.Entities.OrganizationRequest;
using Microsoft.AspNetCore.Http;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public record AddDocumentForOrganizationRequestDto
{
    public ulong RequestId { get; set; }
    public RequestType OrganizationRequestType { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? Description { get; set; }
    public ulong ProductId { get; set; }
}