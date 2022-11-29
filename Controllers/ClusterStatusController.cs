using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Communication.Services;
using WingsAPI.Communication.Services.Requests;
using WingsAPI.Communication.Services.Responses;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClusterStatusController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<ClusterStatusController> _logger;
    public ClusterStatusController(ILogger<ClusterStatusController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("GetAllServicesStatus")]
    public ServiceGetAllResponse GetAllServicesStatus() =>
        _container.GetService<IClusterStatusService>().GetAllServicesStatus(new()).Result;

    [Authorize]
    [HttpGet("GetServiceStatusByName")]
    public ServiceGetStatusByNameResponse GetServiceStatusByName(OnlyAnStringRequest Req) 
    {
        return _container.GetService<IClusterStatusService>().GetServiceStatusByNameAsync(new()
        {
            ServiceName = Req.Value
        }).Result;
    }
        

    [Authorize]
    [HttpPost("EnableMaintenanceMode")]
    public BasicRpcResponse EnableMaintenanceMode(OnlyAnStringRequest Req)
    {
        return _container.GetService<IClusterStatusService>().EnableMaintenanceMode(new()
        {
            ServiceName = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("DisableMaintenanceMode")]
    public BasicRpcResponse DisableMaintenanceMode(OnlyAnStringRequest Req)
    {
        return _container.GetService<IClusterStatusService>().DisableMaintenanceMode(new()
        {
            ServiceName = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("ScheduleGeneralMaintenance")]
    public BasicRpcResponse ScheduleGeneralMaintenance(ServiceScheduleGeneralMaintenanceRequest Req) =>
        _container.GetService<IClusterStatusService>().ScheduleGeneralMaintenance(Req).Result;

    [Authorize]
    [HttpPost("UnscheduleGeneralMaintenance")]
    public BasicRpcResponse UnscheduleGeneralMaintenance(EmptyRpcRequest Req) =>
        _container.GetService<IClusterStatusService>().UnscheduleGeneralMaintenance(Req).Result;

    [Authorize]
    [HttpPost("ExecuteGeneralEmergencyMaintenance")]
    public BasicRpcResponse ExecuteGeneralEmergencyMaintenance(ServiceExecuteGeneralEmergencyMaintenanceRequest Req) =>
        _container.GetService<IClusterStatusService>().ExecuteGeneralEmergencyMaintenance(Req).Result;

    [Authorize]
    [HttpPost("LiftGeneralMaintenance")]
    public BasicRpcResponse LiftGeneralMaintenance(EmptyRpcRequest Req) =>
        _container.GetService<IClusterStatusService>().LiftGeneralMaintenance(Req).Result;
}
