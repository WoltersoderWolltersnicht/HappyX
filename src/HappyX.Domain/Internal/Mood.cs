namespace HappyX.Domain.Internal;

public class Mood : BaseEntity
{
    public Mood(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; init; }
    public IEnumerable<Record> Records { get; set; }
}