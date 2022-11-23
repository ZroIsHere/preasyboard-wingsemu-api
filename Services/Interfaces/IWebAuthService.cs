using System;
using System.Collections.Generic;

using PreasyBoard.Api.RequestEntities;
using PreasyBoard.Api.ResponseEntities;
using noswebapp.RequestEntities;

namespace PreasyBoard.Api.Services.Interfaces;

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