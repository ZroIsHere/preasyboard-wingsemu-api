using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
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
    
    [Authorize]
    [HttpGet("GetBazaarItemById")]
    public BazaarItemResponse GetBazaarItemById(OnlyAnLongRequest Req)
    {
        return _container.GetService<IBazaarService>().GetBazaarItemById(new()
        {
            BazaarItemId = Req.Value
        }).Result;
    }
    
    [Authorize]
    [HttpPost("AddItemToBazaar")]
    public BazaarItemResponse AddItemToBazaar(AddItemToBazaarRequest Req)
    {
        return _container.GetService<IBazaarService>().AddItemToBazaar(new()
        {
            ChannelId = 1,
            OwnerName = Req.OwnerName,
            MaximumListedItems = Req.MaxListCount,
            BazaarItemDto = Req.BazaarItemDto
        }).Result;
    }
    
    [Authorize]
    [HttpPost("RemoveItemFromBazaar")]
    public BazaarItemResponse RemoveItemFromBazaar(RemoveItemFromBazaarRequest Req)
    {
        return _container.GetService<IBazaarService>().RemoveItemFromBazaar(new()
        {
            ChannelId = 1,
            RequesterCharacterId = Req.RequesterCharacterId,
            BazaarItemDto = Req.BazaarItemDto
        }).Result;
    }
    
    [Authorize]
    [HttpPost("ChangeItemPriceFromBazaar")]
    public BazaarItemResponse ChangeItemPriceFromBazaar(ChangeItemPriceFromBazaarRequest Req)
    {
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
    
    [Authorize]
    [HttpGet("GetItemsByCharacterIdFromBazaar")]
    public BazaarGetItemsByCharIdResponse GetItemsByCharacterIdFromBazaar(OnlyAnLongRequest Req)
    {
        return _container.GetService<IBazaarService>().GetItemsByCharacterIdFromBazaar(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
    
    [Authorize]
    [HttpPost("RemoveItemsByCharacterIdFromBazaar")]
    public BazaarRemoveItemsByCharIdResponse RemoveItemsByCharacterIdFromBazaar(OnlyAnLongRequest Req)
    {
        return _container.GetService<IBazaarService>().RemoveItemsByCharacterIdFromBazaar(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
    
    [Authorize]
    [HttpGet("SearchBazaarItems")]
    public BazaarSearchBazaarItemsResponse SearchBazaarItems(BazaarSearchContext Req)
    {
        return _container.GetService<IBazaarService>().SearchBazaarItems(new()
        {
            BazaarSearchContext = Req
        }).Result;
    }
    
    [Authorize]
    [HttpPost("BuyItemFromBazaar")]
    public BazaarItemResponse BuyItemFromBazaar(BazaarBuyItemRequest Req)
    {
        return _container.GetService<IBazaarService>().BuyItemFromBazaar(Req).Result;
    }
    
    [Authorize]
    [HttpPost("UnlistItemsFromBazaarWithVnum")]
    public UnlistItemFromBazaarResponse UnlistItemsFromBazaarWithVnumAsync(List<OnlyAnIntRequest> Req)
    {
        return _container.GetService<IBazaarService>().UnlistItemsFromBazaarWithVnumAsync(new()
        {
            Vnum = Req.Select(s => s.Value).ToList()
        }).Result;
    }
    
    [Authorize]
    [HttpPost("UnlistCharacterItemsFromBazaar")]
    public UnlistItemFromBazaarResponse UnlistCharacterItemsFromBazaarAsync(OnlyAnIntRequest Req)
    {
        return _container.GetService<IBazaarService>().UnlistCharacterItemsFromBazaarAsync(new()
        {
            Id = Req.Value
        }).Result;
    }
}