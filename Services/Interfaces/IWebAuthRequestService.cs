using System;
using System.Collections.Generic;
using noswebapp_api.InternalEntities;
using noswebapp_api.RequestEntities;
using noswebapp_api.ResponseEntities;

namespace noswebapp_api.Services.Interfaces;

public interface IWebAuthRequestService
{
    List<LoginRequest> GetChallenges();
    LoginRequest GetChallengeById(int id);

    LoginRequest AddChallenge();

    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<LoginRequest> GetAll();

    LoginRequest GetById(int id);

    String RandomString(int size, bool lowerCase);
}