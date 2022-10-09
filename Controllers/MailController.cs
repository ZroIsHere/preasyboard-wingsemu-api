﻿using System;
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
    public CreateMailResponse CreateMailAsync(CreateMailRequest req, string AuthKey)
    {
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailAsync(req).Result;
    }
    
    [HttpPost("CreateMailBatch")]
    public CreateMailBatchResponse CreateMailBatchAsync(CreateMailBatchRequest req, string AuthKey)
    {
        if (AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IMailService>().CreateMailBatchAsync(req).Result;
    }
    
    [HttpGet("RemoveMail")]
    public BasicRpcResponse RemoveMailAsync(long characterid, long mailid, string AuthKey)
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
    public GetMailsResponse GetMailsByCharacterId(long characterid, string AuthKey)
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