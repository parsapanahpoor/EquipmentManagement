using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;

public class FilterOrganizationRequestsDto : BasePaging<OrganziationRequestEntity>
{
    #region properties

    public string? Title { get; set; }

    #endregion
}