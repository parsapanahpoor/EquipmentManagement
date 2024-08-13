using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationRequest.Command.AddRequestSelectedOrganziation;

public record AddRequestSelectedOrganziationQueryHandler : 
    IRequestHandler<AddRequestSelectedOrganziationQuery, bool>
{
    private readonly IOrganziationRequestCommandRepository _commandRepository;
    private readonly IOrganizationChartQueryRepository _queryRepository;
    private readonly IOrganziationRequestQueryRepository _RequestQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddRequestSelectedOrganziationQueryHandler(IOrganziationRequestCommandRepository commandRepository,
        IUnitOfWork unitOfWork,
        IOrganizationChartQueryRepository queryRepository,
        IOrganziationRequestQueryRepository RequestQueryRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
        _unitOfWork = unitOfWork;
        _RequestQueryRepository = RequestQueryRepository;
    }

    public async Task<bool> Handle(AddRequestSelectedOrganziationQuery request, CancellationToken cancellationToken)
    {
        if (request.organizationChartIds == null ||
            !request.organizationChartIds.Any())
            return false;

        var Request = await _RequestQueryRepository.GetByIdAsync(cancellationToken, request.RequestId);
        if (Request == null)
            return false;

        //Remove Lastest Records
        var RequestSelectedOrganizationCharts = await _RequestQueryRepository.Get_RequestDecisionMaker_ByRequestId(request.RequestId, cancellationToken);
        if (RequestSelectedOrganizationCharts != null &&
            RequestSelectedOrganizationCharts.Any())
        {
            foreach (var RequestChart in RequestSelectedOrganizationCharts)
            {
                _commandRepository.Delete_RequestDecisionMaker(RequestChart);
            }
        }

        //Add New Records
        foreach (var chartOrganizationId in request.organizationChartIds)
        {
            var record = new RequestDecisionMaker()
            {
                CreateDate = DateTime.Now,
                OrganizationChartId = chartOrganizationId,
                OrganziationRequestId = Request.Id,
            };

            await _commandRepository.Add_RequestDecisionMaker(record, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
