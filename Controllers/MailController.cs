using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;
using RemoveMailRequest = WingsAPI.Communication.Mail.RemoveMailRequest;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MailController : Controller
{
    private readonly IServiceProvider _container;
    
    private readonly ILogger<MailController> _logger;

    public MailController(ILogger<MailController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpPost("CreateMail")]
    public CreateMailResponse CreateMailAsync(CreateMailRequest req) =>
        _container.GetService<IMailService>().CreateMailAsync(req).Result;

    [Authorize]
    [HttpPost("CreateMailBatch")]
    public CreateMailBatchResponse CreateMailBatchAsync(CreateMailBatchRequest req) =>
        _container.GetService<IMailService>().CreateMailBatchAsync(req).Result;

    [Authorize]
    [HttpPost("RemoveMail")]
    public BasicRpcResponse RemoveMailAsync(RemoveMailRequest Req)
    {
        return _container.GetService<IMailService>().RemoveMailAsync(new()
        {
            CharacterId = Req.CharacterId,
            MailId = Req.CharacterId
        }).Result;
    }

    [Authorize]
    [HttpGet("GetMailsByCharacterId")]
    public GetMailsResponse GetMailsByCharacterId(OnlyAnLongRequest Req)
    {
        return _container.GetService<IMailService>().GetMailsByCharacterId(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
}