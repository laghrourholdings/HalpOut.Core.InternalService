using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace InternalService.Slots;

public class LogCreateObjectResponseConsumer : IConsumer<UpdateObjectLogHandle>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    private readonly ILogger _logger;

    public LogCreateObjectResponseConsumer(IObjectRepository<IIObject> objectRepository, ILogger logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<UpdateObjectLogHandle> context)
    {
        var logContext = context.Message.Payload;
        var obj = logContext.Subject;
        if (obj == null)
        {
            _logger.Error("{@Descriptor} | Object is null", logContext);
            await Task.CompletedTask; 
        }
        await _objectRepository.UpdateAsync(obj);
        _logger.Information("Object with ID: {@ObjectID} assigned LogHandleId: {@LogHandleID}", obj.Id,obj.LogHandleId);
        await Task.CompletedTask;
    }
}