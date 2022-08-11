﻿using System.Net;
using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations;
using CommonLibrary.Interfaces;
using CommonLibrary.Repositories;
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
        var response = new IIObjectServiceBusResponse
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