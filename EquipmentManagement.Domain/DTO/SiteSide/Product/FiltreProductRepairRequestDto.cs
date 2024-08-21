using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.Entities.OrganizationRequest;

namespace EquipmentManagement.Domain.DTO.SiteSide.Product;

public class FiltreProductRepairRequestDto : BasePaging<RepairRequest>
{
    #region properties

    public ulong ProductId { get; set; }

    #endregion
}
