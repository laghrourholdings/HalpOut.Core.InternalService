using CommonLibrary.AspNetCore;
using CommonLibrary.AspNetCore.Logging.LoggingService;
using CommonLibrary.Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace InternalService.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InternalController : ControllerBase
{
    private readonly ILoggingService _loggingService;
    private readonly IObjectRepository<IIObject> _objectRepository;

    public InternalController(IObjectRepository<IIObject> objectRepository,
        ILoggingService loggingService)
    {
        _objectRepository = objectRepository;
        _loggingService = loggingService;
    }


    [HttpPost()]
    public async Task<IActionResult> CreateObject2()
    {
        await _objectRepository.CreateAsync(null);
        return Ok("");
    }

    [HttpGet()]
    public async Task<IActionResult> Test(
        [FromServices] IConfiguration configuration,
        [FromServices] IPublishEndpoint publishEndpoint)
    {
      
        return Ok("");
    }
}