using Microsoft.AspNetCore.Mvc;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Data.Account;

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
}