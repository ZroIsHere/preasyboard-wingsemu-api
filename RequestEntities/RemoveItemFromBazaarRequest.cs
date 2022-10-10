using WingsAPI.Data.Bazaar;

namespace noswebapp_api.RequestEntities;

public class RemoveItemFromBazaarRequest
{
    public long RequesterCharacterId { get; set; }
    public BazaarItemDTO BazaarItemDto { get; set; }
}