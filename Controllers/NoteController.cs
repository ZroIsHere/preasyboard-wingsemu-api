using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.Attributes;
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

    [Authorize]
    [HttpPost("CreateNote")]
    public CreateNoteResponse CreateNoteAsync(CreateNoteRequest req)
    {
        return _container.GetService<INoteService>().CreateNoteAsync(req).Result;
    }

    [Authorize]
    [HttpPost("RemoveNote")]
    public BasicRpcResponse RemoveNoteAsync(OnlyAnLongRequest Req)
    {
        return _container.GetService<INoteService>().RemoveNoteAsync(new()
        {
            NoteId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("OpenNote")]
    public BasicRpcResponse OpenNoteAsync(OnlyAnLongRequest Req)
    {
        return _container.GetService<INoteService>().OpenNoteAsync(new()
        {
            NoteId = Req.Value
        }).Result;
    }
}