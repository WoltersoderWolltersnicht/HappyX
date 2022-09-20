using HappyX.Domain.ApiInput.User;
using HappyX.Domain.ApiOutput.User;
using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
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
    
    [HttpPost("AddUser", Name = "AddUser")]
    public async Task<IActionResult> AddUser([FromBody] UserInput userInput)
    {
        var users = await _workUnit.UserRepository.Get(user => user.SlackId == userInput.SlackId);
        var user = users.FirstOrDefault();

        if (user is null){
            user = new User(userInput.UserName, userInput.SlackId, true);
            _workUnit.UserRepository.Insert(user);
        }else{
            user.Subscribed = true;
            _workUnit.UserRepository.Update(user);
        };
        
        await _workUnit.SaveAsync();
        return Ok();
    }

    [HttpDelete("DeleteUser", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] UserFilter userFilter)
    {
        var users = await _workUnit.UserRepository.Get(user => user.SlackId == userFilter.SlackId);
        var user = users.FirstOrDefault();

        if (user is null) return BadRequest();

        user.Subscribed = false;
        _workUnit.UserRepository.Update(user);
        
        await _workUnit.SaveAsync();
        return Ok();
    }
    
    [HttpGet("GetUsers", Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        IEnumerable<User> users = await _workUnit.UserRepository.Get(u => u.Subscribed == true);
        IEnumerable<UserOutput> userOutputs = users.Select(u => new UserOutput(u.Username, u.SlackId));
        return Ok(userOutputs);
    }
}