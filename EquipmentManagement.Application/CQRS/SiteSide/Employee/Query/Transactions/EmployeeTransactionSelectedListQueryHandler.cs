using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeTransaction.Query;

internal class EmployeeTransactionSelectedListQueryHandler : IRequestHandler<EmployeeTransactionSelectedListQuery, FilterEmployeeTransaction>
{
    #region Ctor

    private readonly IEmployeeTransactionQueryRepository _EmployeeTransactionQueryRepository;

    public EmployeeTransactionSelectedListQueryHandler(IEmployeeTransactionQueryRepository EmployeeTransactionQueryRepository)
    {
        _EmployeeTransactionQueryRepository = EmployeeTransactionQueryRepository;
    }

    #endregion

    public async Task<FilterEmployeeTransaction> Handle(EmployeeTransactionSelectedListQuery request, CancellationToken cancellationToken)
    {
     
        return await _EmployeeTransactionQueryRepository.FilterEmployeeTransaction(new FilterEmployeeTransaction {EmployeeId=request.EmployeeId ,EmployeeName=request.EmployeeName });
    }
}

