using System.Linq.Expressions;
using CommonLibrary.Core;
using CommonLibrary.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InternalService.Implementations;

public class ObjectRepository : IObjectRepository<IObject>
{
    private readonly ServiceDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public ObjectRepository(ServiceDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<IEnumerable<IObject>> GetAllAsync()
    {
        return await _context.Objects.ToListAsync();
    }

    public Task<IEnumerable<IObject>> GetAllAsync(Expression<Func<IObject, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<IObject> GetAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IObject> GetAsync(Expression<Func<IObject, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(IObject entity)
    {
        await _context.Objects.AddAsync((IIObject)entity);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(IObject entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(IObject entity)
    {
        throw new NotImplementedException();
    }

    public Task SuspendAsync(IObject entity)
    {
        throw new NotImplementedException();
    }
}