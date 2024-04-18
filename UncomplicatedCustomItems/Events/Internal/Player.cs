﻿using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using UncomplicatedCustomItems.API.Extensions;
using UncomplicatedCustomItems.API.Features;
using UncomplicatedCustomItems.API.Features.Data;
using EventSource = Exiled.Events.Handlers.Player;

namespace UncomplicatedCustomItems.Events.Internal
{
    internal static class Player
    {
        public static void Register()
        {
            EventSource.UsingItem += CancelUsingCustomItemOnUsingItem;
            EventSource.Hurting += SetDamageFromCustomWeaponOnHurting;
            EventSource.ItemAdded += ShowItemInfoOnItemAdded;
            EventSource.ItemRemoved += SetPlayerNullOnItemRemoved;
        }

        public static void Unregister()
        {
            EventSource.UsingItem -= CancelUsingCustomItemOnUsingItem;
            EventSource.Hurting -= SetDamageFromCustomWeaponOnHurting;
            EventSource.ItemAdded -= ShowItemInfoOnItemAdded;
            EventSource.ItemRemoved -= SetPlayerNullOnItemRemoved;
        }

        private static void SetPlayerNullOnItemRemoved(ItemRemovedEventArgs ev)
        {
            if (!Plugin.API.TryGet(ev.Item.Serial, out var result))
            {
                return;
            }

            result.Player = null;
        }

        /// <summary>
        /// Show item name if it is custom item
        /// </summary>
        /// <param name="ev"></param>
        private static void ShowItemInfoOnItemAdded(ItemAddedEventArgs ev)
        {
            if (!Plugin.API.TryGet(ev.Item.Serial, out var result))
            {
                return;
            }

            var player = ev.Player;

            player.ShowHint(result.Name);

            result.Player = player;
        }

        /// <summary>
        /// Set damage if weapon is custom item
        /// </summary>
        /// <param name="ev"></param>
        private static void SetDamageFromCustomWeaponOnHurting(HurtingEventArgs ev)
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

            ev.DamageHandler.Damage = (customWeapon.Info as WeaponInfo).Damage;
        }

        /// <summary>
        /// Cancel using if it is custom item
        /// </summary>
        /// <param name="ev"></param>
        private static void CancelUsingCustomItemOnUsingItem(UsingItemEventArgs ev)
        {
            if (!ev.IsAllowed)
            {
                return;
            }

            ev.IsAllowed = !ev.Item.IsCustomItem();
        }
    }
}
