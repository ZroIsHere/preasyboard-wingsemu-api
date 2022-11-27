using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Communication.Families;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FamilyController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<FamilyController> _logger;
    public FamilyController(ILogger<FamilyController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("FamilyCreate")]
    public FamilyCreateResponse CreateFamilyAsync(FamilyCreateRequest Req) =>
        _container.GetService<IFamilyService>().CreateFamilyAsync(Req).Result;

    [Authorize]
    [HttpPost("FamilyDisband")]
    public BasicRpcResponse FamilyDisbandAsync(FamilyDisbandRequest Req) =>
        _container.GetService<IFamilyService>().DisbandFamilyAsync(Req).Result;

    [Authorize]
    [HttpPost("ChangeAuthorityById")]
    public EmptyResponse ChangeAuthorityById(FamilyChangeAuthorityRequest Req) =>
        _container.GetService<IFamilyService>().ChangeAuthorityByIdAsync(Req).Result;

    [Authorize]
    [HttpPost("ChangeFactionById")]
    public FamilyChangeFactionResponse ChangeFactionById(FamilyChangeFactionRequest Req) =>
        _container.GetService<IFamilyService>().ChangeFactionByIdAsync(Req).Result;

    [Authorize]
    [HttpPost("ChangeTitleById")]
    public EmptyResponse ChangeTitleById(FamilyChangeTitleRequest Req) =>
        _container.GetService<IFamilyService>().ChangeTitleByIdAsync(Req).Result;

    [Authorize]
    [HttpPost("AddMemberToFamily")]
    public EmptyResponse AddMemberToFamily(FamilyAddMemberRequest Req) =>
        _container.GetService<IFamilyService>().AddMemberToFamilyAsync(Req).Result;

    [Authorize]
    [HttpPost("RemoveMemberToFamily")]
    public EmptyResponse RemoveMemberToFamily(FamilyRemoveMemberRequest Req) =>
        _container.GetService<IFamilyService>().RemoveMemberToFamilyAsync(Req).Result;

    [Authorize]
    [HttpPost("RemoveMemberByCharId")]
    public BasicRpcResponse RemoveMemberByCharId(FamilyRemoveMemberByCharIdRequest Req) =>
        _container.GetService<IFamilyService>().RemoveMemberByCharIdAsync(Req).Result;

    [Authorize]
    [HttpGet("GetFamilyById")]
    public FamilyIdResponse GetFamilyById(OnlyAnLongRequest Req)
    {
        return _container.GetService<IFamilyService>().GetFamilyByIdAsync(new()
        {
            FamilyId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetFamilyMembersByFamilyId")]
    public FamilyListMembersResponse GetFamilyMembersByFamilyId(OnlyAnLongRequest Req)
    {
        return _container.GetService<IFamilyService>().GetFamilyMembersByFamilyId(new()
        {
            FamilyId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetMembershipByCharacterId")]
    public MembershipResponse GetMembershipByCharacterId(OnlyAnLongRequest Req)
    {
        return _container.GetService<IFamilyService>().GetMembershipByCharacterIdAsync(new()
        {
            CharacterId = Req.Value
        }).Result;
    }
        

    [Authorize]
    [HttpPost("ChangeAuthorityById")]
    public MembershipTodayResponse CanPerformTodayMessage(MembershipTodayRequest Req) =>
        _container.GetService<IFamilyService>().CanPerformTodayMessageAsync(Req).Result;

    [Authorize]
    [HttpPost("UpdateFamilySettings")]
    public BasicRpcResponse UpdateFamilySettings(FamilySettingsRequest Req) =>
        _container.GetService<IFamilyService>().UpdateFamilySettingsAsync(Req).Result;
}
