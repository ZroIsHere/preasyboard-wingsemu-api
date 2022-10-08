using Microsoft.AspNetCore.Mvc;
using WingsAPI.Communication.DbServer.AccountService;
using WingsAPI.Data.Account;

namespace WebApplication1.Controllers;

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

    [HttpGet("GetAccountByName")]
    public AccountDTO GetAccountByName(string accountname)
    {
        return _container.GetService<IAccountService>().LoadAccountByName(new()
        {
            Name = accountname
        }).Result.AccountDto;
    }
}