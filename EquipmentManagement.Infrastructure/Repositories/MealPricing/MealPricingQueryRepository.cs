using EquipmentManagement.Application.Utilities;
using EquipmentManagement.Domain.DTO.Common;
using EquipmentManagement.Domain.DTO.SiteSide.MealPricing;
using EquipmentManagement.Domain.IRepositories.MealPricing;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.MealPricing;

public class MealPricingQueryRepository : QueryGenericRepository<Domain.Entities.MealPricing.MealPricing>, IMealPricingQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public MealPricingQueryRepository(EquipmentManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<FilterMealPricing>> FillSelectListOfMealPricingDTO(FilterMealPricing filter, CancellationToken cancellation)
    {
        return _context.MealPricing
                    .AsNoTracking()
                    .Where(p => !p.IsDelete)
                    .Select(p => new FilterMealPricing()
                    {
                        Price = p.Id,
                        MealType = p.MealType
                    })
                    .ToList();
    }

    #endregion

    #region Admin Side

    public async Task<FilterMealPricing> FilterMealPricing(FilterMealPricing filter)
    {
        var query = _context.MealPricing
                                        .AsNoTracking()
                                        .Where(p => !p.IsDelete)
                                        .OrderByDescending(p => p.CreateDate)
                                        .AsQueryable();

        #region filter

        if (filter.MealType!=null)
        {
            query = query.Where(u => u.MealType==(filter.MealType));
        }
        if (filter.Price.HasValue)
        {
            query = query.Where(u => u.Price>=(filter.Price));
        }
        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }


 

    public async Task<List<FilterMealPricing>> FilterMealPricing(FilterMealPricing filter, CancellationToken cancellationToken)
    {
        return  _context.MealPricing
                       .AsNoTracking()
                       .Where(p => !p.IsDelete)
                       .Select(p => new FilterMealPricing()
                       {
                           Price = p.Id,
                           MealType = p.MealType
                       })
                       .ToList();
    }

    #endregion
}

