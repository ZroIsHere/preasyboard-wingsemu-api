using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.Attributes;
using WingsAPI.Communication;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : Controller
{
    
    private readonly IServiceProvider _container;
    
    private readonly ILogger<CharacterController> _logger;

    public CharacterController(ILogger<CharacterController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }
    
    [Authorize]
    [HttpPost("SaveCharacters")]
    public BasicRpcResponse SaveCharacters()
    {
        return null;
    }

    [Authorize]
    [HttpPost("SaveCharacter")]
    public BasicRpcResponse SaveCharacter()
    {
        return null;
    }

    [Authorize]
    [HttpPost("CreateCharacter")]
    public BasicRpcResponse CreateCharacter()
    {
        return null;
    }

    [Authorize]
    [HttpGet("GetCharacters")]
    public BasicRpcResponse GetCharaters()
    {
        return null;
    }

    [Authorize]
    [HttpGet("GetCharacterBySlot")]
    public BasicRpcResponse GetCharacterBySlot()
    {
        return null;
    }

    [Authorize]
    [HttpGet("GetCharacterById")]
    public BasicRpcResponse GetCharacterById()
    {
        return null;
    }

    [Authorize]
    [HttpGet("SaveCharacters")]
    public BasicRpcResponse GetCharacterByName()
    {
        return null;
    }

    [Authorize]
    [HttpPost("FlushCharacterSaves")]
    public BasicRpcResponse FlushCharacterSaves()
    {
        return null;
    }

    [Authorize]
    public BasicRpcResponse DeleteCharacter()
    {
        return null;
    }

    [Authorize]
    public BasicRpcResponse ForceRemoveCharacterFromCache()
    {
        return null;
    }

    [Authorize]
    public BasicRpcResponse GetTopPoints()
    {
        return null;
    }

    [Authorize]
    public BasicRpcResponse GetTopReputation()
    {
        return null;
    }

    [Authorize]
    public BasicRpcResponse RefreshRanking()
    {
        return null;
    }
}