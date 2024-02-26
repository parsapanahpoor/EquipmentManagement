#region Usings

using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;
using EquipmentManagement.Infrastructure.Repositories.Places;
using EquipmentManagement.Infrastructure.Repositories.Product;
using EquipmentManagement.Infrastructure.Repositories.ProductCategory;
using EquipmentManagement.Infrastructure.Repositories.Role;
using EquipmentManagement.Infrastructure.Repositories.User;
using EquipmentManagement.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EquipmentManagement.IoC;

#endregion

public static class DependencyContainer
{
    public static void ConfigureDependencies(IServiceCollection services)
    {
        #region Repositories

        //User
        services.AddScoped<IUserCommandRepository, UserCommandRepository>();
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        //Product Category
        services.AddScoped<IProductCategoryCommandRepository, ProductCategoryCommandRepository>();
        services.AddScoped<IProductCategoryQueryRepository, ProductCategoryQueryRepository>();

        //Role
        services.AddScoped<IRoleCommandRepository, RoleCommandRepository>();
        services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();

        //Places
        services.AddScoped<IPlacesCommandRepository, PlacesCommandRepository>();
        services.AddScoped<IPlacesQueryRepository, PlacesQueryRepository>();

        //Product
        services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

        #endregion

        #region Unit Of Work 

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
    }
}
