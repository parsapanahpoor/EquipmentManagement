using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.RepairRequestDetail;

public record RepairRequestDetailQueryHandler(
    IOrganziationRequestQueryRepository OrganziationRequestQueryRepository,
    IUserQueryRepository UserQueryRepository,
    IProductQueryRepository ProductQueryRepository) :
    IRequestHandler<RepairRequestDetailQuery, RepairRequestDetailDto?>
{
    public async Task<RepairRequestDetailDto?> Handle(RepairRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        //Get Reuqest By Id 
        var repairRequest = await OrganziationRequestQueryRepository.GetRepairRequestById(request.RepairRequestId,
            cancellationToken);
        if (repairRequest == null)
            return null;

        //Check That is current user in visit expert of this request 
        var expertOpinion = await OrganziationRequestQueryRepository.Get_ExpertOpinion_ByRepairRequestId(request.RepairRequestId,
            cancellationToken);

        if (expertOpinion == null)
            return null;

        var repairRequestDecisions = await OrganziationRequestQueryRepository.Get_DecisionRepairRequestDto_ByRequestId(
            repairRequest.Id,
            cancellationToken);

        if (repairRequestDecisions == null &&
            expertOpinion.ExpertUserId != request.UserId)
            return null;

        if (repairRequestDecisions != null  && 
            repairRequestDecisions.Any() &&
             (!repairRequestDecisions.Any(x => x.User!.Id == request.UserId) &&
            expertOpinion.ExpertUserId != request.UserId))
            return null;

        return new RepairRequestDetailDto()
        {
            Employee = await UserQueryRepository.GetByIdAsync(cancellationToken, repairRequest.EmployeeUserId),
            Product = await ProductQueryRepository.GetByIdAsync(cancellationToken, repairRequest.ProductId),
            ExpertVisitorOpinion = expertOpinion,
            ExpertVisitor = await UserQueryRepository.GetByIdAsync(cancellationToken, expertOpinion.ExpertUserId),
            RepairRequest = repairRequest,
            DecisionsRespons = repairRequestDecisions
        };
    }
}
