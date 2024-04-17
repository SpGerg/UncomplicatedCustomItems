﻿using Exiled.API.Features;
using System.ComponentModel;
using System.Runtime.Serialization;
using UncomplicatedCustomItems.API.Features;
using UncomplicatedCustomItems.API.Serializable.Interfaces;
using UnityEngine;

namespace UncomplicatedCustomItems.API.Serializable
{
    public class SerializableCustomItem : SerializableThing<CustomItem>
    {
        [Description("Name")]
        public override string Name { get; set; }

        [Description("Description")]
        public override string Description { get; set; }

        [Description("Use response")]
        public override int Id { get; set; }

        [Description("Model")]
        public ItemType Model { get; set; }

        [Description("Scale")]
        public Vector3 Scale { get; set; }

        [Description("Command to execute")]
        public string Command { get; set; }

        [Description("Use response")]
        public string Response { get; set; }

        /// <summary>
        /// Return custom item by serializable 
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override CustomItem Create(Player player)
        {
            return new CustomItem(player, this);
        }
    }
}
