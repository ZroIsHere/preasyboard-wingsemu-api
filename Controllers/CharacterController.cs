using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using noswebapp_api;

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
    public void SaveCharacters([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void SaveCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void CreateCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetCharaters([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetCharacterBySlot([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetCharacterById([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetCharacterByName([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void FlushCharacterSaves([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void DeleteCharacter([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void ForceRemoveCharacterFromCache([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetTopPoints([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void GetTopReputation([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }

    public void RefreshRanking([FromHeader] string AuthKey)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
    }
}