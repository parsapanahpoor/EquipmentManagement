using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.Employee;
using EquipmentManagement.Domain.Entities.Employee;
using EquipmentManagement.Domain.IRepositories.Common;

namespace EquipmentManagement.Domain.IRepositories.Employee;

public interface IEmployeeTransactionQueryRepository : IQueryGenericRepository<EmployeeTransaction>
{
    #region General Methods

    Task<FilterEmployeeTransaction> FilterEmployeeTransaction(FilterEmployeeTransaction filter);



    #endregion
}
