using CommonLibrary.Core;
using CommonLibrary.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace InternalService.EFCore;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildCommonLibrary();
    }
    public DbSet<IIObject?> Objects { get; set; }
}