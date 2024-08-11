using EquipmentManagement.Domain.Entities.OrganizationChart;

namespace EquipmentManagement.Domain.IRepositories.OrganizationChart;

public interface IOrganizationChartCommandRepository
{
    Task AddAsync(OrganizationChartAggregate aggregate, 
        CancellationToken cancellationToken);

    void Update(OrganizationChartAggregate aggregate);
}
