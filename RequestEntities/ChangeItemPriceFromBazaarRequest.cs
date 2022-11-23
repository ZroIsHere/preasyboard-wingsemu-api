using WingsAPI.Data.Bazaar;

namespace PreasyBoard.Api.RequestEntities;

public class ChangeItemPriceFromBazaarRequest
{
    public BazaarItemDTO BazaarItemDto { get; set; }

    public long ChangerCharacterId { get; init; }

    public long NewPrice { get; set; }

    public long NewSaleFee { get; set; }

    public long SunkGold { get; set; }
}