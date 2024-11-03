using EquipmentManagement.Domain.DTO.SiteSide.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Query.AbolitionRequestDetail;

public record AbolitionRequestDetailQueryHandler(
    IOrganziationRequestQueryRepository OrganziationRequestQueryRepository,
    IUserQueryRepository UserQueryRepository,
    IProductQueryRepository ProductQueryRepository) :
    IRequestHandler<AbolitionRequestDetailQuery, AbolitionRequestDetailDto?>
{
    public async Task<AbolitionRequestDetailDto?> Handle(AbolitionRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        //Get Reuqest By Id 
        var AbolitionRequest = await OrganziationRequestQueryRepository.GetAbolitionRequestById(request.abolitionRequestId,
            cancellationToken);
        if (AbolitionRequest == null)
            return null;

        //Check That is current user in visit expert of this request 
        var expertOpinion = await OrganziationRequestQueryRepository.Get_ExpertOpinion_ByAbolitionRequestId(request.abolitionRequestId,
            cancellationToken);

        if (expertOpinion == null)
            return null;

        var abolitionRequestDecisions = await OrganziationRequestQueryRepository.Get_DecisionAbolitionRequestDto_ByRequestId(
           AbolitionRequest.Id,
           cancellationToken);

        if (abolitionRequestDecisions == null &&
            expertOpinion.ExpertUserId != request.UserId)
            return null;

        if (abolitionRequestDecisions != null &&
            abolitionRequestDecisions.Any() &&
             (!abolitionRequestDecisions.Any(x => x.User!.Id == request.UserId) &&
            expertOpinion.ExpertUserId != request.UserId))
            return null;

        return new AbolitionRequestDetailDto()
        {
            Employee = await UserQueryRepository.GetByIdAsync(cancellationToken, AbolitionRequest.EmployeeUserId),
            Product = await ProductQueryRepository.GetByIdAsync(cancellationToken, AbolitionRequest.ProductId),
            ExpertVisitorOpinion = expertOpinion,
            ExpertVisitor = await UserQueryRepository.GetByIdAsync(cancellationToken, expertOpinion.ExpertUserId),
            AbolitionRequest = AbolitionRequest,
            DecisionsRespons = abolitionRequestDecisions
        };
    }
}
