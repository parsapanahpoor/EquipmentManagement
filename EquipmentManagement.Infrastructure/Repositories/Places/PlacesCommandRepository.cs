using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Infrastructure.Repositories.Places;

public class PlacesCommandRepository : CommandGenericRepository<Domain.Entities.Places.Place> , IPlacesCommandRepository
{
	#region Ctor

	private readonly EquipmentManagementDbContext _context;

    public PlacesCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}
