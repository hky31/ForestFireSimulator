using ForestFireSimulator.Domain.Entities;
using ForestFireSimulator.Domain.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ForestController : ControllerBase
{
    private static Forest _forest = new Forest(20);
    private static FireSimulationService _simulator = new FireSimulationService(_forest);

    [HttpPost("init")]
    public IActionResult Initialize()
    {
        _simulator.InitForest();
        return Ok(_forest);
    }

    [HttpPost("step")]
    public IActionResult Step()
    {
        _simulator.Step();
        return Ok(_forest);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_forest);
    }
}
