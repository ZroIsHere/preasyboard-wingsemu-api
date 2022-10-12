using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using noswebapp_api.InternalEntities;

namespace noswebapp_api;

public class StaticDataManagement
{
    //TODO: DELETE ALL REQUEST THAT HAVE A TIMESTAMP DELTA BIGGER THAN SOME SECONDS, BUT I WILL IMPLEMENT LATER
    public static Dictionary<int, LoginRequest> ChallengeAttempts = new();
    public static Dictionary<string, DateTime> ValidatedTokens = new();

    public static Task RemoveAfter7Days()
    {
        while (true)
        {
            Task.Delay(TimeSpan.FromMinutes(5));

            foreach ((string key, DateTime value) in ValidatedTokens)
            {
                if (DateTime.UtcNow > value)
                {
                    ValidatedTokens.Remove(key);
                }
            }
        }
    }
}