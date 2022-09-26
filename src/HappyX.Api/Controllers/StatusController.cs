using HappyX.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using HappyX.Domain.ApiInput.Status;

namespace HappyX.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private readonly WorkUnit _workUnit;

    public StatusController(WorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    [HttpGet("Health")]
    public string Health()
    {
        return "ok";
    }

    [HttpPost("CheckToken", Name = "CheckToken")]
    public async Task<IActionResult> CheckToken([FromBody] StatusInput statusInput)
    {    
        if (statusInput.Token is "ALX"){
            return Ok();
        }else{
            return Unauthorized();
        }
    }
}