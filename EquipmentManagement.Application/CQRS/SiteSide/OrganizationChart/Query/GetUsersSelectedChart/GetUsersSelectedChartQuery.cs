
namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.GetUsersSelectedChart;

public record GetUsersSelectedChartQuery(ulong OrganizationChartId) : 
    IRequest<List<Domain.Entities.Users.User>>;
