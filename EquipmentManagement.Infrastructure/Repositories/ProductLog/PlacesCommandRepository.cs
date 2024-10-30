using EquipmentManagement.Domain.DTO.SiteSide.ProductLog;
using EquipmentManagement.Domain.Entities.ProductLog;
using EquipmentManagement.Domain.IRepositories.ProductLog;

namespace EquipmentManagement.Infrastructure.Repositories.ProductLog;

public class ProductLogCommandRepository : CommandGenericRepository<Domain.Entities.ProductLog.ProductLog> , IProductLogCommandRepository
{
	#region Ctor

	private readonly EquipmentManagementDbContext _context;

    public ProductLogCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    public async Task AddProductLog(CreateProductLogDto model , CancellationToken cancellationToken)
    {
        var record = new Domain.Entities.ProductLog.ProductLog()
        {
            CreateDate = DateTime.Now,
            Description = model.Description,
            PlaceId = model.PlaceId,
            ProductId = model.ProductId,
            UserId = model.UserId
        };

        await AddAsync(record , cancellationToken);
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

        await AddRangeAsync(records, cancellationToken);
    }
}
