/*
using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts.Objects;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
using CommonLibrary.AspNetCore.Settings;
using CommonLibrary.Core;
using MassTransit;

namespace InternalService.Slots.Objects;

public class GetAllObjectsConsumer : IConsumer<GetAllObjects>
{
    
    private readonly IObjectRepository<IIObject> _objectRepository;
    
    public GetAllObjectsConsumer(IObjectRepository<IIObject> objectRepository)
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
*/