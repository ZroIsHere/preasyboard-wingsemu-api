using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;

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
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailAsync(req).Result;
    }
    
    [HttpPost("CreateMailBatch")]
    public CreateMailBatchResponse CreateMailBatchAsync([FromHeader] string AuthKey, CreateMailBatchRequest req)
    {
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailBatchAsync(req).Result;
    }
    
    [HttpGet("RemoveMail")]
    public BasicRpcResponse RemoveMailAsync([FromHeader] string AuthKey, long characterid, long mailid)
    {
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().RemoveMailAsync(new()
        {
            CharacterId = characterid,
            MailId = mailid
        }).Result;
    }

    [HttpGet("GetMailsByCharacterId")]
    public GetMailsResponse GetMailsByCharacterId([FromHeader] string AuthKey, long characterid)
    {
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().GetMailsByCharacterId(new()
        {
            CharacterId = characterid
        }).Result;
    }
}