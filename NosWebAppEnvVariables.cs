namespace noswebapp_api;

public class NosWebAppEnvVariables
{
    static string _key = "";
    public static string AuthKey
    {
        get
        {
            if (string.IsNullOrEmpty(_key))
            {
                _key = Environment.GetEnvironmentVariable("API_AUTH_KEY") ?? "skSdhgASdS";
            }
            return _key;
        }
    }
}