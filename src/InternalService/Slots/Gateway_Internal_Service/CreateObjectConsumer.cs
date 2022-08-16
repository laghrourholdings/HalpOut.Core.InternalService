using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using MassTransit;
using ILogger = Serilog.ILogger;
namespace InternalService.Slots;

public class CreateObjectConsumer : IConsumer<CreateObject>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateObjectConsumer(
        IObjectRepository<IIObject> objectRepository,
        ILogger logger,
        IConfiguration configuration,
        IPublishEndpoint publishEndpoint)
    {
        _objectRepository = objectRepository;
        _configuration = configuration;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task Consume(ConsumeContext<CreateObject> context)
    {
        var requestedGuid = Guid.NewGuid();
        IIObject obj = new IIObject
        {
            Id = requestedGuid,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            LogHandleId = default,
            Descriptor = null
        };
        var request = new ServiceBusRequest<IIObject>
        {
            Subject = obj,
            Descriptor = $"Requesting LogHandleId for object {requestedGuid}"
        };
        _logger.Information("Received request: {@Context}",context);
        var logContext = request.GetLogContext(_configuration, LogLevel.Information);
        _logger.GeneralToBusLog(
            logContext,
            $"Object created: {requestedGuid}",
            _publishEndpoint, new LogCreateObject(logContext));
        await _objectRepository.UpdateOrCreateAsync(obj);
    }
}