using EquipmentManagement.Domain.DTO.SiteSide.FilterPlaces;
namespace EquipmentManagement.Application.CQRS.SiteSide.Places.Query;

public class FilterPlacesForExcelFileQuery : 
    IRequest<List<FilterPlacesForExcelFileDTO>>;