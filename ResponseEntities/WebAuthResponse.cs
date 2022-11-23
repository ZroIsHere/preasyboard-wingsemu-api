
using noswebapp.RequestEntities;

namespace PreasyBoard.Api.ResponseEntities;

public class WebAuthResponse
{
    public int Id { get; set; }
    public string Challenge { get; set; }
    public long Timestamp { get; set; }
    public string Token { get; set; }


    public WebAuthResponse(WebAuthRequest loginRequest, string token)
    {
        Id = loginRequest.Id;
        Challenge = loginRequest.Challenge;
        Timestamp = loginRequest.TimeStamp;
        Token = token;
    }
}