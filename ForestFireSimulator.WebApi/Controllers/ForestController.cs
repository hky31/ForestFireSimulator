using ForestFireSimulator.Domain.Entities;
using ForestFireSimulator.Domain.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ForestController : ControllerBase
{
    private static Forest _forest = new Forest(1);
    private static FireSimulationService _simulator = new FireSimulationService(_forest);

    [HttpPost("init")]
    public IActionResult Initialize([FromBody] int size)
    {
        _forest = new Forest(size);
        _simulator = new FireSimulationService(_forest);
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

    [HttpPost("save")]
    public IActionResult Save()
    {
        _simulator.saveForest();
        return Ok(_forest);
    }

    [HttpGet("load")]
    public IActionResult Load()
    {
        _simulator.loadForest();
        return Ok(_forest);
    }
}
