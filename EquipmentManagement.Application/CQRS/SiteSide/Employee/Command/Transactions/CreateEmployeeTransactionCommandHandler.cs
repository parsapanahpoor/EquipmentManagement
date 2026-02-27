
using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.CQRS.SiteSide.Employee.Command.Transactions;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Employee;

namespace EquipmentManagement.Application.CQRS.SiteSide.EmployeeTransaction.Command;

public record CreateEmployeeTransactionCommandHandler : IRequestHandler<CreateEmployeeTransactionCommand, (bool,ulong)>
{
    #region Ctor 

    private readonly IEmployeeCommandRepository _EmployeeCommandRepository;
    private readonly IEmployeeTransactionCommandRepository _employeeTransactionCommandRepository;
    private readonly IEmployeeQueryRepository _EmployeeQueryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeTransactionCommandHandler(IEmployeeCommandRepository EmployeeCommandRepository,
                                    IEmployeeQueryRepository EmployeeQueryRepository,
                                    IUnitOfWork unitOfWork,
                                    IEmployeeTransactionCommandRepository employeeTransactionCommandRepository)
    {
        _EmployeeCommandRepository = EmployeeCommandRepository;
        _EmployeeQueryRepository = EmployeeQueryRepository;
        _unitOfWork = unitOfWork;
        _employeeTransactionCommandRepository = employeeTransactionCommandRepository;
    }

    #endregion

    public async Task<(bool,ulong)> Handle(CreateEmployeeTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!await _EmployeeQueryRepository.IsExistAnyEmployeeById(request.EmployeeId , cancellationToken))
            return (false,0);


        #region Add EmployeeTransaction To The Data Base

        EquipmentManagement.Domain.Entities.Employee.EmployeeTransaction transaction = new EquipmentManagement.Domain.Entities.Employee.EmployeeTransaction()
        {
           EmployeeId = request.EmployeeId ,
           Amount = request.Amount ,
           CreateDate=DateTime.Now,
           UpdateDate=DateTime.Now,
           Paid=true,
           Description=request.Description ,
           
        };

        await _employeeTransactionCommandRepository.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        #endregion

        return (true, transaction.Id);
    }
}
