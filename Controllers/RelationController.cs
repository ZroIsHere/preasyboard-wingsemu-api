using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication;
using WingsAPI.Communication.Mail;
using WingsAPI.Communication.Relation;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RelationController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<RelationController> _logger;

    public RelationController(ILogger<RelationController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpPost("AddRelation")]
    public RelationAddResponse AddRelation(RelationAddRequest Req) =>
        _container.GetService<IRelationService>().AddRelationAsync(Req).Result;

    [Authorize]
    [HttpGet("GetRelationsById")]
    public RelationGetAllResponse GetRelationsByIdAsync(OnlyAnLongRequest Req)
    {
        return _container.GetService<IRelationService>().GetRelationsByIdAsync(new()
        {
            CharacterId = Req.Value
        }).Result;
    }

        [Authorize]
    [HttpPost("RemoveRelation")]
    public BasicRpcResponse RemoveRelationAsync(RelationRemoveRequest Req) =>
        _container.GetService<IRelationService>().RemoveRelationAsync(Req).Result;
}
