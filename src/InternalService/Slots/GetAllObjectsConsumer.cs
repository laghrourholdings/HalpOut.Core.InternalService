using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
using CommonLibrary.Core;
using MassTransit;

namespace InternalService.Slots;

public class GetAllObjectsConsumer : IConsumer<GetAllObjects>
{
    
    private readonly IObjectRepository<IObject> _objectRepository;
    
    public GetAllObjectsConsumer(IObjectRepository<IObject> objectRepository)
    {
        _objectRepository = objectRepository;
    }
    
    public async Task Consume(ConsumeContext<GetAllObjects> context)
    {
        var iiObjects = await _objectRepository.GetAllAsync();
        var response = new IiObjectServiceBusMessageResponse
        {
            Subjects = iiObjects,
            Descriptor = $"All objects requested from: {context.Message.Payload.Contract}",
            InitialRequest = context.Message.Payload,
            Contract = nameof(GetAllObjectsResponse),
            StatusCode = HttpStatusCode.OK
        };
        await context.RespondAsync(new GetAllObjectsResponse(response));
    }
}