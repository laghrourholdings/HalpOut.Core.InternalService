using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using CommonLibrary.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace InternalService.EFCore;

public class ServiceDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt, IConfiguration configuration) : base(opt)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ServiceSettings serviceSettings = _configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>() ?? throw new InvalidOperationException("ServiceSettings is null");
        optionsBuilder.UseNpgsql(serviceSettings.PostgresConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCommonLibrary();
    }
    public DbSet<IIObject?> Objects { get; set; }
}