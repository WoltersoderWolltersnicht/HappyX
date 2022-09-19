using HappyX.Domain.ApiInput.User;
using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace HappyX.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly WorkUnit _workUnit;

    public UserController(WorkUnit workUnit)
    {
        _workUnit = workUnit;
    }
    
    [HttpPost("ActivateUser", Name = "ActivateUser")]
    public async Task<IActionResult> ActivateUser([FromBody] UserFilter userFilter)
    {
        var users = await _workUnit.UserRepository.Get(user => user.SlackId == userFilter.SlackId);
        var user = users.FirstOrDefault();

        if (user == null) return BadRequest();

        user.Subscribed = true;
        _workUnit.UserRepository.Update(user);
        
        await _workUnit.SaveAsync();
        return Ok();
    }

    [HttpPost("DisableUser", Name = "DisableUser")]
    public async Task<IActionResult> DisableUser([FromBody] UserFilter userFilter)
    {
        var users = await _workUnit.UserRepository.Get(user => user.SlackId == userFilter.SlackId);
        var user = users.FirstOrDefault();

        if (user == null) return BadRequest();

        user.Subscribed = false;
        _workUnit.UserRepository.Update(user);

        await _workUnit.SaveAsync();
        return Ok();
    }
    
    [HttpPost("AddUsers", Name = "AddUsers")]
    public async Task<IActionResult> AddUsers([FromBody] IEnumerable<UserInput> userInputs)
    {
        List<User> users = userInputs.Select(userInput => new User(userInput.UserName, userInput.SlackId, userInput.Subscribed)).ToList();
        _workUnit.UserRepository.InsertMany(users);
        
        await _workUnit.SaveAsync();
        return Ok();
    }
}