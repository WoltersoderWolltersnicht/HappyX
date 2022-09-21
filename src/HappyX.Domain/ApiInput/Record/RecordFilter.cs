using System.ComponentModel.DataAnnotations;

namespace HappyX.Domain.ApiInput.Record;

public class RecordFilter
{
    public IEnumerable<string> SlackIds { get; set; } = Enumerable.Empty<string>();
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}