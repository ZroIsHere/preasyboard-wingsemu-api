using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using noswebapp_api.Attributes;
using noswebapp_api.RequestEntities;
using PhoenixLib.Caching;
using PhoenixLib.MultiLanguage;
using Plugin.ResourceLoader;
using Plugin.ResourceLoader.Loaders;
using WingsAPI.Data.GameData;
using WingsEmu.DTOs.Items;
using WingsEmu.Game._i18n;

namespace noswebapp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : Controller
{
    private readonly List<ItemDTO> _cachedItems;
    //private readonly List<GameDataTranslationDto> _cachedTexts;
    private readonly IResourceLoader<ItemDTO> _itemDao;

    public ItemsController(IResourceLoader<ItemDTO> itemDao, IResourceLoader<GameDataTranslationDto> textsDao)
    {
        //_cachedTexts = textsDao.LoadAsync().Result.Where(s => s.Language.Equals(RegionLanguageType.EN) && s.DataType.Equals(GameDataType.Item)).ToList();
        foreach (ItemDTO itemDto in itemDao.LoadAsync().Result)
        {
            //itemDto.Name = _cachedTexts.FirstOrDefault(s => s.Key.Equals(itemDto.Name)).Value;
            _cachedItems.Add(itemDto);
        }
    }
    
    [Authorize]
    [HttpGet("GetAll")]
    public List<ItemDTO> GetAll()
    {
        return _cachedItems;
    }
    
    [Authorize]
    [HttpGet("GetByVnum")]
    public ItemDTO GetByVnum(OnlyAnIntRequest Req)
    {
        return _cachedItems.FirstOrDefault(s=> s.Id.Equals(Req.Value));
    }
}