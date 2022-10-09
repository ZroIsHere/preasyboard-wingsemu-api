using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

    public AccountController(IServiceProvider container) => _container = container;
    
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [HttpGet("LoadAccountByName")]
    public AccountDTO LoadAccountByName(string accountname)
    {
        return _container.GetService<IAccountService>().LoadAccountByName(new()
        {
            Name = accountname
        }).Result.AccountDto;
    }
    
    [HttpGet("LoadAccountById")]
    public AccountDTO LoadAccountById(long id)
    {
        return _container.GetService<IAccountService>().LoadAccountById(new()
        {
            AccountId = id
        }).Result.AccountDto;
    }

    [HttpPost("SaveAccount")]
    public AccountSaveResponse SaveAccount(AccountDTO dto)
    {
        return _container.GetService<IAccountService>().SaveAccount(new()
        {
            AccountDto = dto
        }).Result;
    }

    [HttpGet("GetAccountBan")]
    public AccountBanGetResponse GetAccountBan(long id)
    {
        return _container.GetService<IAccountService>().GetAccountBan(new()
        {
            AccountId = id
        }).Result;
    }

    [HttpPost("SaveAccountBan")]
    public AccountBanSaveResponse SaveAccountBan(AccountBanDto dto)
    {
        return _container.GetService<IAccountService>().SaveAccountBan(new()
        {
            AccountBanDto = dto
        }).Result;
    }

    [HttpGet("GetAccountPenalties")]
    public AccountPenaltyGetAllResponse GetAccountPenalties(long id)
    {
        return _container.GetService<IAccountService>().GetAccountPenalties(new()
        {
            AccountId = id
        }).Result;
    }

    [HttpPost("SaveAccountPenalties")]
    public AccountPenaltyMultiSaveResponse SaveAccountPenalties(List<AccountPenaltyDto> list)
    {
        return _container.GetService<IAccountService>().SaveAccountPenalties(new()
        {
            AccountPenaltyDtos = list
        }).Result;
    }
    
    [HttpPost("CreateAccount")]
    public BasicRpcResponse CreateAccount(string accountname, string password, string email)
    {
        var factory = _container.GetRequiredService<IDbContextFactory<GameContext>>();
        using GameContext dbcontext = factory.CreateDbContext();
        if (dbcontext.Account.Any(s => s.Name.Equals(accountname)))
        {
            return new(){ ResponseType = RpcResponseType.UNKNOWN_ERROR };
        }
        dbcontext.Account.Add(new AccountEntity
        {
            Authority = AuthorityType.User,
            Language = AccountLanguage.EN,
            Name = accountname,
            Password = password.ToSha512()
        });
        dbcontext.SaveChangesAsync();
        return new() { ResponseType = RpcResponseType.SUCCESS };
    }
}