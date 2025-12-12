namespace FileStack.Application.APIResponses;

public class LoginResponse
{
    public  string Token { get; set; }= string.Empty;
    public List<string> Roles { get; set; }=new List<string>();
    public DateTime ExpiresAt { get; set; }= DateTime.MinValue;
    public string Message { get; set; } = string.Empty;
    


}
