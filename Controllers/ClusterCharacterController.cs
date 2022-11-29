using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication.Bazaar;
using WingsAPI.Communication.Player;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClusterCharacterController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<ClusterCharacterController> _logger;

    public ClusterCharacterController(ILogger<ClusterCharacterController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("GetCharacterById")]
    public ClusterCharacterResponse GetCharacterById(OnlyAnLongRequest Req)
    {
        return _container.GetService<IClusterCharacterService>().GetCharacterById(new()
        {
            CharacterId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetCharacterByName")]
    public ClusterCharacterResponse GetCharacterByName(OnlyAnStringRequest Req)
    {
        return _container.GetService<IClusterCharacterService>().GetCharacterByName(new()
        {
            CharacterName = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetCharacterById")]
    public ClusterCharacterGetMultipleResponse GetCharactersByChannelId() => 
        _container.GetService<IClusterCharacterService>().GetCharactersByChannelId(new()).Result;

    [Authorize]
    [HttpGet("GetCharacterById")]
    public ClusterCharacterGetSortedResponse GetCharactersSortedByChannel() => 
        _container.GetService<IClusterCharacterService>().GetCharactersSortedByChannel(new()).Result;

    [Authorize]
    [HttpGet("GetCharacterById")]
    public ClusterCharacterGetMultipleResponse GetAllCharacters() => 
        _container.GetService<IClusterCharacterService>().GetAllCharacters(new()).Result;
}
