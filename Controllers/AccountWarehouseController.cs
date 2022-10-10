using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.RequestEntities;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Communication.DbServer.WarehouseService;
using WingsAPI.Data.Account;

namespace noswebapp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountWarehouseController : Controller
{
    
    private readonly IServiceProvider _container;
    
    private readonly ILogger<AccountWarehouseController> _logger;

    public AccountWarehouseController(ILogger<AccountWarehouseController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }
    
    [HttpGet("GetItems")]
    public AccountWarehouseGetItemsResponse GetItems([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        return _container.GetService<IAccountWarehouseService>().GetItems(new()
        {
            AccountId = Req.Value
        }).Result;
    }
    
    [HttpGet("GetItem")]
    public AccountWarehouseGetItemResponse GetItem([FromHeader] string AuthKey, AccountWarehouseGetItemRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        return _container.GetService<IAccountWarehouseService>().GetItem(Req).Result;
    }
    
    [HttpGet("AddItem")]
    public AccountWarehouseAddItemResponse AddItem([FromHeader] string AuthKey, AccountWarehouseItemDto Dto)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        return _container.GetService<IAccountWarehouseService>().AddItem(new()
        {
            Item = Dto
        }).Result;
    }
    
    [HttpPost("WithdrawItem")]
    public AccountWarehouseWithdrawItemResponse WithdrawItem([FromHeader] string AuthKey, AccountWarehouseWithdrawItemRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        return _container.GetService<IAccountWarehouseService>().WithdrawItem(Req).Result;
    }
    
    [HttpPost("MoveItem")]
    public AccountWarehouseMoveItemResponse MoveItem([FromHeader] string AuthKey, AccountWarehouseMoveItemRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        
        return _container.GetService<IAccountWarehouseService>().MoveItem(Req).Result;
    }
}