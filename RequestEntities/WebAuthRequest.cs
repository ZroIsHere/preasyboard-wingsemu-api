
namespace noswebapp.RequestEntities;
public class WebAuthRequest
{
    private int _id;
    private string _challenge;
    private string _timestamp;
    private WebAuthRequest req;

    public int id { get;  set; }
    public string challenge { get;  set; }
    public string timestamp { get;  set; }
    public WebAuthRequest()
    {
    }

    public WebAuthRequest(int id, string challenge, string timestamp)
    {
        _id = id;
        _challenge = challenge;
        _timestamp = timestamp;
    }

    public WebAuthRequest(WebAuthRequest req)
    {
        this.req = req;
    }
}