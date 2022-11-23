using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Communication.DbServer.WarehouseService;
using WingsAPI.Data.Account;

namespace PreasyBoard.Api.Controllers;

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
    
    [Authorize]
    [HttpGet("GetItems")]
    public AccountWarehouseGetItemsResponse GetItems(OnlyAnLongRequest Req)
    {
        return _container.GetService<IAccountWarehouseService>().GetItems(new()
        {
            AccountId = Req.Value
        }).Result;
    }
    
    [Authorize]
    [HttpGet("GetItem")]
    public AccountWarehouseGetItemResponse GetItem(AccountWarehouseGetItemRequest Req) => 
        _container.GetService<IAccountWarehouseService>().GetItem(Req).Result;

    [Authorize]
    [HttpPost("AddItem")]
    public AccountWarehouseAddItemResponse AddItem(AccountWarehouseItemDto Dto)
    {
        return _container.GetService<IAccountWarehouseService>().AddItem(new()
        {
            Item = Dto
        }).Result;
    }
    
    [Authorize]
    [HttpPost("WithdrawItem")]
    public AccountWarehouseWithdrawItemResponse WithdrawItem(AccountWarehouseWithdrawItemRequest Req) =>
        _container.GetService<IAccountWarehouseService>().WithdrawItem(Req).Result;

    [Authorize]
    [HttpPost("MoveItem")]
    public AccountWarehouseMoveItemResponse MoveItem(AccountWarehouseMoveItemRequest Req) =>
        _container.GetService<IAccountWarehouseService>().MoveItem(Req).Result;
}