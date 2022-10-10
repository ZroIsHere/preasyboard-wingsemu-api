using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using noswebapp_api;
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
    
    [HttpPost("SaveCharacters")]
    public BasicRpcResponse SaveCharacters([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse SaveCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse CreateCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetCharaters([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetCharacterBySlot([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetCharacterById([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetCharacterByName([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse FlushCharacterSaves([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse DeleteCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse ForceRemoveCharacterFromCache([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetTopPoints([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse GetTopReputation([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }

    public BasicRpcResponse RefreshRanking([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return null;
    }
}