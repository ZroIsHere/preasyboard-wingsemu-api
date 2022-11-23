using WingsAPI.Data.Bazaar;

namespace PreasyBoard.Api.RequestEntities;

public class AddItemToBazaarRequest
{
    public string OwnerName { get; set; }
    public BazaarItemDTO BazaarItemDto { get; set; }
    public int MaxListCount { get; set; }
}