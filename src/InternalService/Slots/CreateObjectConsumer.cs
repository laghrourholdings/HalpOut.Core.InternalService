using System.Net;
using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations;
using CommonLibrary.Repositories;
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
        var response = new IIObjectServiceBusResponse
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