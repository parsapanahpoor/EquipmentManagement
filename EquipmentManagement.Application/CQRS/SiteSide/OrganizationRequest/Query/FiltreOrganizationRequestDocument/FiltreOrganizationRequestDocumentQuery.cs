using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.FiltreOrganizationRequestDocument;

public record FiltreOrganizationRequestDocumentQuery(
    FiltreOrganizationRequestDocumentDto filter) : 
    IRequest<FiltreOrganizationRequestDocumentDto>;
