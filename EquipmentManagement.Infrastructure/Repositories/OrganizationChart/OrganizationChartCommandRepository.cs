using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

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

    public async Task Add_UserSelectedOrganizatiuonChart(UserSelectedOrganizationChartEntity model  , 
        CancellationToken cancellationToken)
    => await _context.UserSelectedOrganizationCharts.AddAsync(model , cancellationToken);

    public void Delete_UserSelectedOrganziationChart(UserSelectedOrganizationChartEntity model)
        => _context.UserSelectedOrganizationCharts.Remove(model);
}
