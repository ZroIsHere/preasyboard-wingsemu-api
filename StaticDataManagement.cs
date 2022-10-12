using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using noswebapp_api.InternalEntities;

namespace noswebapp_api;

public class StaticDataManagement
{
    //TODO: DELETE ALL REQUEST THAT HAVE A TIMESTAMP DELTA BIGGER THAN SOME SECONDS, BUT I WILL IMPLEMENT LATER
    public static Dictionary<int, LoginRequest> ChallengeAttempts = new();
    public static Dictionary<string, DateTime> ValidatedTokens = new();

    public static Task RemoveTokensLoop()
    {
        while (true)
        {
            Task.Delay(TimeSpan.FromMinutes(5));

            foreach ((string key, DateTime value) in ValidatedTokens.Where(s => s.Value < DateTime.UtcNow))
            {
                ValidatedTokens.Remove(key);
            }
        }
    }

    public static Task RemoveAttemptsLoop()
    {
        int removeafterseconds = 2;
        while (true)
        {
            Task.Delay(TimeSpan.FromSeconds(removeafterseconds));
            foreach ((int key, LoginRequest value) in ChallengeAttempts.Where(s => new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(s.Value.TimeStamp + removeafterseconds) < DateTime.Now))
            {
                ChallengeAttempts.Remove(key);
            }
        }
    }
}