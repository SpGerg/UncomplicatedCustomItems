﻿using Exiled.API.Features.Items;

namespace UncomplicatedCustomItems.API.Features.Data
{
    public class ItemInfo : ThingInfo
    {
        public string Response { get; set; }

        public string[] Commands { get; set; }

        public override void Set(Item item)
        {
            return;
        }
    }
}