using System;
using System.Collections.Generic;

using noswebapp_api.RequestEntities;
using noswebapp_api.ResponseEntities;
using noswebapp.RequestEntities;

namespace noswebapp_api.Services.Interfaces;

public interface IWebAuthService
{
    List<WebAuthRequest> GetChallenges();
    WebAuthRequest GetChallengeById(int id);

    WebAuthRequest AddChallenge();

    WebAuthResponse Authenticate(AuthenticateRequest model);
    IEnumerable<WebAuthRequest> GetAll();

    WebAuthRequest GetById(int id);

    String RandomString(int size, bool lowerCase);
}