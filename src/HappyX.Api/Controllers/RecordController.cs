using HappyX.Domain.ApiInput.Record;
using HappyX.Domain.ApiOutput.Record;
using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace HappyX.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RecordController : ControllerBase
{
    private readonly WorkUnit _workUnit;

    public RecordController(WorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    [HttpGet("health")]
    public string Health()
    {
        return "ok";
    }

    [HttpPost("AddRecord", Name = "AddRecord")]
    public async Task<IActionResult> AddRecord([FromBody] RecordInput recordInput)
    {
        var users = await _workUnit.UserRepository.Get(user => user.SlackId == recordInput.SlackId);
        var user = users.FirstOrDefault();
        if (user == null) return BadRequest("User not found");

        var moods = await _workUnit.MoodRepository.Get(mood => mood.Name == recordInput.MoodName);
        var mood = moods.FirstOrDefault();
        if (mood == null) return BadRequest("Mood not found");
        
        Record record = new(user.Id, mood.Id);
        var savedRecord = _workUnit.RecordRepository.Insert(record);
        await _workUnit.SaveAsync();
        return Ok();
    }

    [HttpPost("GetRecord", Name = "GetRecord")]
    public async Task<IActionResult> GetRecords([FromBody] RecordFilter recordFilter)
    {
        IEnumerable<Record> records = await _workUnit.RecordRepository.Get(record =>
            (!recordFilter.SlackIds.Any() || recordFilter.SlackIds.Contains(record.User.SlackId)) &&
            (recordFilter.StartDate == null || recordFilter.StartDate >= record.CreationDate) &&
            (recordFilter.EndDate == null  || recordFilter.EndDate <= record.CreationDate),
            null,
            "User,Mood");

        List<Record> recordsList = records.ToList();
        if (!recordsList.Any()) return NotFound();
        
        return Ok(recordsList.Select(r => new RecordOutput(r.User?.SlackId, r.CreationDate, r.Mood.Name)));
    }
}