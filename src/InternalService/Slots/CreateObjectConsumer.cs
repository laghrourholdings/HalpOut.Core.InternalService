using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Implementations.InternalService;
using CommonLibrary.Repository;
using MassTransit;

namespace InternalService.Slots;

public class CreateObjectConsumer : IConsumer<CreateObject>
{
    
    private readonly IObjectRepository<IObject> _repository;
    
    public CreateObjectConsumer(IObjectRepository<IObject> repository)
    {
        _repository = repository;
    }
    
    public async Task Consume(ConsumeContext<CreateObject> context)
    {
        var message = context.Message;
        // var item = await _repository.GetAsync(message.ItemId);
        // if (item != null)
        // {
        //     return;
        // }
        // item = new CatalogItem()
        // {
        //     Id = message.ItemId,
        //     Name = message.Name,
        //     Description = message.Description
        // };
        Console.WriteLine($"Verifying... {message.obj.Id}");
        await context.RespondAsync(new CreateObjectResponse(message.obj));
        //await _repository.CreateAsync(item);
    }
}