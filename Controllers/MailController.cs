using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.Attributes;
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

    [Authorize]
    [HttpPost("CreateMail")]
    public CreateMailResponse CreateMailAsync(CreateMailRequest req)
    {
        return _container.GetService<IMailService>().CreateMailAsync(req).Result;
    }
    
    [Authorize]
    [HttpPost("CreateMailBatch")]
    public CreateMailBatchResponse CreateMailBatchAsync(CreateMailBatchRequest req)
    {
        return _container.GetService<IMailService>().CreateMailBatchAsync(req).Result;
    }
    
    [Authorize]
    [HttpGet("RemoveMail")]
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