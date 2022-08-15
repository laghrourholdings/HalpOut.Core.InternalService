using CommonLibrary.Core;
using Microsoft.EntityFrameworkCore;

namespace InternalService.EFCore;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt)
    {
            
    }

    public DbSet<IIObject> Objects { get; set; }
}