using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using MassTransit;
using ILogger = Serilog.ILogger;

namespace InternalService.Slots;

public class LogCreateObjectResponseConsumer : IConsumer<LogCreateObjectResponse>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    private readonly ILogger _logger;

    public LogCreateObjectResponseConsumer(IObjectRepository<IIObject> objectRepository, ILogger logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
    }

    
    public async Task Consume(ConsumeContext<LogCreateObjectResponse> context)
    {
        var logContext = context.Message.Payload;
        var obj = logContext.Subject;
        var response = new IiObjectServiceBusMessageResponse
        {
            Subject = obj,
            Descriptor = $"Creation for object {obj.Id} completed with success.",
            InitialRequest = logContext.InitialRequest,
            Contract = nameof(CreateObjectResponse),
            StatusCode = HttpStatusCode.OK
        };
        _logger.Information("{@Descriptor} | Object with ID: {@ObjectID} assigned LogHandleId: {@LogHandleID}",response.Descriptor, obj.Id,obj.LogHandleId); 
        await context.RespondAsync(new CreateObjectResponse(response));
    }
}