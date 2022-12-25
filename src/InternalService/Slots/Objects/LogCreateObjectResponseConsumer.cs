using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using MassTransit;

namespace InternalService.Slots.Objects;

public class LogCreateObjectResponseConsumer : IConsumer<UpdateObjectLogHandle>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _config;

    public LogCreateObjectResponseConsumer(
        IObjectRepository<IIObject> objectRepository,
        ILoggingService loggingService,
        IConfiguration config)
    {
        _objectRepository = objectRepository;
        _loggingService = loggingService;
        _config = config;
    }

    
    public async Task Consume(ConsumeContext<UpdateObjectLogHandle> context)
    {
        var objectId = context.Message.ObjectId;
        var logHandleId = context.Message.LogHandleId;
        
        var obj = await _objectRepository.GetAsync(x=>x.Id == objectId);
        if (obj == null)
        {
            _loggingService.Error($"Object {objectId} is null", logHandleId);
            await Task.CompletedTask; 
        }
        obj.LogHandleId = logHandleId;
        await _objectRepository.UpdateAsync(obj);
        await Task.CompletedTask;
    }
}