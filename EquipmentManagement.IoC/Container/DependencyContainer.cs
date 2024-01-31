#region Usings

using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.IoC.Container;

#endregion

public static class DependencyContainer
{
    public static void ConfigureDependencies(IServiceCollection services)
    {
        #region Repositories

        //services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
        //services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();

        #endregion

        #region Unit Of Work 

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
    }
}
