using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using noswebapp_api;
using noswebapp_api.RequestEntities;
using PhoenixLib.Extensions;
using Plugin.Database.DB;
using Plugin.Database.Entities.Account;
using WingsAPI.Communication;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Data.Account;
using WingsEmu.DTOs.Account;

namespace noswebapp.Controllers;

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

    [HttpGet("LoadAccountByName")]
    public AccountDTO LoadAccountByName([FromHeader] string AuthKey, OnlyAnStringRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().LoadAccountByName(new()
        {
            Name = Req.Value
        }).Result.AccountDto;
    }
    
    [HttpGet("LoadAccountById")]
    public AccountDTO LoadAccountById([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().LoadAccountById(new()
        {
            AccountId = Req.Value
        }).Result.AccountDto;
    }

    [HttpPost("SaveAccount")]
    public AccountSaveResponse SaveAccount([FromHeader] string AuthKey, AccountDTO dto)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().SaveAccount(new()
        {
            AccountDto = dto
        }).Result;
    }

    [HttpGet("GetAccountBan")]
    public AccountBanGetResponse GetAccountBan([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().GetAccountBan(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [HttpPost("SaveAccountBan")]
    public AccountBanSaveResponse SaveAccountBan([FromHeader] string AuthKey, AccountBanDto dto)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().SaveAccountBan(new()
        {
            AccountBanDto = dto
        }).Result;
    }

    [HttpGet("GetAccountPenalties")]
    public AccountPenaltyGetAllResponse GetAccountPenalties([FromHeader] string AuthKey, OnlyAnLongRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().GetAccountPenalties(new()
        {
            AccountId = Req.Value
        }).Result;
    }

    [HttpPost("SaveAccountPenalties")]
    public AccountPenaltyMultiSaveResponse SaveAccountPenalties([FromHeader] string AuthKey, List<AccountPenaltyDto> list)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        return _container.GetService<IAccountService>().SaveAccountPenalties(new()
        {
            AccountPenaltyDtos = list
        }).Result;
    }
    
    [HttpPost("CreateAccount")]
    public BasicRpcResponse CreateAccount([FromHeader] string AuthKey, CreateAccountRequest Req)
    {
        if (!AuthKey.Equals(NosWebAppEnvVariables.AuthKey))
        {
            return null;
        }
        using GameContext dbcontext = _container.GetRequiredService<IDbContextFactory<GameContext>>().CreateDbContext();
        if (dbcontext.Account.Any(s => s.Name.Equals(Req.AcccountName)))
        {
            return new(){ ResponseType = RpcResponseType.UNKNOWN_ERROR };
        }
        dbcontext.Account.Add(new AccountEntity
        {
            Authority = AuthorityType.User,
            Language = AccountLanguage.EN,
            Name = Req.AcccountName,
            Password = Req.Password.ToSha512()
        });
        dbcontext.SaveChangesAsync();
        return new() { ResponseType = RpcResponseType.SUCCESS };
    }
}