using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Infrastructure.Repositories.PlaceOfServices;

public class PlaceOfServicesCommandRepository :
    CommandGenericRepository<Domain.Entities.PlaceOfService.PlaceOfService> , IPlaceOfServicesCommandRepository
{
	#region Ctor

	private readonly EquipmentManagementDbContext _context;

    public PlaceOfServicesCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}
