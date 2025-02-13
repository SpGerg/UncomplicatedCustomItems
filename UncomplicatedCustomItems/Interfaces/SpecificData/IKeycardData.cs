using Interactables.Interobjects.DoorUtils;

namespace UncomplicatedCustomItems.Interfaces.SpecificData
{
    public interface IKeycardData : IData
    {
        public abstract KeycardPermissions Permissions { get; set; }
    }
}
