namespace HappyX.Domain.ApiInput.User;

public class UserInput
{
    public string SlackId { get; set; }
    public string UserName { get; set; }
    public bool Subscribed { get; set; }
    
    public UserInput(string slackId, string userName, bool subscribed)
    {
        SlackId = slackId;
        UserName = userName;
        Subscribed = subscribed;
    }
}