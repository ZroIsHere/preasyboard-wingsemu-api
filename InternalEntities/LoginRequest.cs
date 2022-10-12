namespace noswebapp_api.InternalEntities;

using System.Text.Json.Serialization;

public class LoginRequest
{
    public int Id { get; set; }
    public string Challenge { get; set; }
    public long TimeStamp { get; set; }
}