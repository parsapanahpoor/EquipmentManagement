using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Delete;

public record DeleteOrganizationChartCommandHandler :
    IRequestHandler<DeleteOrganizationChartCommand, bool>
{
    #region Ctor

    private readonly IOrganizationChartCommandRepository _organizationChartCommandRepository;
    private readonly IOrganizationChartQueryRepository _organizationChartQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrganizationChartCommandHandler(IOrganizationChartCommandRepository organizationChartCommandRepository,
        IOrganizationChartQueryRepository organizationChartQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _organizationChartQueryRepository = organizationChartQueryRepository;
        _organizationChartCommandRepository = organizationChartCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(DeleteOrganizationChartCommand request, CancellationToken cancellationToken)
    {
        //Get Organization Chart by Id 
        var organizationChart = await _organizationChartQueryRepository.GetByIdAsync(cancellationToken,
            request.OrganizationChartId);
        if (organizationChart == null) return false;

        organizationChart.IsDelete = true;

        _organizationChartCommandRepository.Update(organizationChart);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
