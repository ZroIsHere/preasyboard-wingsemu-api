namespace noswebapp_api.RequestEntities;

public class CreateAccountRequest
{
    public string AcccountName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}