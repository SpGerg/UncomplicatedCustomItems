using MapGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace UncomplicatedCustomItems.Interfaces
{
    public interface ISpawn
    {
        public abstract bool DoSpawn { get; set; }

        public abstract uint Count { get; set; }
        
        public abstract List<Vector3> Coords { get; set; }

        public abstract List<RoomName> Rooms { get; set; }

        public abstract List<FacilityZone> Zones { get; set; }

        public abstract bool ReplaceExistingPickup { get; set; }

        public abstract bool ForceItem { get; set; }
    }
}