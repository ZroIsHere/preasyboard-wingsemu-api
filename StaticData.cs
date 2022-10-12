using System.Collections.Generic;
using noswebapp_api.InternalEntities;

namespace noswebapp_api;

public class StaticData
{
    //TODO: DELETE ALL REQUEST THAT HAVE A TIMESTAMP DELTA BIGGER THAN SOME SECONDS, BUT I WILL IMPLEMENT LATER
    public static Dictionary<int, LoginRequest> ChallengeAttempts = new Dictionary<int, LoginRequest>();
}