namespace HappyX.Domain.ApiOutput.Record;

public class RecordOutput
{
    public RecordOutput(string slackId, DateTime date, string mood)
    {
        SlackId = slackId;
        CreationDate = date.ToString("yyyy-MM-dd");
        Mood = mood;
    }
    
    public string SlackId { get; set; }
    public string CreationDate { get; set; }
    public string Mood { get; set; }
}