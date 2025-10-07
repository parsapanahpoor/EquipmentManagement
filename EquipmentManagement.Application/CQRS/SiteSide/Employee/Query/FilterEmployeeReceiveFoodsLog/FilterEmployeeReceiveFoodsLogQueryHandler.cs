using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.Employee.Query.FilterEmployeeReceiveFoodsLog;

public record FilterEmployeeReceiveFoodsLogQueryHandler(
    IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository Repository) : 
    IRequestHandler<FilterEmployeeReceiveFoodsLogQuery, FilterEmployeeReceiveFoodsLogDto>
{

    public async Task<FilterEmployeeReceiveFoodsLogDto> Handle(
        FilterEmployeeReceiveFoodsLogQuery request, 
        CancellationToken cancellationToken)
            => await Repository.FilterEmployeeReceiveFoodsLog(new FilterEmployeeReceiveFoodsLogDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Mobile = request.Mobile,
                PersonnelCode = request.PersonnelCode,
            },
            cancellationToken);
}
