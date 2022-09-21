using System.ComponentModel.DataAnnotations;

namespace HappyX.Domain.ApiInput.Record;

public class RecordInput
{
    [Required]
    public string SlackId { get; set; }
    [Required]
    public string MoodName { get; set; }
    
    public RecordInput(string slackId, string moodName)
    {
        SlackId = slackId;
        MoodName = moodName;
    }
}