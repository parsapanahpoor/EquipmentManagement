#region Usings

using EquipmentManagement.Application.Common.IUnitOfWork;
using EquipmentManagement.Application.Service;
using EquipmentManagement.Domain.IRepositories.Employee;
using EquipmentManagement.Domain.IRepositories.EmployeeShiftMeals;
using EquipmentManagement.Domain.IRepositories.EmployeeShifts;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using EquipmentManagement.Domain.IRepositories.OranizationRequest;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.Place;
using EquipmentManagement.Domain.IRepositories.PlaceOfService;
using EquipmentManagement.Domain.IRepositories.Product;
using EquipmentManagement.Domain.IRepositories.ProductCategory;
using EquipmentManagement.Domain.IRepositories.ProductLog;
using EquipmentManagement.Domain.IRepositories.PropertyInquiry;
using EquipmentManagement.Domain.IRepositories.Role;
using EquipmentManagement.Domain.IRepositories.User;
using EquipmentManagement.Infrastructure.Repositories.Employee;
using EquipmentManagement.Infrastructure.Repositories.EmployeeShift;
using EquipmentManagement.Infrastructure.Repositories.EmployeeShiftMeal;
using EquipmentManagement.Infrastructure.Repositories.MealPricing;
using EquipmentManagement.Infrastructure.Repositories.OrganizationChart;
using EquipmentManagement.Infrastructure.Repositories.OrganizationRequest;
using EquipmentManagement.Infrastructure.Repositories.PlaceOfServices;
using EquipmentManagement.Infrastructure.Repositories.Places;
using EquipmentManagement.Infrastructure.Repositories.Product;
using EquipmentManagement.Infrastructure.Repositories.ProductCategory;
using EquipmentManagement.Infrastructure.Repositories.ProductLog;
using EquipmentManagement.Infrastructure.Repositories.PropertyInquiry;
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

        //Place Of Services
        services.AddScoped<IPlaceOfServicesCommandRepository, PlaceOfServicesCommandRepository>();
        services.AddScoped<IPlaceOfServicesQueryRepository, PlaceOfServicesQueryRepository>();

        //Product
        services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

        //Property Inquiry
        services.AddScoped<IPropertyInquiryCommandRepository , PropertyInquiryCommandRepository>();
        services.AddScoped<IPropertyInquiryQueryRepository, PropertyInquiryQueryRepository>();

        //Organization Chart
        services.AddScoped<IOrganizationChartCommandRepository , OrganizationChartCommandRepository>();
        services.AddScoped<IOrganizationChartQueryRepository , OrganizationChartQueryRepository>();

        //Organization Request
        services.AddScoped<IOrganziationRequestCommandRepository, OrganziationRequestCommandRepository>();
        services.AddScoped<IOrganziationRequestQueryRepository, OrganziationRequestQueryRepository>();

        //Product Log
        services.AddScoped<IProductLogCommandRepository, ProductLogCommandRepository>();
        services.AddScoped<IProductLogQueryRepository, ProductLogQueryRepository>();

        //MealPricing
        services.AddScoped<IMealPricingCommandRepository, MealPricingCommandRepository>();
        services.AddScoped<IMealPricingQueryRepository, MealPricingQueryRepository>();


        //EmployeeShiftMeal
        services.AddScoped<IEmployeeShiftMealSelectedCommandRepository, EmployeeShiftMealSelectedCommandRepository>();
        services.AddScoped<IEmployeeShiftMealSelectedQueryRepository, EmployeeShiftMealSelectedQueryRepository>();


        //EmployeeShift
        services.AddScoped<IEmployeeShiftSelectedCommandRepository, EmployeeShiftSelectedCommandRepository>();
        services.AddScoped<IEmployeeShiftSelectedQueryRepository, EmployeeShiftSelectedQueryRepository>();

        //Employee 
        services.AddScoped<IPDFService, PDFService>();
        services.AddScoped<IEmployeeCommandRepository, EmployeeCommandRepository>();
        services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();
        services.AddScoped<IEmployeeReceiveFoodDeliveryReceiptLogCommandRepository, EmployeeReceiveFoodDeliveryReceiptLogCommandRepository>();
        services.AddScoped<IEmployeeReceiveFoodDeliveryReceiptLogQueryRepository , EmployeeReceiveFoodDeliveryReceiptLogQueryRepository>();

        #endregion

        #region Unit Of Work 

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
    }
}
