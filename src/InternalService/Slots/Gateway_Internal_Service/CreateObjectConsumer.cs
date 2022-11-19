using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.Logging;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
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
        var payload = context.Message.Payload;
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
        var response = new ServiceBusMessageReponse<IIObject>
        {
            Subject = obj,
            Descriptor =ServiceSettings.GetMessage($"Creation for object {obj.Id} completed with success.") ,
            InitialRequest = payload,
            Contract = nameof(ObjectCreated),
            StatusCode = HttpStatusCode.OK
        };
        _logger.Debug("{@Response}",response);
        await context.Publish(new ObjectCreated(response));
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