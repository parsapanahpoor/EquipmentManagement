﻿using EquipmentManagement.Application.CQRS.SiteSide.OrganizationChart.Query.Filter;
using EquipmentManagement.Domain.DTO.SiteSide.OrganizationChart;
using EquipmentManagement.Domain.DTO.SiteSide.ProductCategory;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.IRepositories.OrganizationChart;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Infrastructure.Repositories.OrganizationChart;

public class OrganizationChartQueryRepository :
    QueryGenericRepository<OrganizationChartAggregate>,
    IOrganizationChartQueryRepository
{
    #region Ctor

    private readonly EquipmentManagementDbContext _context;

    public OrganizationChartQueryRepository(EquipmentManagementDbContext context) : base(context)
    => _context = context;

    #endregion

    public async Task<FilterOrganizationChartsDto> FilterOrganizationChart(FilterOrganizationChartsDto organizationChartAggregate, 
            CancellationToken cancellationToken)
    {
        var query = _context.OrganizationCharts
                                            .AsNoTracking()
                                            .Where(p => !p.IsDelete && !p.ParentId.HasValue)
                                            .OrderByDescending(p => p.CreateDate)
                                            .AsQueryable();

        if (organizationChartAggregate.ParentId.HasValue)
            query = _context.OrganizationCharts
                                             .AsNoTracking()
                                             .Where(p => !p.IsDelete && p.ParentId == organizationChartAggregate.ParentId)
                                             .OrderByDescending(p => p.CreateDate)
                                             .AsQueryable();

        #region filter

        if ((!string.IsNullOrEmpty(organizationChartAggregate.Title)))
            query = query.Where(u => u.Title!.Contains(organizationChartAggregate.Title));

        #endregion

        #region paging

        await organizationChartAggregate.Paging(query);

        #endregion

        return organizationChartAggregate;
    }

    public async Task<OrganizationChartEntryModel?> FillOrganizationChartEntryModel(ulong organizationChartId,
        CancellationToken cancellation)
    {
        return await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == organizationChartId)
                             .Select(p => new OrganizationChartEntryModel()
                             {
                                 Description = p.Description,
                                 OrganizationChartId = organizationChartId,
                                 ParentId = p.ParentId,
                                 Title = p.Title,
                             })
                             .FirstOrDefaultAsync();
    }

    public async Task<List<ulong>?> Get_OrganizationChartsIds_ByUserId(ulong userId ,
        CancellationToken cancellation)
    => await _context.UserSelectedOrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete &&
                                    p.Id == userId)
                             .Select(p => p.OrganizationChartId)
                             .ToListAsync();

    public async Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(CancellationToken cancellation)
    => await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete && !p.ParentId.HasValue)
                             .Select(p => new OrganizationChartSelectedForUserDto()
                             {
                                 OrganizationChart = p , 
                                 OrganizationChartChildren = _context.OrganizationCharts
                                 .AsNoTracking()
                                 .Where(c=> !c.IsDelete && 
                                 c.ParentId == p.Id)
                                 .Select(c=> new OrganizationChartAggregate()
                                 {
                                     Id = c.Id,
                                     ParentId= c.ParentId,
                                     IsDelete = c.IsDelete,
                                     CreateDate = c.CreateDate,
                                     Description = c.Description,
                                     Title = c.Title,
                                     UpdateDate = c.UpdateDate,
                                 })
                                 .ToList()
                             })
                             .ToListAsync();

    public async Task<List<OrganizationChartSelectedForUserDto>?> FillOrganizationChartSelectedForUserDto(string brandTitle,
        CancellationToken cancellation)
    {
        return await _context.OrganizationCharts
                             .AsNoTracking()
                             .Where(p => !p.IsDelete)
                             .Select(p => new OrganizationChartSelectedForUserDto()
                             {
                                 OrganizationChart = p,
                                 OrganizationChartChildren = _context.OrganizationCharts
                                 .AsNoTracking()
                                 .Where(c => !c.IsDelete &&
                                 c.ParentId == p.Id && 
                                 c.Title.Contains(brandTitle))
                                 .Select(c => new OrganizationChartAggregate()
                                 {
                                     Id = c.Id,
                                     ParentId = c.ParentId,
                                     IsDelete = c.IsDelete,
                                     CreateDate = c.CreateDate,
                                     Description = c.Description,
                                     Title = c.Title,
                                     UpdateDate = c.UpdateDate,
                                 })
                                 .ToList()
                             })
                             .ToListAsync();
    }
}