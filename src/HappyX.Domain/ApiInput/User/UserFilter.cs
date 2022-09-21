using System.ComponentModel.DataAnnotations;

namespace HappyX.Domain.ApiInput.User;

public class UserFilter
{
    [Required]
    public string SlackId { get; set; }
}