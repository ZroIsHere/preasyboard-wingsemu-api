using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.RequestEntities;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;
using RemoveMailRequest = WingsAPI.Communication.Mail.RemoveMailRequest;

namespace noswebapp.Controllers;

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

    [HttpPost("CreateMail")]
    public CreateMailResponse CreateMailAsync([FromHeader] string AuthKey, CreateMailRequest req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailAsync(req).Result;
    }
    
    [HttpPost("CreateMailBatch")]
    public CreateMailBatchResponse CreateMailBatchAsync([FromHeader] string AuthKey, CreateMailBatchRequest req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailBatchAsync(req).Result;
    }
    
    [HttpGet("RemoveMail")]
    public BasicRpcResponse RemoveMailAsync([FromHeader] string AuthKey, RemoveMailRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().RemoveMailAsync(new()
        {
            CharacterId = Req.CharacterId,
            MailId = Req.CharacterId
        }).Result;
    }

    [HttpGet("GetMailsByCharacterId")]
    public GetMailsResponse GetMailsByCharacterId([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().GetMailsByCharacterId(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
}