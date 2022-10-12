using noswebapp_api.InternalEntities;

namespace noswebapp_api.ResponseEntities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Challenge { get; set; }
    public long Timestamp { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(LoginRequest loginRequest, string token)
    {
        Id = loginRequest.Id;
        Challenge = loginRequest.Challenge;
        Timestamp = loginRequest.TimeStamp;
        Token = token;
    }
}