using System;

namespace PreasyBoard.Api.RequestEntities;

public class WebAuthRequest
{
    public WebAuthRequest(string _challenge)
    {
        Id = new Random().Next(1, 255);
        Challenge = _challenge;
        TimeStamp = DateTime.UtcNow.ToFileTime();
    }

    public int Id { get; set; }
    public string Challenge { get; set; }
    public long TimeStamp { get; set; }

}