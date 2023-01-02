using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using WingsAPI.Communication;
using WingsAPI.Communication.DbServer.CharacterService;
using WingsAPI.Data.Character;

namespace PreasyBoard.Api.Controllers;

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
    public DbServerSaveCharactersResponse SaveCharacters(DbServerSaveCharactersRequest Req) =>
        _container.GetService<ICharacterService>().SaveCharacters(Req).Result;

    [Authorize]
    [HttpPost("SaveCharacter")]
    public DbServerSaveCharacterResponse SaveCharacter(DbServerSaveCharacterRequest Req) =>
        _container.GetService<ICharacterService>().SaveCharacter(Req).Result;

    [Authorize]
    [HttpPost("CreateCharacter")]
    public DbServerSaveCharacterResponse CreateCharacter(DbServerSaveCharacterRequest Req) =>
        _container.GetService<ICharacterService>().CreateCharacter(Req).Result;


    [HttpGet("GetCharacters")]
    public DbServerGetCharactersResponse GetCharaters(OnlyAnLongRequest Req)
    {
        return _container.GetService<ICharacterService>().GetCharacters(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetCharacterBySlot")]
    public DbServerGetCharacterResponse GetCharacterBySlot(DbServerGetCharacterFromSlotRequest Req) =>
         _container.GetService<ICharacterService>().GetCharacterBySlot(Req).Result;

    [Authorize]
    [HttpGet("GetCharacterById")]
    public DbServerGetCharacterResponse GetCharacterById(DbServerGetCharacterByIdRequest Req) =>
        _container.GetService<ICharacterService>().GetCharacterById(Req).Result;

    [Authorize]
    [HttpGet("GetCharacterByName")]
    public DbServerGetCharacterResponse GetCharacterByName(OnlyAnStringRequest Req)
    {
        return _container.GetService<ICharacterService>().GetCharacterByName(new()
        {
            CharacterName = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("FlushCharacterSaves")]
    public DbServerFlushCharacterSavesResponse FlushCharacterSaves() =>
        _container.GetService<ICharacterService>().FlushCharacterSaves(new()).Result;

    [Authorize]
    [HttpPost("DeleteCharacter")]
    public DbServerDeleteCharacterResponse DeleteCharacter(CharacterDTO dto)
    {
        return _container.GetService<ICharacterService>().DeleteCharacter(new()
        {
            CharacterDto = dto
        }).Result;
    }

    [Authorize]
    [HttpPost("ForceRemoveCharacterFromCache")]
    public DbServerGetCharacterResponse ForceRemoveCharacterFromCache(OnlyAnStringRequest Req)
    {
        return _container.GetService<ICharacterService>().ForceRemoveCharacterFromCache(new()
        {
            CharacterName = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetTopPoints")]
    public CharacterGetTopResponse GetTopPoints() =>
        _container.GetService<ICharacterService>().GetTopPoints(new()).Result;

    [Authorize]
    [HttpGet("GetTopReputation")]
    public CharacterGetTopResponse GetTopReputation() =>
        _container.GetService<ICharacterService>().GetTopReputation(new()).Result;

    [Authorize]
    [HttpGet("GetTopCompliment")]
    public CharacterGetTopResponse GetTopCompliment() =>
        _container.GetService<ICharacterService>().GetTopCompliment(new()).Result;
    
    [Authorize]
    [HttpGet("RefreshRanking")]
    public CharacterRefreshRankingResponse RefreshRanking() =>
        _container.GetService<ICharacterService>().RefreshRanking(new()).Result;
}