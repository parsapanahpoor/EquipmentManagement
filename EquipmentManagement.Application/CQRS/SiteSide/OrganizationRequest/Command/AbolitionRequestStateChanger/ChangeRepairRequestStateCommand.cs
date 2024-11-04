using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Utilities.Security;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AbolitionRequestStateChanger;

public record AbolitionRequestStateChangerCommand(
        ulong userId,
        ulong abolitionRequestId,
        AbolitionRequestState requestState,
        string description) :
    IRequest<bool>;

public record AbolitionRequestStateChangerCommandHandler(
    IUserQueryRepository UserQueryRepository,
    IOrganziationRequestQueryRepository OrganziationRequestQueryRepository,
    IOrganziationRequestCommandRepository OrganziationRequestCommandRepository,
    IOrganizationChartQueryRepository OrganziationChartQueryRepository,
    IProductLogCommandRepository ProductLogCommandRepository,
    IProductCommandRepository ProductCommandRepository,
    IProductQueryRepository ProductQueryRepository,
    IUnitOfWork UnitOfWork) :
    IRequestHandler<AbolitionRequestStateChangerCommand, bool>
{
    public async Task<bool> Handle(AbolitionRequestStateChangerCommand request,
        CancellationToken cancellationToken)
    {
        //Get Reuqest By Id 
        var AbolitionRequest = await OrganziationRequestQueryRepository.GetAbolitionRequestById(request.abolitionRequestId,
            cancellationToken);

        if (AbolitionRequest == null)
            return false;

        var realProduct = await ProductQueryRepository.GetByIdAsync(cancellationToken, AbolitionRequest.ProductId);
        if (realProduct == null)
            return false;

        //Check That is current user in visit expert of this request 
        var expertOpinion = await OrganziationRequestQueryRepository.Get_ExpertOpinion_ByAbolitionRequestId(request.abolitionRequestId,
            cancellationToken);

        if (expertOpinion == null ||
            expertOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Reject)
            return false;

        if (expertOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.WaitingForRespons)
        {
            if (expertOpinion.ExpertUserId != request.userId)
                return false;

            //Change Expert Visitor Opinion 
            expertOpinion.UpdateDate = DateTime.Now;
            expertOpinion.Description = request.description.SanitizeText();
            expertOpinion.ResponsType = request.requestState == AbolitionRequestState.WaitingForProductsCollectorRespons ?
                Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Accepted :
                Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Reject;

            OrganziationRequestCommandRepository.Update_ExpertVisitorResponse(expertOpinion);

            if (expertOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Accepted)
            {
                AbolitionRequest.ExpertVisitorAbolitionRequestState = ExpertAbolitionRequestState.Accepted;
                OrganziationRequestCommandRepository.Update_AbolitionRequest(AbolitionRequest);

                //Add Product Log
                var productLog = new Domain.Entities.ProductLog.ProductLog()
                {
                    UserId = request.userId,
                    Description = "تایید اسقاط کالا توسط مدیر",
                    PlaceId = null,
                    ProductId = AbolitionRequest.ProductId
                };
                await ProductCommandRepository.AddProductLog(productLog, cancellationToken);
            }
            if (expertOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Reject)
            {
                AbolitionRequest.ExpertVisitorAbolitionRequestState = ExpertAbolitionRequestState.Reject;
                OrganziationRequestCommandRepository.Update_AbolitionRequest(AbolitionRequest);

                //Add Product Log
                var productLog = new Domain.Entities.ProductLog.ProductLog()
                {
                    UserId = request.userId,
                    Description = "رد اسقاط کالا توسط مدیر",
                    PlaceId = null,
                    ProductId = AbolitionRequest.ProductId
                };
                await ProductCommandRepository.AddProductLog(productLog, cancellationToken);
            }

            //Add Record for Abolition Request Decisioners
            var userSelectedOrganizationChart = await OrganziationChartQueryRepository.Get_AbolitionRequestDesiciners(cancellationToken);
            if (userSelectedOrganizationChart != null && userSelectedOrganizationChart.Any())
            {
                foreach (var desicionoresUserId in userSelectedOrganizationChart)
                {
                    await OrganziationRequestCommandRepository.Add_DecisionAbolitionRequestRespons(
                        new DecisionAbolitionRequestRespons()
                        {
                            EmployeeUserId = desicionoresUserId.UserId,
                            OrganizationChartId = desicionoresUserId.OrganizationChartAggregateId,
                            RejectDescription = null,
                            IsDelete = false,
                            Response = DecisionAbolitionRespons.WaitingForRespons,
                            Sign = 1234,
                            AbolitionRequestId = request.abolitionRequestId,
                        }, cancellationToken);
                }
            }

            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        if (expertOpinion.ResponsType == Domain.Entities.OrganizationRequest.AbolitionRequest.ExpertVisitorResponsType.Accepted)
        {
            var decisionsResponses = await OrganziationRequestQueryRepository
                .Get_DecisionAbolitionRequestRespons_ByRequestIdAndUserId(request.abolitionRequestId,
                request.userId,
                cancellationToken);

            if (decisionsResponses is null || !decisionsResponses.Any())
                return false;

            //Update Decision Reponse
            if (decisionsResponses.Any(p => p.EmployeeUserId == request.userId && p.Response == DecisionAbolitionRespons.WaitingForRespons))
            {
                foreach (var decisionsRespons in decisionsResponses.Where(p => p.Response == DecisionAbolitionRespons.WaitingForRespons))
                {
                    decisionsRespons.UpdateDate = DateTime.Now;
                    decisionsRespons.RejectDescription = request.description;
                    decisionsRespons.Response = request.requestState ==
                        AbolitionRequestState.WaitingForManagerRespons ?
                        DecisionAbolitionRespons.Accepted :
                        DecisionAbolitionRespons.Reject;

                    OrganziationRequestCommandRepository.Update_DecisionAbolitionRequestRespons(decisionsRespons);
                }
            }

            if (request.requestState == AbolitionRequestState.WaitingForProductsCollectorRespons)
            {
                AbolitionRequest.RequestState = AbolitionRequestState.Finally;
                OrganziationRequestCommandRepository.Update_AbolitionRequest(AbolitionRequest);

                //Add Product Log
                var productLog = new Domain.Entities.ProductLog.ProductLog()
                {
                    UserId = request.userId,
                    Description = "تایید اسقاط کالا توسط جمع دار اموال",
                    PlaceId = null,
                    ProductId = AbolitionRequest.ProductId
                };
                await ProductCommandRepository.AddProductLog(productLog, cancellationToken);

                //Chnage Product Abolition State
                realProduct.IsAbolition = true;
                ProductCommandRepository.Update(realProduct);
            }
            if (request.requestState == AbolitionRequestState.Reject)
            {
                AbolitionRequest.RequestState = AbolitionRequestState.Reject;
                OrganziationRequestCommandRepository.Update_AbolitionRequest(AbolitionRequest);

                //Add Product Log
                var productLog = new Domain.Entities.ProductLog.ProductLog()
                {
                    UserId = request.userId,
                    Description = "رد اسقاط کالا توسط جمع دار اموال",
                    PlaceId = null,
                    ProductId = AbolitionRequest.ProductId
                };
                await ProductCommandRepository.AddProductLog(productLog, cancellationToken);
            }

            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
