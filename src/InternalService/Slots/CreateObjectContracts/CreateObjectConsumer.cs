using System.Net;
using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations;
using CommonLibrary.Interfaces;
using CommonLibrary.Repositories;
using InternalService.Implementations;
using MassTransit;

namespace InternalService.Slots.CreateObjectContracts;

public class CreateObjectConsumer : IConsumer<CreateObject>
{
    
    private readonly IObjectRepository _objectRepository;
    
    public CreateObjectConsumer(IObjectRepository objectRepository)
    {
        _objectRepository = objectRepository;
    }
    
    public async Task Consume(ConsumeContext<CreateObject> context)
    {
        var message = context.Message.Payload;
        var newObjectGuid = message.Subject;
        Console.WriteLine($"Creating... {newObjectGuid}");
        IIObject obj = new IIObject
        {
            Id = newObjectGuid,
            CreationDate = DateTimeOffset.Now,
            IsDeleted = false,
            DeletedDate = default,
            IsSuspended = false,
            SuspendedDate = default,
            LogHandleId = default,
            Descriptor = null
        };
        
        await _objectRepository.CreateAsync(obj);
        var response = new ObjectServiceBusResponse<Guid, IObject>
        {
            Subject = obj,
            Descriptor = $"Creation for object {obj.Id} completed with success.",
            InitialRequest = message,
            Contract = nameof(CreateObjectResponse),
            StatusCode = HttpStatusCode.OK
        };
        await context.RespondAsync(new CreateObjectResponse(response));
        //await _repository.CreateAsync(item);
    }
}