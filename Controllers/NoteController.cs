using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.RequestEntities;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController: Controller
{
    private readonly IServiceProvider _container;
    
    private readonly ILogger<NoteController> _logger;

    public NoteController(ILogger<NoteController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [HttpPost("CreateNote")]
    public CreateNoteResponse CreateNoteAsync([FromHeader] string AuthKey, CreateNoteRequest req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<INoteService>().CreateNoteAsync(req).Result;
    }

    [HttpPost("RemoveNote")]
    public BasicRpcResponse RemoveNoteAsync([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<INoteService>().RemoveNoteAsync(new()
        {
            NoteId = Req.Value
        }).Result;
    }

    [HttpPost("OpenNote")]
    public BasicRpcResponse OpenNoteAsync([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<INoteService>().OpenNoteAsync(new()
        {
            NoteId = Req.Value
        }).Result;
    }
}