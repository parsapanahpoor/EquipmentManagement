
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.AddUserSelectedOrganziation;

public record AddUserSelectedOrganziationQueryHandler : IRequestHandler<AddUserSelectedOrganziationQuery, bool>
{
    private readonly IOrganizationChartCommandRepository _commandRepository;
    private readonly IOrganizationChartQueryRepository _queryRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserSelectedOrganziationQueryHandler(IOrganizationChartCommandRepository commandRepository,
        IUnitOfWork unitOfWork,
        IOrganizationChartQueryRepository queryRepository,
        IUserQueryRepository userQueryRepository)
    {
        _queryRepository = queryRepository;
        _commandRepository = commandRepository;
        _unitOfWork = unitOfWork;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<bool> Handle(AddUserSelectedOrganziationQuery request, CancellationToken cancellationToken)
    {
        if (request.organizationChartIds == null ||
            !request.organizationChartIds.Any())
            return false;

        var user = await _userQueryRepository.GetByIdAsync(cancellationToken, request.userId);
        if (user == null)
            return false;

        //Remove Lastest Records
        var userSelectedOrganizationCharts = await _queryRepository.Get_UserSelectedOrganizationCharts_ByUserId(request.userId, cancellationToken);
        if (userSelectedOrganizationCharts != null &&
            userSelectedOrganizationCharts.Any())
        {
            foreach (var userChart in userSelectedOrganizationCharts)
            {
                _commandRepository.Delete_UserSelectedOrganziationChart(userChart);
            }
        }

        //Add New Records
        foreach (var chartOrganizationId in request.organizationChartIds)
        {
            var record = new UserSelectedOrganizationChartEntity()
            {
                CreateDate = DateTime.Now,
                OrganizationChartAggregateId = chartOrganizationId,
                UserId = user.Id,
            };

            await _commandRepository.Add_UserSelectedOrganizatiuonChart(record, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
