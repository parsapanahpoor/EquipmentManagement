using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;

namespace EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Command.Create;

public record CreateOrganizationChartCommandHandler : IRequestHandler<CreateOrganizationChartCommand, bool>
{
    #region Ctor

    private readonly IOrganizationChartCommandRepository _organizationChartCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrganizationChartCommandHandler(IOrganizationChartCommandRepository organizationChartCommandRepository,
                                     IUnitOfWork unitOfWork)
    {
        _organizationChartCommandRepository = organizationChartCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreateOrganizationChartCommand request, CancellationToken cancellationToken)
    {
        await _organizationChartCommandRepository.AddAsync(new OrganizationChartAggregate()
        { 
            CreateDate = DateTime.Now,
            Description = request.model.Description,
            IsDelete = false,
            ParentId = request.model.ParentId,
            Title = request.model.Title
        }, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
