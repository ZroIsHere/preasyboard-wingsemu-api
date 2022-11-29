using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication;
using WingsAPI.Communication.Families;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FamilyInvitationController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<FamilyInvitationController> _logger;
    public FamilyInvitationController(ILogger<FamilyInvitationController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpPost("SaveFamilyInvitation")]
    public EmptyResponse SaveFamilyInvitationAsync(FamilyInvitationSaveRequest Req) =>
        _container.GetService<IFamilyInvitationService>().SaveFamilyInvitationAsync(Req).Result;

    [Authorize]
    [HttpGet("ContainsFamilyInvitation")]
    public FamilyInvitationContainsResponse ContainsFamilyInvitationAsync(FamilyInvitationRequest Req) =>
        _container.GetService<IFamilyInvitationService>().ContainsFamilyInvitationAsync(Req).Result;

    [Authorize]
    [HttpGet("GetFamilyInvitation")]
    public FamilyInvitationGetResponse GetFamilyInvitationAsync(FamilyInvitationRequest Req) =>
        _container.GetService<IFamilyInvitationService>().GetFamilyInvitationAsync(Req).Result;

    [Authorize]
    [HttpPost("RemoveFamilyInvitation")]
    public EmptyResponse RemoveFamilyInvitationAsync(OnlyAnLongRequest Req)
    {
        return _container.GetService<IFamilyInvitationService>().RemoveFamilyInvitationAsync(new()
        {
            SenderId = Req.Value
        }).Result;
    }
}
