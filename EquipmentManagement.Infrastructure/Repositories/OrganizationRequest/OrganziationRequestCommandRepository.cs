using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationRequest;

public class OrganziationRequestCommandRepository : CommandGenericRepository<OrganziationRequestEntity>, IOrganziationRequestCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public OrganziationRequestCommandRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion


}

