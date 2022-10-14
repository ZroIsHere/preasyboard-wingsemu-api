using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using noswebapp_api.Attributes;
using noswebapp_api.RequestEntities;
using PhoenixLib.Caching;
using WingsAPI.Data.GameData;
using WingsEmu.DTOs.Items;

namespace noswebapp_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : Controller
{
    private readonly IReadOnlyList<ItemDTO> _cachedItems;
    private readonly IResourceLoader<ItemDTO> _itemDao;

    public ItemsController(IResourceLoader<ItemDTO> itemDao, ILongKeyCachedRepository<ItemDTO> cachedItems)
    {
        _cachedItems = itemDao.LoadAsync().Result;
    }
    
    [Authorize]
    [HttpGet("GetAll")]
    public List<ItemDTO> GetAll()
    {
        return _cachedItems.ToList();
    }
    
    [Authorize]
    [HttpGet("GetByVnum")]
    public ItemDTO GetByVnum(OnlyAnIntRequest Req)
    {
        return _cachedItems.FirstOrDefault(s=> s.Id.Equals(Req.Value));
    }
}