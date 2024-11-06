using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FiltreProductAbolitionRequestDto : BasePaging<AbolitionRequest>
{
    #region properties

    public ulong ProductId { get; set; }

    #endregion
}

public class FiltreOrganizationRequestDocumentDto : BasePaging<OrganizationRequestDocumentEntity>
{
    #region properties

    public ulong ProductId { get; set; }

    public RequestType RequestType { get; set; }

    public ulong OrganizationRequestId { get; set; }

    #endregion
}
