using System.Net;
using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Contracts;
using CommonLibrary.AspNetCore.ServiceBus;
using CommonLibrary.AspNetCore.ServiceBus.Implementations;
using CommonLibrary.Core;
using MassTransit;

namespace InternalService.Slots;

public class CreateObjectConsumer : IConsumer<CreateObject>
{
    
    private readonly IObjectRepository<IObject> _objectRepository;
    
    public CreateObjectConsumer(IObjectRepository<IObject> objectRepository)
    {
        _objectRepository = objectRepository;
    }
    
    public async Task Consume(ConsumeContext<CreateObject> context)
    {
        var message = context.Message.Payload;
        var requestedGuid = message.Subject;
        Console.WriteLine($"Creating... {requestedGuid}");
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
        
        await _objectRepository.CreateAsync(obj);
        var response = new ServiceBusRequestReponse<Guid, IIObject>()
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