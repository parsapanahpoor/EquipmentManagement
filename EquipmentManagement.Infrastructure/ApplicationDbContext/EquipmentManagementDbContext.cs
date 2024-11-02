#region properties

using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.Entities.OperatorLogger;
using EquipmentManagement.Domain.Entities.OrganizationChart;
using EquipmentManagement.Domain.Entities.OrganizationRequest;
using EquipmentManagement.Domain.Entities.OrganizationRequest.AbolitionRequest;
using EquipmentManagement.Domain.Entities.Places;
using EquipmentManagement.Domain.Entities.Product;
using EquipmentManagement.Domain.Entities.ProductCategory;
using EquipmentManagement.Domain.Entities.ProductLog;
using EquipmentManagement.Domain.Entities.PropertyInquiry;
using EquipmentManagement.Domain.Entities.Role;
using EquipmentManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
namespace EquipmentManagement.Infrastructure.ApplicationDbContext;

#endregion

public class EquipmentManagementDbContext : DbContext
{
    #region Ctor

    public EquipmentManagementDbContext(DbContextOptions<EquipmentManagementDbContext> options)
           : base(options) { }

    #endregion

    #region Entity

    #region User

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<UserRole> UserRole { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    #endregion

    #region Product Category

    public DbSet<ProductCategory> ProductCategories { get; set; }

    #endregion

    #region Place

    public DbSet<Place> Places { get; set; }

    #endregion

    #region Product

    public DbSet<Product> Products { get; set; }

    #endregion

    #region Property Inquiry

    public DbSet<PropertyInquiry> PropertyInquiries { get; set; }

    public DbSet<PropertyInquiryDetail> PropertyInquiryDetails { get; set; }

    #endregion

    #region Logger 

    public DbSet<OperatorExcelUploadLogger> OperatorExcelUploadLoggers { get; set; }

    #endregion

    #region Organization Chart

    public DbSet<OrganziationRequestEntity> OrganziationRequests { get; set; }
    public DbSet<RequestDecisionMaker> RequestDecisionMakers { get; set; }

    #endregion

    #region Organization Request

    public DbSet<OrganizationChartAggregate> OrganizationCharts { get; set; }
    public DbSet<UserSelectedOrganizationChartEntity> UserSelectedOrganizationCharts { get; set; }

    #region Repair Request

    public DbSet<RepairRequest> RepairRequests { get; set; }
    public DbSet<DecisionRepairRequestRespons> DecisionRepairRequestRespons { get; set; }
    public DbSet<ExpertVisitorOpinionEntity> ExpertVisitorOpinions { get; set; }

    #endregion

    #region Abolition Request 

    public DbSet<AbolitionRequest> AbolitionRequests { get; set; }
    public DbSet<ExpertVisitorOpinionForAbolitionRequestEntity> ExpertVisitorOpinionForAbolitionRequestEntities { get; set; }

    #endregion

    #endregion

    #region Product Log

    public DbSet<ProductLog> ProductLogs { get; set; }

    #endregion

    #endregion

    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}
