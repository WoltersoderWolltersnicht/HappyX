namespace HappyX.Domain.ApiOutput.User;

public class UserOutput
{
    
    public string SlackId { get; set; }
    public string Username { get; set; }
    
    public UserOutput(string username, string slackId)
    {
        Username = username;
        SlackId = slackId;
    }
}