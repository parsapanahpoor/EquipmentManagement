
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.Entities.Places;
using EquipmentManagement.Domain.IRepositories.Place;

namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Command;

public record CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, bool>
{
    #region Ctor

    private readonly IPlacesCommandRepository _placesCommandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlaceCommandHandler(IPlacesCommandRepository placesCommandRepository,
                                     IUnitOfWork unitOfWork)
    {
        _placesCommandRepository = placesCommandRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<bool> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        #region Fill Model 

        var model = new Place()
        {
            CreateDate = DateTime.Now,
            ParentId = request.ParentId,
            PlaceTitle = request.Title,
        };

        #endregion 

        await _placesCommandRepository.AddAsync(model, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
