namespace HappyX.Domain.ApiOutput.Record;

public class RecordOutput
{
    public RecordOutput(string slackId, DateTime date, string mood)
    {
        SlackId = slackId;
        Date = date;
        Mood = mood;
    }
    
    public string SlackId { get; set; }
    public DateTime Date { get; set; }
    public string Mood { get; set; }
}