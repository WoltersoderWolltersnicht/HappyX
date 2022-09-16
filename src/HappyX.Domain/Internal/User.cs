namespace HappyX.Domain.Internal;

public class User : BaseEntity
{
    public User(){}
    public User(string username, string slackId, bool subscribed)
    {
        Username = username;
        SlackId = slackId;
        Subscribed = subscribed;
    }

    public string Username { get; set; }
    public string SlackId { get; set; }
    public bool Subscribed { get; set; }
    public IEnumerable<Record> Records { get; set; }
}