using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.Product.Command.CreateAbolitionRequest;

public record CreateAbolitionRequestCommandHandler(
    IUserQueryRepository userQueryRepository,
    IProductQueryRepository productQueryRepository,
    IOrganziationRequestQueryRepository organziationRequestQueryRepository , 
    IOrganziationRequestCommandRepository organziationRequestCommandRepository , 
    IUnitOfWork unitOfWork
    ) :
    IRequestHandler<CreateAbolitionRequestCommand, CreateAbolitionRequestCommandRespons>
{
    public async Task<CreateAbolitionRequestCommandRespons> Handle(CreateAbolitionRequestCommand request, CancellationToken cancellationToken)
    {
        //Check That is exist any configuration 
        var requestConfiguration = await organziationRequestQueryRepository.GetFirstConfiguration_ForAbolitionRequest(cancellationToken);
        if (requestConfiguration == null) 
            return CreateAbolitionRequestCommandRespons.DosentConfig;

        //Check That Exist Any  Desicion For This Request 
        if (!await organziationRequestQueryRepository.IExistAny_DesicionMaker_ForRequest(requestConfiguration.Id ,  
            cancellationToken))
            return CreateAbolitionRequestCommandRespons.DosentConfig;

        var AbolitionRequest = new AbolitionRequest()
        {
            Description = request.Description,
            EmployeeUserId = request.EmployeeId,
            ProductId = request.ProductId,
            ExpertVisitorAbolitionRequestState = ExpertAbolitionRequestState.WaitingForRespons,
            RequestState = AbolitionRequestState.WaitingForManagerRespons
        };

        await organziationRequestCommandRepository.Add_AbolitionRequest(AbolitionRequest , cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await organziationRequestCommandRepository.Add_ExpertVisitorRequest(new ExpertVisitorOpinionForAbolitionRequestEntity()
        {
            ExpertUserId = request.ExpertUserId,
            Description = null , 
            AbolitionRequestId = AbolitionRequest.Id , 
            ResponsType = ExpertVisitorResponsType.WaitingForRespons , 
        } , cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        //Can Send SMS To The Expert

        return CreateAbolitionRequestCommandRespons.Success;
    }
}
