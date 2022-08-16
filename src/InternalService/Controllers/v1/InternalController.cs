using CommonLibrary.AspNetCore;
using CommonLibrary.Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace InternalService.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class InternalController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IObjectRepository<IIObject> _objectRepository;

    public InternalController(IObjectRepository<IIObject> objectRepository, ILogger logger)
    {
        _objectRepository = objectRepository;
        _logger = logger;
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