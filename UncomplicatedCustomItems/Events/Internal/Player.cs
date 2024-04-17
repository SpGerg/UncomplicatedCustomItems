﻿using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using UncomplicatedCustomItems.API.Extensions;
using UncomplicatedCustomItems.API.Features;
using EventSource = Exiled.Events.Handlers.Player;

namespace UncomplicatedCustomItems.Events.Internal
{
    internal static class Player
    {
        public static void Register()
        {
            EventSource.UsingItem += CancelUsingCustomItemOnUsingItem;
            EventSource.Hurting += SetDamageFromCustomWeaponOnHurting;
        }

        public static void Unregister()
        {
            EventSource.UsingItem -= CancelUsingCustomItemOnUsingItem;
            EventSource.Hurting -= SetDamageFromCustomWeaponOnHurting;
        }

        public static void SetDamageFromCustomWeaponOnHurting(HurtingEventArgs ev)
        {
            if (ev.DamageHandler.Type is not Exiled.API.Enums.DamageType.Firearm)
            {
                return;
            }

            if (ev.Attacker.CurrentItem is not Firearm)
            {
                return;
            }

            if (!Plugin.API.TryGet(ev.Attacker.CurrentItem.Serial, out var result) || result is not CustomWeapon customWeapon)
            {
                return;
            }

            ev.DamageHandler.Damage = customWeapon.Info.Damage;
        }

        /// <summary>
        /// Cancel using if it is custom item
        /// </summary>
        /// <param name="ev"></param>
        public static void CancelUsingCustomItemOnUsingItem(UsingItemEventArgs ev)
        {
            if (!ev.IsAllowed)
            {
                return;
            }

            ev.IsAllowed = !ev.Item.IsCustomItem();
        }
    }
}
