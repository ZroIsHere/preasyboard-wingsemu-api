using System.Collections.Generic;
using System.Threading.Tasks;
using WingsAPI.Communication.Families.Warehouse;
using WingsAPI.Communication;
using WingsAPI.Data.Families;
using Microsoft.AspNetCore.Mvc;
using PreasyBoard.Api.Attributes;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FamilyWarehouseController : Controller
{

    private readonly IServiceProvider _container;

    private readonly ILogger<FamilyWarehouseController> _logger;

    public FamilyWarehouseController(ILogger<FamilyWarehouseController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("GetLogs")]
    public FamilyWarehouseGetLogsResponse GetLogs(FamilyWarehouseGetLogsRequest Req) => 
        _container.GetService<IFamilyWarehouseService>().GetLogs(Req).Result;

    [Authorize]
    [HttpGet("GetItems")]
    public FamilyWarehouseGetItemsResponse GetItems(FamilyWarehouseGetItemsRequest Req) =>
        _container.GetService<IFamilyWarehouseService>().GetItems(Req).Result;

    [Authorize]
    [HttpGet("GetItem")]
    public FamilyWarehouseGetItemResponse GetItem(FamilyWarehouseGetItemRequest Req) =>
        _container.GetService<IFamilyWarehouseService>().GetItem(Req).Result;

    [Authorize]
    [HttpPost("AddItem")]
    public FamilyWarehouseAddItemResponse AddItem(FamilyWarehouseAddItemRequest Req) =>
        _container.GetService<IFamilyWarehouseService>().AddItem(Req).Result;

    [Authorize]
    [HttpPost("WithdrawItem")]
    public FamilyWarehouseWithdrawItemResponse WithdrawItem(FamilyWarehouseWithdrawItemRequest Req) =>
        _container.GetService<IFamilyWarehouseService>().WithdrawItem(Req).Result;

    [Authorize]
    [HttpPost("MoveItem")]
    public FamilyWarehouseMoveItemResponse MoveItem(FamilyWarehouseMoveItemRequest Req) =>
        _container.GetService<IFamilyWarehouseService>().MoveItem(Req).Result;
}
