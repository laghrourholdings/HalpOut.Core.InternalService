using System.Net;
using CommonLibrary.Contracts.Gateway_Internal_Contracts;
using CommonLibrary.Entities.InternalService;
using CommonLibrary.Interfaces;
namespace InternalService.Slots.CreateObjectContracts;

public class CreateObjectSbResponse
    : IObjectServiceBusResponse<Guid, IObject>
{
    public string Contract { get; set; }
    public IObject Subject { get; set; }
    public string? Descriptor { get; set; }
    public string? Data { get; set; }
    public Guid? LogHandleId { get; set; }
    public IServiceBusRequest<Guid> InitialRequest { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}