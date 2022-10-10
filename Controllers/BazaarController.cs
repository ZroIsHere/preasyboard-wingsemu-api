using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.RequestEntities;
using WingsAPI.Communication.Bazaar;
using WingsEmu.Game.Algorithm;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class BazaarController : Controller
{
    private readonly IServiceProvider _container;
    
    private readonly ILogger<BazaarController> _logger;

    public BazaarController(ILogger<BazaarController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }
    
    [HttpGet("GetBazaarItemById")]
    public BazaarItemResponse GetBazaarItemById([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().GetBazaarItemById(new()
        {
            BazaarItemId = Req.Value
        }).Result;
    }
    
    [HttpPost("AddItemToBazaar")]
    public BazaarItemResponse AddItemToBazaar([FromHeader] string AuthKey, AddItemToBazaarRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().AddItemToBazaar(new()
        {
            ChannelId = 1,
            OwnerName = Req.OwnerName,
            MaximumListedItems = Req.MaxListCount,
            BazaarItemDto = Req.BazaarItemDto
        }).Result;
    }
    
    [HttpPost("RemoveItemFromBazaar")]
    public BazaarItemResponse RemoveItemFromBazaar([FromHeader] string AuthKey, RemoveItemFromBazaarRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().RemoveItemFromBazaar(new()
        {
            ChannelId = 1,
            RequesterCharacterId = Req.RequesterCharacterId,
            BazaarItemDto = Req.BazaarItemDto
        }).Result;
    }
    
    [HttpPost("ChangeItemPriceFromBazaar")]
    public BazaarItemResponse ChangeItemPriceFromBazaar([FromHeader] string AuthKey, ChangeItemPriceFromBazaarRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().ChangeItemPriceFromBazaar(new()
        {
            ChannelId = 1,
            BazaarItemDto = Req.BazaarItemDto,
            ChangerCharacterId = Req.ChangerCharacterId,
            NewPrice = Req.NewPrice,
            NewSaleFee = Req.NewSaleFee,
            SunkGold = Req.SunkGold
        }).Result;
    }
    
    [HttpGet("GetItemsByCharacterIdFromBazaar")]
    public BazaarGetItemsByCharIdResponse GetItemsByCharacterIdFromBazaar([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().GetItemsByCharacterIdFromBazaar(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
    
    [HttpPost("RemoveItemsByCharacterIdFromBazaar")]
    public BazaarRemoveItemsByCharIdResponse RemoveItemsByCharacterIdFromBazaar([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().RemoveItemsByCharacterIdFromBazaar(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
    
    [HttpPost("RemoveItemsByCharacterIdFromBazaar")]
    public BazaarSearchBazaarItemsResponse SearchBazaarItems([FromHeader] string AuthKey, BazaarSearchContext Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().SearchBazaarItems(new()
        {
            BazaarSearchContext = Req
        }).Result;
    }
    
    [HttpPost("BuyItemFromBazaar")]
    public BazaarItemResponse BuyItemFromBazaar([FromHeader] string AuthKey, BazaarBuyItemRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().BuyItemFromBazaar(Req).Result;
    }
    
    [HttpPost("UnlistItemsFromBazaarWithVnum")]
    public UnlistItemFromBazaarResponse UnlistItemsFromBazaarWithVnumAsync([FromHeader] string AuthKey, List<OnlyAnIntRequest> Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().UnlistItemsFromBazaarWithVnumAsync(new()
        {
            Vnum = Req.Select(s => s.Value).ToList()
        }).Result;
    }
    
    [HttpPost("UnlistCharacterItemsFromBazaar")]
    public UnlistItemFromBazaarResponse UnlistCharacterItemsFromBazaarAsync([FromHeader] string AuthKey, OnlyAnIntRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }

        return _container.GetService<IBazaarService>().UnlistCharacterItemsFromBazaarAsync(new()
        {
            Id = Req.Value
        }).Result;
    }
}