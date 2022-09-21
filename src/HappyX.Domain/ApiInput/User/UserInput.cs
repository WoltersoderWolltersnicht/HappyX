using System.ComponentModel.DataAnnotations;

namespace HappyX.Domain.ApiInput.User;

public class UserInput
{
    [Required]
    public string SlackId { get; set; }
    [Required]
    public string UserName { get; set; }
    
    public UserInput(string slackId, string userName)
    {
        SlackId = slackId;
        UserName = userName;
    }
}