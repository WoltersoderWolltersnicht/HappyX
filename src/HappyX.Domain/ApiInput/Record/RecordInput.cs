namespace HappyX.Domain.ApiInput.Record;

public class RecordInput
{
    public RecordInput(string slackId, string moodName)
    {
        SlackId = slackId;
        MoodName = moodName;
    }

    public string SlackId { get; set; }
    public string MoodName { get; set; }
}