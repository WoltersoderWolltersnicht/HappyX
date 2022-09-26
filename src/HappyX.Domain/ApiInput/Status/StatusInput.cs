namespace HappyX.Domain.ApiInput.Status;

public class StatusInput
{
    public string Token { get; set; }
    public StatusInput(string token)
    {
        Token = token;
    }
}