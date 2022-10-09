namespace noswebapp_api;

public class NosWebAppEnvVariables
{
    public static string AuthKey
    {
        get
        {
            return Environment.GetEnvironmentVariable("API_AUTH_KEY") ?? "skSdhgASdS";
        }
    }
}