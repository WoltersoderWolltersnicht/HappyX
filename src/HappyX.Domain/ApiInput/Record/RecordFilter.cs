namespace HappyX.Domain.ApiInput.Record;

public class RecordFilter
{
    public IEnumerable<string> SlackIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}