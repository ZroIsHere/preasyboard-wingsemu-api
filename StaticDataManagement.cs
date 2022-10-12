using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using noswebapp_api.InternalEntities;
using noswebapp.RequestEntities;

namespace noswebapp_api;

public class StaticDataManagement
{
    //TODO: DELETE ALL REQUEST THAT HAVE A TIMESTAMP DELTA BIGGER THAN SOME SECONDS, BUT I WILL IMPLEMENT LATER
    public static Dictionary<int, WebAuthRequest> ChallengeAttempts = new();
    public static Dictionary<string, DateTime> ValidatedTokens = new();

    public static void RemoveTokensLoop()
    {
        foreach ((string key, DateTime value) in ValidatedTokens.Where(s => s.Value < DateTime.UtcNow))
        {
            ValidatedTokens.Remove(key);
        }
    }

    public static void RemoveAttemptsLoop()
    {
        foreach ((int key, WebAuthRequest value) in ChallengeAttempts.Where(s => new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(s.Value.TimeStamp + 2) <= DateTime.Now))
        {
            ChallengeAttempts.Remove(key);
        }
    }
}