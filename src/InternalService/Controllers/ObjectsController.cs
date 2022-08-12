using CommonLibrary.Core;
using CommonLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InternalService.Controllers;

[Route("[controller]")]
[ApiController]
public class ObjectsController : ControllerBase
{
    private readonly IObjectRepository<IObject> _objectRepository;
    
    public ObjectsController(IObjectRepository<IObject> objectRepository)
    {
        _objectRepository = objectRepository;
    }
    
    
    [HttpPost()]
    public async Task<IActionResult> CreateObject()
    {
        await _objectRepository.CreateAsync(null);
        return Ok("");
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAllObjects()
    {
        var objs = await _objectRepository.GetAllAsync();
        return Ok(objs);
    }
}