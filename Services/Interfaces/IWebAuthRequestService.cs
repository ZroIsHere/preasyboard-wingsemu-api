using System;
using System.Collections.Generic;
using noswebapp_api.InternalEntities;
using noswebapp_api.RequestEntities;
using noswebapp_api.ResponseEntities;
using noswebapp.RequestEntities;

namespace noswebapp_api.Services.Interfaces;

public interface IWebAuthRequestService
{
    List<WebAuthRequest> GetChallenges();
    WebAuthRequest GetChallengeById(int id);

    WebAuthRequest AddChallenge();

    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<WebAuthRequest> GetAll();

    WebAuthRequest GetById(int id);

    String RandomString(int size, bool lowerCase);
}