using EquipmentManagement.Domain.DTO.SiteSide.Product;

namespace EquipmentManagement.Domain.IRepositories.Product;

public interface IProductQueryRepository
{
    #region Site Side

    Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);

    Task<bool> IsExistAny_Product_ByBarCode(string barCode,CancellationToken cancellationToken);
                                                         

    #endregion
}
