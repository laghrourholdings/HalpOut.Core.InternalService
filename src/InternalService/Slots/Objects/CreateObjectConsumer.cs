using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using MassTransit;

namespace InternalService.Slots.Objects;

public class CreateObjectConsumer : IConsumer<CreateObject>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    private readonly ILoggingService _loggingService;
    private readonly IConfiguration _configuration;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateObjectConsumer(
        IObjectRepository<IIObject> objectRepository,
        ILoggingService loggingService,
        IConfiguration configuration,
        IPublishEndpoint publishEndpoint)
    {
        _objectRepository = objectRepository;
        _configuration = configuration;
        _loggingService = loggingService;
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
            Descriptor = "Basic object"
        };
        await _objectRepository.CreateAsync(obj);
        await context.Publish(new ObjectCreated(requestedGuid));
    }
}

/*
 var request = new ServiceBusPayload<IIObject>
{
    Subject = obj,
    Descriptor = $"Requesting LogHandleId for object {requestedGuid}"
};
var logContext = request.GetLogContext(_configuration, LogLevel.Information);
_logger.GeneralToBusLog(
    logContext,
    $"Object created: {requestedGuid}",
    _publishEndpoint, new LogCreateObject(logContext));
*/