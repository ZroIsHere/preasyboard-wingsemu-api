using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;

namespace PreasyBoard.Api.Controllers;

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
    public CreateNoteResponse CreateNoteAsync(CreateNoteRequest req) =>
        _container.GetService<INoteService>().CreateNoteAsync(req).Result;

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