
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.User;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.AddUserSelectedOrganziation;

public record AddUserSelectedOrganziationQueryHandler : IRequestHandler<AddUserSelectedOrganziationQuery, bool>
{
    private readonly IOrganizationChartCommandRepository _commandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserSelectedOrganziationQueryHandler(IOrganizationChartCommandRepository commandRepository ,
        IUnitOfWork unitOfWork ,
        IUserQueryRepository userQueryRepository)
    {
        _commandRepository = commandRepository;
        _unitOfWork = unitOfWork;
        _userQueryRepository = userQueryRepository;
    }

    public async Task<bool> Handle(AddUserSelectedOrganziationQuery request, CancellationToken cancellationToken)
    {
        if (request.organizationChartIds == null || 
            !request.organizationChartIds.Any()) 
            return false;

        var user = await _userQueryRepository.GetByIdAsync(cancellationToken , request.userId);
        if (user == null) 
            return false;

        foreach (var chartOrganizationId in request.organizationChartIds)
        {
            await _commandRepository.Add_UserSelectedOrganizatiuonChart(new UserSelectedOrganizationChartEntity()
            {
                CreateDate = DateTime.Now,
                OrganizationChartId = chartOrganizationId,
                UserId = user.Id,
            }, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
