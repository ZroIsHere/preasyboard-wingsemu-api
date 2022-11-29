using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using System;
using WingsAPI.Communication;
using WingsAPI.Communication.Families;
using WingsAPI.Communication.Translations;
using WingsEmu.Game._i18n;

namespace PreasyBoard.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GameLanguageController : Controller
{
    private readonly IServiceProvider _container;

    private readonly ILogger<GameLanguageController> _logger;
    public GameLanguageController(ILogger<GameLanguageController> logger, IServiceProvider container)
    {
        _logger = logger;
        _container = container;
    }

    [Authorize]
    [HttpGet("GetTranslations")]
    public GetTranslationsResponse GetTranslations(EmptyRpcRequest Req) =>
        _container.GetService<ITranslationService>().GetTranslations(Req).Result;

    [Authorize]
    [HttpGet("GetForbiddenWords")]
    public GetForbiddenWordsResponse GetForbiddenWords(EmptyRpcRequest Req) =>
        _container.GetService<ITranslationService>().GetForbiddenWords(Req).Result;
}
