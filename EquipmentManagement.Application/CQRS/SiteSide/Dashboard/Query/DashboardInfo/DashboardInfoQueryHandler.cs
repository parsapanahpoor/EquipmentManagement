using EquipmentManagement.Domain.DTO.SiteSide.Dashboard;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;

namespace EquipmentManagement.Application.CQRS.SiteSide.Dashboard.Query.DashboardInfo;

public record DashboardInfoQueryHandler(
    IOrganziationRequestQueryRepository organziationRequestQueryRepository) :
    IRequestHandler<DashboardInfoQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(
        DashboardInfoQuery request,
        CancellationToken cancellationToken)
    {
        var repairRequests = new List<RepairRequestDto>();
        repairRequests.AddRange(await organziationRequestQueryRepository.FillRepairRequestDto(request.UserId, cancellationToken));
        repairRequests.AddRange(await organziationRequestQueryRepository.GetLastestNewRequestAsDecisinorsForCurrentUser(request.UserId, cancellationToken));


        return new DashboardDto()
        {
            RepairRequest = repairRequests.DistinctBy(p=> p.RepairRequestId).ToList(),
        };
    }
}

