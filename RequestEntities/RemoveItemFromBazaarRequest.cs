﻿using WingsAPI.Data.Bazaar;

namespace PreasyBoard.Api.RequestEntities;

public class RemoveItemFromBazaarRequest
{
    public long RequesterCharacterId { get; set; }
    public BazaarItemDTO BazaarItemDto { get; set; }
}