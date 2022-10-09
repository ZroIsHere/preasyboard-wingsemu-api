using Microsoft.AspNetCore.Mvc;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController: Controller
{
    private readonly IServiceProvider _container;

    public NoteController(IServiceProvider container) => _container = container;
    
    private readonly ILogger<NoteController> _logger;

    public NoteController(ILogger<NoteController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [HttpPost("CreateNote")]
    public CreateNoteResponse CreateNoteAsync(CreateNoteRequest req)
    {
        return _container.GetService<INoteService>().CreateNoteAsync(req).Result;
    }

    [HttpPost("RemoveNote")]
    public BasicRpcResponse RemoveNoteAsync(long noteid)
    {
        return _container.GetService<INoteService>().RemoveNoteAsync(noteid).Result;
    }

    [HttpPost("OpenNote")]
    public CreateNoteResponse OpenNoteAsync(long noteid)
    {
        return _container.GetService<INoteService>().OpenNoteAsync(noteid).Result;
    }
}