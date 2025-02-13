﻿using LabApi.Loader.Features.Plugins.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using UncomplicatedCustomItems.Interfaces;
using UnityEngine;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using LabApi;
using LabApi.Events.Handlers;
using LabApi.Loader.Features.Plugins;
using MapGeneration;
using PluginAPI.Core.Zones;

namespace UncomplicatedCustomItems.API.Features
{
    public class Spawn : ISpawn
    {
        /// <summary>
        /// Decide if the item can naturally spawn or not
        /// </summary>
        public bool DoSpawn { get; set; } = false;

        /// <summary>
        /// How many custom items istances of this item should be spawned?
        /// </summary>
        public uint Count { get; set; } = 1;

        /// <summary>
        /// The <see cref="Vector3">Positions</see> where the item is allowed to spawn, this is an array so you can set multiple values and one random one will be choose.
        /// If this is empty then the <see cref="Rooms"/> parameter will be used instead.
        /// </summary>
        public List<Vector3> Coords { get; set; } = new();
        /// <summary>
        /// The <see cref="RoomType">Rooms</see> where the item is allowed to spawn.
        /// If this is empty then the <see cref="Zone"/> parameter will be used instead.
        /// </summary>
        public List<FacilityRoom> Rooms { get; set; } = new();

        /// <summary>
        /// The <see cref="ZoneType">Zones</see> where the item is allowed to spawn.
        /// If <see cref="Rooms"/> is empty then this parameter will be used.
        /// </summary>
        public List<MapGeneration.FacilityZone> Zones { get; set; } = new()
        {
            MapGeneration.FacilityZone.HeavyContainment,
            MapGeneration.FacilityZone.Entrance
        };

        /// <summary>
        /// If <see cref="true"/> this item will replace an existing pickup.
        /// If <see cref="ForceItem"/> is false a random pickup will be replaced with the item's one.
        /// If <see cref="false"/> this item will be spawned on the floor of the room.
        /// </summary>
        public bool ReplaceExistingPickup { get; set; } = false;

        /// <summary>
        /// If <see cref="true"/> this item will replace only another pickup with the same <see cref="ItemType"/> of the custom item.
        /// </summary>
        public bool ForceItem { get; set; } = false;
    }
}