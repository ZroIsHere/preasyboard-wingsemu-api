using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication.Bazaar;
using WingsAPI.Communication.Sessions;
using WingsAPI.Communication.Sessions.Response;
using WingsEmu.Game.RespawnReturn.Event;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SessionController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<SessionController> _logger;

    public SessionController(ILogger<SessionController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpPost("DisconnectById")]
    public SessionResponse DisconnectById(OnlyAnLongRequest Req) => Disconnect(GetSessionByAccountId(Req));

    [Authorize]
    [HttpPost("DisconnectByName")]
    public SessionResponse DisconnectByName(OnlyAnStringRequest Req) => Disconnect(GetSessionByAccountName(Req));

    [Authorize]
    [HttpGet("GetSessionByAccountId")]
    public SessionResponse GetSessionByAccountId(OnlyAnLongRequest Req)
    {
        return _container.GetService<ISessionService>().GetSessionByAccountId(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetSessionByAccountName")]
    public SessionResponse GetSessionByAccountName(OnlyAnStringRequest Req)
    {
        return _container.GetService<ISessionService>().GetSessionByAccountName(new()
        {
            AccountName = Req.Value
        }).Result;
    }

    private SessionResponse Disconnect(SessionResponse Response)
    {
        return _container.GetService<ISessionService>().Disconnect(new()
        {
            AccountId = Response.Session.AccountId,
            EncryptionKey = Response.Session.EncryptionKey,
            ForceDisconnect = true
        }).Result;
    }
}
