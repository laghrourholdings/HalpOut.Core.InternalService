using System.Linq.Expressions;
using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations.InternalService;
using CommonLibrary.Repository;
using MassTransit;

namespace InternalService.Implementations;

public class ObjectRepository : IObjectRepository<IIObject>
{
    private readonly ServiceDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public ObjectRepository(ServiceDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }
    public Task<IReadOnlyCollection<IIObject>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<IIObject>> GetAllAsync(Expression<Func<IIObject, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public Task<IIObject> GetAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IIObject> GetAsync(Expression<Func<IIObject, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(IIObject entity)
    {
        await _context.Objects.AddAsync(entity);
        await _publishEndpoint.Publish(new ObjectCreated(entity));
    }

    public Task UpdateAsync(IIObject entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(IIObject entity)
    {
        throw new NotImplementedException();
    }

    public Task SuspendAsync(IIObject entity)
    {
        throw new NotImplementedException();
    }
}