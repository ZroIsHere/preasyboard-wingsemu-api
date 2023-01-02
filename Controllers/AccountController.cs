using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using PhoenixLib.Extensions;
using Plugin.Database.DB;
using Plugin.Database.Entities.Account;
using WingsAPI.Communication;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Data.Account;
using WingsEmu.DTOs.Account;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    
    private readonly IServiceProvider _container;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }
    
    [Authorize]
    [HttpGet("LoadAccountByName")]
    public AccountLoadResponse LoadAccountByName(OnlyAnStringRequest Req)
    {
        return _container.GetService<IAccountService>().LoadAccountByName(new()
        {
            Name = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpGet("GetAllAccounts")]
    public AccountGetAllResponse GetAllAccounts() => _container.GetService<IAccountService>().GetAllAccounts(new()).Result;

    [Authorize]
    [HttpGet("LoadAccountById")]
    public AccountLoadResponse LoadAccountById(OnlyAnLongRequest Req)
    {
        return _container.GetService<IAccountService>().LoadAccountById(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("SaveAccount")]
    public AccountSaveResponse SaveAccount(AccountDTO dto)
    {
        return _container.GetService<IAccountService>().SaveAccount(new()
        {
            AccountDto = dto
        }).Result;
    }

    [Authorize]
    [HttpGet("GetAccountBan")]
    public AccountBanGetResponse GetAccountBan(OnlyAnLongRequest Req)
    {
        return _container.GetService<IAccountService>().GetAccountBan(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("SaveAccountBan")]
    public AccountBanSaveResponse SaveAccountBan(AccountBanDto dto)
    {
        return _container.GetService<IAccountService>().SaveAccountBan(new()
        {
            AccountBanDto = dto
        }).Result;
    }

    [Authorize]
    [HttpGet("GetAccountPenalties")]
    public AccountPenaltyGetAllResponse GetAccountPenalties(OnlyAnLongRequest Req)
    {
        return _container.GetService<IAccountService>().GetAccountPenalties(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [Authorize]
    [HttpPost("SaveAccountPenalties")]
    public AccountPenaltyMultiSaveResponse SaveAccountPenalties(List<AccountPenaltyDto> list)
    {
        return _container.GetService<IAccountService>().SaveAccountPenalties(new()
        {
            AccountPenaltyDtos = list
        }).Result;
    }
    
    [Authorize]
    [HttpPost("CreateAccount")]
    public AccountLoadResponse CreateAccount(CreateAccountRequest Req)
    {
        using GameContext dbcontext = _container.GetRequiredService<IDbContextFactory<GameContext>>().CreateDbContext();
        if (dbcontext.Account.Any(s => s.Name.Equals(Req.AccountName)))
        {
            return new()
            { 
                ResponseType = RpcResponseType.UNKNOWN_ERROR,
                AccountDto = null
            };
        }
        dbcontext.Account.Add(new AccountEntity
        {
            Authority = AuthorityType.User,
            Language = AccountLanguage.EN,
            Name = Req.AccountName,
            Password = Req.Password.ToSha512()
        });
        dbcontext.SaveChangesAsync();
        return LoadAccountByName(new() { Value = Req.AccountName });
    }
}