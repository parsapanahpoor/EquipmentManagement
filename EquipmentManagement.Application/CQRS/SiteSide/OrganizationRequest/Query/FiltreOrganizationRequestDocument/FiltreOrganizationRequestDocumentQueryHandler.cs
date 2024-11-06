using EquipmentManagement.Domain.DTO.SiteSide.Product;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.FiltreOrganizationRequestDocument;

public record FiltreOrganizationRequestDocumentQueryHandler (
    IProductQueryRepository productQueryRepository) : 
    IRequestHandler<FiltreOrganizationRequestDocumentQuery, FiltreOrganizationRequestDocumentDto>
{
    public async Task<FiltreOrganizationRequestDocumentDto> Handle(
        FiltreOrganizationRequestDocumentQuery request,
        CancellationToken cancellationToken)
        => await productQueryRepository.FiltreOrganizationRequestDocument(request.filter , 
            cancellationToken);
}