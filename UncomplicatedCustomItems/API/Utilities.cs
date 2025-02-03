﻿using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UncomplicatedCustomItems.API.Features;
using UncomplicatedCustomItems.Interfaces;
using UncomplicatedCustomItems.Interfaces.SpecificData;

namespace UncomplicatedCustomItems.API
{
    public static class Utilities
    {
        /// <summary>
        /// Check if a <see cref="ICustomItem"/> is valid and can be registered
        /// </summary>
        /// <param name="item"></param>
        /// <param name="error"></param>
        /// <returns><see cref="false"/> if there's any problem. Every error will be outputted with <paramref name="error"/></returns>
        public static bool CustomItemValidator(ICustomItem item, out string error)
        {
            if (CustomItem.CustomItems.ContainsKey(item.Id))
            {
                error = $"There's already another ICustomItem registered with the same Id ({item.Id})!";
                return false;
            }

            
            switch (item.CustomItemType)
            {
                case CustomItemType.Item:
                    if (item.CustomData is null)
                    {
                        error = $"The item has been flagged as 'Item' but the CustomData class is not 'IData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    break;

                case CustomItemType.Weapon:
                    if (item.CustomData is not IWeaponData)
                    {
                        error = $"The item has been flagged as 'Weapon' but the CustomData class is not 'IWeaponData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (!item.Item.IsWeapon())
                    {
                        error = $"The item has been flagged as 'Weapon' but the item {item.Item} is not a weapon in the game!";
                        return false;
                    }

                    break;

                case CustomItemType.Keycard:
                    if (item.CustomData is not IKeycardData)
                    {
                        error = $"The item has been flagged as 'Keycard' but the CustomData class is not 'IKeycardData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (!item.Item.IsKeycard())
                    {
                        error = $"The item has been flagged as 'Keycard' but the item {item.Item} is not a keycard in the game!";
                        return false;
                    }

                    break;

                case CustomItemType.Armor:
                    if (item.CustomData is not IArmorData)
                    {
                        error = $"The item has been flagged as 'Armor' but the CustomData class is not 'IArmorData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (!item.Item.IsArmor())
                    {
                        error = $"The item has been flagged as 'Armor' but the item {item.Item} is not a armor in the game!";
                        return false;
                    }

                    break;

                case CustomItemType.ExplosiveGrenade:
                    if (item.CustomData is not IExplosiveGrenadeData)
                    {
                        error = $"The item has been flagged as 'ExplosiveGrenade' but the CustomData class is not 'IExplosiveGrenadeData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.GrenadeHE)
                    {
                        error = $"The Item has been flagged as 'ExplosiveGrenade' but the item {item.Item} is not a GrenadeHE";
                        return false;
                    }

                    break;

                case CustomItemType.FlashGrenade:
                    if (item.CustomData is not IFlashGrenadeData)
                    {
                        error = $"The item has been flagged as 'FlashGrenade' but the CustomData class is not 'IFlashGrenadeData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.GrenadeFlash)
                    {
                        error = $"The Item has been flagged as 'FlashGrenade' but the item {item.Item} is not a GrenadeFlash";
                        return false;
                    }

                    break;

                case CustomItemType.Jailbird:
                    if (item.CustomData is not IJailbirdData)
                    {
                        error = $"The item has been flagged as 'Jailbird' but the CustomData class is not 'IJailbirdData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.Jailbird)
                    {
                        error = $"The Item has been flagged as 'Jailbird' but the item {item.Item} is not a Jailbird";
                        return false;
                    }

                    break;

                case CustomItemType.Medikit:
                    if (item.CustomData is not IMedikitData)
                    {
                        error = $"The item has been flagged as 'Medikit' but the CustomData class is not 'IMedikitData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.Medkit)
                    {
                        error = $"The Item has been flagged as 'Medikit' but the item {item.Item} is not a Medikit";
                        return false;
                    }

                    break;

                case CustomItemType.Painkillers:
                    if (item.CustomData is not IPainkillersData)
                    {
                        error = $"The item has been flagged as 'Painkillers' but the CustomData class is not 'IPainkillersData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.Painkillers)
                    {
                        error = $"The Item has been flagged as 'Painkillers' but the item {item.Item} is not a Painkillers";
                        return false;
                    }

                    break;

                case CustomItemType.Adrenaline:
                    if (item.CustomData is not IAdrenalineData)
                    {
                        error = $"The item has been flagged as 'Adrenaline' but the CustomData class is not 'IAdrenalineData', found '{item.CustomData.GetType().Name}'";
                        return false;
                    }

                    if (item.Item is not ItemType.Adrenaline)
                    {
                        error = $"The Item has been flagged as 'Adrenaline' but the item {item.Item} is not a Adrenaline";
                        return false;
                    }

                    break;

                default:
                    error = "Unknown error? Uhm please report it on our discord server!";
                    return false;
            }

            error = "";
            return true;
        }

        /// <summary>
        /// Check if a <see cref="ICustomItem"/> is valid and can be registered.
        /// Does not return the error as text!
        /// </summary>
        /// <param name="item"></param>
        /// <returns><see cref="false"/> if there's any problem.</returns>
        public static bool CustomItemValidator(ICustomItem item)
        {
            return CustomItemValidator(item, out _);
        }

        /// <summary>
        /// Parse a <see cref="IResponse"/> object as response to a player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="response"></param>
        public static void ParseResponse(Player player, IItemData response)
        {
            if (response.ConsoleMessage is not null && response.ConsoleMessage.Length > 1) // FUCK 1 char messages!
            {
                player.SendConsoleMessage(response.ConsoleMessage, string.Empty);
            }

            if (response.BroadcastMessage.Length > 1 && response.BroadcastDuration > 0)
            {
                player.Broadcast(response.BroadcastDuration, response.BroadcastMessage);
            }

            if (response.HintMessage.Length > 1 && response.HintDuration > 0)
            {
                player.ShowHint(response.HintMessage, response.HintDuration);
            }
        }

        /// <summary>
        /// Try to get a <see cref="SummonedCustomItem"/> by it's serial
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="item"></param>
        /// <returns><see cref="true"/> if succeeded</returns>
        public static bool TryGetSummonedCustomItem(ushort serial, out SummonedCustomItem item) => SummonedCustomItem.TryGet(serial, out item);

        /// <summary>
        /// Get a <see cref="SummonedCustomItem"/> by it's serial
        /// </summary>
        /// <param name="serial"></param>
        /// <returns><see cref="SummonedCustomItem"/> if succeeded, <see cref="default"/> if not</returns>
        public static SummonedCustomItem GetSummonedCustomItem(ushort serial) => SummonedCustomItem.Get(serial);

        /// <summary>
        /// Check if an item is a <see cref="SummonedCustomItem"/> by it's serial
        /// </summary>
        /// <param name="serial"></param>
        /// <returns><see cref="true"/> if it is</returns>
        public static bool IsSummonedCustomItem(ushort serial) => SummonedCustomItem.Get(serial) is not null;

        /// <summary>
        /// Try to get a <see cref="ICustomItem"/> by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns><see cref="true"/> if the item exists and <paramref name="item"/> is not <see cref="null"/> or <see cref="default"/></returns>
        public static bool TryGetCustomItem(uint id, out ICustomItem item) => CustomItem.CustomItems.TryGetValue(id, out item);

        /// <summary>
        /// Get a <see cref="ICustomItem"/> by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ICustomItem"/> if it exists, otherwhise a <see cref="default"/> will be returned</returns>
        public static ICustomItem GetCustomItem(uint id) => CustomItem.CustomItems[id];

        /// <summary>
        /// Check if the given Id is already registered as a <see cref="ICustomItem"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsCustomItem(uint id) => CustomItem.CustomItems.ContainsKey(id);

        /// <summary>
        /// Summon a CustomItem
        /// </summary>
        /// <param name="CustomItem"></param>
        internal static void SummonCustomItem(ICustomItem CustomItem)
        {
            ISpawn Spawn = CustomItem.Spawn;

            if (Spawn.Coords.Count() > 0)
            {
                new SummonedCustomItem(CustomItem, Spawn.Coords.RandomItem());
                return;
            }

            else if (Spawn.Rooms.Count() > 0)
            {
                RoomType Room = Spawn.Rooms.RandomItem();
                if (Spawn.ReplaceExistingPickup)
                {
                    List<Pickup> FilteredPickups = Pickup.List.Where(pickup => pickup.Room.Type == Room && !IsSummonedCustomItem(pickup.Serial)).ToList();

                    if (Spawn.ForceItem)
                        FilteredPickups = FilteredPickups.Where(pickup => pickup.Type == CustomItem.Item).ToList();

                    if (FilteredPickups.Count() > 0)
                        new SummonedCustomItem(CustomItem, FilteredPickups.RandomItem());

                    return;
                }
                else
                    new SummonedCustomItem(CustomItem, Exiled.API.Features.Room.Get(Room).Position);
            }
            else if (Spawn.Zones.Count() > 0)
            {
                ZoneType Zone = Spawn.Zones.RandomItem();
                if (Spawn.ReplaceExistingPickup)
                {
                    List<Pickup> FilteredPickups = Pickup.List.Where(pickup => pickup.Room.Zone == Zone && !IsSummonedCustomItem(pickup.Serial)).ToList();

                    if (Spawn.ForceItem)
                        FilteredPickups = FilteredPickups.Where(pickup => pickup.Type == CustomItem.Item).ToList();

                    if (FilteredPickups.Count() > 0)
                    {
                        new SummonedCustomItem(CustomItem, FilteredPickups.RandomItem());
                    }
                    return;
                }
                else
                {
                    new SummonedCustomItem(CustomItem, Room.List.Where(room => room.Zone == Zone).ToList().RandomItem().Position);
                }
            }
        }

        /// <summary>
        /// Reproduce the SCP:SL painkillers healing process but with custom things :)
        /// </summary>
        /// <param name="player"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        internal static IEnumerator<float> PainkillersCoroutine(Player player, IPainkillersData Data)
        {
            float TotalHealed = 0;
            yield return Timing.WaitForSeconds(Data.TimeBeforeStartHealing);
            while (TotalHealed < Data.TotalHealing && player.IsAlive)
            {
                player.Heal(Data.TickHeal);
                TotalHealed += Data.TickHeal;
                yield return Timing.WaitForSeconds(Data.TickTime);
            }
        }
    }
}