using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using EquipmentManagement.Domain.IRepositories.Product;

namespace EquipmentManagement.Infrastructure.Repositories.Product;

public class ProductCommandRepository : CommandGenericRepository<Domain.Entities.Product.Product> , IProductCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public ProductCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Site Side

    public async Task AddProductLog(Domain.Entities.ProductLog.ProductLog model, CancellationToken cancellationToken)
    {
        await _context.ProductLogs.AddAsync(model, cancellationToken);
    }

    public async Task AddProductLog(List<CreateProductLogDto> model, CancellationToken cancellationToken)
    {
        var records = new List<Domain.Entities.ProductLog.ProductLog>();

        foreach (var item in model)
        {
            var record = new Domain.Entities.ProductLog.ProductLog()
            {
                CreateDate = DateTime.Now,
                Description = item.Description,
                PlaceId = item.PlaceId,
                ProductId = item.ProductId,
                UserId = item.UserId
            };

            records.Add(record);
        }

        await _context.ProductLogs.AddRangeAsync(records, cancellationToken);
    }

    #endregion
}
