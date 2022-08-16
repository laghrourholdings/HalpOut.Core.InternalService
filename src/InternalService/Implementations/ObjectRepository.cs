using System.Linq.Expressions;
using AutoMapper;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.Core;
using InternalService.EFCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;
namespace InternalService.Implementations;

public class ObjectRepository : IObjectRepository<IIObject>
{
    private readonly ServiceDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private static IMapper _mapper;
    private readonly ILogger _logger;

    public ObjectRepository(
        ServiceDbContext context,
        IPublishEndpoint publishEndpoint,
        IMapper mapper,
        ILogger logger)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<IEnumerable<IIObject>> GetAllAsync()
    {
        return await _context.Objects.ToListAsync();
    }

    public async Task<IEnumerable<IIObject>> GetAllAsync(Expression<Func<IIObject, bool>> filter)
    {
        return await _context.Objects.ToListAsync();
    }

    public Task<IIObject?> GetAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IIObject?> GetAsync(Expression<Func<IIObject, bool>> filter)
    {
        return await _context.Objects.SingleOrDefaultAsync(filter);
    }

    public async Task CreateAsync(
        IIObject entity)
    {
        IIObject? obj = await _context.Objects.SingleOrDefaultAsync(x => x != null && x.Id == entity.Id);
        if (obj is not null)
        {
            _logger.Critical($"Object {entity} already exists");
            throw new Exception($"Object with Id {entity.Id} already exists");
        }

        await _context.Objects.AddAsync(obj);
        await _context.SaveChangesAsync();
    }
    

    public Task UpdateAsync(IIObject entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrCreateAsync(
        IIObject entity)
    {
        IIObject? obj = await _context.Objects.SingleOrDefaultAsync(x => x != null && x.Id == entity.Id);
        if (obj is null)
        {
            try
            {
                await CreateAsync(entity);
            }catch(Exception ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                await UpdateOrCreateAsync(entity);
                return;
            }
        }
        else
        {
            _context.Objects.Update(obj);
        }
        await _context.SaveChangesAsync();
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