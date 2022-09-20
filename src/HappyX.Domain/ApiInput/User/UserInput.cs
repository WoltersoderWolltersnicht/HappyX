namespace HappyX.Domain.ApiInput.User;

public class UserInput
{
    public string SlackId { get; set; }
    public string UserName { get; set; }
    
    public UserInput(string slackId, string userName)
    {
        SlackId = slackId;
        UserName = userName;
    }
}