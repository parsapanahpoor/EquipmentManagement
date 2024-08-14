using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;

namespace EquipmentManagement.Application.CQRS.SiteSide.Dashboard.Query.DashboardInfo;

public record DashboardInfoQuery(
    ulong UserId) :
    IRequest<DashboardDto>;

