using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FiltreProductAbolitionRequestDto : BasePaging<AbolitionRequest>
{
    #region properties

    public ulong ProductId { get; set; }

    #endregion
}
