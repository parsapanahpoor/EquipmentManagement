
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.PlaceOfService;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;

namespace EquipmentManagement.Application.CQRS.SiteSide.PlaceOfServices.Command;

public record CreatePlaceOfServiceCommandHandler : IRequestHandler<CreatePlaceOfServiceCommand, bool>
{
    #region Ctor

    private readonly IPlaceOfServicesCommandRepository _PlaceOfServicesCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlaceOfServiceCommandHandler(IPlaceOfServicesCommandRepository PlaceOfServicesCommandRepository,
                                     IUnitOfWork unitOfWork)
    {
        _PlaceOfServicesCommandRepository = PlaceOfServicesCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreatePlaceOfServiceCommand request, CancellationToken cancellationToken)
    {
        #region Fill Model 

        var model = new Domain.Entities.PlaceOfService.PlaceOfService()
        {
            CreateDate = DateTime.Now,
            ParentId = request.ParentId,
            Title = request.Title,
        };

        #endregion 

        await _PlaceOfServicesCommandRepository.AddAsync(model, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
