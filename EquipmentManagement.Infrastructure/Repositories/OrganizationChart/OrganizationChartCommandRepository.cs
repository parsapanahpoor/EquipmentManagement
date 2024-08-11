using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationChart;

public class OrganizationChartCommandRepository : 
    CommandGenericRepository<OrganizationChartAggregate>, 
    IOrganizationChartCommandRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public OrganizationChartCommandRepository(EquipmentManagementDbContext context) : base(context)
    => _context = context;

    #endregion
}
