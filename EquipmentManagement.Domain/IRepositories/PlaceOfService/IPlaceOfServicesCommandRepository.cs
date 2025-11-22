namespace EquipmentManagement.Domain.IRepositories.PlaceOfService;

public interface IPlaceOfServicesCommandRepository
{
    Task AddAsync(Domain.Entities.PlaceOfService.PlaceOfService PlaceOfService, CancellationToken cancellationToken);

    void Update(Domain.Entities.PlaceOfService.PlaceOfService PlaceOfService);
}
