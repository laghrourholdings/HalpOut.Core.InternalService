using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations.InternalService;
using Microsoft.EntityFrameworkCore;

namespace InternalService.Implementations;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> opt) : base(opt)
    {
            
    }

    public DbSet<IIObject> Objects { get; set; }
}